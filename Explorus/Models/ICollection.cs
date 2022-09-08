﻿using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal interface ICollection
    {
        Sprites[,] map { get; }
        int total { get; }
        int acquired { get; }
        Sprites sprite { get; }
        Bars barName { get; }
        bool defaultFull { get; }
        void Count();
    }
}