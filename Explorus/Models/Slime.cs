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
        protected Direction currentDirection = Direction.None;
        protected Direction LastNotNoneDirection = Direction.Down;
        private Image2D _image;

        public override Image2D image
        {
            get
            {
                return _image;
            }
        }

        public Slime(int x, int y, Image2D image) : base(x, y, null)
        {
            _image = image;
            hitboxXOffset = (Constants.unit * 2 - Constants.slimusHitboxLength) / 2;
            hitboxYOffset = (Constants.unit * 2 - Constants.slimusHitboxHeight) / 2;
            destinationPoint = new Point(x, y);
        }
        public void Move(Direction direction)
        {
            if (currentDirection != Direction.None)
            {
                return;
            }
            currentDirection = direction;
            switch (currentDirection)
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

        public void UpdatePosition(int deltaT)
        {
            double distance = GetCurrentDistanceWithDestinationPoint();
            if (currentDirection == Direction.None)
            {
                return;
            }
            if (distance < Constants.snapDistance)
            {
                SetAnimationState(0);
                currentDirection = Direction.None;
                x = destinationPoint.X;
                y = destinationPoint.Y;
                UpdateHitbox();
                return;
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
        }

        private void UpdateHitbox()
        {
            hitbox = new Rectangle(x + hitboxXOffset, y + hitboxYOffset, Constants.slimusHitboxLength, Constants.slimusHitboxHeight);
        }

        public void ChangeDirection(Direction dir)
        {
            if(currentDirection == Direction.None && dir != Direction.None)
            {
                _image = animationImages[0, (int)dir];
                LastNotNoneDirection = dir;
            }
        }
        public void SetAnimationState(int index)
        {
            _image = animationImages[index, (int)currentDirection];
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
