using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Controllers
{
    internal interface ILabyrinth: IObservable<Sprites[,]>
    {
        Sprites[,] map { get; }
    }
}
