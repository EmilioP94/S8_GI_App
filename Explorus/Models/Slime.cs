using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Models
{
    internal abstract class Slime: LabyrinthComponent
    {
        private int hitboxXOffset;
        private int hitboxYOffset;
        protected Image2D[,] animationImages;

        public Collection gems { get; private set; }

        public override Image2D image
        {
            get
            {
                return animationImages[animationCycleIndex, (int)currentDirection];
            }
        }

        protected FacingDirection currentDirection;
        protected int animationCycleIndex = 0;

        public Slime(int x, int y) : base(x, y, null)
        {
            hitboxXOffset = (Constants.unit * 2 - Constants.slimusHitboxLength) / 2;
            hitboxYOffset = (Constants.unit * 2 - Constants.slimusHitboxHeight) / 2;

            UpdateHitbox();
        }

        public Direction Move(Direction currentDirection, double distance, Point destinationPoint, int deltaT)
        {
            Direction newDirection = currentDirection;
            if (currentDirection == Direction.None)
            {
                SetAnimationState(0);
                return currentDirection;
            }

            if (distance < Constants.snapDistance)
            {
                newDirection = Direction.None;
                x = destinationPoint.X;
                y = destinationPoint.Y;
            }
            else if (currentDirection == Direction.Up)
            {
                y -= (int)(deltaT * Constants.playerSpeed);
            }
            else if (currentDirection == Direction.Right)
            {
                x += (int)(deltaT * Constants.playerSpeed);
            }
            else if (currentDirection == Direction.Down)
            {
                y += (int)(deltaT * Constants.playerSpeed);
            }
            else if (currentDirection == Direction.Left)
            {
                x -= (int)(deltaT * Constants.playerSpeed);
            }

            if (distance > Constants.animationChangeThreshold)
            {
                SetAnimationState(1);
            }
            else
            {
                SetAnimationState(2);
            }

            UpdateHitbox();
            return newDirection;
        }

        private void UpdateHitbox()
        {
            hitbox = new Rectangle(x + hitboxXOffset, y + hitboxYOffset, Constants.slimusHitboxLength, Constants.slimusHitboxHeight);
        }

        public void ChangeDirection(FacingDirection dir)
        {
            currentDirection = dir;
        }
        public void SetAnimationState(int index)
        {
            animationCycleIndex = index;
        }

        public void SetCollections(Collection gems)
        {
            this.gems = gems;
        }
    }
}
