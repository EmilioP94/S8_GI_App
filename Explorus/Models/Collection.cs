using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Explorus.Models
{
    internal class Collection : ICollection, IObservable<Collection>
    {
        public Sprites[,] map { get; private set; }
        public int total { get; private set ; }
        public int acquired { get ; private set ; }
        public Sprites sprite { get; private set ; }
        public Bars barName { get; private set ; }
        public bool defaultFull { get; private set; }

        private List<IObserver<Collection>> observers = new List<IObserver<Collection>>();

        public Collection(Sprites[,] map, Sprites sprite, Bars barName, bool defaultFull)
        {
            this.map = map;
            this.sprite = sprite;
            this.barName = barName;
            this.defaultFull = defaultFull;
            Count();
        }

        private void Count()
        {
            int count = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == sprite)
                    {
                        count++;
                    }
                }
            }
           
            if(defaultFull)
            {
                total = 3;
                acquired = 3;
            }
            else
                total = count;
        }

        public void Acquire()
        {
            acquired++;
            NotifyObservers();
        }

        private void NotifyObservers()
        {
            foreach (IObserver<Collection> observer in observers)
            {
                observer.OnNext(this);
            }
        }

        public IDisposable Subscribe(IObserver<Collection> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber<Collection>(observers, observer);
        }
    }
}
