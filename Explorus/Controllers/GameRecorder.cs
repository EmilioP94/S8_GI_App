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
        private int millSinceLast = 5000;
        private IEnumerator<IGameEvent> replayEnumerator = null;
        ILabyrinth lab = null;
        int totalElapsed = 0;
        IGameEvent firstEvent = null;
        public bool hasPlayed { get; private set; } = false;

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

        public void PrepareLabForReplay(ILabyrinth lab)
        {
            this.lab = lab;
            bool isFastForward = true;
            _isRecording = false;
            List<IGameEvent> replayEvents = gameEvents.ToList();
            int lastEvent = replayEvents.Last().timestamp;
            replayEnumerator = replayEvents.GetEnumerator();
            while (isFastForward)
            {
                if(replayEnumerator.Current == null)
                {
                    if (!replayEnumerator.MoveNext())
                    {
                        isFastForward = false;
                    }
                }

                if (replayEnumerator.Current.timestamp < lastEvent - millSinceLast)
                {
                    replayEnumerator.Current.Execute(lab, true);
                    if (!replayEnumerator.MoveNext())
                    {
                        isFastForward = false;
                    }
                }
                else
                {
                    isFastForward = false;
                }
            }
            firstEvent = replayEnumerator.Current;
        }

        //Start executing the events for replay, blocking call
        public void ExecuteNextEvents(int elapsed)
        {
            totalElapsed += elapsed;
            Console.WriteLine("elapsed {0}, total {1}", elapsed, totalElapsed);
            bool isReplaying = true;
            while (isReplaying)
            {
                if(replayEnumerator.Current == null)
                {
                    if (!replayEnumerator.MoveNext())
                    {
                        hasPlayed = true;
                        return;
                    }
                }
                if (replayEnumerator.Current.timestamp - firstEvent.timestamp <= totalElapsed)
                {
                    replayEnumerator.Current.Execute(lab, false);
                    if (!replayEnumerator.MoveNext())
                    {
                        hasPlayed = true;
                        isReplaying = false;
                    }
                }
                else isReplaying = false;
            }
        }
    }
}
