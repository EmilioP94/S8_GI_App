using System.Drawing;
using System.Drawing.Imaging;

namespace Explorus.Models
{
    internal class Door : LabyrinthComponent
    {
        public Door(ILabyrinthComponent component) : base(component)
        {
        }

        public Door(int x, int y, Image2D image) : base(x, y, image)
        {
            ColorMatrix matrix = new ColorMatrix();
            matrix.Matrix33 = 0.5f;
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            this.attributes = attributes;

            isSolid = true;
            hitbox = new Rectangle(x, y, Constants.unit * 2, Constants.unit * 2);
            this.initialState = new Door(this);
        }

        public override bool Collide(ILabyrinthComponent comp)
        {
            if (comp is Slimus)
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = 0.0f;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                this.attributes = attributes;
            }
            else if (comp is Bubble)
            {
                Bubble bubble = (Bubble)comp;
                bubble.PopBubble();
            }
            return false;
        }

        public override bool IsValidDestination(Slime player)
        {
            if(player is Slimus)
            {
                Slimus slimus = (Slimus)player;
                if (slimus.gems.total == slimus.gems.acquired)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override void Reset()
        {
            base.Reset();
            isSolid = true;
            hitbox = new Rectangle(x, y, Constants.unit * 2, Constants.unit * 2);
        }
    }
}
