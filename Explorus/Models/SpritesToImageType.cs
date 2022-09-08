using Explorus.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal static class SpritesToImageType
    {
        public static ImageType ConvertSpritesToImageType(Sprites sprite)
        {
            switch (sprite)
            {
                case Sprites.wall:
                    return ImageType.Wall;

                case Sprites.heart:
                case Sprites.bigBubble:
                case Sprites.gem:
                    return ImageType.Collectible;

                case Sprites.miniSlime:
                    return ImageType.MiniSlime;

                case Sprites.slimusDownSmall:
                case Sprites.slimusDownMedium:
                case Sprites.slimusDownLarge:
                    return ImageType.Player;

                case Sprites.empty:
                    return ImageType.Nothing;

                case Sprites.door:
                    return ImageType.Door;

                case Sprites.poppedBubble:
                case Sprites.smallBubble:
                case Sprites.title:
                case Sprites.key:
                default:
                    return ImageType.Other;
            }
        }
    }
}
