using Explorus.Models;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Explorus.Game
{
    internal static class ImageCutter
    {

        public static Bitmap GetSprites(Bitmap image, Rectangle zone)
        {
                return image.Clone(zone, image.PixelFormat);
        }
    }
}
