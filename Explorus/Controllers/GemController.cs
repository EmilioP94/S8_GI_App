using Explorus.Models;
using Explorus.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Controllers
{
    internal class GemController : IObservable<Collectible>
    {
        private readonly ILabyrinth lab;
        private List<IObserver<Collectible>> observers = new List<IObserver<Collectible>>();

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
            foreach (IObserver<Collectible> observer in observers)
            {
                observer.OnNext(lab.gems);
            }
        }

        public IDisposable Subscribe(IObserver<Collectible> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber<Collectible>(observers, observer);
        }


    }
}
