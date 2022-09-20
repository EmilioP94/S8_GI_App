using Explorus.Controllers;
using Explorus.Threads;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Explorus.Models
{
    internal class Labyrinth: ILabyrinth
    {
        public Sprites[,] map { get; private set; }
        public BlockingCollection<ILabyrinthComponent> labyrinthComponentList { get; private set; }
        public Slimus playerCharacter { get; private set; }

        public List<MiniSlime> miniSlimes { get; private set; }

        public bool gameEnded { get
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
            labyrinthComponentList = new BlockingCollection<ILabyrinthComponent>();
            

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
            foreach (MiniSlime slime in labyrinthComponentList.OfType<MiniSlime>())
            {
                miniSlimes.Add(slime);
            }
            foreach(ToxicSlime slime in labyrinthComponentList.OfType<ToxicSlime>())
            {
                toxicSlimes.Add(slime);
            }
        }

        public void Reload(Sprites[,] map)
        {
            this.map = map;
            labyrinthComponentList = new BlockingCollection<ILabyrinthComponent>();
            miniSlimes = new List<MiniSlime>();
            toxicSlimes = new List<ToxicSlime>();


            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if(map[i, j] != Sprites.slimusDownLarge)
                    {
                        ILabyrinthComponent comp = LabyrinthComponentFactory.GetLabyrinthComponent(map[i, j], Constants.unit * j * 2, Constants.unit * i * 2);
                        labyrinthComponentList.Add(comp);
                    }
                    else
                    {
                        if(playerCharacter != null)
                        {
                            playerCharacter.NewLevel(Constants.unit * j * 2, Constants.unit * i * 2);
                            labyrinthComponentList.Add(playerCharacter);
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
        public void CreateBubble()
        {
            Bubble bubble = playerCharacter.Shoot();
            if(bubble != null)
            {
                labyrinthComponentList.Add(bubble);
            }
            AudioThread.GetInstance().QueueSound(SoundTypes.sound15);
        }

        public void CreateGems(int x, int y)
        {
            ILabyrinthComponent comp = LabyrinthComponentFactory.GetLabyrinthComponent(Sprites.gem, x, y);
            labyrinthComponentList.Add(comp);
        }
    }
}
