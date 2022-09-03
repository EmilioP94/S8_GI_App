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

        public static readonly Models.Sprites[,] level_1 =
        {
            {Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.slimusDownLarge, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall},
        };
    }
}
