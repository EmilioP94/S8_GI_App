using Explorus.Controllers;
using Explorus.Models;
using Explorus.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Explorus
{
    internal class GameView
    {
        private float _framerate = 0;
        public delegate void HandleInput(object sender, KeyEventArgs e);
        public delegate void HandleResize(object sender, EventArgs e);
        private LabyrinthView labyrinthView;
        public double scaleFactor { get; set;}
        public double originalSmallestSide { get; set;}
       
        private HeaderView headerView;

        public float framerate
        {
            get
            {
                return _framerate;
            }
            set
            {
                _framerate   = value;
            }
        }

        GameForm oGameForm;
        public GameView(HandleInput doHandle, ILabyrinth lab, HeaderController headerController)
        {
            //add header in calculation ( its 2 units )
            this.originalSmallestSide = Math.Min(Constants.LabyrinthHeight, Constants.LabyrinthHeight) * Constants.unit * 2;
            oGameForm = new GameForm();
            this.scaleFactor = this.computeScaleFactor(originalSmallestSide, Math.Min(oGameForm.ClientSize.Width,oGameForm.ClientSize.Height));
            oGameForm.Paint += new PaintEventHandler(this.GameRenderer);
            oGameForm.KeyDown += new KeyEventHandler(doHandle);
            oGameForm.Resize += new EventHandler(ProcessResize);
            labyrinthView = new LabyrinthView(lab);
            headerView = new HeaderView(headerController);
        }

        public void Show() { Application.Run(oGameForm); }

        public void Render()
        {
            if (oGameForm.Visible)
                oGameForm.BeginInvoke((MethodInvoker)delegate
                {
                    oGameForm.Refresh();
                });
        }

        public void Close()
        {
            if (oGameForm.Visible)
                oGameForm.BeginInvoke((MethodInvoker)delegate {
                    oGameForm.Close();
                });
        }

        private void GameRenderer(object sender, PaintEventArgs e) 
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            g.ScaleTransform((float)this.scaleFactor,(float)this.scaleFactor);
            headerView.Render(sender, e);
            labyrinthView.Render(sender, e);
            oGameForm.Text = String.Format("Explorus - FPS {0}", framerate.ToString());
        }

        private void ProcessResize(object sender, EventArgs e)
        {
            if(oGameForm.ClientSize.Width >= oGameForm.ClientSize.Height)
            {
                double newScaleFactor = this.computeScaleFactor(this.originalSmallestSide, (double)oGameForm.ClientSize.Height);
                this.scaleFactor = newScaleFactor;
                int horizontalPadding = (oGameForm.ClientSize.Width - oGameForm.ClientSize.Height)/2;
                //oGameForm.Padding = new System.Windows.Forms.Padding(horizontalPadding,0,horizontalPadding,0);
            }
            if(oGameForm.ClientSize.Height >= oGameForm.ClientSize.Width)
            {
                double newScaleFactor = this.computeScaleFactor(this.originalSmallestSide, (double)oGameForm.ClientSize.Width);
                this.scaleFactor = newScaleFactor;
                int verticalPadding = (oGameForm.ClientSize.Height - oGameForm.ClientSize.Width)/2;
                //oGameForm.Padding = new System.Windows.Forms.Padding(0,verticalPadding,0,verticalPadding);
            }
        }

        private double computeScaleFactor(double originalSideLength, double newSideLength)
        {
            double newScaleFactordelta = newSideLength / originalSideLength;
            return Math.Round(newScaleFactordelta, 2);

        }
    }
}
