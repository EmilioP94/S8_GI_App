using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        LabyrinthController labyrinthController;

        private const int msPerFrame = 14;
        private int lastGameLoop;


        public GameEngine()
        {
            labyrinthController = new LabyrinthController();
            oView = new GameView(ProcessInput, labyrinthController.lab);
            lastGameLoop = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
            Thread thread = new Thread(new ThreadStart(GameLoop));
            thread.Start();
            oView.Show();
        }

        private void ProcessInput(object sender, KeyEventArgs e)
        {
            labyrinthController.ProcessInput(e);
        }

        private void Update(int elapseTime)
        {
            oView.framerate = 1000/elapseTime;
            labyrinthController.ProcessMovement(elapseTime);
        }

        private void GameLoop()
        {
            while (true)
            {
                int startFrameTime = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
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
