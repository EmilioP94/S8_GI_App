using Explorus.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Controllers
{
    internal class GameRecorder
    {
        private ConcurrentQueue<IGameEvent> gameEvents = new ConcurrentQueue<IGameEvent> ();
        private static GameRecorder _instance = null;
        private bool _isRecording = true;

        private GameRecorder() { }

        public static GameRecorder GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameRecorder ();
            }
            return _instance;
        }

        public void AddEvent(IGameEvent newEvent) {
            if(_isRecording)
            {
                gameEvents.Enqueue(newEvent);
            }
        }
        public void NewGame()
        {
            gameEvents = new ConcurrentQueue<IGameEvent>();
            _isRecording = true;
        }

        public void EndGame()
        {
            _isRecording = false;
        }
    }
}
