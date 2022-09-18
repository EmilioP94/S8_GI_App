using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Explorus.Controllers
{
    enum Direction
    {
        Up,
        Right,
        Down,
        Left,
        None
    }
    internal class LabyrinthController
    {
        public ILabyrinth lab { get; private set; }
        public Point playerDestinationPoint;
        public GameState gameState;

        public LabyrinthController()
        {
            gameState = new GameState();
            lab = new Labyrinth(Constants.levels[gameState.level].map);
        }

        public void ProcessInput(KeyEventArgs e)
        {
            switch (gameState.state)
            {
                case GameStates.Play:
                    ProcessPlayControls((char)e.KeyValue);
                    break;
                case GameStates.Pause:
                    ProcessPauseControls((char)e.KeyValue);
                    break; 
                case GameStates.Resume:
                    ProcessResumeControls((char)e.KeyValue);
                    break;
                default:
                    break;
            }
        }

        // processes input when the game is in the "Play" state
        private void ProcessPlayControls(char keyValue)
        {
            if (keyValue == (char)Keys.P)
            {
                if (gameState.state == GameStates.Play)
                {
                    gameState.Pause(true);
                }
            }

            if (keyValue == (char)Keys.Up)
            {
                lab.playerCharacter.ChangeDirection(Direction.Up);
                if (CheckValidDestination(lab.playerCharacter.x, lab.playerCharacter.y - Constants.unit * 2))
                {
                    lab.playerCharacter.Move(Direction.Up);
                }
            }
            if (keyValue == (char)Keys.Left)
            {
                lab.playerCharacter.ChangeDirection(Direction.Left);
                if (CheckValidDestination(lab.playerCharacter.x - Constants.unit * 2, lab.playerCharacter.y))
                {
                    lab.playerCharacter.Move(Direction.Left);
                }
            }
            if (keyValue == (char)Keys.Right)
            {
                lab.playerCharacter.ChangeDirection(Direction.Right);
                if (CheckValidDestination(lab.playerCharacter.x + Constants.unit * 2, lab.playerCharacter.y))
                {
                    lab.playerCharacter.Move(Direction.Right);
                }
            }
            if (keyValue == (char)Keys.Down)
            {
                lab.playerCharacter.ChangeDirection(Direction.Down);
                if (CheckValidDestination(lab.playerCharacter.x, lab.playerCharacter.y + Constants.unit * 2))
                {
                    lab.playerCharacter.Move(Direction.Down);
                }
            }
        }

        // processes input when the game is in the "Pause" state
        private void ProcessPauseControls(char keyValue)
        {
            if (keyValue == (char)Keys.R)
            {
                gameState.Resume();
            }
            // add controls for sound menu here 
        }

        private void ProcessResumeControls(char keyValue)
        {
            if(keyValue == (char)Keys.P)
            {
                gameState.Pause(true);
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

        public void ProcessMovement(int elapseTime)
        {
            CheckForCollision(lab.playerCharacter);
            lab.playerCharacter.UpdatePosition(elapseTime);
        }

        public bool NextLevel()
        {
            if (gameState.level < gameState.maxLevel - 1)
            {
                gameState.NextLevel();
                lab = new Labyrinth(Constants.levels[gameState.level].map);
                return true;
            }
            else return false;
        }
    }
}
