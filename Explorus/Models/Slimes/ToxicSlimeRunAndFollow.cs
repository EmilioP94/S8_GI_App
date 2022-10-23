using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models.Slimes
{
    internal class ToxicSlimeRunAndFollow : ToxicSlimeFollow
    {
        private bool shouldRun = false;
        private int lastDecision = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
        private int shouldWaitTillNextDecision = 5000;//wait at least 5 seconds before changing behavior

        public ToxicSlimeRunAndFollow(int x, int y) : base(x, y)
        {
        }

        private Direction ShouldRunTo(Direction relativePlayerDirection)
        {
            switch (relativePlayerDirection)
            {
                case Direction.Left:
                    return Direction.Right;
                case Direction.Up:
                    return Direction.Down;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Down:
                    return Direction.Up;
            }
            return Direction.None;
        }

        private bool RunFromPlayer(ILabyrinth lab)
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
                //Run in opposite direction until we collide with a wall
                if (!MoveToValidDestination(ShouldRunTo(directionToSlimus.Value), lab))
                {
                    lastSlimusPosition = null;
                    directionToSlimus = null;
                    chasedPlayer = null;
                }
                return true;
            }
            return false;
        }

        public override void MoveToNextDestination(ILabyrinth lab)
        {
            int elapsed = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds() - lastDecision;
            //Console.WriteLine("Elapsed, {0}", elapsed);
            if(elapsed > shouldWaitTillNextDecision)
            {
                shouldRun = Convert.ToBoolean(random.Next(0, 2));
                lastDecision = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
            }
            if (shouldRun)
            {
                if (!RunFromPlayer(lab))
                {
                    RandomMovements(lab);
                }
            }
            else
            {
                base.MoveToNextDestination(lab);
            }
        }
    }
}
