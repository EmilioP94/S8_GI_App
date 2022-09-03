using Explorus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Controllers
{
    internal class LabyrinthController
    {
        public ILabyrinth lab { get; private set; } = new Labyrinth();
        public LabyrinthComponent[,] components;

        private int x, y = 0;

        public void ProcessInput(KeyEventArgs e)
        {

            // find slimus position in x, y
            // if target space is empty or contains collectible, move slimus, replace with empty
            // else don't do shit

            // slimusPosition[0] =­> x
            // slimusPosition[1] => y
            Console.WriteLine(e.KeyValue);
            if (e.KeyValue == (char)Keys.Up)
            {
                if (lab.map[lab.slimusPosition[0] - 1, lab.slimusPosition[1]]  != Models.Sprites.wall)
                {
                    lab.map[lab.slimusPosition[0], lab.slimusPosition[1]] = Models.Sprites.empty;
                    lab.slimusPosition[0] = lab.slimusPosition[0] - 1;
                    lab.map[lab.slimusPosition[0], lab.slimusPosition[1]] = Models.Sprites.slimusDownLarge;
                }
            }
            if (e.KeyValue == (char)Keys.Left)
            {
                if (lab.map[lab.slimusPosition[0], lab.slimusPosition[1] - 1] != Models.Sprites.wall)
                {
                    lab.map[lab.slimusPosition[0], lab.slimusPosition[1]] = Models.Sprites.empty;
                    lab.slimusPosition[1] = lab.slimusPosition[1] - 1;
                    lab.map[lab.slimusPosition[0], lab.slimusPosition[1]] = Models.Sprites.slimusDownLarge;
                }
            }
            if (e.KeyValue == (char)Keys.Right)
            {
                if (lab.map[lab.slimusPosition[0], lab.slimusPosition[1] + 1] != Models.Sprites.wall)
                {
                    lab.map[lab.slimusPosition[0], lab.slimusPosition[1]] = Models.Sprites.empty;
                    lab.slimusPosition[1] = lab.slimusPosition[1] + 1;
                    lab.map[lab.slimusPosition[0], lab.slimusPosition[1]] = Models.Sprites.slimusDownLarge;
                }
            }
            if (e.KeyValue == (char)Keys.Down)
            {
                if (lab.map[lab.slimusPosition[0] + 1, lab.slimusPosition[1]] != Models.Sprites.wall)
                {
                    lab.map[lab.slimusPosition[0], lab.slimusPosition[1]] = Models.Sprites.empty;
                    lab.slimusPosition[0] = lab.slimusPosition[0] + 1;
                    lab.map[lab.slimusPosition[0], lab.slimusPosition[1]] = Models.Sprites.slimusDownLarge;
                }
            }
            Console.WriteLine(x);
            Console.WriteLine(y);
        }
    }
}
