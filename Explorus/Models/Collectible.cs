using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class Collectible : ICollectible
    {
        public Sprites[,] map { get; set; }
        public int total { get; set ; }
        public int acquired { get ; set ; }
        public Sprites sprite { get; set ; }
        public Bars barName { get; set ; }
        public bool defaultFull { get; set; }

        public Collectible(Sprites[,] map, Sprites sprite, Bars barName, bool defaultFull)
        {
            this.map = map;
            this.sprite = sprite;
            this.barName = barName;
            this.defaultFull = defaultFull;
            Count();
        }

        public void Count()
        {
            int count = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == sprite)
                    {
                        count++;
                    }
                }
            }
           
            if(defaultFull)
            {
                total = 3;
                acquired = 3;
            }
            else
                total = count;
        }

        public void Acquire()
        {
            acquired++;
        }
    }
}
