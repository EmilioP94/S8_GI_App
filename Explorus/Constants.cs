using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus
{
    internal static class Constants
    {
        public const int unit = 48;//48px

        public const int LabyrinthWidth = 5;
        public const int LabyrinthHeight = 5;

        public const double playerSpeed = 5.0 / 100000000;
        public const int snapDistance = 5; //quand la distance en pixel entre slimus et sa destination est inferieur a cette valeur, il 'snap' vers la destination

        public static readonly Models.Sprites[,] level_1 =
        {
            {Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.gem, Models.Sprites.empty, Models.Sprites.gem, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.slimusDownLarge, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.door, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall},
        };

        public static readonly int[] initialSlimusPosition = { 3, 1 };
    }
}
