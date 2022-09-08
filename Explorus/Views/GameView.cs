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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Explorus
{
    internal class GameView
    {
        private float _framerate = 0;
        public delegate void HandleInput(object sender, KeyEventArgs e);
        public delegate void HandleResize(object sender, EventArgs e);
        private LabyrinthView labyrinthView;
        public double scaleFactor { get; set;}
        private int originalWidth { get; set;}

        private int originalHeight { get; set; }
       
        private HeaderView headerView;
        private Point offset { get; set; }

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
            offset = new Point(0, 0);
            //add header in calculation ( its 2 units )
            originalHeight = (lab.map.GetLength(0) + 1) * 2 * Constants.unit;
            originalWidth = lab.map.GetLength(1) * 2 * Constants.unit;
            oGameForm = new GameForm();
            oGameForm.MinimumSize = new Size(600, 600);
            DoProcessResize();
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
            headerView.Render(sender, e, offset);
            labyrinthView.Render(sender, e, offset);
            oGameForm.Text = String.Format("Explorus - FPS {0}", framerate.ToString());
        }

        private void ProcessResize(object sender, EventArgs e)
        {
            DoProcessResize();
        }

        private void DoProcessResize()
        {
            float clientAspectRatio = (float)oGameForm.ClientSize.Width / (float)oGameForm.ClientSize.Height;
            float labyrinthAspectRatio = (float)originalWidth / (float)originalHeight;
            if (clientAspectRatio >= labyrinthAspectRatio)
            {
                double newScaleFactor = this.computeScaleFactor(originalHeight, (double)oGameForm.ClientSize.Height);
                this.scaleFactor = newScaleFactor;
                int horizontalPadding = (int)(oGameForm.ClientSize.Width - (double)originalWidth * scaleFactor) / 2;
                offset = new Point(horizontalPadding, 0);
            }
            if (labyrinthAspectRatio >= clientAspectRatio)
            {
                double newScaleFactor = this.computeScaleFactor(originalWidth, (double)oGameForm.ClientSize.Width);
                this.scaleFactor = newScaleFactor;
                int verticalPadding = (int)(oGameForm.ClientSize.Height - (double)originalHeight * scaleFactor) / 2;
                offset = new Point(0, verticalPadding);
            }
        }

        private double computeScaleFactor(double originalSideLength, double newSideLength)
        {
            double newScaleFactordelta = newSideLength / originalSideLength;
            return Math.Round(newScaleFactordelta, 2);

        }
    }
}
