using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Models
{
    internal interface ILabyrinthComponent
    {
        int x { get; set; }
        int y { get; set; }
        Image2D image { get; }


        bool isSolid { get; set; }

        Rectangle hitbox { get; set; }

        void Show(PaintEventArgs e, int yOffset);

    }
}
