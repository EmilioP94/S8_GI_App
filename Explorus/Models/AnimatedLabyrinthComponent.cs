using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class AnimatedLabyrinthComponent : LabyrinthComponent, IAnimatedComponent
    {
        public AnimatedLabyrinthComponent(int x, int y, Image2D image) : base(x, y, image)
        {
        }

        public void Animate()
        {
            throw new NotImplementedException();
        }
    }
}
