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
        private LabyrinthView labyrinthView;
        private HeaderView headerView;

        public float framerate
        {
            get
            {
                return _framerate;
            }
            set
            {
                _framerate = value;
            }
        }

        GameForm oGameForm;
        public GameView(HandleInput doHandle, ILabyrinth lab)
        {
            oGameForm = new GameForm();
            oGameForm.Paint += new PaintEventHandler(this.GameRenderer);
            oGameForm.KeyDown += new KeyEventHandler(doHandle);
            labyrinthView = new LabyrinthView(lab);
            headerView = new HeaderView();
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
            headerView.Render(sender, e);
            labyrinthView.Render(sender, e);
            oGameForm.Text = String.Format("Explorus - FPS {0}", framerate.ToString());
        }
    }
}
