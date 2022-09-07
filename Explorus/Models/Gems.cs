using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class Gems: ICollectible
    {
        public Sprites[,] map { get; set; }
        public int total { get; set; } = 0;
        public int acquired { get; set; } = 0;
        public Sprites sprite { get; set; } = Sprites.gem;
        public Bars barName { get; set; } = Bars.yellow;

        public Gems(Sprites[,] map )
        {
            this.map = map;
            Count();
        }

        public void Count()
        {
            int count = 0;
            for (int i = 0; i < Constants.LabyrinthHeight; i++)
            {
                for (int j = 0; j < Constants.LabyrinthWidth; j++)
                {
                    if (map[i, j] == Sprites.gem)
                    {
                        count++;
                    }
                }
            }
            total = count;
        }

        public void Acquire()
        {
            acquired++;
        }

    }
}
