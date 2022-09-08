using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    enum ImageType
    {
        Wall,
        Player,
        Collectible,
        MiniSlime,
        Door,
        Other,
        Nothing
    }
    internal class Image2D
    {
        public ImageType type { get; private set; }
        public Bitmap image { get; private set; }

        public Image2D(Bitmap image, ImageType type)
        {
           this.image = image;
           this.type = type;
        }
    }
}
