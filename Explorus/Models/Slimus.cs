using System.Drawing;
using Explorus.Controllers;

namespace Explorus.Models
{
    internal class Slimus : Slime
    {
        public Collection gems { get; private set; }
        public Collection hearts { get; private set; }
        public Collection bubbles { get; private set; }
        public Slimus(int x, int y) : base(x, y, SpriteFactory.GetInstance().GetSprite(Sprites.slimusDownLarge))
        {
            gems = new Collection(Sprites.gem, Bars.yellow, false);
            hearts = new Collection(Sprites.heart, Bars.red, true);
            bubbles = new Collection(Sprites.smallBubble, Bars.blue, true);
            animationImages = new Image2D[3, 4];

            var SFInstance = SpriteFactory.GetInstance();

            animationImages[0, 0] = SFInstance.GetSprite(Sprites.slimusUpLarge);
            animationImages[1, 0] = SFInstance.GetSprite(Sprites.slimusUpMedium);
            animationImages[2, 0] = SFInstance.GetSprite(Sprites.slimusUpSmall);

            animationImages[0, 1] = SFInstance.GetSprite(Sprites.slimusRightLarge);
            animationImages[1, 1] = SFInstance.GetSprite(Sprites.slimusRightMedium);
            animationImages[2, 1] = SFInstance.GetSprite(Sprites.slimusRightSmall);

            animationImages[0, 2] = SFInstance.GetSprite(Sprites.slimusDownLarge);
            animationImages[1, 2] = SFInstance.GetSprite(Sprites.slimusDownMedium);
            animationImages[2, 2] = SFInstance.GetSprite(Sprites.slimusDownSmall);

            animationImages[0, 3] = SFInstance.GetSprite(Sprites.slimusLeftLarge);
            animationImages[1, 3] = SFInstance.GetSprite(Sprites.slimusLeftMedium);
            animationImages[2, 3] = SFInstance.GetSprite(Sprites.slimusLeftSmall);
        }
        public void NewLevel(int x, int y)
        {
            this.x = x;
            this.y = y;
            currentDirection = Direction.None;
            destinationPoint = new Point(x, y);
            gems.Empty();
        }

        public Direction GetDirection()
        {
            return LastNotNoneDirection;
        }
    }
}
