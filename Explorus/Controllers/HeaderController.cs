using Explorus.Models;
using Explorus.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Controllers
{
    internal class HeaderController: IObserver<ICollectible>, IObservable<List<HeaderComponent>>
    {
        private ILabyrinth lab;
        public List<HeaderComponent> components { get; set; }
        private int spacing = Constants.unit / 2;
        private int unit = Constants.unit;
        private List<ICollectible> barList = new List<ICollectible>(3);
        private ICollectible redBar;
        private ICollectible blueBar;
        private ICollectible yellowBar;
        private List<IObserver<List<HeaderComponent>>> _observers = new List<IObserver<List<HeaderComponent>>>();

        public HeaderController(ILabyrinth lab)
        {
            redBar = new Collectible(lab.map, Sprites.heart, Bars.red, true);
            blueBar = new Collectible(lab.map, Sprites.bigBubble, Bars.blue, true);
            yellowBar = lab.gems;
            barList.Add(redBar);
            barList.Add(blueBar);
            barList.Add(yellowBar);
            GenerateBars();
        }

        private HeaderComponent GetComponent(int xMultiplier, Sprites sprite, Bars barName, string name, bool space = false)
        {
            int pixelSpace = 0;
            if (space)
                pixelSpace = spacing;
            return new HeaderComponent((unit * xMultiplier) + pixelSpace, 0, SpriteFactory.GetInstance().GetSprite(sprite), barName, name);
        }

        private Sprites GetBarSprite(Bars bar, bool full)
        {
            switch (bar)
            {
                case (Bars.red):
                    return full ? Sprites.redBarFull : Sprites.redBarHalf;
                case (Bars.blue):
                    return full ? Sprites.blueBarFull : Sprites.blueBarHalf;
                case (Bars.yellow):
                    return full ? Sprites.yellowBarFull : Sprites.yellowBarHalf;
                default:
                    return Sprites.emptyBar;
            }
        }

        private void GenerateBars()
        {
            components = new List<HeaderComponent>();
            components.Add(GetComponent(0, Sprites.title, Bars.none, "title"));
            int position = 4; // starts after the title position
            foreach (ICollectible bar in barList)
            {
                components.Add(GetComponent(position, bar.sprite, bar.barName, $"{bar.barName} icon", true));
                position++;
                components.Add(GetComponent(position, Sprites.leftBarTip, bar.barName, $"{bar.barName} left"));
                position++;
                for (int i = 0; i < bar.acquired; i++)
                {
                    components.Add(GetComponent(position, GetBarSprite(bar.barName, true), bar.barName, $"{bar.barName} full"));
                    position++;
                }
                for (int j = 0; j < bar.total - bar.acquired; j++)
                {
                    components.Add(GetComponent(position, Sprites.emptyBar, bar.barName, $"{bar.barName} empty"));
                    position++;
                }
                components.Add(GetComponent(position, Sprites.rightBarTip, bar.barName, $"{bar.barName} right"));
                if(bar.barName == Bars.yellow && bar.acquired == bar.total)
                {
                    components.Add(GetComponent(position, Sprites.key, bar.barName, $"{bar.barName} key", true));
                }
            }
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(ICollectible value)
        {
            Console.WriteLine($"collected a {value.sprite}");
            if(value.sprite == Sprites.gem)
            {
                yellowBar = value;
                Console.WriteLine($"collected {yellowBar.acquired} of {yellowBar.total}");
            }
            GenerateBars();
            NotifyObservers();

        }
        private void NotifyObservers()
        {
            foreach (IObserver<List<HeaderComponent>> observer in _observers)
            {
                observer.OnNext(components);
            }
        }

        public IDisposable Subscribe(IObserver<List<HeaderComponent>> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber<List<HeaderComponent>>(_observers, observer);
        }
    }
}
