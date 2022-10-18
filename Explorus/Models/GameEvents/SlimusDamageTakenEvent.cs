using Explorus.Controllers;
using Explorus.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models.GameEvents
{
    internal class SlimusDamageTakenEvent : GameEvent
    {
        public SlimusDamageTakenEvent(Guid id) : base(id)
        {
        }

        public override void Execute(ILabyrinth lab, bool fastForward)
        {
            ILabyrinthComponent component = FindComponent(lab);
            Slimus slimus = component as Slimus;
            if(slimus != null)
            {
                slimus.hearts.Decrement();
                slimus.ShouldDie();
                AudioThread.GetInstance().QueueSound(SoundTypes.ennemyCollision);
            }
        }
    }
}
