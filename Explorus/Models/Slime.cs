using Explorus.Controllers;
using Explorus.Models.GameEvents;
using Explorus.Threads;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Explorus.Models
{
    internal abstract class Slime : LabyrinthComponent
    {
        private int hitboxXOffset;
        private int hitboxYOffset;
        protected Image2D[,] animationImages;
        protected Point destinationPoint;
        protected Direction currentDirection = Direction.None;
        protected Direction LastNotNoneDirection = Direction.Down;
        public bool isDead { get; protected set; }  = false;
        private Image2D _image;
        protected SoundTypes movementSound = SoundTypes.None;
        protected SoundTypes wallCollisionSound = SoundTypes.None;
        private Random random;

        public override Image2D image
        {
            get
            {
                if (isDead)
                    return null;
                else
                    return _image;
            }
        }

        public Slime(int x, int y, Image2D image) : base(x, y, null)
        {
            _image = image;
            hitboxXOffset = (Constants.unit * 2 - Constants.slimusHitboxLength) / 2;
            hitboxYOffset = (Constants.unit * 2 - Constants.slimusHitboxHeight) / 2;
            destinationPoint = new Point(x, y);
            UpdateHitbox();
            random = new Random();
        }

        public void Teleport(Point newLocation)
        {
            this.x = newLocation.X;
            this.y = newLocation.Y;
            this.destinationPoint = newLocation;
            currentDirection = Direction.None;
            UpdateHitbox();
        }

        public void SetDestination(Point newDestination, Direction dir)
        {
            destinationPoint = newDestination;
            currentDirection = dir;
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
            GameRecorder.GetInstance().AddEvent(new MoveEvent(id, destinationPoint, currentDirection));
            if (movementSound != SoundTypes.None)
            {
                AudioThread.GetInstance().QueueSound(movementSound);
            }
        }

        private bool ShouldSnapX()
        {
            if (currentDirection == Direction.Left)
            {
                return ((x - Constants.snapDistance) <= destinationPoint.X);
            }
            else if (currentDirection == Direction.Right)
            {
                return ((x + Constants.snapDistance) >= destinationPoint.X);
            }
            return true;
        }

        private bool ShouldSnapY()
        {
            if (currentDirection == Direction.Up)
            {
                return ((y - Constants.snapDistance) <= destinationPoint.Y);
            }
            else if (currentDirection == Direction.Down)
            {
                return ((y + Constants.snapDistance) >= destinationPoint.Y);
            }

            return true;
        }

        public void UpdatePosition(int deltaT)
        {
            if (isDead)
                return;
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
            //Process vertical movements
            if (ShouldSnapY())
            {
                y = destinationPoint.Y;
            }
            else if (currentDirection == Direction.Up)
            {
                y -= (int)(deltaT * Constants.playerSpeed);
            }
            else if (currentDirection == Direction.Down)
            {
                y += (int)(deltaT * Constants.playerSpeed);
            }

            //Process horizontal movements
            if (ShouldSnapX())
            {
                x = destinationPoint.X;
            }
            else if (currentDirection == Direction.Right)
            {
                x += (int)(deltaT * Constants.playerSpeed);
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
            GameRecorder.GetInstance().AddEvent(new DirectionEvent(id, dir));
            if (currentDirection == Direction.None && dir != Direction.None)
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

        private bool CheckValidDestination(Direction direction, ILabyrinth lab)
        {
            int newX = x;
            int newY = y;
            switch (direction)
            {
                case Direction.Up:
                    newX = x;
                    newY = y - Constants.unit * 2;
                    break;
                case Direction.Down:
                    newX = x;
                    newY = y + Constants.unit * 2;
                    break;
                case Direction.Right:
                    newX = x + Constants.unit * 2;
                    newY = y;
                    break;
                case Direction.Left:
                    newX = x - Constants.unit * 2;
                    newY = y;
                    break;
            }
            Rectangle newPosition = new Rectangle(newX, newY, Constants.unit * 2, Constants.unit * 2);
            foreach (ILabyrinthComponent comp in lab.GetComponentListCopy())
            {
                if (comp.hitbox.IntersectsWith(newPosition))
                {
                    return comp.IsValidDestination(this);
                }
            }
            return true;
        }

        public void MoveToValidDestination(Direction direction, ILabyrinth lab)
        {
            ChangeDirection(direction);
            if (CheckValidDestination(direction, lab))
            {
                Move(direction);
            }
            else if (wallCollisionSound != SoundTypes.None)
            {
                //TODO: Le son est pas jouer, on dirais que AudioThread est pas accessible a partir de ce thread, et la memoire explose quand on essaye de queueSound
                //AudioThread.GetInstance().QueueSound(wallCollisionSound);
            }
        }

        public override void Reset()
        {
            base.Reset();
            destinationPoint = new Point(x, y);
            currentDirection = Direction.None;
            LastNotNoneDirection = Direction.None;
            isDead = false;
            UpdateHitbox();
        }

        public void RandomMovement(ILabyrinth lab)
        {
            List<Direction> validDirections = new List<Direction>();
            if (CheckValidDestination(Direction.Up, lab))
                validDirections.Add(Direction.Up);
            if (CheckValidDestination(Direction.Right, lab))
                validDirections.Add(Direction.Right);
            if (CheckValidDestination(Direction.Down, lab))
                validDirections.Add(Direction.Down);
            if (CheckValidDestination(Direction.Left, lab))
                validDirections.Add(Direction.Left);

            if (validDirections.Count >= 3)
            {
                int number = random.Next(0, validDirections.Count);
                MoveToValidDestination(validDirections[number], lab);
            }
            else if (validDirections.Count == 2)
            {
                validDirections.Remove(GetOppositeDirection(LastNotNoneDirection));
                MoveToValidDestination(validDirections[0], lab);
            }
            else
            {
                MoveToValidDestination(validDirections[0], lab);
            }
        }

        private Direction GetOppositeDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.Left:
                    return Direction.Right;                    
                case Direction.Up:
                    return Direction.Down;                   
                case Direction.Down:
                    return Direction.Up;
                case Direction.Right:
                    return Direction.Left;
                default:
                    return Direction.None;
            }
        }

    }
}
