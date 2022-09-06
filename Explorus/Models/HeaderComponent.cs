using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Models
{
    internal class HeaderComponent : ILabyrinthComponent
    {
        public int x { get; set; }
        public int y { get; set; }

        public Image2D image { get; private set; }

        public HeaderComponent(int x, int y, Image2D image)
        {
            this.x = x;
            this.y = y + Constants.unit/2;
            this.image = image;
        }

        public void Show(PaintEventArgs e, int yOffset)
        {
            e.Graphics.DrawImage(image.image, x, y + yOffset);
        }
    }
}
