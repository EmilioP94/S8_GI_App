﻿using Explorus.Controllers;
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
        Thread thread;
        ILabyrinth lab;
        GameState gameState;
        int lastVerification;

        public PhysicsThread(ILabyrinth lab, GameState gameState)
        {
            this.lab = lab;
            this.gameState = gameState;
            lastVerification = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
            thread = new Thread(new ThreadStart(DoPhysics));
        }

        private void MoveBubbles(int elapseTime)
        {
            foreach (Bubble bubble in lab.labyrinthComponentList.OfType<Bubble>().ToList())
            {
                bubble.DeleteCheck();
                bubble.Move(elapseTime);
                CheckForCollision(bubble);
            }
        }

        public void MoveToxicSlimes(int elapseTime)
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
            foreach (ILabyrinthComponent comp in lab.labyrinthComponentList)
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
            while (true)
            {
                int startFrameTime = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
                if (gameState.state == GameStates.Play)
                {
                    int elapseTime = startFrameTime - lastVerification;
                    Console.WriteLine(elapseTime);
                    CheckForCollision(lab.playerCharacter);
                    lab.playerCharacter.UpdatePosition(elapseTime);
                    MoveToxicSlimes(elapseTime);
                    MoveBubbles(elapseTime);
                    lab.playerCharacter.RechargeBubbles(elapseTime);
                    lastVerification = startFrameTime;
                }
                Thread.Sleep(1);
            }
        }

        public void Start()
        {
            if (thread == null)
            {
                return;
            }
            thread.Start();
        }
    }
}
