using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal interface ILabyrinthComponent
    {
        int x { get; }
        int y { get; }
        Image2D image { get; }
    }
}
