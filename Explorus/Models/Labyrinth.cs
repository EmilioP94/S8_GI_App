using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class Labyrinth: ILabyrinth
    {
        public Sprites[,] map { get; private set; }
        private List<IObserver<Sprites[,]>> observers;

        public Labyrinth()
        {
            map = Constants.level_1;
        }

        public IDisposable Subscribe(IObserver<Sprites[,]> observer)
        {
            throw new NotImplementedException();
        }
    }
}
