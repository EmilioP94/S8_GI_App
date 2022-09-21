using Explorus.Controllers;
using Explorus.Threads;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{

    internal class Bubble : LabyrinthComponent
    {
        Direction direction;
        Image2D poppedBubbleImage;
        bool isMoving;

        public Bubble(int x, int y, Image2D defaultImage, Image2D poppedBubbleImage, Direction dir) : base(x, y, defaultImage)
        {
            this.x += Constants.unit / 2;
            this.y += Constants.unit / 2;
            hitbox = new Rectangle(x, y, Constants.unit * 2, Constants.unit * 2);

            direction = dir;
            isMoving = true;

            this.poppedBubbleImage = poppedBubbleImage;
        }

        public override bool Collide(ILabyrinthComponent comp)
        {
            return false;
        }

        public void PopBubble()
        {
            AudioThread.GetInstance().QueueSound(SoundTypes.bubbleExplode);
            isMoving = false;
            base.image = poppedBubbleImage;
            hitbox = new Rectangle();
        }

        public bool DeleteCheck()
        {
            if(!isMoving)
            {
                base.image = null;
                return true;
            }
            else
            {
                return false;
            }    
            
        }

        public void Move(int deltaT)
        {
            if(!isMoving)
                return;

            if (direction == Direction.Up)
            {
                y -= (int)(deltaT * Constants.playerSpeed * 2);
            }
            else if (direction == Direction.Right)
            {
                x += (int)(deltaT * Constants.playerSpeed * 2);
            }
            else if (direction == Direction.Down)
            {
                y += (int)(deltaT * Constants.playerSpeed * 2);
            }
            else if (direction == Direction.Left)
            {
                x -= (int)(deltaT * Constants.playerSpeed * 2);
            }
            hitbox = new Rectangle(x, y, Constants.unit, Constants.unit);
        }
    }
}
