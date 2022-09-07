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
        int[] slimusPosition { get; set; }

        List<ILabyrinthComponent> labyrinthComponentList { get; set; }

        Slimus playerCharacter { get; set; }

    }
}
