/*
FSP:

RENDERTHREAD = (start -> RUNNING),
RUNNING = (doRender -> RENDER | stop -> RENDERTHREAD),
RENDER = (stop -> RENDERTHREAD | update -> RENDER).
 */
using Explorus.Controllers;
using Explorus.Models;
using Explorus.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Threads
{
    internal class RenderThread
    {
        public Thread thread;
        
        GameView view;

        bool running;
        int lastFrameTime;

        public RenderThread(GameView view)
        {
            this.view = view;
            thread = new Thread(new ThreadStart(DoRender));
            lastFrameTime = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
            running = true;
        }

        private void DoRender()
        {
            while(running)
            {
                int currentFrameTime = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
                Update(currentFrameTime - lastFrameTime);

                lastFrameTime = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
                int waitTime = currentFrameTime + Constants.msPerFrame - lastFrameTime;

                if (waitTime > 0)
                {
                    Thread.Sleep(waitTime);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
            Console.WriteLine("render thread stopping");
        }
        private void Update(int elapseTime)
        {
            view.framerate = 1000 / elapseTime;
            view.state = GameState.GetInstance().state;
            view.level = GameState.GetInstance().level;

            view.Render();

        }
        public void Start()
        {
            if (thread == null)
            {
                return;
            }
            thread.Start();
        }
        public void Stop()
        {
            running = false;
        }
    }
}
