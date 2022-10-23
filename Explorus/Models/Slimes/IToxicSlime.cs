using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models.Slimes
{
    internal interface IToxicSlime
    {
        void MoveToNextDestination(ILabyrinth lab);
    }
}
