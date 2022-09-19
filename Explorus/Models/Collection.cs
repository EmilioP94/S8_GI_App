using System;
using System.Collections.Generic;

namespace Explorus.Models
{
    internal class Collection : ICollection, IObservable<ICollection>
    {
        public int total { get; private set ; } = 6;
        public int acquired { get ; private set ; }
        public Sprites sprite { get; private set ; }
        public Bars barName { get; private set ; }

        private List<IObserver<ICollection>> observers = new List<IObserver<ICollection>>();

        public Collection(Sprites sprite, Bars barName, bool defaultFull)
        {
            this.sprite = sprite;
            this.barName = barName;
            if (defaultFull)
            {
                acquired = total;
            }
        }

        public void Acquire()
        {
            if(acquired < total)
            {
                acquired++;
                NotifyObservers();
            }
        }

        public void Decrement()
        {
            if (acquired > 0)
            {
                acquired--;
                NotifyObservers();
            }
        }

        public void Empty()
        {
            acquired = 0;
            NotifyObservers();
        }

        private void NotifyObservers()
        {
            foreach (IObserver<ICollection> observer in observers)
            {
                observer.OnNext(this);
            }
        }

        public IDisposable Subscribe(IObserver<ICollection> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber<ICollection>(observers, observer);
        }
    }
}
