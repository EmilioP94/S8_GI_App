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
        public static readonly Sprites[,] testMap =
        {
            {Sprites.wall, Sprites.wall,  Sprites.wall,            Sprites.wall,  Sprites.wall,                Sprites.wall, Sprites.wall,      Sprites.wall },
            {Sprites.wall, Sprites.empty, Sprites.empty,           Sprites.empty, Sprites.empty,               Sprites.wall, Sprites.wall,      Sprites.wall },
            {Sprites.wall, Sprites.empty, Sprites.slimusDownLarge, Sprites.empty, Sprites.toxicSlimeDownLarge, Sprites.door, Sprites.miniSlime, Sprites.wall },
            {Sprites.wall, Sprites.empty, Sprites.gem,             Sprites.empty, Sprites.empty,               Sprites.wall, Sprites.wall,      Sprites.wall },
            {Sprites.wall, Sprites.wall,  Sprites.wall,            Sprites.wall,  Sprites.wall,                Sprites.wall, Sprites.wall,      Sprites.wall },
        };
    }
}
