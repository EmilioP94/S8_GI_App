using Explorus.Models;
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
        public const int msPerFrame = 14;

        public const int unit = 48;//48px

        public const double playerSpeed = 0.4;

        public const int snapDistance = 15; //quand la distance en pixel entre slimus et sa destination est inferieur a cette valeur, il 'snap' vers la destination

        public const int animationChangeThreshold = 60; //valeur entre 0 et 96 

        public const int slimusHitboxHeight = 50;
        public const int slimusHitboxLength = 65;

        public const int maxNumberOfSound = 10;

        public const bool showHitbox = false; //useful for debugging

        public const bool defaultMultiplayer = false;

        public const int initialToxicSlimeHp = 2;

        public static readonly Models.Sprites[,] level_1 =
        {
            {Sprites.wall, Sprites.wall,               Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,               Sprites.wall,            Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,               Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.toxicSlimeFollow,Sprites.empty,           Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.wall,  Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,               Sprites.wall,            Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,  Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.toxicSlimeFollow,Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty,              Sprites.empty,           Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.toxicSlimeParallel,Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.empty,              Sprites.wall,            Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.toxicSlimeParallel,Sprites.empty,           Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,  Sprites.door,               Sprites.wall,            Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.wall,               Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.miniSlime,          Sprites.empty,           Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.wall,               Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,  Sprites.wall,               Sprites.wall,            Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty,              Sprites.empty,           Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.empty,              Sprites.wall,            Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.toxicSlimeRunAndFollow,Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty,              Sprites.empty,           Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.toxicSlimeRunAndFollow,Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.wall,  Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,               Sprites.wall,            Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,  Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.slimusDownLarge,    Sprites.player2DownLarge, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.wall,               Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,               Sprites.wall,            Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,               Sprites.wall},
        };
        public static readonly Models.Sprites[,] level_2 =
        {
            {Sprites.wall, Sprites.wall,               Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,               Sprites.wall,            Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,               Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.toxicSlimeFollow,Sprites.empty,           Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.wall,  Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,               Sprites.wall,            Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,  Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.toxicSlimeFollow,Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty,              Sprites.empty,           Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.toxicSlimeFollow,Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.empty,              Sprites.wall,            Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.toxicSlimeFollow,Sprites.empty,           Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,  Sprites.door,               Sprites.wall,            Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.wall,               Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.miniSlime,          Sprites.empty,           Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.wall,               Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,  Sprites.wall,               Sprites.wall,            Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty,              Sprites.empty,           Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.empty,              Sprites.wall,            Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.toxicSlimeFollow,Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty,              Sprites.empty,           Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.toxicSlimeFollow,Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.wall,  Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,               Sprites.wall,            Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.wall,  Sprites.wall,  Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.empty,              Sprites.empty, Sprites.empty, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.slimusDownLarge,    Sprites.player2DownLarge, Sprites.empty, Sprites.wall,  Sprites.empty, Sprites.empty, Sprites.empty, Sprites.empty,              Sprites.wall},
            {Sprites.wall, Sprites.wall,               Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,               Sprites.wall,            Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,  Sprites.wall,               Sprites.wall},
        };

        public static readonly Models.Sprites[,] level_3 =
        {
            {Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.miniSlime, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.gem, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.door, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall},
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.gem, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.wall},
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall},
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.slimusDownLarge, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.gem, Models.Sprites.empty, Models.Sprites.wall},
            {Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall},
        };

        public static readonly Models.Level[] levels =
        {
            new Level(level_1),
            new Level(level_2),
            new Level(level_3)
        };

    }
}
