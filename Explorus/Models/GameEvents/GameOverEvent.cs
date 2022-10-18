using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models.GameEvents
{
    internal class GameOverEvent : IGameEvent
    {
        public int timestamp { get; private set; }


        public GameOverEvent()
        {
            timestamp = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
        }
        public void Execute(ILabyrinth lab, bool fastForward)
        {
            GameState.GetInstance().GameOver();
        }
    }
}
