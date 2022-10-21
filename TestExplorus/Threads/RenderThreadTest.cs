using Explorus.Controllers;
using Explorus.Models;
using Explorus.Threads;
using Explorus.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestExplorus.Threads
{
    [TestClass]
    public class RenderThreadTest
    {
        [TestMethod]
        public void TestInitRender()
        {
            LabyrinthController labyrinthController = new LabyrinthController();
            HeaderController headerController = new HeaderController(labyrinthController.lab);

            GameView oView = new GameView(ProcessInputKeyDown, ProcessInputKeyUp, labyrinthController.lab, headerController);
            RenderThread renderThread = new RenderThread(oView);

            renderThread.Start();
            Thread.Sleep(200);

            Assert.IsTrue(renderThread.thread != null);
            renderThread.Stop();
        }

        private void ProcessInputKeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void ProcessInputKeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}
