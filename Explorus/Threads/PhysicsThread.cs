/*
 *
PHYSICSTHREAD = (start -> RUNNING),
RUNNING = (pause -> PAUSED | checkForCollision -> CHECKCOLLISION),
CHECKCOLLISION = (moveComponents -> MOVECOMPONENTS),
MOVECOMPONENTS = (nextLoop -> RUNNING),
PAUSED = (stop -> PHYSICSTHREAD | resume -> RUNNING). 
 * 
 */
using Explorus.Controllers;
using Explorus.Models;
using Explorus.Models.GameEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Explorus.Threads
{
    internal class PhysicsThread
    {
        public Thread thread;
        ILabyrinth lab;
        GameState gameState;
        int lastVerification;
        bool running;
        readonly int msPerFrame = 16;

        public PhysicsThread(ILabyrinth lab, GameState gameState)
        {
            this.lab = lab;
            this.gameState = gameState;
            lastVerification = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
            thread = new Thread(new ThreadStart(DoPhysics));
            running = true;
        }

        private void MoveBubbles(int elapseTime)
        {
            foreach (Bubble bubble in lab.GetComponentListCopy().OfType<Bubble>().ToList())
            {
                bubble.DeleteCheck();
                bubble.Move(elapseTime);
                CheckForCollision(bubble);
            }
        }

        private void MoveToxicSlimes(int elapseTime)
        {
            Random random = new Random();
            foreach (ToxicSlime slime in lab.toxicSlimes)
            {
                slime.MoveToNextDestination(lab);
                slime.UpdatePosition(elapseTime);
            }
        }
        protected bool CheckForCollision(ILabyrinthComponent srcComp)
        {
            foreach (ILabyrinthComponent comp in lab.GetComponentListCopy())
            {
                if (srcComp == comp)//ignore  collision with itself
                    continue;

                if (srcComp != null && comp.hitbox.IntersectsWith(srcComp.hitbox))
                {
                    bool result = false;
                    if (comp is ToxicSlime && srcComp is Slimus)
                    {
                        result = srcComp.Collide(comp);
                        if ((GameState.GetInstance().multiplayer && lab.players.ElementAt(0).hearts.acquired == 0 && lab.players.ElementAt(1).hearts.acquired == 0) ||(!GameState.GetInstance().multiplayer && lab.players.ElementAt(0).hearts.acquired == 0))
                        {
                            gameState.GameOver();
                            GameRecorder.GetInstance().AddEvent(new GameOverEvent());
                        }
                    }
                    else result = comp.Collide(srcComp);
                    if (result) //true seulement si c'est une collision entre une bulle et un toxicSlime
                    {
                        lab.CreateGems(comp.x, comp.y);
                    }
                    return result;
                }
            }
            return false;
        }

        private void UpdatePlayers(int elapseTime)
        {
            lab.players.ForEach(player =>
            {
                if(player != null)
                {
                    CheckForCollision(player);
                    player.UpdatePosition(elapseTime);
                    player.RechargeBubbles(elapseTime);
                }
            });
        }

        private void DoPhysics()
        {
            while (running)
            {
                int startFrameTime = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
                if (gameState.state == GameStates.Play)
                {
                    int elapseTime = startFrameTime - lastVerification;
                    UpdatePlayers(elapseTime);
                    MoveToxicSlimes(elapseTime);
                    MoveBubbles(elapseTime);
                }
                if (gameState.state == GameStates.ReplayPlaying)
                {

                    int elapseTime = startFrameTime - lastVerification;
                    GameRecorder.GetInstance().ExecuteNextEvents(elapseTime);
                    UpdatePlayers(elapseTime);
                    foreach (ToxicSlime slime in lab.toxicSlimes)
                    {
                        slime.UpdatePosition(elapseTime);
                    }
                    MoveBubbles(elapseTime);
                }
                int endFrameTime = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
                int waitTime = startFrameTime + msPerFrame - endFrameTime;
                lastVerification = startFrameTime;
                if (waitTime > 0)
                {
                    Thread.Sleep(waitTime);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
            Console.WriteLine("physics thread stopping");
        }

        public void Start()
        {
            thread.Start();
        }

        public void Stop()
        {
            running = false;
        }
    }
}
