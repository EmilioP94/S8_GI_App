using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Views
{
    internal class HeaderView : IRenderableComponent
    {
        private List<HeaderComponent> _components { get; set; }

        private int spacing = Constants.unit / 2;
        private int unit = Constants.unit;

        public HeaderView()
        {
            _components = new List<HeaderComponent>();
            //_components.Add(new HeaderComponent(0, 0, SpriteFactory.GetInstance().GetSprite(Sprites.title)));
            _components.Add(GetComponent(0, Sprites.title));
            _components.Add(GetComponent(4, Sprites.heart, true));
            _components.Add(GetComponent(5, Sprites.leftBarTip));
            _components.Add(GetComponent(6, Sprites.redBarFull));
            _components.Add(GetComponent(7, Sprites.redBarFull));
            _components.Add(GetComponent(8, Sprites.rightBarTip));
            _components.Add(GetComponent(8, Sprites.bigBubble, true));
            _components.Add(GetComponent(9, Sprites.leftBarTip));
            _components.Add(GetComponent(10, Sprites.blueBarFull));
            _components.Add(GetComponent(11, Sprites.blueBarFull));
            _components.Add(GetComponent(12, Sprites.rightBarTip));
            _components.Add(GetComponent(12, Sprites.gem, true));
            _components.Add(GetComponent(13, Sprites.leftBarTip));
            _components.Add(GetComponent(14, Sprites.emptyBar));
            _components.Add(GetComponent(15, Sprites.emptyBar));
            _components.Add(GetComponent(16, Sprites.rightBarTip));
        }

        private HeaderComponent GetComponent(int xMultiplier, Sprites sprite, bool space = false)
        {
            int pixelSpace = 0;
            if (space)
                pixelSpace = spacing;
            return new HeaderComponent((unit * xMultiplier) + pixelSpace, 0, SpriteFactory.GetInstance().GetSprite(sprite));
        }
        public void Render(object sender, PaintEventArgs e)
        {
            foreach(HeaderComponent component in _components)
            {
                component.Show(e, 0);
            }
        }
    }
}
