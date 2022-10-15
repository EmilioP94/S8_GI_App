using Explorus.Models;
using Explorus.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Controllers
{
    internal class PauseMenuController : IInputController
    {
        public void ProcessInput(object sender, KeyEventArgs e, bool isKeyDown = true)
        {
            switch ((char)e.KeyValue)
            {
                case (char)Keys.R:
                    GameState.GetInstance().Resume();
                    break;
                case (char)Keys.Down:
                case (char)Keys.Up:
                    GameState.GetInstance().NavigateMenu();
                    break;
                case (char)Keys.Left:
                    Decrement();
                    break;
                case (char)Keys.Right:
                    Increment();
                    break;
            }
        }
        private void Decrement()
        {
            if(GameState.GetInstance().menuIndex == 0)
            {
                AudioThread.GetInstance().QueueEvent(SoundsEvents.DecrementMusic);
            }
            else
            {
                AudioThread.GetInstance().QueueEvent(SoundsEvents.DecrementSounds);
            }
        }
        private void Increment()
        {
            if(GameState.GetInstance().menuIndex == 0)
            {
                AudioThread.GetInstance().QueueEvent(SoundsEvents.IncrementMusic);
            } 
            else
            {
                AudioThread.GetInstance().QueueEvent(SoundsEvents.IncrementSounds);
            }
        }
    }
}
