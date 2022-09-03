using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Views
{
    internal interface IRenderableComponent
    {
        void Render(object sender, PaintEventArgs e);
    }
}
