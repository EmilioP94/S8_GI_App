using Explorus.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Explorus.Models
{
    enum GameStates
    {
        New,
        Play,
        Replay,
        ReplayPlaying,
        Pause,
        Resume,
        Stop,
        Over
    }

    enum MenuTypes
    {
        Main,
        Audio
    }

    internal class GameState
    {
        public GameStates state { get; private set; } = GameStates.New;
        public MenuTypes menu { get; private set; } = MenuTypes.Main;
        public int level { get; private set; } = 0;
        public int maxLevel = Constants.levels.Length;
        public bool manual;
        public bool multiplayerSwitched { get; private set; } = false;
        public bool multiplayer;

        private static GameState _instance = null;

        public static GameState GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameState();
            }
            return _instance;
        }

        private GameState()
        {
            multiplayer = Constants.defaultMultiplayer;
        }

        public void Pause(bool manual)
        {
            MainMenu();
            state = GameStates.Pause;
            this.manual = manual;
            AudioThread.GetInstance().Pause();
        }

        public void Play()
        {
                state = GameStates.Play;
                AudioThread.GetInstance().Resume();
        }

        public void Resume()
        {
                state = GameStates.Resume;
                Task.Delay(new TimeSpan(0, 0, 3)).ContinueWith(o => { if (state == GameStates.Resume) Play(); });
        }

        public void Stop()
        {
            state = GameStates.Stop;
        }

        public void GameOver()
        {
            state = GameStates.Over;
            AudioThread.GetInstance().StopMusic();
            AudioThread.GetInstance().QueueSound(SoundTypes.gameOver);
        }

        public void NextLevel()
        {
            level++;
        }

        public void Reset()
        {
            level = 0;
            multiplayer = Constants.defaultMultiplayer;
            state = GameStates.New;
        }

        public void ExitGame()
        {
            AudioThread.GetInstance().StopMusic();
            System.Windows.Forms.Application.Exit();
        }

        public void MainMenu()
        {
            menu = MenuTypes.Main;
        }

        public void AudioMenu()
        {
            menu = MenuTypes.Audio;
        }

        public void ToggleMultiplayer()
        {
            multiplayerSwitched = true;
            multiplayer = !multiplayer;
        }

        public void ResetMultiplayerSwitched()
        {
            multiplayerSwitched = false;
        }

        public void StartReplay()
        {
            state = GameStates.ReplayPlaying;
        }

        public void Replay()
        {
            state = GameStates.Replay;
        }
    }
}
