using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models.Slimes
{
    internal class ToxicSlimeFollow : ToxicSlime
    {
        protected Rectangle? lastSlimusPosition = null;
        protected Direction? directionToSlimus = null;
        protected Slimus chasedPlayer = null;
        public ToxicSlimeFollow(int x, int y) : base(x, y)
        {
        }

        public override void MoveToNextDestination(ILabyrinth lab)
        {
            Slimus player = GetParallelPlayer(lab);

            if (player != null)
            {
                if (!IsWallBetweenSlimus(player, lab))
                {
                    chasedPlayer = player;
                    lastSlimusPosition = player.hitbox;
                    directionToSlimus = GetRelativePlayerPosition(player);
                }
            }
            if (directionToSlimus.HasValue && lastSlimusPosition.HasValue)
            {
                if (lastSlimusPosition.Value.IntersectsWith(hitbox))
                {
                    lastSlimusPosition = null;
                    directionToSlimus = null;
                    chasedPlayer = null;
                }
                else if (!MoveToValidDestination(directionToSlimus.Value, lab))
                {
                    lastSlimusPosition = null;
                    directionToSlimus = null;
                    chasedPlayer = null;
                }
            }
            else
            {
                base.MoveToNextDestination(lab);
            }
        }

        protected bool IsWallBetweenSlimus(Slimus player, ILabyrinth lab)
        {
            List<ILabyrinthComponent> components = lab.GetComponentListCopy();
            foreach(ILabyrinthComponent component in components)
            {
                if (component.isSolid)
                {
                    if(IsBetweenSlimus(player, component))
                    {
                        return true;

                    }
                }
            }
            return false;
        }

        private bool IsBetweenSlimus(Slimus player, ILabyrinthComponent component)
        {
            Direction dir = GetRelativePlayerPosition(player);
            switch (dir)
            {
                case Direction.Up:
                    if(component.y < y && component.y > player.y && IsWithinRange(component.x, x, 24))
                    {
                        return true;
                    }
                    return false;
                case Direction.Down:
                    if (component.y > y && component.y < player.y && IsWithinRange(component.x, x, 24))
                    {
                        return true;
                    }
                    return false;
                case Direction.Right:
                    if (component.x > x && component.x < player.x && IsWithinRange(component.y, y, 24))
                    {
                        return true;
                    }
                    return false;
                case Direction.Left:
                    if (component.x < x && component.x > player.x && IsWithinRange(component.y, y, 24))
                    {
                        return true;
                    }
                    return false;
            }
            return false;
        }
    }
}
