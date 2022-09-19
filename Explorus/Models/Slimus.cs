using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using Explorus.Controllers;

namespace Explorus.Models
{
    internal class Slimus : Slime
    {
        public Collection gems { get; private set; }
        public Collection hearts { get; private set; }
        public Collection bubbles { get; private set; }

        private int rechargeTime = 500;
        private int elapsedTime = 0;
        public bool invincible = false;
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
            hitbox = new Rectangle(x, y, Constants.unit * 2, Constants.unit * 2);
            currentDirection = Direction.None;
            destinationPoint = new Point(x, y);
            gems.Empty();
        }

        public Direction GetDirection()
        {
            return LastNotNoneDirection;
        }

        public override bool Collide(ILabyrinthComponent comp)
        {
            if(!invincible && comp.GetType() == typeof(ToxicSlime))
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = 0.5f;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                this.attributes = attributes;
                invincible = true;
                hearts.Decrement();
                Task.Delay(new TimeSpan(0, 0, 3)).ContinueWith(o =>
                {
                    // trouver une facon de flasher, live ca fait juste nous transparenter sti
                    invincible = false;
                    matrix = new ColorMatrix();
                    matrix.Matrix33 = 1;
                    attributes = new ImageAttributes();
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    this.attributes = attributes;
                });
            }
            return false;
        }

        public Bubble Shoot()
        {
            if (bubbles.acquired == bubbles.total)
            {
                bubbles.Empty();
                return new Bubble(
                   x,
                   y,
                   SpriteFactory.GetInstance().GetSprite(Sprites.bigBubble),
                   SpriteFactory.GetInstance().GetSprite(Sprites.poppedBubble),
                   GetDirection()
                   );
            }
            else return null;
        }

        public void RechargeBubbles(int elapsedTime)
        {
            this.elapsedTime = this.elapsedTime + elapsedTime;
            if(this.elapsedTime > rechargeTime)
            {
                bubbles.Acquire();
                this.elapsedTime = 0;
            }
        }
    }
}
