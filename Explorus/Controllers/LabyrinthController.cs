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
                    lab.playerCharacter.MoveToValidDestination(Direction.Up, lab);
                    break;
                case (char)Keys.Left:
                    lab.playerCharacter.MoveToValidDestination(Direction.Left, lab);
                    break;
                case (char)Keys.Right:
                    lab.playerCharacter.MoveToValidDestination(Direction.Right, lab);
                    break;
                case (char)Keys.Down:
                    lab.playerCharacter.MoveToValidDestination(Direction.Down, lab);
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

