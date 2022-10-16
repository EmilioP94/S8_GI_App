using Explorus.Models;
using Explorus.Threads;
using Explorus.Views;
using System;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using MainMenu = Explorus.Models.MainMenu;

namespace Explorus.Controllers
{
    internal class GameEngine: Models.IObserver<WindowEvents>
    {
        GameView oView;
        LabyrinthController labyrinthController;
        MenuController menuController;
        HeaderController headerController;
        PhysicsThread physicsThread;
        AudioThread audioThread;
        RenderThread renderThread;
        GameMenu mainMenu;
        GameMenu audioMenu;

        private int lastGameLoop;


        public GameEngine()
        {
            labyrinthController = new LabyrinthController();
            menuController = new MenuController();
            mainMenu = MainMenu.GetInstance();
            audioMenu = AudioMenu.GetInstance();
            headerController = new HeaderController(labyrinthController.lab);
            labyrinthController.lab.playerCharacter.gems.Subscribe(headerController);
            labyrinthController.lab.playerCharacter.bubbles.Subscribe(headerController);
            labyrinthController.lab.playerCharacter.hearts.Subscribe(headerController);
            labyrinthController.lab.player2.gems.Subscribe(headerController);
            labyrinthController.lab.player2.bubbles.Subscribe(headerController);
            labyrinthController.lab.player2.hearts.Subscribe(headerController);
            oView = new GameView(ProcessInputKeyDown, ProcessInputKeyUp, labyrinthController.lab, headerController);
            oView.Subscribe(this);
            lastGameLoop = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
            physicsThread = new PhysicsThread(labyrinthController.lab, GameState.GetInstance());
            physicsThread.Start();
            audioThread = AudioThread.GetInstance();
            renderThread = new RenderThread(oView, labyrinthController);
            renderThread.Start();

            Thread thread = new Thread(new ThreadStart(GameLoop));
            thread.Start();
            oView.Show();
        }

        private void ProcessInputKeyDown(object sender, KeyEventArgs e)
        {
            switch(GameState.GetInstance().state)
            {
                case GameStates.Play:
                case GameStates.Resume:
                    labyrinthController.ProcessInput(sender, e);
                    break;
                case GameStates.Pause:
                case GameStates.New:
                    switch(GameState.GetInstance().menu)
                    {
                        // enables extending with more menus in the future
                        case MenuTypes.Main:
                            menuController.ProcessInput(sender, e, true, mainMenu);
                            break;
                        case MenuTypes.Audio:
                            menuController.ProcessInput(sender, e, true, audioMenu);
                            break;
                    }
                    break;
            }
        }
        private void ProcessInputKeyUp(object sender, KeyEventArgs e)
        {
            switch (GameState.GetInstance().state)
            {
                case GameStates.Play:
                case GameStates.Resume:
                    labyrinthController.ProcessInput(sender, e, false, null);
                    break;
                case GameStates.Pause:                    
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
                labyrinthController.InputLoop();
                if(GameState.GetInstance().multiplayerSwitched)
                {
                    labyrinthController.lab.Reload(Constants.levels[GameState.GetInstance().level].map);
                }
                if (labyrinthController.lab.gameEnded && endTimer == null)
                {
                    // need to figure out how to reload the next level map when a level is completed 

                    if (!labyrinthController.NextLevel())
                    {
                        GameState.GetInstance().Stop();
                        endTimer = new System.Timers.Timer(3000);
                        endTimer.Elapsed += OnGameEnded;
                        endTimer.Start();
                    }
                }

            }
            AudioThread.GetInstance().Stop();
            physicsThread.Stop();
            renderThread.Stop();
        }

        private void OnGameEnded(Object source, ElapsedEventArgs e)
        {
            oView.Close(null, null);
            physicsThread.Stop();
            AudioThread.GetInstance().Stop();
        }

        public void OnNext(WindowEvents value)
        {
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
