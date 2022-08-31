using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus
{
    internal class GameView
    {
        GameForm oGameForm;
        public GameView()
        {
            oGameForm = new GameForm();
            oGameForm.Paint += GameRenderer;
        }

        public void Show() { Application.Run(oGameForm); }

        public void Render()
        {
            if (oGameForm.Visible)
                oGameForm.BeginInvoke((MethodInvoker)delegate
                {
                    oGameForm.Refresh();
                });
        }

        public void Close()
        {
            if (oGameForm.Visible)
                oGameForm.BeginInvoke((MethodInvoker)delegate {
                    oGameForm.Close();
                });
        }

        private void GameRenderer(object sender, PaintEventArgs e) 
        {
        
        }
    }
}
