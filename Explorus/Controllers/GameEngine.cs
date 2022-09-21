using Explorus.Models;
using Explorus.Threads;
using Explorus.Views;
using System;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace Explorus.Controllers
{
    internal class GameEngine: Models.IObserver<WindowEvents>
    {
        GameView oView;
        LabyrinthController labyrinthController;
        PauseMenuController pauseMenuController;
        HeaderController headerController;
        PhysicsThread physicsThread;
        AudioThread audioThread;

        private const int msPerFrame = 14;
        private int lastGameLoop;


        public GameEngine()
        {
            labyrinthController = new LabyrinthController();
            pauseMenuController = new PauseMenuController();
            headerController = new HeaderController(labyrinthController.lab);
            labyrinthController.lab.playerCharacter.gems.Subscribe(headerController);
            labyrinthController.lab.playerCharacter.bubbles.Subscribe(headerController);
            labyrinthController.lab.playerCharacter.hearts.Subscribe(headerController);
            oView = new GameView(ProcessInput, labyrinthController.lab, headerController);
            oView.Subscribe(this);
            lastGameLoop = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
            physicsThread = new PhysicsThread(labyrinthController.lab, GameState.GetInstance());
            physicsThread.Start();
            audioThread = AudioThread.GetInstance();

            Thread thread = new Thread(new ThreadStart(GameLoop));
            thread.Start();
            oView.Show();
        }

        private void ProcessInput(object sender, KeyEventArgs e)
        {
            switch(GameState.GetInstance().state)
            {
                case GameStates.Play:
                case GameStates.Resume:
                    labyrinthController.ProcessInput(sender, e);
                    break;
                case GameStates.Pause:
                    pauseMenuController.ProcessInput(sender, e);
                    break;
            }
        }

        private void Update(int elapseTime)
        {
            oView.framerate = 1000 / elapseTime;
            oView.state = GameState.GetInstance().state;
            oView.level = GameState.GetInstance().level;
        }

        private void GameLoop()
        {
            System.Timers.Timer endTimer = null;
            while (oView.running)
            {
                int startFrameTime = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
                Update(startFrameTime - lastGameLoop);
                lastGameLoop = startFrameTime;
                oView.Render();
                int endFrameTime = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
                int waitTime = startFrameTime + msPerFrame - endFrameTime;

                if (waitTime > 0)
                {
                    Thread.Sleep(waitTime);
                }
                else
                {
                    Thread.Sleep(1);
                }
                if (labyrinthController.lab.gameEnded && endTimer == null)
                {
                    // need to figure out how to reload the next level map when a level is completed 

                    if (!labyrinthController.NextLevel())
                    {
                        Console.WriteLine("Stop");
                        GameState.GetInstance().Stop();
                        endTimer = new System.Timers.Timer(3000);
                        endTimer.Elapsed += OnGameEnded;
                        endTimer.Start();
                    }
                }
            }
            AudioThread.GetInstance().Stop();
            physicsThread.Stop();
        }

        private void OnGameEnded(Object source, ElapsedEventArgs e)
        {
            oView.Close(null, null);
            physicsThread.Stop();
            AudioThread.GetInstance().Stop();
        }

        public void OnNext(WindowEvents value)
        {
            Console.WriteLine(Enum.GetName(typeof(WindowEvents), value));
            if(value == WindowEvents.Minimize || value == WindowEvents.Unfocus && GameState.GetInstance().state != GameStates.Pause)
            {
                GameState.GetInstance().Pause(false);
            }
            else if(GameState.GetInstance().state == GameStates.Pause && !GameState.GetInstance().manual)
            {
                GameState.GetInstance().Resume();
            }
        }
    }
}
