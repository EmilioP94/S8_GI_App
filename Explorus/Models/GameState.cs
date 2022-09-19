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
        public void Pause(bool manual)
        {
            state = GameStates.Pause;
            this.manual = manual;
        }

        public void Play()
        {
            state = GameStates.Play;
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
        }

        public void NextLevel()
        {
            level++;
        }
    }
}
