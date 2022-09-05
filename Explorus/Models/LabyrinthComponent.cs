using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Explorus.Models
{
    internal class LabyrinthComponent : ILabyrinthComponent
    {
        public int x { get; set; }

        public int y { get; set; }

        public Image2D image { get; private set; }

        public bool isSolid = false;
        public Rectangle hitbox { get; private set; }

        public LabyrinthComponent(int x, int y, Image2D image)
        {
            this.x = x;
            this.y = y;
            this.image = image;

            if (image.type == ImageType.Collectible)
            {
                this.x += Constants.unit / 2;
                this.y += Constants.unit / 2;
                hitbox = new Rectangle(x, y, Constants.unit * 2, Constants.unit * 2);
            }

            if(image.type == ImageType.Wall || image.type == ImageType.Door)
            {
                isSolid = true;
                hitbox = new Rectangle(x, y, Constants.unit * 2, Constants.unit * 2);
            }
        }

        public void Show(PaintEventArgs e)
        {
            if(image.type == ImageType.Door)
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = 0.5f;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                e.Graphics.DrawImage(image.image, new Rectangle(x, y, image.image.Width, image.image.Height), 0, 0, image.image.Width, image.image.Height,GraphicsUnit.Pixel, attributes);
            }
            else
            {
                e.Graphics.DrawImage(image.image, x, y);
            }
        }
    }
}
