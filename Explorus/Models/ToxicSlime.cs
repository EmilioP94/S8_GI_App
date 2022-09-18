using Explorus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class ToxicSlime : Slime
    {
        public ToxicSlime(int x, int y) : base(x, y, SpriteFactory.GetInstance().GetSprite(Sprites.toxicSlimeDownLarge))
        {
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
    }
}
