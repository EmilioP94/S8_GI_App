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
    internal class LabyrinthController : IInputController
    {
        public ILabyrinth lab { get; private set; }
        public Point playerDestinationPoint;
        public Point player2DestinationPoint;

        DirectionInput slimusDirectionInput;
        DirectionInput player2DirectionInput;

        public LabyrinthController()
        {
            lab = new Labyrinth(Constants.levels[GameState.GetInstance().level].map);
        }

        public void ProcessInput(object sender, KeyEventArgs e, bool isKeyDown = true, GameMenu menu = null)
        {
            switch (GameState.GetInstance().state)
            {
                case GameStates.Play:
                    if (isKeyDown)
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
            handlePlayerInput(lab.players.ElementAt(0), slimusDirectionInput);
            if (GameState.GetInstance().multiplayer)
            {
                handlePlayerInput(lab.players.ElementAt(1), player2DirectionInput);
            }
        }

        private void handlePlayerInput(Slimus player, DirectionInput playerDirectionInput)
        {
            if (player.GetDirection() == Direction.None)
            {
                if ((playerDirectionInput & DirectionInput.Up) == DirectionInput.Up)
                    player.MoveToValidDestination(Direction.Up, lab);

                else if ((playerDirectionInput & DirectionInput.Right) == DirectionInput.Right)
                    player.MoveToValidDestination(Direction.Right, lab);

                else if ((playerDirectionInput & DirectionInput.Down) == DirectionInput.Down)
                    player.MoveToValidDestination(Direction.Down, lab);

                else if ((playerDirectionInput & DirectionInput.Left) == DirectionInput.Left)
                    player.MoveToValidDestination(Direction.Left, lab);
            }
        }

        // processes input when the game is in the "Play" state
        private void ProcessPlayControlsKeyDown(char keyValue)
        {
            switch (keyValue)
            {
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
                    lab.CreateBubble(lab.players.ElementAt(0));
                    break;
                case (char)Keys.W:
                    player2DirectionInput |= DirectionInput.Up;
                    break;
                case (char)Keys.A:
                    player2DirectionInput |= DirectionInput.Left;
                    break;
                case (char)Keys.S:
                    player2DirectionInput |= DirectionInput.Down;
                    break;
                case (char)Keys.D:
                    player2DirectionInput |= DirectionInput.Right;
                    break;
                case (char)Keys.Q:
                    lab.CreateBubble(lab.players.ElementAt(1));
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
                case (char)Keys.W:
                    player2DirectionInput &= ~DirectionInput.Up;
                    break;
                case (char)Keys.A:
                    player2DirectionInput &= ~DirectionInput.Left;
                    break;
                case (char)Keys.S:
                    player2DirectionInput &= ~DirectionInput.Down;
                    break;
                case (char)Keys.D:
                    player2DirectionInput &= ~DirectionInput.Right;
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

