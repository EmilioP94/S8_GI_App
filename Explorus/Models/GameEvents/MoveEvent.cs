using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models.GameEvents
{
    internal class MoveEvent : GameEvent
    {
        Point destination;
        public MoveEvent(Guid id, Point destination) : base(id)
        {
            this.destination = destination;
        }

        public override void Execute(ILabyrinth lab, bool fastForward)
        {
            ILabyrinthComponent component = FindComponent(lab);
            Slime slime = component as Slime;
            if (fastForward)
            {
                slime.Teleport(destination);
            }
            else
            {
                slime.SetDestination(destination);
            }
        }
    }
}
