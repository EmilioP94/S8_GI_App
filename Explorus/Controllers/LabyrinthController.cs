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
    enum DirectionInput
    {
        None = 0,
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8
    }
    internal class LabyrinthController: IInputController
    {
        public ILabyrinth lab { get; private set; }
        public Point playerDestinationPoint;

        DirectionInput slimusDirectionInput;

        public LabyrinthController()
        {
            lab = new Labyrinth(Constants.levels[GameState.GetInstance().level].map);
        }

        public void ProcessInput(object sender, KeyEventArgs e, bool isKeyDown = true, GameMenu menu = null)
        {
            switch (GameState.GetInstance().state) 
            {
                case GameStates.Play:
                    if(isKeyDown)
                        ProcessPlayControlsKeyDown((char)e.KeyValue);
                    else
                        ProcessPlayControlsKeyUp((char)e.KeyValue);
                    break;
                case GameStates.Resume:
                    ProcessResumeControls((char)e.KeyValue);
                    break;
                default:
                    break;
            }
        }

        public void InputLoop()
        {
            if (lab.playerCharacter.GetDirection() == Direction.None)
            {
                if ((slimusDirectionInput & DirectionInput.Up) == DirectionInput.Up)
                    lab.playerCharacter.MoveToValidDestination(Direction.Up, lab);

                else if ((slimusDirectionInput & DirectionInput.Right) == DirectionInput.Right)
                    lab.playerCharacter.MoveToValidDestination(Direction.Right, lab);

                else if ((slimusDirectionInput & DirectionInput.Down) == DirectionInput.Down)
                    lab.playerCharacter.MoveToValidDestination(Direction.Down, lab);

                else if ((slimusDirectionInput & DirectionInput.Left) == DirectionInput.Left)
                    lab.playerCharacter.MoveToValidDestination(Direction.Left, lab);
            }            
        }

        // processes input when the game is in the "Play" state
        private void ProcessPlayControlsKeyDown(char keyValue)
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
                    slimusDirectionInput |= DirectionInput.Up;
                    break;
                case (char)Keys.Left:
                    slimusDirectionInput |= DirectionInput.Left;
                    break;
                case (char)Keys.Right:
                    slimusDirectionInput |= DirectionInput.Right;
                    break;
                case (char)Keys.Down:
                    slimusDirectionInput |= DirectionInput.Down;
                    break;
                case (char)Keys.Space:
                    lab.CreateBubble();
                    break;
            }
        }

        private void ProcessPlayControlsKeyUp(char keyValue)
        {
            switch (keyValue)
            {               
                case (char)Keys.Up:
                    slimusDirectionInput &= ~DirectionInput.Up;                    
                    break;
                case (char)Keys.Left:
                    slimusDirectionInput &= ~DirectionInput.Left;                    
                    break;
                case (char)Keys.Right:
                    slimusDirectionInput &= ~DirectionInput.Right;                    
                    break;
                case (char)Keys.Down:
                    slimusDirectionInput &= ~DirectionInput.Down;                    
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

