using Explorus.Models;
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
        HeaderController headerController;

        private const int msPerFrame = 14;
        private int lastGameLoop;


        public GameEngine()
        {
            labyrinthController = new LabyrinthController();
            headerController = new HeaderController(labyrinthController.lab);
            labyrinthController.lab.gems.Subscribe(headerController);
            oView = new GameView(ProcessInput, labyrinthController.lab, headerController);
            oView.Subscribe(this);
            lastGameLoop = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
            Thread thread = new Thread(new ThreadStart(GameLoop));
            thread.Start();
            oView.Show();
        }

        private void ProcessInput(object sender, KeyEventArgs e)
        {
            labyrinthController.ProcessInput(e);
        }

        private void Update(int elapseTime)
        {
            oView.framerate = 1000 / elapseTime;
            oView.state = labyrinthController.gameState.state;
            oView.level = labyrinthController.gameState.level;
            labyrinthController.ProcessMovement(elapseTime);
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

                    //if(labyrinthController.NextLevel())
                    //{
                    //    headerController = new HeaderController(labyrinthController.lab);
                    //    labyrinthController.lab.gems.Subscribe(headerController);
                    //    oView = new GameView(ProcessInput, labyrinthController.lab, headerController);
                    //    oView.Render();
                    //}
                    //else
                    //{
                    labyrinthController.gameState.Stop();
                    endTimer = new System.Timers.Timer(3000);
                    endTimer.Elapsed += OnGameEnded;
                    endTimer.Start();
                    //}
                }
            }
        }

        private void OnGameEnded(Object source, ElapsedEventArgs e)
        {
            oView.Close(null, null);
        }

        public void OnNext(WindowEvents value)
        {
            Console.WriteLine(Enum.GetName(typeof(WindowEvents), value));
            if(value == WindowEvents.Minimize || value == WindowEvents.Unfocus && labyrinthController.gameState.state != GameStates.Pause)
            {
                labyrinthController.gameState.Pause(false);
            }
            else if(labyrinthController.gameState.state == GameStates.Pause && !labyrinthController.gameState.manual)
            {
                labyrinthController.gameState.Resume();
            }
        }
    }
}
