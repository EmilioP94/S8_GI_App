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
        private Sprites[,] _map;
        public Sprites[,] map { get { return _map; } private set { this._map = value; this.NotifyObservers(); } }
        public int[] slimusPosition { get; set; }
   
        private List<IObserver<Sprites[,]>> observers = new List<IObserver<Sprites[,]>>();

        private void NotifyObservers()
        {
            foreach (IObserver<Sprites[,]> observer in observers)
            {
                observer.OnNext(map);
            }
        }

        public Labyrinth()
        {
            map = Constants.level_1;
            slimusPosition = Constants.initialSlimusPosition;
            NotifyObservers();
        }

        public IDisposable Subscribe(IObserver<Sprites[,]> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Sprites[,]>> _observers;
            private IObserver<Sprites[,]> _observer;

            public Unsubscriber(List<IObserver<Sprites[,]>> observers, IObserver<Sprites[,]> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

    }
}
