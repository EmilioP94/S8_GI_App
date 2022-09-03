﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus
{
    internal class GameEngine
    {
        GameView oView;

        private const int msPerFrame = 16;
        private int lastGameLoop;


        public GameEngine()
        {
            oView = new GameView();
            lastGameLoop = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
            Thread thread = new Thread(new ThreadStart(GameLoop));
            thread.Start();
            oView.Show();
        }

        private void ProcessInput()
        {

        }

        private void Update(int elapseTime)
        {
            oView.framerate = 1000/elapseTime;
        }

        private void GameLoop()
        {
            while (true)
            {
                int startFrameTime = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
                ProcessInput();
                Update(startFrameTime - lastGameLoop);
                lastGameLoop = startFrameTime;

                oView.Render();
                int endFrameTime = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
                int waitTime = startFrameTime + msPerFrame - endFrameTime;

                if(waitTime > 0)
                {
                    Thread.Sleep(waitTime);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }
    }
}