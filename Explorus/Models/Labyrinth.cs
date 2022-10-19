using Explorus.Controllers;
using Explorus.Models.GameEvents;
using Explorus.Threads;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Explorus.Models
{
    internal class Labyrinth : ILabyrinth
    {
        public Sprites[,] map { get; private set; }
        public List<ILabyrinthComponent> labyrinthComponentList { get; private set; }

        public List<Slimus> players { get; private set; } = new List<Slimus>(2);

        public List<MiniSlime> miniSlimes { get; private set; }

        public bool gameEnded
        {
            get
            {
                return miniSlimes.All(slime => slime.isCollected);
            }
        }

        public List<ToxicSlime> toxicSlimes { get; private set; }

        public Labyrinth(Sprites[,] map)
        {
            miniSlimes = new List<MiniSlime>();
            toxicSlimes = new List<ToxicSlime>();
            this.map = map;
            labyrinthComponentList = new List<ILabyrinthComponent>();
            Collection gems = new Collection(Sprites.gem, Bars.yellow, false);

            for (int i = 0; i < map.GetLength(0); i++)
            {
                bool p1Added = false;
                bool p2Added = false;
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    ILabyrinthComponent comp = LabyrinthComponentFactory.GetLabyrinthComponent(map[i, j], Constants.unit * j * 2, Constants.unit * i * 2);
                    if(comp is Slimus)
                    {
                        if(!p1Added)
                        {
                            p1Added = true;
                            labyrinthComponentList.Add(comp);
                        }
                        else if (!p2Added && GameState.GetInstance().multiplayer)
                        {
                            p2Added = true;
                            labyrinthComponentList.Add(comp);
                        }
                        else
                        {
                            labyrinthComponentList.Add(LabyrinthComponentFactory.GetLabyrinthComponent(Sprites.empty, comp.x, comp.y));
                        }
                    }
                    else
                    {
                        labyrinthComponentList.Add(comp);
                    }
                }
            }
            foreach ((Slimus player, int index) in labyrinthComponentList.OfType<Slimus>().Select((value, i) => (value, i)))
            {
                if (players.Count() == 0)
                {
                    player.gems = gems;
                    players.Add(player);
                }
                else if (GameState.GetInstance().multiplayer)
                {
                    Console.WriteLine("Game is multiplayer");
                    if (players.Count() == 1)
                    {
                        Console.WriteLine("adding player 2");
                        player.gems = gems;
                        players.Add(player);
                    }
                }
            }
            foreach (MiniSlime slime in labyrinthComponentList.OfType<MiniSlime>())
            {
                miniSlimes.Add(slime);
            }
            foreach (ToxicSlime slime in labyrinthComponentList.OfType<ToxicSlime>())
            {
                toxicSlimes.Add(slime);
            }
        }

        public void Reload(Sprites[,] map)
        {
            this.map = map;
            labyrinthComponentList = new List<ILabyrinthComponent>();
            miniSlimes = new List<MiniSlime>();
            toxicSlimes = new List<ToxicSlime>();

            lock (labyrinthComponentList)
            {
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] != Sprites.slimusDownLarge)
                        {
                            ILabyrinthComponent comp = LabyrinthComponentFactory.GetLabyrinthComponent(map[i, j], Constants.unit * j * 2, Constants.unit * i * 2);
                            labyrinthComponentList.Add(comp);
                        }
                        else
                        {
                            if (players.ElementAt(0) != null)
                            {
                                players.ElementAt(0).NewLevel(Constants.unit * j * 2, Constants.unit * i * 2);
                                labyrinthComponentList.Add(players.ElementAt(0));
                            }
                            if(GameState.GetInstance().multiplayer && players.ElementAt(1) != null)
                            {
                                players.ElementAt(1).NewLevel(Constants.unit * j * 2, Constants.unit * i * 2);
                                labyrinthComponentList.Add(players.ElementAt(1));
                            }
                        }
                    }
                }
            }
            foreach (MiniSlime slime in labyrinthComponentList.OfType<MiniSlime>())
            {
                miniSlimes.Add(slime);
            }
            foreach (ToxicSlime slime in labyrinthComponentList.OfType<ToxicSlime>())
            {
                toxicSlimes.Add(slime);
            }
        }
        public void CreateBubble(Slimus player)
        {
            Bubble bubble = player.Shoot();
            if (bubble != null)
            {
                labyrinthComponentList.Add(bubble);
                AudioThread.GetInstance().QueueSound(SoundTypes.bubbleShoot);
            }

        }

        public void CreateGems(int x, int y)
        {
            ILabyrinthComponent comp = LabyrinthComponentFactory.GetLabyrinthComponent(Sprites.gem, x, y);
            lock (labyrinthComponentList)
            {
                labyrinthComponentList.Add(comp);
            }
        }

        public void RegisterPlayerCollections(HeaderController hc)
        {
            foreach(Slimus player in players)
            {
                player.gems.Subscribe(hc);
                player.hearts.Subscribe(hc);
                player.bubbles.Subscribe(hc);
            }
        }

        public List<ILabyrinthComponent> GetComponentListCopy()
        {
            lock (labyrinthComponentList)
            {
                return labyrinthComponentList.ToList();
            }
        }

        public void Reset()
        {
            foreach (ILabyrinthComponent comp in labyrinthComponentList)
            {
                comp.Reset();
            }
        }
    }
}
