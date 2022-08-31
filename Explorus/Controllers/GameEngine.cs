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

        private void GameLoop()
        {
            oView.Render();
            Thread.Sleep(5000);
            oView.Close();
        }
    }
}
