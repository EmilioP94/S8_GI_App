using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    //Command pattern
    internal interface IGameEvent
    {
        void Execute(ILabyrinth lab, bool fastForward);
    }
}
