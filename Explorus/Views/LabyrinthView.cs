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
    internal class LabyrinthView : IRenderableComponent
    {
        private ILabyrinth lab;

        public LabyrinthView(ILabyrinth labyrinthe)
        {
            lab = labyrinthe;
        }

        public void Render(object sender, PaintEventArgs e, Point offset)
        {
            if (lab != null)
            {
                foreach (ILabyrinthComponent component in lab.labyrinthComponentList)
                {
                    if(component.image != null)
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
        }
    }
}
