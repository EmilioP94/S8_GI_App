using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class MiniSlime : LabyrinthComponent
    {
        public MiniSlime(int x, int y, Image2D image) : base(x, y, image)
        {
            this.x += Constants.unit / 2;
            this.y += Constants.unit / 2;
            hitbox = new Rectangle(x, y, Constants.unit * 2, Constants.unit * 2);
        }
    }
}
