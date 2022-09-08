using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class Wall : LabyrinthComponent
    {
        public Wall(int x, int y, Image2D image) : base(x, y, image)
        {
            isSolid = true;
            hitbox = new Rectangle(x, y, Constants.unit * 2, Constants.unit * 2);
        }
    }
}
