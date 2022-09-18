﻿using Explorus.Models;
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

        public LabyrinthController()
        {
            lab = new Labyrinth(Constants.level_1);
            currentDirection = Direction.None;
        }

        public void ProcessInput(KeyEventArgs e)
        {
            if (currentDirection == Direction.None)
            {
                if (e.KeyValue == (char)Keys.Up)
                {
                    lab.playerCharacter.ChangeDirection(FacingDirection.Up);
                    if (CheckValidDestination(lab.playerCharacter.x, lab.playerCharacter.y - Constants.unit * 2))
                    {
                        currentDirection = Direction.Up;
                        playerDestinationPoint = new Point(lab.playerCharacter.x, lab.playerCharacter.y - Constants.unit * 2);
                    }
                }
                if (e.KeyValue == (char)Keys.Left)
                {
                    lab.playerCharacter.ChangeDirection(FacingDirection.Left);
                    if (CheckValidDestination(lab.playerCharacter.x - Constants.unit * 2, lab.playerCharacter.y))
                    {
                        currentDirection = Direction.Left;
                        playerDestinationPoint = new Point(lab.playerCharacter.x - Constants.unit * 2, lab.playerCharacter.y);
                    }
                }
                if (e.KeyValue == (char)Keys.Right)
                {
                    lab.playerCharacter.ChangeDirection(FacingDirection.Right);
                    if (CheckValidDestination(lab.playerCharacter.x + Constants.unit * 2, lab.playerCharacter.y))
                    {
                        currentDirection = Direction.Right;
                        playerDestinationPoint = new Point(lab.playerCharacter.x + Constants.unit * 2, lab.playerCharacter.y);
                    }
                }
                if (e.KeyValue == (char)Keys.Down)
                {
                    lab.playerCharacter.ChangeDirection(FacingDirection.Down);
                    if (CheckValidDestination(lab.playerCharacter.x, lab.playerCharacter.y + Constants.unit * 2))
                    {
                        currentDirection = Direction.Down;
                        playerDestinationPoint = new Point(lab.playerCharacter.x, lab.playerCharacter.y + Constants.unit * 2);
                    }
                }
            }


            if (e.KeyValue == (char)Keys.Space)
            {
                lab.CreateBubble();
            }
        }
        private bool CheckForCollision(ILabyrinthComponent srcComp)
        {
            foreach (ILabyrinthComponent comp in lab.labyrinthComponentList)
            {
                if (srcComp == comp)//ignore  collision with itself
                    continue;

                if (comp.hitbox.IntersectsWith(srcComp.hitbox))
                {
                    return comp.Collide(srcComp);
                }
            }
            return false;
        }

        private bool CheckValidDestination(int newX, int newY)
        {
            Rectangle newPosition = new Rectangle(newX, newY, Constants.unit * 2, Constants.unit * 2);
            foreach (ILabyrinthComponent comp in lab.labyrinthComponentList)
            {
                if (comp.hitbox.IntersectsWith(newPosition))
                {
                    return comp.IsValidDestination(lab.playerCharacter);
                }
            }
            return true;
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
            //BubblesMovement
            foreach (Bubble bubble in lab.labyrinthComponentList.OfType<Bubble>().ToList())
            {
                if(bubble.DeleteCheck())
                {
                    lab.labyrinthComponentList.Remove(bubble);
                }
                bubble.Move(deltaT);
                CheckForCollision(bubble);
            }

            //SlimusMovement
            CheckForCollision(lab.playerCharacter);
            if (currentDirection == Direction.None)
            {
                lab.playerCharacter.SetAnimationState(0);
            }
            else
            {
                double distance = GetCurrentDistanceWithDestinationPoint();
                currentDirection = lab.playerCharacter.Move(currentDirection, distance, playerDestinationPoint, deltaT);
            }
        }
    }
}
