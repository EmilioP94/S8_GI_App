using Explorus.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    enum Sprites
    {
        wall,
        title,
        heart,
        bigBubble,
        smallBubble,
        poppedBubble,
        gem,
        key,
        slimusDownLarge,
        slimusDownMedium,
        slimusDownSmall
    }
    internal class SpriteFactory
    {
        private int unit = 48; // 48px per block 
        private Dictionary<Sprites, Rectangle> _spritesRegions = new Dictionary<Sprites, Rectangle>();
        private Dictionary<Sprites, Image2D> _images = new Dictionary<Sprites, Image2D>();
        private static SpriteFactory _instance = null;

        private SpriteFactory() 
        {
            _spritesRegions.Add(Sprites.wall, new Rectangle(0, 0, unit * 2, unit * 2));
            _spritesRegions.Add(Sprites.title, new Rectangle(unit * 2, 0, unit * 4, unit));
            _spritesRegions.Add(Sprites.heart, new Rectangle(0, 0, unit * 2, unit * 2));
            _spritesRegions.Add(Sprites.bigBubble, new Rectangle(unit * 8, 0, unit, unit));
            _spritesRegions.Add(Sprites.smallBubble, new Rectangle(unit * 9, 0, unit, unit));
            _spritesRegions.Add(Sprites.poppedBubble, new Rectangle(unit * 10, 0, unit, unit));
            _spritesRegions.Add(Sprites.gem, new Rectangle(unit * 11, 0, unit, unit));
            _spritesRegions.Add(Sprites.key, new Rectangle(unit * 12, 0, unit, unit));
            _spritesRegions.Add(Sprites.slimusDownLarge, new Rectangle(0, unit * 2, unit * 2, unit * 2));
        }

        public static SpriteFactory GetInstance()
        {
            if(_instance == null)
            {
                _instance = new SpriteFactory();
            }
            return _instance;
        }

        public Flyweight GetSprite(Sprites sprite)
        {
            if (_images.ContainsKey(sprite))
            {
                return new Flyweight(_images[sprite]);
            }
            if (_spritesRegions.ContainsKey(sprite))
            {
                Bitmap image = ImageCutter.GetSprites(Properties.Resources.TilesSheet, _spritesRegions[sprite]);
                Image2D image2d = new Image2D(image);
                _images.Add(sprite, image2d);
                return new Flyweight(_images[sprite]);
            }
            else
            {
                throw new Exception();
            }

        }

    }
}
