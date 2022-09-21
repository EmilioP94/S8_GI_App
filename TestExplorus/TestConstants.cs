using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorus;
using Explorus.Models;

namespace TestExplorus
{
    internal static class TestConstants
    {
        public static readonly Sprites[,] level_1 =
        {
            {Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall },
            {Sprites.wall, Sprites.toxicSlimeDownLarge, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.toxicSlimeDownLarge, Sprites.empty, Sprites.toxicSlimeDownLarge, Sprites.wall },
            {Sprites.wall, Sprites.empty, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.empty, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.empty, Sprites.wall },
            {Sprites.wall, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.toxicSlimeDownLarge, Sprites.empty, Sprites.wall, Sprites.miniSlime, Sprites.wall, Sprites.empty, Sprites.wall },
            {Sprites.wall, Sprites.empty, Sprites.wall, Sprites.empty, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.door, Sprites.wall, Sprites.empty, Sprites.wall},
            {Sprites.wall, Sprites.empty, Sprites.wall, Sprites.empty, Sprites.wall, Sprites.empty, Sprites.toxicSlimeDownLarge, Sprites.empty, Sprites.toxicSlimeDownLarge, Sprites.empty, Sprites.wall},
            {Sprites.wall, Sprites.empty, Sprites.wall, Sprites.empty, Sprites.wall, Sprites.wall, Sprites.empty, Sprites.empty, Sprites.wall, Sprites.empty, Sprites.wall},
            {Sprites.wall, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.slimusDownLarge, Sprites.wall, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall},
            {Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall},
        };

        public static readonly Sprites[,] level_2 =
        {
            {Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall },
            {Sprites.wall, Sprites.empty, Sprites.wall, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall },
            {Sprites.wall, Sprites.empty, Sprites.empty, Sprites.wall, Sprites.wall, Sprites.empty, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.empty, Sprites.wall },
            {Sprites.wall, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall, Sprites.miniSlime, Sprites.wall, Sprites.empty, Sprites.wall },
            {Sprites.wall, Sprites.gem, Sprites.wall, Sprites.empty, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.door, Sprites.wall, Sprites.empty, Sprites.wall},
            {Sprites.wall, Sprites.empty, Sprites.wall, Sprites.empty, Sprites.wall, Sprites.gem, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall},
            {Sprites.wall, Sprites.empty, Sprites.wall, Sprites.empty, Sprites.wall, Sprites.wall, Sprites.empty, Sprites.wall, Sprites.wall, Sprites.empty, Sprites.wall},
            {Sprites.wall, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.slimusDownLarge, Sprites.wall, Sprites.empty, Sprites.empty, Sprites.gem, Sprites.empty, Sprites.wall},
            {Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall, Sprites.wall},
        };

        public static readonly Level[] levels =
        {
            new Level(level_1),
            new Level(level_2),
        };
    }
}
