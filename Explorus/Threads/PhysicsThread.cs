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
                Direction direction = (Direction)random.Next(0, 4);
                slime.MoveToValidDestination(direction, lab);
                slime.UpdatePosition(elapseTime);
            }
        }
        private bool CheckForCollision(ILabyrinthComponent srcComp)
        {
            foreach (ILabyrinthComponent comp in lab.GetComponentListCopy())
            {
                if (srcComp == comp)//ignore  collision with itself
                    continue;

                if (comp.hitbox.IntersectsWith(srcComp.hitbox))
                {
                    bool result = false;
                    if (comp.GetType() == typeof(ToxicSlime) && srcComp.GetType() == typeof(Slimus))
                    {
                        result = srcComp.Collide(comp);
                        if (lab.playerCharacter.hearts.acquired == 0)
                        {
                            gameState.GameOver();
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

        private void DoPhysics()
        {
            while (running)
            {
                int startFrameTime = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
                if (gameState.state == GameStates.Play)
                {
                    int elapseTime = startFrameTime - lastVerification;
                    CheckForCollision(lab.playerCharacter);
                    lab.playerCharacter.UpdatePosition(elapseTime);
                    MoveToxicSlimes(elapseTime);
                    MoveBubbles(elapseTime);
                    lab.playerCharacter.RechargeBubbles(elapseTime);
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
