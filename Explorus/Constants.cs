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
        public const int unit = 48;//48px

        public const double playerSpeed = 0.4;

        public const int snapDistance = 15; //quand la distance en pixel entre slimus et sa destination est inferieur a cette valeur, il 'snap' vers la destination

        public const int animationChangeThreshold = 60; //valeur entre 0 et 96 

        public const int slimusHitboxHeight = 50;
        public const int slimusHitboxLength = 65;

        public const bool showHitbox = false; //useful for debugging

        public static readonly Models.Sprites[,] level_1 =
        {
            {Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.toxicSlimeDownLarge, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.toxicSlimeDownLarge, Models.Sprites.empty, Models.Sprites.toxicSlimeDownLarge, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.toxicSlimeDownLarge, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.miniSlime, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall },
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.door, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall},
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.toxicSlimeDownLarge, Models.Sprites.empty, Models.Sprites.toxicSlimeDownLarge, Models.Sprites.empty, Models.Sprites.wall},
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.wall},
            {Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.slimusDownLarge, Models.Sprites.wall, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.empty, Models.Sprites.wall},
            {Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall, Models.Sprites.wall},
        };
        
        public static readonly Models.Sprites[,] level_2 =
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
        };

    }
}
