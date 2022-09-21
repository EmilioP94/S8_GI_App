using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers;
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
        private bool _invincible = false;
        private bool _isTransparent = true;
        private Timer flickerTimer = new Timer(100);
        public bool invincible
        {
            get
            {
                return _invincible;
            }
            private set
            {
                _invincible = value;
            }
        }
        public Slimus(int x, int y) : base(x, y, SpriteFactory.GetInstance().GetSprite(Sprites.slimusDownLarge))
        {
            flickerTimer.Elapsed += (sender, e) => ToggleTransparency();
            flickerTimer.AutoReset = true;
            flickerTimer.Enabled = false;
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

        private void ToggleTransparency()
        {
            _isTransparent = !_isTransparent;
            SetTransparency(_isTransparent);
        }

        private void SetTransparency(bool isTransparent)
        {
            ColorMatrix matrix = new ColorMatrix();
            if (isTransparent)
            {
                matrix.Matrix33 = 0.5f;
            }
            else
            {
                matrix.Matrix33 = 1;
            }
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            this.attributes = attributes;
        }

        public override bool Collide(ILabyrinthComponent comp)
        {
            if(!invincible && comp is ToxicSlime)
            {
                flickerTimer.Enabled = true;
                _isTransparent = true;
                SetTransparency(true);
                invincible = true;
                hearts.Decrement();
                Task.Delay(new TimeSpan(0, 0, 3)).ContinueWith(o =>
                {
                    invincible = false;
                    flickerTimer.Enabled = false;
                    SetTransparency(false);
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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
