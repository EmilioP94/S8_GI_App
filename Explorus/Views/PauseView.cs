using Explorus.Models;
using Explorus.Threads;
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
            AudioThread audioThread = AudioThread.GetInstance();
            Rectangle rect = new Rectangle(offset.X, offset.Y, width, height);
            Rectangle rect1 = new Rectangle(offset.X, offset.Y, width, height/3);
            Rectangle rect2 = new Rectangle(offset.X, height/3, width, height/3);
            Rectangle rect3 = new Rectangle(offset.X, (2*height)/3, width, height/3);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;      // Horizontal Alignment
            stringFormat.LineAlignment = StringAlignment.Center;  // Vertical Alignment
            SolidBrush brush = new SolidBrush(Color.FromArgb(190, Color.Black));
            SolidBrush selectedOptionBrush = new SolidBrush(Color.FromArgb(190, Color.Red));
            e.Graphics.FillRectangle(brush, rect);
            e.Graphics.DrawString("Paused", new Font("Times New Roman", 48), Brushes.White, rect1, stringFormat);
            e.Graphics.DrawString($"Music volume: {(int)(100 * audioThread.musicVolume)}", new Font("Times New Roman", 32), GameState.GetInstance().menuIndex == 0 ? selectedOptionBrush : Brushes.White, rect2, stringFormat);
            e.Graphics.DrawString($"FX volume: {(int)(100 * audioThread.soundVolume)}", new Font("Times New Roman", 32), GameState.GetInstance().menuIndex == 1 ? selectedOptionBrush : Brushes.White, rect3, stringFormat);

        }

    }
}
