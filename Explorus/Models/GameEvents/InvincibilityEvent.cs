using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models.GameEvents
{
    internal class InvincibilityEvent : GameEvent
    {
        bool isInvincible;
        public InvincibilityEvent(Guid id, bool isInvicible) : base(id)
        {
            this.isInvincible = isInvicible;
        }

        public override void Execute(ILabyrinth lab, bool fastForward)
        {
            ILabyrinthComponent component = FindComponent(lab);
            Slimus slimus = component as Slimus;
            if(slimus != null)
            {
                slimus.IsInvincible(isInvincible);
            }
        }
    }
}