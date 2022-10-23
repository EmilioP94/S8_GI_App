using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models.Slimes
{
    internal class ToxicSlimeParallel : ToxicSlime
    {
        public ToxicSlimeParallel(int x, int y) : base(x, y)
        {
        }

        public override void MoveToNextDestination(ILabyrinth lab)
        {
            Slimus player = GetParallelPlayer(lab);
            if (player != null)
            {
                Direction directionToSlimus = GetRelativePlayerPosition(player);
                if(directionToSlimus == Direction.Left || directionToSlimus == Direction.Right)
                {
                    if(player.y < y && player.GetDirection() != Direction.Down)
                    {
                        MoveToValidDestination(Direction.Up, lab);
                    }
                    else if(player.y > y && player.GetDirection() != Direction.Up)
                    {
                        MoveToValidDestination(Direction.Down, lab);
                    }
                    else
                    {
                        MoveToValidDestination(directionToSlimus, lab);
                    }
                }
                else
                {
                    if (player.x < x && player.GetDirection() != Direction.Right)
                    {
                        MoveToValidDestination(Direction.Left, lab);
                    }
                    else if (player.x > x && player.GetDirection() != Direction.Left)
                    {
                        MoveToValidDestination(Direction.Right, lab);
                    }
                    else
                    {
                        MoveToValidDestination(directionToSlimus, lab);
                    }
                }
            }
            else
            {
                base.MoveToNextDestination(lab);
            }
        }
    }
}
