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

        List<ILabyrinthComponent> labyrinthComponentList { get; }

        Slimus playerCharacter { get; }
        Slimus player2 { get; }

        List<ToxicSlime> toxicSlimes { get; }

        bool gameEnded { get; }
        [MethodImpl(MethodImplOptions.Synchronized)]
        void CreateBubble(Slimus player);
        [MethodImpl(MethodImplOptions.Synchronized)]
        void CreateGems(int x,int y);
        void Reload(Sprites[,] map);

        List<ILabyrinthComponent> GetComponentListCopy();
    }
}
