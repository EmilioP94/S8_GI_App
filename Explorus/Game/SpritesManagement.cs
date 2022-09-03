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
                { Sprites.title, new Rectangle(Constants.unit * 2, 0, Constants.unit * 4, Constants.unit)},
                { Sprites.heart, new Rectangle(0, 0, Constants.unit * 2, Constants.unit * 2)},
                { Sprites.bigBubble, new Rectangle(Constants.unit * 8, 0, Constants.unit, Constants.unit)},
                { Sprites.smallBubble, new Rectangle(Constants.unit * 9, 0, Constants.unit, Constants.unit)},
                { Sprites.poppedBubble, new Rectangle(Constants.unit * 10, 0, Constants.unit, Constants.unit)},
                { Sprites.gem, new Rectangle(Constants.unit * 11, 0, Constants.unit, Constants.unit) },
                { Sprites.key, new Rectangle(Constants.unit * 12, 0, Constants.unit, Constants.unit) },
                { Sprites.slimusDownLarge, new Rectangle(0, Constants.unit * 2, Constants.unit * 2, Constants.unit * 2) },
            };
            return spritesRegions;
        }
    }
}
