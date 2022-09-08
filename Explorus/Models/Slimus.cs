using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Explorus.Controllers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Explorus.Models
{
    enum FacingDirection
    {        
        Up,
        Right,
        Down,
        Left
    }
    internal class Slimus : LabyrinthComponent
    {
        private Image2D[,] slimusImages;

        public Collection gems { get; private set; }

        public Collection hearts { get; private set; }

        public Collection bubbles { get; private set; }

        public override Image2D image { get
            {
                return slimusImages[animationCycleIndex, (int)currentDirection];
            }
        }

        FacingDirection currentDirection;
        int animationCycleIndex = 0;

        public Slimus(int x, int y) : base(x, y, null)
        {
            slimusImages = new Image2D[3, 4];

            var SFInstance = SpriteFactory.GetInstance();

            slimusImages[0, 0] = SFInstance.GetSprite(Sprites.slimusUpLarge);
            slimusImages[1, 0] = SFInstance.GetSprite(Sprites.slimusUpMedium);
            slimusImages[2, 0] = SFInstance.GetSprite(Sprites.slimusUpSmall);

            slimusImages[0, 1] = SFInstance.GetSprite(Sprites.slimusRightLarge);
            slimusImages[1, 1] = SFInstance.GetSprite(Sprites.slimusRightMedium);
            slimusImages[2, 1] = SFInstance.GetSprite(Sprites.slimusRightSmall);

            slimusImages[0, 2] = SFInstance.GetSprite(Sprites.slimusDownLarge);
            slimusImages[1, 2] = SFInstance.GetSprite(Sprites.slimusDownMedium);
            slimusImages[2, 2] = SFInstance.GetSprite(Sprites.slimusDownSmall);

            slimusImages[0, 3] = SFInstance.GetSprite(Sprites.slimusLeftLarge);
            slimusImages[1, 3] = SFInstance.GetSprite(Sprites.slimusLeftMedium);
            slimusImages[2, 3] = SFInstance.GetSprite(Sprites.slimusLeftSmall);
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
            return newDirection;
        }

        public void ChangeDirection(FacingDirection dir)
        {
            currentDirection = dir;
        }
        public void SetAnimationState(int index)
        {
            animationCycleIndex = index;
        }

        public void SetCollections(Collection gems, Collection hearts, Collection bubbles)
        {
            this.gems = gems;
            this.hearts = hearts;
            this.bubbles = bubbles;
        }
    }
}
