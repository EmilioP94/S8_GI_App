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
                { Sprites.slimusDownMedium, new Rectangle(unit * 2, unit * 2, unit * 2, unit * 2) },
                { Sprites.slimusDownSmall, new Rectangle(unit * 4, unit * 2, unit * 2, unit * 2) },
                { Sprites.slimusRightLarge, new Rectangle(unit * 6, unit * 2, unit * 2, unit * 2) },
                { Sprites.slimusRightMedium, new Rectangle(unit * 8, unit * 2, unit * 2, unit * 2) },
                { Sprites.slimusRightSmall, new Rectangle(unit * 10, unit * 2, unit * 2, unit * 2) },
                { Sprites.slimusUpLarge, new Rectangle(0, unit * 4, unit * 2, unit * 2) },
                { Sprites.slimusUpMedium, new Rectangle(unit * 2, unit * 4, unit * 2, unit * 2) },
                { Sprites.slimusUpSmall, new Rectangle(unit * 4, unit * 4, unit * 2, unit * 2) },
                { Sprites.slimusLeftLarge, new Rectangle(unit * 6, unit * 4, unit * 2, unit * 2) },
                { Sprites.slimusLeftMedium, new Rectangle(unit * 8, unit * 4, unit * 2, unit * 2) },
                { Sprites.slimusLeftSmall, new Rectangle(unit * 10, unit * 4, unit * 2, unit * 2) },
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
                { Sprites.pinkMiniSlime, new Rectangle(unit * 12, unit, unit, unit) },
                { Sprites.toxicSlimeDownLarge, new Rectangle(0, unit * 6, unit * 2, unit * 2) },
                { Sprites.toxicSlimeDownMedium, new Rectangle(unit * 2, unit * 6, unit * 2, unit * 2) },
                { Sprites.toxicSlimeDownSmall, new Rectangle(unit * 4, unit * 6, unit * 2, unit * 2) },
                { Sprites.toxicSlimeRightLarge, new Rectangle(unit * 6, unit * 6, unit * 2, unit * 2) },
                { Sprites.toxicSlimeRightMedium, new Rectangle(unit * 8, unit * 6, unit * 2, unit * 2) },
                { Sprites.toxicSlimeRightSmall, new Rectangle(unit * 10, unit * 6, unit * 2, unit * 2) },
                { Sprites.toxicSlimeUpLarge, new Rectangle(0, unit * 6, unit * 2, unit * 2) },
                { Sprites.toxicSlimeUpMedium, new Rectangle(unit * 2, unit * 8, unit * 2, unit * 2) },
                { Sprites.toxicSlimeUpSmall, new Rectangle(unit * 4, unit * 8, unit * 2, unit * 2) },
                { Sprites.toxicSlimeLeftLarge, new Rectangle(unit * 6, unit * 8, unit * 2, unit * 2) },
                { Sprites.toxicSlimeLeftMedium, new Rectangle(unit * 8, unit * 8, unit * 2, unit * 2) },
                { Sprites.toxicSlimeLeftSmall, new Rectangle(unit * 10, unit * 8, unit * 2, unit * 2) },
                { Sprites.player2DownLarge, new Rectangle(0, unit * 10, unit * 2, unit * 2) },
                { Sprites.player2DownMedium, new Rectangle(unit * 2, unit * 10, unit * 2, unit * 2) },
                { Sprites.player2DownSmall, new Rectangle(unit * 4, unit * 10, unit * 2, unit * 2) },
                { Sprites.player2RightLarge, new Rectangle(unit * 6, unit * 10, unit * 2, unit * 2) },
                { Sprites.player2RightMedium, new Rectangle(unit * 8, unit * 10, unit * 2, unit * 2) },
                { Sprites.player2RightSmall, new Rectangle(unit * 10, unit * 10, unit * 2, unit * 2) },
                { Sprites.player2UpLarge, new Rectangle(0, unit * 12, unit * 2, unit * 2) },
                { Sprites.player2UpMedium, new Rectangle(unit * 2, unit * 12, unit * 2, unit * 2) },
                { Sprites.player2UpSmall, new Rectangle(unit * 4, unit * 12, unit * 2, unit * 2) },
                { Sprites.player2LeftLarge, new Rectangle(unit * 6, unit * 12, unit * 2, unit * 2) },
                { Sprites.player2LeftMedium, new Rectangle(unit * 8, unit * 12, unit * 2, unit * 2) },
                { Sprites.player2LeftSmall, new Rectangle(unit * 10, unit * 12, unit * 2, unit * 2) },
            };
            return spritesRegions;
        }
    }
}
