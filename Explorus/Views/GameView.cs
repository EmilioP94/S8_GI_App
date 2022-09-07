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
        public GameView(HandleInput doHandle, ILabyrinth lab)
        {
            //add header in calculation ( its 2 units )
            this.originalSmallestSide = Math.Min(Constants.LabyrinthHeight, Constants.LabyrinthHeight) * Constants.unit;
            oGameForm = new GameForm();
            //math.min heigth vs width
            this.scaleFactor = this.computeScaleFactor(originalSmallestSide, oGameForm.ClientSize.Width);
            oGameForm.Paint += new PaintEventHandler(this.GameRenderer);
            oGameForm.KeyDown += new KeyEventHandler(doHandle);
            oGameForm.ResizeEnd += new EventHandler(ProcessResize);
            labyrinthView = new LabyrinthView(lab);
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
            g.ScaleTransform(1.0F,1.0F);//((float)this.scaleFactor,(float)this.scaleFactor);
            labyrinthView.Render(sender, e);
            oGameForm.Text = String.Format("Explorus - FPS {0}", framerate.ToString());
        }

        private void ProcessResize(object sender, EventArgs e)
        {
            int smallestWindowSide = Math.Min(oGameForm.ClientSize.Width, oGameForm.ClientSize.Height);
            double newScaleFactor = this.computeScaleFactor(this.originalSmallestSide, (double)smallestWindowSide);
            this.scaleFactor = newScaleFactor;
        }

        private double computeScaleFactor(double originalSideLength, double newSideLength)
        {
            double newScaleFactordelta = newSideLength / originalSideLength;
            return 1 + (Math.Round(newScaleFactordelta, 2)/10);

        }
    }
}
