using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Explorus.Models
{
    internal class LabyrinthComponent : ILabyrinthComponent
    {
        public int x { get; set; }

        public int y { get; set; }

        public Image2D image { get; private set; }

        public bool isSolid = false;
        public Rectangle hitbox { get; private set; }

        public LabyrinthComponent(int x, int y, Image2D image)
        {
            this.x = x;
            this.y = y;
            this.image = image;

            //TODO - for now we can only collide with wall, we need to add gem and other stuff
            if(image.type == ImageType.Wall)
            {
                isSolid = true;
                hitbox = new Rectangle(x, y, Constants.unit * 2, Constants.unit * 2);
            }
        }

        public void Show(PaintEventArgs e)
        {
            e.Graphics.DrawImage(image.image, x, y);
        }
    }
}
