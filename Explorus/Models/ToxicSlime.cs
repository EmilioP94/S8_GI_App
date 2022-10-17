using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class ToxicSlime : Slime
    {
        int hp = 2;
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
        }


        public override bool Collide(ILabyrinthComponent comp)
        {
            if(comp.GetType() == typeof(Bubble))
            {
                Bubble bubble = (Bubble)comp;
                bubble.PopBubble();
                Hit();
                hitbox = new System.Drawing.Rectangle();
                return isDead;
            }
            return false;
        }
    }
}
