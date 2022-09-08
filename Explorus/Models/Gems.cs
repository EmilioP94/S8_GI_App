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
            hitbox = new Rectangle(x, y, Constants.unit * 2, Constants.unit * 2);
        }

        public override bool Collide(Slimus player)
        {
            if (isCollected)
            {
                return false;
            }
            player.gems.Acquire();
            isCollected = true;
            image = null;
            return false;
        }
    }
}
