using Explorus.Models;
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
        private int x = 0;
        private int y = 0;
        private float _framerate = 0;

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
        public GameView()
        {
            oGameForm = new GameForm();
            oGameForm.Paint += new PaintEventHandler(this.GameRenderer);
            oGameForm.KeyDown += new KeyEventHandler(this.InputListener);
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

        private void InputListener(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyValue);
            if(e.KeyValue == (char)Keys.Left)
            {
               if(x > 0)
                {
                   x -= Constants.unit;
                }
            }
            if (e.KeyValue == (char)Keys.Up)
            {
                if (y > 0)
                {
                    y -= Constants.unit;
                }
            }
            if (e.KeyValue == (char)Keys.Right)
            {
                x += Constants.unit;
            }
            if (e.KeyValue == (char)Keys.Down)
            {
                y += Constants.unit;
            }
            //Console.WriteLine(x);
            //Console.WriteLine(y);
        }

        private void GameRenderer(object sender, PaintEventArgs e) 
        {

            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            Image2D myImage = SpriteFactory.GetInstance().GetSprite(Sprites.slimusDownLarge);
            e.Graphics.DrawImage(myImage.image, x, y);

            
            //Pen pen = new Pen(Color.Yellow);
            //Rectangle rect = new Rectangle(x, y, 20, 20);
            //g.DrawRectangle(pen, rect);

            

            oGameForm.Text = String.Format("Explorus - FPS {0}", framerate.ToString());
        }
    }
}
