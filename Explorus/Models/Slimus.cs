using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Models
{
    enum FacingDirection
    {        
        Up,
        Right,
        Down,
        Left
    }
    internal class Slimus : LabyrinthComponent
    {
        Image2D [,] slimusImages;

        FacingDirection currentDirection;
        int animationCycleIndex = 0;

        public Slimus(int x, int y, Image2D image) : base(x, y, image)
        {
            slimusImages = new Image2D[3, 4];

            var SFInstance = SpriteFactory.GetInstance();

            slimusImages[0, 0] = SFInstance.GetSprite(Sprites.slimusUpLarge);
            slimusImages[1, 0] = SFInstance.GetSprite(Sprites.slimusUpMedium);
            slimusImages[2, 0] = SFInstance.GetSprite(Sprites.slimusUpSmall);

            slimusImages[0, 1] = SFInstance.GetSprite(Sprites.slimusRightLarge);
            slimusImages[1, 1] = SFInstance.GetSprite(Sprites.slimusRightMedium);
            slimusImages[2, 1] = SFInstance.GetSprite(Sprites.slimusRightSmall);

            slimusImages[0, 2] = SFInstance.GetSprite(Sprites.slimusDownLarge);
            slimusImages[1, 2] = SFInstance.GetSprite(Sprites.slimusDownMedium);
            slimusImages[2, 2] = SFInstance.GetSprite(Sprites.slimusDownSmall);

            slimusImages[0, 3] = SFInstance.GetSprite(Sprites.slimusLeftLarge);
            slimusImages[1, 3] = SFInstance.GetSprite(Sprites.slimusLeftMedium);
            slimusImages[2, 3] = SFInstance.GetSprite(Sprites.slimusLeftSmall);
        }

        public override void Show(PaintEventArgs e)
        {
            e.Graphics.DrawImage(slimusImages[animationCycleIndex, (int)currentDirection].image, x, y);
        }

        public void ChangeDirection(FacingDirection dir)
        {
            currentDirection = dir;
        }
        public void SetAnimationState(int index)
        {
            animationCycleIndex = index;
        }
    }
}
