using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Explorus.Models
{
    internal class Hearts : ICollectible
    {
        public Sprites[,] map { get; set; } 
        public int total { get; set; } = 3;
        public int acquired { get; set; } = 3;
        public Sprites sprite { get; set; } = Sprites.heart;
        public Bars barName { get; set; } = Bars.red;

        public Hearts(Sprites[,] map)
        {
            this.map = map;
            //Count();
        }
        public void Count()
        {
            int count = 0;
            for (int i = 0; i < Constants.LabyrinthHeight; i++)
            {
                for (int j = 0; j < Constants.LabyrinthWidth; j++)
                {
                    if (map[i, j] == Sprites.heart)
                    {
                        count++;
                    }
                }
            }
            total = count;
        }
    }
}
