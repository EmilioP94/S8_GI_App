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
    internal class LabyrinthController: IInputController
    {
        public ILabyrinth lab { get; private set; }
        public Point playerDestinationPoint;

        public LabyrinthController()
        {
            lab = new Labyrinth(Constants.levels[GameState.GetInstance().level].map);
        }

        public void ProcessInput(object sender, KeyEventArgs e, GameMenu menu = null)
        {
            switch (GameState.GetInstance().state) 
            {
                case GameStates.Play:
                    ProcessPlayControls((char)e.KeyValue);
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
                    if (GameState.GetInstance().state == GameStates.Play)
                    {
                        GameState.GetInstance().Pause(true);
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

        private void ProcessResumeControls(char keyValue)
        {
            if (keyValue == (char)Keys.P)
            {
                GameState.GetInstance().Pause(true);
            }
        }

        public bool NextLevel()
        {
            if (GameState.GetInstance().level < GameState.GetInstance().maxLevel - 1)
            {
                GameState.GetInstance().NextLevel();
                lab.Reload(Constants.levels[GameState.GetInstance().level].map);
                return true;
            }
            else return false;
        }
    }
}

