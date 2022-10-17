using System.Drawing;

namespace Explorus.Models
{
    internal class MiniSlime : LabyrinthComponent
    {
        public bool isCollected { get; private set; }
        public MiniSlime(int x, int y, Image2D image) : base(x, y, image)
        {
            this.x += Constants.unit / 2;
            this.y += Constants.unit / 2;
            hitbox = new Rectangle(x, y, Constants.unit * 2, Constants.unit * 2);
        }

        public override bool Collide(ILabyrinthComponent comp)
        {
            isCollected = true;
            image = null;
            return false;
        }

        public override void Reset()
        {
            base.Reset();
            isCollected = false;
            this.x += Constants.unit / 2;
            this.y += Constants.unit / 2;
            hitbox = new Rectangle(x, y, Constants.unit * 2, Constants.unit * 2);
        }
    }
}
