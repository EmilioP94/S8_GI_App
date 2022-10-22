using Explorus.Controllers;
using Explorus.Models.GameEvents;
using Explorus.Models.Slimes;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class ToxicSlime : Slime, IToxicSlime
    {
        Random random = new Random();
        protected int fieldOfView = 24;//how many pixels off center will the toxic slime see a player
        public int hp { get; private set; } = Constants.initialToxicSlimeHp;
        public ToxicSlime(int x, int y) : base(x, y, SpriteFactory.GetInstance().GetSprite(Sprites.toxicSlimeDownLarge))
        {
            isDead = false;
            animationImages = new Image2D[3, 4];

            var SFInstance = SpriteFactory.GetInstance();

            animationImages[0, 0] = SFInstance.GetSprite(Sprites.toxicSlimeUpLarge);
            animationImages[1, 0] = SFInstance.GetSprite(Sprites.toxicSlimeUpMedium);
            animationImages[2, 0] = SFInstance.GetSprite(Sprites.toxicSlimeUpSmall);

            animationImages[0, 1] = SFInstance.GetSprite(Sprites.toxicSlimeRightLarge);
            animationImages[1, 1] = SFInstance.GetSprite(Sprites.toxicSlimeRightMedium);
            animationImages[2, 1] = SFInstance.GetSprite(Sprites.toxicSlimeRightSmall);

            animationImages[0, 2] = SFInstance.GetSprite(Sprites.toxicSlimeDownLarge);
            animationImages[1, 2] = SFInstance.GetSprite(Sprites.toxicSlimeDownMedium);
            animationImages[2, 2] = SFInstance.GetSprite(Sprites.toxicSlimeDownSmall);

            animationImages[0, 3] = SFInstance.GetSprite(Sprites.toxicSlimeLeftLarge);
            animationImages[1, 3] = SFInstance.GetSprite(Sprites.toxicSlimeLeftMedium);
            animationImages[2, 3] = SFInstance.GetSprite(Sprites.toxicSlimeLeftSmall);
        }

        public void Hit()
        {
            GameRecorder.GetInstance().AddEvent(new ToxicSlimeHitEvent(id));
            hp--;
            if (hp == 1)
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = 0.5f;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                this.attributes = attributes;
            }
            if (hp == 0) isDead = true;
            hitbox = new System.Drawing.Rectangle();
        }


        public override bool Collide(ILabyrinthComponent comp)
        {
            if (comp is Bubble)
            {
                Bubble bubble = (Bubble)comp;
                bubble.PopBubble();
                Hit();
                return isDead;
            }
            return false;
        }

        public override void Reset()
        {
            base.Reset();
            hp = Constants.initialToxicSlimeHp;
            attributes = null;
        }

        public virtual void MoveToNextDestination(ILabyrinth lab)
        {
            Direction direction = (Direction)random.Next(0, 4);
            MoveToValidDestination(direction, lab);
        }

        protected Slimus GetParallelPlayer(ILabyrinth lab)
        {
            foreach (Slimus player in lab.players)
            {
                if (GetRelativePlayerPosition(player) != Direction.None)
                {
                    return player;
                }
            }
            return null;
        }

        protected Direction GetRelativePlayerPosition(Slimus player)
        {
            if (IsWithinRange(player.x, x, fieldOfView))
            {
                if(player.y < y)
                {
                    return Direction.Up;
                }
                if(player.y > y)
                {
                    return Direction.Down;
                }
            }
            if (IsWithinRange(player.y, y, fieldOfView))
            {
                if (player.x < x)
                {
                    return Direction.Left;
                }
                if (player.x > x)
                {
                    return Direction.Right;
                }
            }
            return Direction.None;
        }

        protected bool IsWithinRange(int value1, int value2, int range)
        {
            return value1 <= value2 + range && value1 >= value2 - range;
        }
    }
}
