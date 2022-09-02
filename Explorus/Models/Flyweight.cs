using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class Flyweight
    {
        public Image2D sprite;

        public Flyweight(Image2D sprite)
        {
            this.sprite = sprite;
        }
    }
}
