
using System.Drawing.Imaging;

namespace Explorus.Models
{
    enum Bars
    {
        none,
        red,
        blue,
        yellow
    }
    internal class HeaderComponent: IRenderableModel
    {
        public int x { get; set; }
        public int y { get; set; }
        public Bars barName;
        public Image2D image { get; private set; }

        public ImageAttributes attributes { get; private set; } = null;

        public string name;

        public HeaderComponent(int x, int y, Image2D image, Bars barName, string name)
        {
            this.x = x;
            this.y = y;
            this.image = image;
            this.barName = barName;
            this.name = name;
        }
    }
}
