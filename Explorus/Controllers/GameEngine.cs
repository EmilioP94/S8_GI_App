using System;
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


        public GameEngine()
        {
            oView = new GameView();
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
                Update(17);

                oView.Render();
                int endFrameTime = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
                int waitTime = startFrameTime + 17 - endFrameTime;
                Console.WriteLine(waitTime);

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
