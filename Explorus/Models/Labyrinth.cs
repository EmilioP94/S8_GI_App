using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Explorus.Models
{
    internal class Labyrinth: ILabyrinth
    {
        private Sprites[,] _map;
        public Sprites[,] map { get { return _map; } private set { this._map = value; this.NotifyObservers(); } }
        public int[] slimusPosition { get; set; }

        private List<ILabyrinthComponent> _labyrinthComponentList;
        public List<ILabyrinthComponent> labyrinthComponentList { get { return _labyrinthComponentList; } set { this._labyrinthComponentList = value; } }

        private ILabyrinthComponent _playerCharacter;
        public ILabyrinthComponent playerCharacter { get { return _playerCharacter; } set { this._playerCharacter = value; } }

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
            labyrinthComponentList = new List<ILabyrinthComponent>();
            slimusPosition = Constants.initialSlimusPosition;
            NotifyObservers();

            for (int i = 0; i < Constants.LabyrinthHeight; i++)
            {
                for (int j = 0; j < Constants.LabyrinthWidth; j++)
                {
                    LabyrinthComponent comp = new LabyrinthComponent(Constants.unit * j * 2, Constants.unit * i * 2, SpriteFactory.GetInstance().GetSprite(map[i, j]));
                    labyrinthComponentList.Add(comp);

                    if (map[i,j]== Sprites.slimusDownLarge)
                    {
                        playerCharacter = comp;
                    }
                }
            }
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
