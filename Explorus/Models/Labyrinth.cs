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
        public Slimus playerCharacter { get; private set; } = null;
        public Slimus player2 { get; private set; } = null;

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
                if (playerCharacter == null)
                {
                    playerCharacter = player;
                    playerCharacter.gems = gems;
                }
                else if (GameState.GetInstance().multiplayer)
                {
                    Console.WriteLine("Game is multiplayer");
                    if (player2 == null)
                    {
                        Console.WriteLine("adding player 2");
                        player2 = player;
                        player.gems = gems;
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
                            if (playerCharacter != null)
                            {
                                playerCharacter.NewLevel(Constants.unit * j * 2, Constants.unit * i * 2);
                                labyrinthComponentList.Add(playerCharacter);
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

        public List<ILabyrinthComponent> GetComponentListCopy()
        {
            lock (labyrinthComponentList)
            {
                return labyrinthComponentList.ToList();
            }
        }
    }
}
