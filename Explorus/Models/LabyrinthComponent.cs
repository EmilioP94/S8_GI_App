using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Explorus.Models
{
    internal class LabyrinthComponent : ILabyrinthComponent
    {
        public int x { get; protected set; }

        public int y { get; protected set; }

        public virtual Image2D image { get; protected set; }
        public ImageAttributes attributes { get; protected set; } = null;

        public bool isSolid { get; protected set; }

        public Rectangle hitbox { get; protected set; }

        public LabyrinthComponent(int x, int y, Image2D image)
        {
            this.x = x;
            this.y = y;
            this.image = image;
        }

        public virtual bool Collide(Slimus player)
        {
            return true;
        }
    }
}
