using Explorus.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Controllers
{
    internal interface ILabyrinth
    {
        Sprites[,] map { get; }
        //int[] slimusPosition { get; set; }

        BlockingCollection<ILabyrinthComponent> labyrinthComponentList { get; }

        Slimus playerCharacter { get; }

        List<ToxicSlime> toxicSlimes { get; }

        bool gameEnded { get; }
        [MethodImpl(MethodImplOptions.Synchronized)]
        void CreateBubble();
        [MethodImpl(MethodImplOptions.Synchronized)]
        void CreateGems(int x,int y);
        void Reload(Sprites[,] map);
    }
}
