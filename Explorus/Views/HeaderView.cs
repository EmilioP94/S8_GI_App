using Explorus.Controllers;
using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Views
{

    internal class HeaderView : IRenderableComponent, Models.IObserver<List<HeaderComponent>>
    {
        private List<HeaderComponent> _components { get; set; }

        public HeaderView(HeaderController headerController)
        {
            _components = headerController.components;
            headerController.Subscribe(this);
        }

        public void Render(object sender, PaintEventArgs e, Point offset)
        {
            foreach (HeaderComponent component in _components)
            {
                e.Graphics.DrawImage(component.image.image, component.x + Constants.unit / 4, component.y + Constants.unit / 2 + offset.Y);
            }
        }

        public void OnNext(List<HeaderComponent> value)
        {
            _components = value;
        }
    }
}
