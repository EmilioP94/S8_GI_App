using Explorus.Models;
using Explorus.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Controllers
{
    enum BarPieces
    {
        empty,
        half,
        full
    }
    internal class HeaderController : Models.IObserver<ICollection>, Models.IObservable<List<HeaderComponent>>
    {
        public List<HeaderComponent> components { get; set; }
        private int spacing = Constants.unit / 2;
        private int unit = Constants.unit;
        private List<ICollection> barList = new List<ICollection>(5);
        private ICollection redBar;
        private ICollection blueBar;
        private ICollection yellowBar;
        private ICollection redBar2;
        private ICollection blueBar2;
        //private ICollection yellowBar2;
        private List<Models.IObserver<List<HeaderComponent>>> _observers = new List<Models.IObserver<List<HeaderComponent>>>();

        public HeaderController(ILabyrinth lab)
        {
            yellowBar = lab.playerCharacter.gems;
            redBar = lab.playerCharacter.hearts;
            blueBar = lab.playerCharacter.bubbles;
            redBar2 = lab.player2.hearts;
            blueBar2 = lab.player2.bubbles;
            barList.Add(yellowBar);
            barList.Add(redBar);
            barList.Add(blueBar);
            barList.Add(redBar2);
            barList.Add(blueBar2);
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
            foreach ((ICollection bar, int index) in barList.Select((value, i) => (value, i)))
            {
                BarPieces[] barContents = new BarPieces[3];
                switch (bar.acquired)
                {
                    case 0:
                        barContents[0] = BarPieces.empty;
                        barContents[1] = BarPieces.empty;
                        barContents[2] = BarPieces.empty;
                        break;
                    case 1:
                        barContents[0] = BarPieces.half;
                        barContents[1] = BarPieces.empty;
                        barContents[2] = BarPieces.empty;
                        break;
                    case 2:
                        barContents[0] = BarPieces.full;
                        barContents[1] = BarPieces.empty;
                        barContents[2] = BarPieces.empty;
                        break;
                    case 3:
                        barContents[0] = BarPieces.full;
                        barContents[1] = BarPieces.half;
                        barContents[2] = BarPieces.empty;
                        break;
                    case 4:
                        barContents[0] = BarPieces.full;
                        barContents[1] = BarPieces.full;
                        barContents[2] = BarPieces.empty;
                        break;
                    case 5:
                        barContents[0] = BarPieces.full;
                        barContents[1] = BarPieces.full;
                        barContents[2] = BarPieces.half;
                        break;
                    case 6:
                        barContents[0] = BarPieces.full;
                        barContents[1] = BarPieces.full;
                        barContents[2] = BarPieces.full;
                        break;
                }

                if(index == 1 || index == 3)
                {
                    position++;
                    components.Add(GetComponent(position, index == 1 ? Sprites.miniSlime : Sprites.pinkMiniSlime, bar.barName, "", false));
                    position++;
                }
                components.Add(GetComponent(position, bar.sprite, bar.barName, $"{bar.barName} icon", true));
                position++;
                components.Add(GetComponent(position, Sprites.leftBarTip, bar.barName, $"{bar.barName} left"));
                position++;
                for (int i = 0; i < barContents.Length; i++)
                {
                    BarPieces barPiece = barContents[i];
                    switch (barPiece)
                    {
                        case BarPieces.empty:
                            components.Add(GetComponent(position, Sprites.emptyBar, bar.barName, $"{bar.barName} empty"));
                            break;
                        case BarPieces.half:
                            components.Add(GetComponent(position, GetBarSprite(bar.barName, false), bar.barName, $"{bar.barName} half"));
                            break;
                        case BarPieces.full:
                            components.Add(GetComponent(position, GetBarSprite(bar.barName, true), bar.barName, $"{bar.barName} full"));
                            break;
                    }
                    position++;
                }
                components.Add(GetComponent(position, Sprites.rightBarTip, bar.barName, $"{bar.barName} right"));
                if (bar.barName == Bars.yellow && bar.acquired == bar.total)
                {
                    components.Add(GetComponent(position, Sprites.key, bar.barName, $"{bar.barName} key", true));
                }
            }
        }

        public void OnNext(ICollection value)
        {
            Console.WriteLine($"collected a {value.sprite}");
            switch (value.sprite)
            {
                case Sprites.gem:
                    yellowBar = value;
                    break;
                case Sprites.smallBubble:
                    blueBar = value;
                    break;
                default:
                    break;
            }

            GenerateBars();
            NotifyObservers();

        }
        private void NotifyObservers()
        {
            foreach (Models.IObserver<List<HeaderComponent>> observer in _observers)
            {
                observer.OnNext(components);
            }
        }

        public IDisposable Subscribe(Models.IObserver<List<HeaderComponent>> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber<List<HeaderComponent>>(_observers, observer);
        }
    }
}
