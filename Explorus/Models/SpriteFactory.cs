﻿using Explorus.Game;
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
        empty,
        wall,
        door,
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
        private Dictionary<Sprites, Rectangle> _spritesRegions;
        private Dictionary<Sprites, Image2D> _images = new Dictionary<Sprites, Image2D>();
        private static SpriteFactory _instance = null;

        private SpriteFactory() 
        {
            _spritesRegions = SpritesManagement.GetSpritesRegions();
        }

        public static SpriteFactory GetInstance()
        {
            if(_instance == null)
            {
                _instance = new SpriteFactory();
            }
            return _instance;
        }

        public Image2D GetSprite(Sprites sprite)
        {
            if (_images.ContainsKey(sprite))
            {
                return _images[sprite];
            }
            if (_spritesRegions.ContainsKey(sprite))
            {
                Bitmap image = ImageCutter.GetSprites(Properties.Resources.TilesSheet, _spritesRegions[sprite]);
                Image2D image2d = new Image2D(image, SpritesToImageType.ConvertSpritesToImageType(sprite));
                _images.Add(sprite, image2d);
                return _images[sprite];
            }
            else
            {
                throw new Exception();
            }

        }

    }
}
