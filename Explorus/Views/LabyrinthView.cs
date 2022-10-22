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
                //Do not render while calculating positions for replay
                if (GameState.GetInstance().state == GameStates.Replay || GameState.GetInstance().state == GameStates.New)
                {
                    return;
                }
                foreach (ILabyrinthComponent component in lab.GetComponentListCopy())
                {
                    if (component.image != null)
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

                    // unreachable code - debug purposes with the switch in Constants
                    if (Constants.showHitbox)
                    {
                        if (component.GetType() != typeof(Wall) && component.GetType() != typeof(Door))
                        {
                            Pen pen = new Pen(Color.Red, 3);

                            Rectangle rect = new Rectangle(
                                component.hitbox.X + offset.X,
                                component.hitbox.Y + Constants.unit * 2 + offset.Y,
                                component.hitbox.Width,
                                component.hitbox.Height
                                );
                            e.Graphics.DrawRectangle(pen, rect);
                        }
                    }
                }
            }
        }
    }
}
