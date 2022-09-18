using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class Level
    {
        public Sprites[,] map { get; private set; }

        public Level(Sprites[,] map)
        {
            this.map = map;
        }
    }
}
