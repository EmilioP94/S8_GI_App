using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models.GameEvents
{
    internal class ToxicSlimeHitEvent : GameEvent
    {
        public ToxicSlimeHitEvent(Guid id) : base(id)
        {
        }

        public override void Execute(ILabyrinth lab, bool fastForward)
        {
            if (!fastForward)
            {
                return;
            }
            ILabyrinthComponent toxicSlime = FindComponent(lab);
            if(toxicSlime is ToxicSlime)
            {
                ((ToxicSlime)toxicSlime).Hit();
            }
        }
    }
}
