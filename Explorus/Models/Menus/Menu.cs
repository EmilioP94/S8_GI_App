using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal abstract class GameMenu
    {
        public List<MenuOption> menuOptions;
        public int selectedMenuOptionIndex = 0;
        public void NavigateMenu(bool down)
        {
            switch (down)
            {
                case true:
                    if (selectedMenuOptionIndex < menuOptions.Count - 1)
                    {
                        selectedMenuOptionIndex++;
                    }
                    else selectedMenuOptionIndex = 0;
                    return;
                case false:
                    if (selectedMenuOptionIndex > 0)
                    {
                        selectedMenuOptionIndex--;
                    }
                    else selectedMenuOptionIndex = menuOptions.Count - 1;
                    return;
            }
        }
        public void SelectOption(int optionIndex)
        {
            menuOptions.ElementAt(optionIndex).ExecuteOption();
        }

        public void IncrementAction(int optionIndex)
        {
            menuOptions.ElementAt(optionIndex).ExecuteIncrement();
        }

        public void DecrementAction(int optionIndex)
        {
            menuOptions.ElementAt(optionIndex).ExecuteDecrement();
        }

        public void Reset()
        {
            selectedMenuOptionIndex = 0;
        }
    }
}
