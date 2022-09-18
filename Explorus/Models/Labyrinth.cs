using Explorus.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace Explorus.Models
{
    internal class Labyrinth: ILabyrinth
    {
        public Sprites[,] map { get; private set; }
        public List<ILabyrinthComponent> labyrinthComponentList { get; private set; }
        public Slimus playerCharacter { get; private set; }

        public List<MiniSlime> miniSlimes { get; private set; }

        public Collection gems { get; private set ; }

        public bool gameEnded { get
            {
                return miniSlimes.All(slime => slime.isCollected);
            } 
        }
        


        public Labyrinth(Sprites[,] map)
        {
            miniSlimes = new List<MiniSlime>();
            this.map = map;
            labyrinthComponentList = new List<ILabyrinthComponent>();
            gems = new Collection(map, Sprites.gem, Bars.yellow, false);

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
                playerCharacter.SetCollections(gems);
            }
            foreach (MiniSlime slime in labyrinthComponentList.OfType<MiniSlime>())
            {
                miniSlimes.Add(slime);
            }
        }
        public void CreateBubble()
        {
            Bubble bubble = new Bubble(
                playerCharacter.x,
                playerCharacter.y,
                SpriteFactory.GetInstance().GetSprite(Sprites.bigBubble),
                SpriteFactory.GetInstance().GetSprite(Sprites.poppedBubble),
                playerCharacter.GetDirection()
                );

            labyrinthComponentList.Add(bubble);
        }
    }
}
