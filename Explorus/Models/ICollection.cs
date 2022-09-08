using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal interface ICollection
    {
        Sprites[,] map { get; set; }
        int total { get; set; }
        int acquired { get; set; }
        Sprites sprite { get; set; }
        Bars barName { get; set; }
        bool defaultFull { get; set; }
        void Count();
    }
}
