using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal interface IRenderableModel
    {
        int x { get; }
        int y { get; }
        Image2D image { get; }
        ImageAttributes attributes { get; }
    }
}
