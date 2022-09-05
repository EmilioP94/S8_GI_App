using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public ILabyrinth lab { get; private set; } = new Labyrinth();
        public LabyrinthComponent[,] components;
        public Direction currentDirection = Direction.None;
        public Point PlayerDestinationPoint;


        private int x, y = 0;

        public void ProcessInput(KeyEventArgs e)
        {

            // find slimus position in x, y
            // if target space is empty or contains collectible, move slimus, replace with empty
            // else don't do shit

            // slimusPosition[0] =­> x
            // slimusPosition[1] => y
            if (currentDirection != Direction.None)
                return;

            if (e.KeyValue == (char)Keys.Up)
            {
                if(!CheckForCollision(lab.playerCharacter.x, lab.playerCharacter.y - Constants.unit * 2))
                {
                    currentDirection = Direction.Up;
                    PlayerDestinationPoint = new Point(lab.playerCharacter.x, lab.playerCharacter.y - Constants.unit * 2);
                }
                    
            }
            if (e.KeyValue == (char)Keys.Left)
            {
                if (!CheckForCollision(lab.playerCharacter.x - Constants.unit * 2, lab.playerCharacter.y))
                {
                    currentDirection = Direction.Left;
                    PlayerDestinationPoint = new Point(lab.playerCharacter.x - Constants.unit * 2, lab.playerCharacter.y);
                }
            }
            if (e.KeyValue == (char)Keys.Right)
            {
                if (!CheckForCollision(lab.playerCharacter.x + Constants.unit * 2, lab.playerCharacter.y))
                {
                    currentDirection = Direction.Right;
                    PlayerDestinationPoint = new Point(lab.playerCharacter.x + Constants.unit * 2, lab.playerCharacter.y);
                }
            }
            if (e.KeyValue == (char)Keys.Down)
            {
                if (!CheckForCollision(lab.playerCharacter.x, lab.playerCharacter.y + Constants.unit * 2))
                {
                    currentDirection = Direction.Down;
                    PlayerDestinationPoint = new Point(lab.playerCharacter.x, lab.playerCharacter.y + Constants.unit * 2);
                }
            }
            //Console.WriteLine(lab.playerCharacter.x);
            //Console.WriteLine(lab.playerCharacter.y);
        }
        private bool CheckForCollision(int newX, int newY)
        {
            Rectangle newPosition = new Rectangle(newX, newY, Constants.unit * 2, Constants.unit * 2);
            foreach (LabyrinthComponent comp in lab.labyrinthComponentList)
            {
                if(comp.isSolid)
                {
                    if (comp.hitbox.IntersectsWith(newPosition))
                        return true;
                }
            }
            return false;
        }
        private double GetCurrentDistanceWithDestinationPoint()
        {
            double xDiff = PlayerDestinationPoint.X - lab.playerCharacter.x;
            double yDiff = PlayerDestinationPoint.Y - lab.playerCharacter.y;

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
                return;
            }

            if (GetCurrentDistanceWithDestinationPoint() < Constants.snapDistance)
            {
                currentDirection = Direction.None;
                lab.playerCharacter.x = PlayerDestinationPoint.X;
                lab.playerCharacter.y = PlayerDestinationPoint.Y;
            }
            else if(currentDirection == Direction.Up)
            {
                lab.playerCharacter.y -= (int)(deltaT * Constants.playerSpeed);
            }
            else if (currentDirection == Direction.Right)
            {
                lab.playerCharacter.x += (int)(deltaT * Constants.playerSpeed);
            }
            else if (currentDirection == Direction.Down)
            {
                lab.playerCharacter.y += (int)(deltaT * Constants.playerSpeed);
            }
            else if (currentDirection == Direction.Left)
            {
                lab.playerCharacter.x -= (int)(deltaT * Constants.playerSpeed);
            }




        }
    }
}
