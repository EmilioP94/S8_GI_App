using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Explorus.Controllers
{
    enum Direction
    {
        None,
        Up,
        Right,
        Down,
        Left
    }
    internal class LabyrinthController
    {
        public ILabyrinth lab { get; private set; }
        public Direction currentDirection;
        public Point playerDestinationPoint;
        public GemController gemController { get; private set; }
        private int animationTimer;
        public Collection gems;
        private int x, y = 0;

        public LabyrinthController()
        {
            lab = new Labyrinth();
            gems = new Collection(lab.map, Sprites.gem, Bars.yellow, false);
            currentDirection = Direction.None;
            gemController = new GemController(lab);
        }

        public void ProcessInput(KeyEventArgs e)
        {
            if (currentDirection != Direction.None)
                return;

            if (e.KeyValue == (char)Keys.Up)
            {
                lab.playerCharacter.ChangeDirection(FacingDirection.Up);
                if (!CheckForCollision(lab.playerCharacter.x, lab.playerCharacter.y - Constants.unit * 2))
                {
                    currentDirection = Direction.Up;
                    playerDestinationPoint = new Point(lab.playerCharacter.x, lab.playerCharacter.y - Constants.unit * 2);
                }
                    
            }
            if (e.KeyValue == (char)Keys.Left)
            {
                lab.playerCharacter.ChangeDirection(FacingDirection.Left);
                if (!CheckForCollision(lab.playerCharacter.x - Constants.unit * 2, lab.playerCharacter.y))
                {
                    currentDirection = Direction.Left;
                    playerDestinationPoint = new Point(lab.playerCharacter.x - Constants.unit * 2, lab.playerCharacter.y);
                }
            }
            if (e.KeyValue == (char)Keys.Right)
            {
                lab.playerCharacter.ChangeDirection(FacingDirection.Right);
                if (!CheckForCollision(lab.playerCharacter.x + Constants.unit * 2, lab.playerCharacter.y))
                {
                    currentDirection = Direction.Right;
                    playerDestinationPoint = new Point(lab.playerCharacter.x + Constants.unit * 2, lab.playerCharacter.y);
                }
            }
            if (e.KeyValue == (char)Keys.Down)
            {
                lab.playerCharacter.ChangeDirection(FacingDirection.Down);
                if (!CheckForCollision(lab.playerCharacter.x, lab.playerCharacter.y + Constants.unit * 2))
                {
                    currentDirection = Direction.Down;
                    playerDestinationPoint = new Point(lab.playerCharacter.x, lab.playerCharacter.y + Constants.unit * 2);
                }
            }
        }
        private bool CheckForCollision(int newX, int newY)
        {
            Rectangle newPosition = new Rectangle(newX, newY, Constants.unit * 2, Constants.unit * 2);
            int index = 0;
            foreach (ILabyrinthComponent comp in lab.labyrinthComponentList)
            {
                if (comp.image.type == ImageType.Collectible)
                {
                    if (comp.hitbox.IntersectsWith(newPosition))
                    {
                        gemController.collectGem();
                        ReplaceComponent(index, newX, newY);
                        return false;
                    }
                }
                if (comp.image.type == ImageType.Door && gemController.openDoor())
                {
                    if (comp.hitbox.IntersectsWith(newPosition))
                    {
                        ReplaceComponent(index, newX, newY);
                        return false;
                    }
                }
                if (comp.isSolid)
                {
                    if (comp.hitbox.IntersectsWith(newPosition))
                        return true;
                }
                if (comp.image.type == ImageType.MiniSlime)
                {
                    if (comp.hitbox.IntersectsWith(newPosition))
                    {
                        // what to do once collected to be implemented
                        ReplaceComponent(index, newX, newY);
                        return false;
                    }
                }
                index++;
            }
            return false;
        }

        private void ReplaceComponent(int index, int newX, int newY)
        {
            lab.labyrinthComponentList[index] = LabyrinthComponentFactory.GetLabyrinthComponent(Sprites.empty, newX, newY);
        }

        private double GetCurrentDistanceWithDestinationPoint()
        {
            double xDiff = playerDestinationPoint.X - lab.playerCharacter.x;
            double yDiff = playerDestinationPoint.Y - lab.playerCharacter.y;

            if(xDiff==0)
            {
                return Math.Abs(yDiff);
            }
            else
            {
                return Math.Abs(xDiff);
            }
        }

        public void ProcessMovement(int deltaT)
        {
            if (currentDirection == Direction.None)
            {
                lab.playerCharacter.SetAnimationState(0);
                return;
            }

            double distance = GetCurrentDistanceWithDestinationPoint();
            currentDirection = lab.playerCharacter.Move(currentDirection, distance, playerDestinationPoint, deltaT);
        }
    }
}
