using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models.GameEvents
{
    internal class DirectionEvent : GameEvent
    {
        Direction direction;
        public DirectionEvent(Guid id, Direction direction) : base(id)
        {
            this.direction = direction;
        }

        public override void Execute(ILabyrinth lab, bool fastForward)
        {
            ILabyrinthComponent component = FindComponent(lab);
            Slime slime = component as Slime;
            if (slime != null)
            {
                slime.ChangeDirection(direction);
            }
        }
    }
}
