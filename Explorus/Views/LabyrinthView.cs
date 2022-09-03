using Explorus.Controllers;
using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Views
{
    internal class LabyrinthView : IRenderableComponent, IObserver<Sprites[,]>
    {
        private Sprites[,] _Sprites = new Sprites[Constants.LabyrinthWidth, Constants.LabyrinthHeight];
        public LabyrinthView(ILabyrinth lab)
        {
            _Sprites = lab.map;
        }
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Sprites[,] value)
        {
            throw new NotImplementedException();
        }

        public void Render(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < Constants.LabyrinthHeight; i++)
            {
                for(int j = 0; j < Constants.LabyrinthWidth; j++)
                {
                    e.Graphics.DrawImage(SpriteFactory.GetInstance().GetSprite(_Sprites[i, j]).image, Constants.unit * j * 2, Constants.unit * i * 2);
                }
            }
        }
    }
}
