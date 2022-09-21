using Explorus.Threads;
using System.Drawing;


namespace Explorus.Models
{
    internal class Gems : LabyrinthComponent
    {
        private bool isCollected = false;
        public Gems(int x, int y, Image2D image) : base(x, y, image)
        {
            this.x += Constants.unit / 2;
            this.y += Constants.unit / 2;
            hitbox = new Rectangle(x + Constants.unit/2, y + Constants.unit/2, Constants.unit, Constants.unit);
        }

        public override bool Collide(ILabyrinthComponent comp)
        {
            if (comp.GetType() != typeof(Slimus))
                return false;

            Slimus player = (Slimus)comp;

            if (isCollected)
            {
                return false;
            }
            player.gems.Acquire();
            isCollected = true;
            image = null;
            hitbox = new Rectangle();
            if (player.gems.acquired == player.gems.total)
            {
                AudioThread.GetInstance().QueueSound(SoundTypes.allGems);
            }
            else
            {
                AudioThread.GetInstance().QueueSound(SoundTypes.gemCollection);
            }
            return false;
        }
    }
}
