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
        //public int[] slimusPosition { get; set; }

        private List<ILabyrinthComponent> _labyrinthComponentList;
        public List<ILabyrinthComponent> labyrinthComponentList { get { return _labyrinthComponentList; } set { this._labyrinthComponentList = value; } }
        public Slimus playerCharacter { get; private set; }

        public Collection gems { get; set ; }

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
            //slimusPosition = Constants.initialSlimusPosition;
            gems = new Collection(map, Sprites.gem, Bars.yellow, false);
            NotifyObservers();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    ILabyrinthComponent comp = LabyrinthComponentFactory.GetLabyrinthComponent(map[i, j], Constants.unit * j * 2, Constants.unit * i * 2);
                    labyrinthComponentList.Add(comp);
                }
            }
            foreach(Slimus player in labyrinthComponentList.OfType<Slimus>())
            {
                playerCharacter = player;
            }
        }

        public IDisposable Subscribe(IObserver<Sprites[,]> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber<Sprites[,]>(observers, observer);
        }
    }
}
