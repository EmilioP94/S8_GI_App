using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Explorus.Models
{
    internal abstract class Slime: LabyrinthComponent
    {
        private int hitboxXOffset;
        private int hitboxYOffset;
        protected Image2D[,] animationImages;
        protected Point destinationPoint;
        Direction currentDirection = Direction.None;
        private Image2D _image;

        public override Image2D image
        {
            get
            {
                if(currentDirection != Direction.None)
                {
                    _image = animationImages[animationCycleIndex, (int)currentDirection];
                }
                return _image;
            }
        }
        int animationCycleIndex = 0;

        public Slime(int x, int y, Image2D image) : base(x, y, null)
        {
            _image = image;
            hitboxXOffset = (Constants.unit * 2 - Constants.slimusHitboxLength) / 2;
            hitboxYOffset = (Constants.unit * 2 - Constants.slimusHitboxHeight) / 2;
            destinationPoint = new Point(x, y);
        }
        public void Move(Direction direction)
        {
            currentDirection = direction;
            switch (direction)
            {
                case Direction.Left:
                    destinationPoint = new Point(x - Constants.unit * 2, y);
                    break;
                case Direction.Up:
                    destinationPoint = new Point(x, y - Constants.unit * 2);
                    break;
                case Direction.Down:
                    destinationPoint = new Point(x, y + Constants.unit * 2);
                    break;
                case Direction.Right:
                    destinationPoint = new Point(x + Constants.unit * 2, y);
                    break;
                default:
                    destinationPoint = new Point(x, y);
                    break;
            }
        }

        public Direction UpdatePosition(int deltaT)
        {
            double distance = GetCurrentDistanceWithDestinationPoint();
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

            hitbox = new Rectangle(x + hitboxXOffset, y + hitboxYOffset, Constants.slimusHitboxLength, Constants.slimusHitboxHeight);
            return newDirection;
        }

        public void ChangeDirection(Direction dir)
        {
            if(currentDirection == Direction.None)
            {
                currentDirection = dir;
            }
        }
        public void SetAnimationState(int index)
        {
            animationCycleIndex = index;
        }

        private double GetCurrentDistanceWithDestinationPoint()
        {
            double xDiff = destinationPoint.X - x;
            double yDiff = destinationPoint.Y - y;

            if (xDiff == 0)
            {
                return Math.Abs(yDiff);
            }
            else
            {
                return Math.Abs(xDiff);
            }
        }
    }
}
