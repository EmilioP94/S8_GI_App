using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Controllers
{
    internal interface IInputController
    {
        void ProcessInput(object sender, KeyEventArgs e);
    }
}
