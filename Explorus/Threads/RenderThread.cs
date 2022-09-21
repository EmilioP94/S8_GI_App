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
        LabyrinthController labyrinthController;

        bool running;
        int lastFrameTime;

        public RenderThread(GameView view, LabyrinthController labyrinthController)
        {
            this.view = view;
            this.labyrinthController = labyrinthController;
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
        }
        private void Update(int elapseTime)
        {
            view.framerate = 1000 / elapseTime;
            view.state = labyrinthController.gameState.state;
            view.level = labyrinthController.gameState.level;

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
