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
    internal class MenuController : IInputController
    {
        public void ProcessInput(object sender, KeyEventArgs e, GameMenu menu)
        {
            switch ((char)e.KeyValue)
            {
                case (char)Keys.R:
                    GameState.GetInstance().Resume();
                    break;
                case (char)Keys.Down:
                    menu.NavigateMenu(true);
                    break;
                case (char)Keys.Up:
                    menu.NavigateMenu(false);
                    break;
                case (char)Keys.Left:
                    menu.DecrementAction(menu.selectedMenuOptionIndex);
                    break;
                case (char)Keys.Right:
                    menu.IncrementAction(menu.selectedMenuOptionIndex);
                    break;
                case (char)Keys.Enter:
                case (char)Keys.M:
                    menu.SelectOption(menu.selectedMenuOptionIndex);
                    break;
            }
        }
    }
}
