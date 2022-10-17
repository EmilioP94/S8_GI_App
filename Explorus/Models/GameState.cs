using Explorus.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    enum GameStates
    {
        Play,
        Replay,
        Pause,
        Resume,
        Stop,
        Over
    }

    internal class GameState
    {
        public GameStates state { get; private set; } = GameStates.Play;
        public int level { get; private set; } = 0;
        public int maxLevel = Constants.levels.Length;
        public bool manual;
        public int menuIndex = 0;
        public int maxMenuIndex = 1;

        private static GameState _instance = null;

        public static GameState GetInstance()
        {
            if(_instance == null)
            {
                _instance = new GameState();
            }
            return _instance;
        }
        public void Pause(bool manual)
        {
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
            menuIndex = 0;
        }

        public void NavigateMenu()
        {
            if (menuIndex == 0)
            {
                menuIndex = 1;
            } else menuIndex = 0;
        }
    }
}
