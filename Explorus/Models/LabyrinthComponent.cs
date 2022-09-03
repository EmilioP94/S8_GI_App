using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class LabyrinthComponent : ILabyrinthComponent
    {
        public int x { get; set; }

        public int y { get; set; }

        public Image2D image { get; private set; }

        public LabyrinthComponent(int x, int y, Image2D image)
        {
            this.x = x;
            this.y = y;
            this.image = image;
        }
    }
}
