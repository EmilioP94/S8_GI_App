using System.Drawing;
using Explorus.Controllers;

namespace Explorus.Models
{
    enum FacingDirection
    {        
        Up,
        Right,
        Down,
        Left
    }
    internal class Slimus : Slime
    {
        public Slimus(int x, int y) : base(x, y)
        {
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

        public FacingDirection GetDirection()
        {
            return currentDirection;
        }

    }
}
