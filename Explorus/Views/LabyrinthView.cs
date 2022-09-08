using Explorus.Controllers;
using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Views
{
    internal class LabyrinthView : IRenderableComponent, IObserver<Sprites[,]>
    {
        private ILabyrinth lab;
        private IDisposable unsubscriber;

        public LabyrinthView(ILabyrinth labyrinthe)
        {
            lab = labyrinthe;
        }
        public void OnCompleted()
        {
            this.Unsubscribe();
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Labyrinth observer error");
        }

        public void OnNext(Sprites[,] value)
        {
            //_Sprites = value;
        }

        public void Render(object sender, PaintEventArgs e, Point offset)
        {
            if (lab != null)
            {
                foreach (ILabyrinthComponent component in lab.labyrinthComponentList)
                {
                    e.Graphics.DrawImage(
                        component.image.image,
                        new Rectangle(component.x + offset.X,
                        component.y + Constants.unit * 2 + offset.Y,
                        component.image.image.Width,
                        component.image.image.Height),
                        0,
                        0,
                        component.image.image.Width,
                        component.image.image.Height,
                        GraphicsUnit.Pixel,
                        component.attributes);
                }
            }
        }

        public virtual void Unsubscribe()
        {
            unsubscriber?.Dispose();
        }
    }
}
