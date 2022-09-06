using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Controllers
{
    internal class GemController
    {
        public int gemTotal { get; private set; }
        public int acquiredGems { get; private set; }

        private readonly ILabyrinth lab;

        public GemController(ILabyrinth labyrinth)
        {
            lab = labyrinth;
            gemTotal = CountGems();
            acquiredGems = 0;
        }

        private int CountGems()
        {
            int count = 0;
            for (int i = 0; i < Constants.LabyrinthHeight; i++)
            {
                for (int j = 0; j < Constants.LabyrinthWidth; j++)
                {
                    if (lab.map[i, j] == Sprites.gem)
                    {
                        count ++;
                    }
                }
            }
            return count;
        }

        public void collectGem()
        {
            acquiredGems++;
        }

        public bool openDoor()
        {
            return acquiredGems == gemTotal;
        }
    }
}
