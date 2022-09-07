using Explorus.Models;
using Explorus.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Controllers
{
    internal class GemController : IObservable<Gems>
    {
        private readonly ILabyrinth lab;
        private List<IObserver<Gems>> observers = new List<IObserver<Gems>>();

        public GemController(ILabyrinth labyrinth)
        {
            lab = labyrinth;
        }


        public void collectGem()
        {
            lab.gems.Acquire();
            NotifyObservers();
        }

        public bool openDoor()
        {
            return lab.gems.acquired == lab.gems.total;
        }

        private void NotifyObservers()
        {
            foreach (IObserver<Gems> observer in observers)
            {
                observer.OnNext(lab.gems);
            }
        }

        public IDisposable Subscribe(IObserver<Gems> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber<Gems>(observers, observer);
        }


    }
}
