using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainMenu = Explorus.Models.MainMenu;

namespace Explorus.Views.Menus
{
    internal class MenuView
    {
        int width;
        int height;
        GameMenu menu;

        public MenuView(int width, int height, MenuTypes menu)
        {
            this.width = width;
            this.height = height;
            switch (menu)
            {
                case MenuTypes.Main:
                    this.menu = MainMenu.GetInstance();
                    break;
                case MenuTypes.Audio:
                    this.menu = AudioMenu.GetInstance();
                    break;
            }
        }

        public void Render(object sender, PaintEventArgs e, Point offset)
        {
            int count = menu.menuOptions.Count;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;      // Horizontal Alignment
            stringFormat.LineAlignment = StringAlignment.Center;  // Vertical Alignment
            SolidBrush bgBrush = new SolidBrush(Color.FromArgb(190, Color.Black));
            SolidBrush titleBrush = new SolidBrush(Color.FromArgb(69, 180, 239));
            SolidBrush selectedOptionBrush = new SolidBrush(Color.FromArgb(190, Color.Red));

            Rectangle menuRect = new Rectangle(offset.X, offset.Y, width, height);
            e.Graphics.FillRectangle(bgBrush, menuRect);

            Rectangle pauseLabelRect = new Rectangle(offset.X, offset.Y, width, (int)(0.2 * height));
            e.Graphics.DrawString("Explorus", new Font("Times New Roman", 54), titleBrush, pauseLabelRect, stringFormat);

            for (int i = 0; i < count; i++)
            {
                MenuOption menuOption = menu.menuOptions.ElementAt(i);
                Rectangle rect = new Rectangle(offset.X, ((i + 1) * height) / (count + 1), width, height / (count + 1));
                e.Graphics.DrawString(menuOption.label, new Font("Times New Roman", 32), menu.selectedMenuOptionIndex == i ? selectedOptionBrush : Brushes.White, rect, stringFormat);
                if(menuOption.value >= 0)
                {
                    Rectangle valueRect = new Rectangle(offset.X, ((i + 1) * height) / (count + 1) + 50, width, height / (count + 1));
                    if(menuOption.disabled)
                    {
                        e.Graphics.DrawString("Muted", new Font("Times New Roman", 20), Brushes.Yellow, valueRect, stringFormat);
                    } else
                    {
                        e.Graphics.DrawString(menuOption.value.ToString(), new Font("Times New Roman", 20), Brushes.Yellow, valueRect, stringFormat);
                    }
                }
            }

        }
    }
}
