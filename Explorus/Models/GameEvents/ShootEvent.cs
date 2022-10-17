﻿using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models.GameEvents
{
    internal class ShootEvent : GameEvent
    {
        public ShootEvent(Guid id) : base(id)
        {
        }

        public override void Execute(ILabyrinth lab, bool fastForward)
        {
            if (!fastForward)
            {
                lab.CreateBubble();
            }
        }
    }
}