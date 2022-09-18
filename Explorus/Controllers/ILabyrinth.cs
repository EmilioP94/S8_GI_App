using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        List<ToxicSlime> toxicSlimes { get; }

        bool gameEnded { get; }
        void CreateBubble();

        void CreateGems(int x,int y);
        void Reload();

    }
}
