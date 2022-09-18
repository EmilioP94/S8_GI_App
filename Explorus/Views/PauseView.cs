using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Views
{
    internal class PauseView : IRenderableComponent
    {
        int width;
        int height;

        public PauseView(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Render(object sender, PaintEventArgs e, Point offset)
        {
            Rectangle rect = new Rectangle(offset.X, offset.Y, width, height);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;      // Horizontal Alignment
            stringFormat.LineAlignment = StringAlignment.Center;  // Vertical Alignment
            e.Graphics.DrawString("Paused", new Font("Times New Roman", 32), Brushes.White, rect, stringFormat);
            SolidBrush brush = new SolidBrush(Color.FromArgb(75, Color.Black));
            e.Graphics.FillRectangle(brush, rect);
        }

    }
}
