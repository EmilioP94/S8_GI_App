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
        Stop
    }

    internal class GameState
    {
        public GameStates state { get; private set; } = GameStates.Play;
        public int level { get; private set; } = 0;
        public int maxLevel = Constants.levels.Length;

        public void Pause()
        {
            state = GameStates.Pause;
        }

        public void Play()
        {
            state = GameStates.Play;
        }

        public void Resume()
        {
            state = GameStates.Resume;
            Task.Delay(new TimeSpan(0, 0, 3)).ContinueWith(o => { Play(); });
        }

        public void Stop()
        {
            state = GameStates.Stop;
        }

        public void NextLevel()
        {
            level++;
        }
    }
}
