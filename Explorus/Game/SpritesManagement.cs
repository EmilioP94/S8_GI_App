using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Game
{
    internal static class SpritesManagement
    {
        public static Dictionary<Sprites, Rectangle> GetSpritesRegions()
        {
            Dictionary<Sprites, Rectangle> spritesRegions = new Dictionary<Sprites, Rectangle>()
            {
                { Sprites.wall, new Rectangle(0, 0, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.door, new Rectangle(0, 0, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.title, new Rectangle(Constants.unit * 2, 0, Constants.unit * 4, Constants.unit)},
                { Sprites.heart, new Rectangle(0, 0, Constants.unit * 2, Constants.unit * 2)},
                { Sprites.bigBubble, new Rectangle(Constants.unit * 7, 0, Constants.unit, Constants.unit)},
                { Sprites.smallBubble, new Rectangle(Constants.unit * 8, 0, Constants.unit, Constants.unit)},
                { Sprites.poppedBubble, new Rectangle(Constants.unit * 9, 0, Constants.unit, Constants.unit)},
                { Sprites.gem, new Rectangle(Constants.unit * 10, 0, Constants.unit, Constants.unit) },
                { Sprites.key, new Rectangle(Constants.unit * 11, 0, Constants.unit, Constants.unit) },
                { Sprites.slimusDownLarge, new Rectangle(0, Constants.unit * 2, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.slimusDownMedium, new Rectangle(Constants.unit * 2, Constants.unit * 2, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.slimusDownSmall, new Rectangle(Constants.unit * 4, Constants.unit * 2, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.slimusRightLarge, new Rectangle(Constants.unit * 6, Constants.unit * 2, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.slimusRightMedium, new Rectangle(Constants.unit * 8, Constants.unit * 2, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.slimusRightSmall, new Rectangle(Constants.unit * 10, Constants.unit * 2, Constants.unit * 2, Constants.unit * 2) },

                { Sprites.slimusUpLarge, new Rectangle(0, Constants.unit * 4, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.slimusUpMedium, new Rectangle(Constants.unit * 2, Constants.unit * 4, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.slimusUpSmall, new Rectangle(Constants.unit * 4, Constants.unit * 4, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.slimusLeftLarge, new Rectangle(Constants.unit * 6, Constants.unit * 4, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.slimusLeftMedium, new Rectangle(Constants.unit * 8, Constants.unit * 4, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.slimusLeftSmall, new Rectangle(Constants.unit * 10, Constants.unit * 4, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.empty, new Rectangle(97, 50, 1, 1) },
            };
            return spritesRegions;
        }
    }
}
