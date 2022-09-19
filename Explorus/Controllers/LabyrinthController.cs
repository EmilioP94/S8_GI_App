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

        private void MoveToValidDestination(Slime slime, Direction direction)
        {
            slime.ChangeDirection(direction);
            if (CheckValidDestination(direction, slime))
            {
                slime.Move(direction);
            }
        }

        // processes input when the game is in the "Play" state
        private void ProcessPlayControls(char keyValue)
        {
            switch (keyValue)
            {
                case (char)Keys.V:
                    lab.Reload(Constants.level_2);
                    break;
                case (char)Keys.P:
                    if (gameState.state == GameStates.Play)
                    {
                        gameState.Pause(true);
                    }
                    break;
                case (char)Keys.Up:
                    MoveToValidDestination(lab.playerCharacter, Direction.Up);
                    break;
                case (char)Keys.Left:
                    MoveToValidDestination(lab.playerCharacter, Direction.Left);
                    break;
                case (char)Keys.Right:
                    MoveToValidDestination(lab.playerCharacter, Direction.Right);
                    break;
                case (char)Keys.Down:
                    MoveToValidDestination(lab.playerCharacter, Direction.Down);
                    break;
                case (char)Keys.Space:
                    lab.CreateBubble();
                    break;
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
            if (keyValue == (char)Keys.P)
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
                    bool result = false;
                    if (comp.GetType() == typeof(ToxicSlime) && srcComp.GetType() == typeof(Slimus))
                    {
                        result = srcComp.Collide(comp);
                        if(lab.playerCharacter.hearts.acquired == 0)
                        {
                            gameState.GameOver();
                        }
                    }
                    else result = comp.Collide(srcComp);
                    if (result) //true seulement si c'est une collision entre une bulle et un toxicSlime
                    {
                        lab.CreateGems(comp.x, comp.y);
                    }
                    return result;
                }
            }
            return false;
        }

        private bool CheckValidDestination(Direction direction, Slime slime)
        {
            int newX = slime.x;
            int newY = slime.y;
            switch (direction)
            {
                case Direction.Up:
                    newX = slime.x;
                    newY = slime.y - Constants.unit * 2;
                    break;
                case Direction.Down:
                    newX = slime.x;
                    newY = slime.y + Constants.unit * 2;
                    break;
                case Direction.Right:
                    newX = slime.x + Constants.unit * 2;
                    newY = slime.y;
                    break;
                case Direction.Left:
                    newX = slime.x - Constants.unit * 2;
                    newY = slime.y;
                    break;
            }
            Rectangle newPosition = new Rectangle(newX, newY, Constants.unit * 2, Constants.unit * 2);
            foreach (ILabyrinthComponent comp in lab.labyrinthComponentList)
            {
                if (comp.hitbox.IntersectsWith(newPosition))
                {
                    return comp.IsValidDestination(slime);
                }
            }
            return true;
        }

        public void ProcessMovement(int elapseTime)
        {
            if (gameState.state != GameStates.Play)
            {
                return;
            }
            CheckForCollision(lab.playerCharacter);
            lab.playerCharacter.UpdatePosition(elapseTime);
            MoveToxicSlimes(elapseTime);
            MoveBubbles(elapseTime);
            lab.playerCharacter.RechargeBubbles(elapseTime);
        }

        private void MoveBubbles(int elapseTime)
        {
            foreach (Bubble bubble in lab.labyrinthComponentList.OfType<Bubble>().ToList())
            {
                bubble.DeleteCheck();
                bubble.Move(elapseTime);
                CheckForCollision(bubble);
            }
        }

        public void MoveToxicSlimes(int elapseTime)
        {
            Random random = new Random();
            foreach (ToxicSlime slime in lab.toxicSlimes)
            {
                Direction direction = (Direction)random.Next(0, 4);
                MoveToValidDestination(slime, direction);
                slime.UpdatePosition(elapseTime);
            }
        }

        public bool NextLevel()
        {
            if (gameState.level < gameState.maxLevel - 1)
            {
                gameState.NextLevel();
                lab.Reload(Constants.levels[gameState.level].map);
                return true;
            }
            else return false;
        }
    }
}

