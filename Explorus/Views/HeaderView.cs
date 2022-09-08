using Explorus.Controllers;
using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Views
{

    internal class HeaderView : IRenderableComponent, IObserver<List<HeaderComponent>>
    {
        private List<HeaderComponent> _components { get; set; }

        public HeaderView(HeaderController headerController)
        {
            _components = headerController.components;
            headerController.Subscribe(this);
        }

        public void Render(object sender, PaintEventArgs e)
        {
            foreach (HeaderComponent component in _components)
            {
                component.Show(e, 0);
            }
        }

        public void OnNext(List<HeaderComponent> value)
        {
            _components = value;
            //foreach(HeaderComponent component in _components)
            //{
            //    Console.WriteLine($"component is {component.name}");
            //}
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}
