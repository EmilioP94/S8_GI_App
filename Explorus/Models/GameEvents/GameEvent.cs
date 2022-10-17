using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models.GameEvents
{
    internal abstract class GameEvent : IGameEvent
    {
        public DateTime timestamp { get; private set; }
        public abstract void Execute(ILabyrinth lab, bool fastForward);
        protected Guid id { get; private set; }

        public GameEvent(Guid id)
        {
            timestamp = DateTime.Now;
            this.id = id;
        }

        protected ILabyrinthComponent FindComponent(ILabyrinth lab)
        {
            return lab.labyrinthComponentList.Find(item => item.id.Equals(id));
        }
    }
}
