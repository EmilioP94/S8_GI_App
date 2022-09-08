using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Models
{
    internal interface ILabyrinthComponent
    {
        int x { get; }
        int y { get; }
        Image2D image { get; }
        ImageAttributes attributes { get; }

        bool isSolid { get; }

        Rectangle hitbox { get; }

        bool Collide(Slimus player);
    }
}
