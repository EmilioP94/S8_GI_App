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
        static int unit = Constants.unit;
        public static Dictionary<Sprites, Rectangle> GetSpritesRegions()
        {
            Dictionary<Sprites, Rectangle> spritesRegions = new Dictionary<Sprites, Rectangle>()
            {
                { Sprites.wall, new Rectangle(0, 0, unit * 2, unit * 2) },
                { Sprites.door, new Rectangle(0, 0, unit * 2, unit * 2) },
                { Sprites.title, new Rectangle(unit * 2, 0, unit * 4, unit)},
                { Sprites.heart, new Rectangle(unit * 6, 0, unit, unit)},
                { Sprites.bigBubble, new Rectangle(unit * 7, 0, unit, unit)},
                { Sprites.smallBubble, new Rectangle(unit * 8, 0, unit, unit)},
                { Sprites.poppedBubble, new Rectangle(unit * 9, 0, unit, unit)},
                { Sprites.gem, new Rectangle(unit * 10, 0, unit, unit) },
                { Sprites.key, new Rectangle(unit * 11, 0, unit, unit) },
                { Sprites.slimusDownLarge, new Rectangle(0, unit * 2, unit * 2, unit * 2) },
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
                { Sprites.leftBarTip, new Rectangle(unit * 2, unit, unit, unit) },
                { Sprites.redBarFull, new Rectangle(unit * 3, unit, unit, unit) },
                { Sprites.redBarHalf, new Rectangle(unit * 4, unit, unit, unit) },
                { Sprites.blueBarFull, new Rectangle(unit * 5, unit, unit, unit) },
                { Sprites.blueBarHalf, new Rectangle(unit * 6, unit, unit, unit) },
                { Sprites.yellowBarFull, new Rectangle(unit * 7, unit, unit, unit) },
                { Sprites.yellowBarHalf, new Rectangle(unit * 8, unit, unit, unit) },
                { Sprites.emptyBar, new Rectangle(unit * 9, unit, unit, unit) },
                { Sprites.rightBarTip, new Rectangle(unit * 10, unit, unit, unit) },
                { Sprites.miniSlime, new Rectangle(unit * 11, unit, unit, unit) },
                { Sprites.toxicSlimeDownLarge, new Rectangle(0, unit * 6, unit * 2, unit * 2) },
                { Sprites.toxicSlimeDownMedium, new Rectangle(Constants.unit * 2, Constants.unit * 6, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.toxicSlimeDownSmall, new Rectangle(Constants.unit * 4, Constants.unit * 6, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.toxicSlimeRightLarge, new Rectangle(Constants.unit * 6, Constants.unit * 6, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.toxicSlimeRightMedium, new Rectangle(Constants.unit * 8, Constants.unit * 6, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.toxicSlimeRightSmall, new Rectangle(Constants.unit * 10, Constants.unit * 6, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.toxicSlimeUpLarge, new Rectangle(0, Constants.unit * 6, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.toxicSlimeUpMedium, new Rectangle(Constants.unit * 2, Constants.unit * 8, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.toxicSlimeUpSmall, new Rectangle(Constants.unit * 4, Constants.unit * 8, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.toxicSlimeLeftLarge, new Rectangle(Constants.unit * 6, Constants.unit * 8, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.toxicSlimeLeftMedium, new Rectangle(Constants.unit * 8, Constants.unit * 8, Constants.unit * 2, Constants.unit * 2) },
                { Sprites.toxicSlimeLeftSmall, new Rectangle(Constants.unit * 10, Constants.unit * 8, Constants.unit * 2, Constants.unit * 2) },
            };
            return spritesRegions;
        }
    }
}
