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
        //Start executing the events for replay, blocking call
        public void StartReplay(ILabyrinth lab)
        {
            bool isFastForward = true;
            _isRecording = false;
            List<IGameEvent> replayEvents = gameEvents.ToList();
            int lastEvent = replayEvents.Last().timestamp.Millisecond;
            IEnumerator<IGameEvent> enumerator = replayEvents.GetEnumerator();
            while (isFastForward)
            {
                IGameEvent currentEvent = enumerator.Current;
                if(currentEvent.timestamp.Millisecond < lastEvent - millSinceLast)
                {
                    currentEvent.Execute(lab, true);
                    if (!enumerator.MoveNext())
                    {
                        isFastForward = false;
                    }
                }
                else
                {
                    isFastForward = false;
                }
            }
            bool isReplaying = true;
            DateTime start = DateTime.Now;
            IGameEvent previousEvent = enumerator.Current;
            while (isReplaying)
            {
                if(enumerator.Current.timestamp.Millisecond - previousEvent.timestamp.Millisecond <= DateTime.Now.Millisecond - start.Millisecond)
                {
                    enumerator.Current.Execute(lab, false);
                    if (!enumerator.MoveNext())
                    {
                        isReplaying = false;
                    }
                }
            }
        }
    }
}
