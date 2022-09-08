using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class Door : LabyrinthComponent
    {
        private bool isOpen = false;
        public Door(int x, int y, Image2D image) : base(x, y, image)
        {
            ColorMatrix matrix = new ColorMatrix();
            matrix.Matrix33 = 0.5f;
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            this.attributes = attributes;

            isSolid = true;
            hitbox = new Rectangle(x, y, Constants.unit * 2, Constants.unit * 2);
        }

        public override bool Collide(Slimus player)
        {
            if (isOpen)
            {
                return false;
            }
            if(!isOpen && player.gems.total == player.gems.acquired)
            {
                isOpen = true;
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = 0.0f;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                this.attributes = attributes;
                return false;
            }
            return true;
        }
    }
}
