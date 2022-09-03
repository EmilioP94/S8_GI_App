﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    enum ImageType
    {
        Wall,
        Player,
        Collectible,
        Other
    }
    internal class Image2D
    {
        private int _id;
        ImageType type;
        string name;
        public Bitmap image;

        public Image2D(Bitmap image)
        {
           this.image = image;
        }
    }
}