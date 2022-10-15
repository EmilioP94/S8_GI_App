using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorus.Models
{
    internal class MenuOption
    {
        public string label;
        Action enterAction;
        Action incrementAction;
        Action decrementAction;
        public int value = -1;
        public bool disabled = false;
        public MenuOption(string label, Action enterAction)
        {
            this.label = label;
            this.enterAction = enterAction;
        }

        public MenuOption(string label, Action enterAction, Action incrementAction, Action decrementAction, int value)
        {
            this.label = label;
            this.enterAction = enterAction;
            this.incrementAction = incrementAction;
            this.decrementAction = decrementAction;
            this.value = value;
        }

        public void ExecuteOption()
        {
            enterAction?.Invoke();
        }

        public void ExecuteDecrement()
        {
            decrementAction?.Invoke();
        }

        public void ExecuteIncrement()
        {
            incrementAction?.Invoke();
        }
    }
}

