using Explorus;
using Explorus.Controllers;
using Explorus.Models;
using Explorus.Threads;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestExplorus.Controllers
{
    [TestClass]
    public class LabyrinthControllerTest
    {
        LabyrinthController labController;
        PhysicsThread physicsThread;
        Slimus player;
        int loopTime = 16;
        int originalX;
        int originalY;
        bool assert;

        [TestInitialize]
        public void Init()
        {
            labController = new LabyrinthController(TestConstants.testMap);
            player = labController.lab.players[0];
            GameState.GetInstance().Reset();
            GameState.GetInstance().Play();
            originalX = player.x;
            originalY = player.y;
            physicsThread = new PhysicsThread(labController.lab, GameState.GetInstance());
            assert = true;
            //Console.WriteLine($"original position is ({originalX}, {originalY})");
        }

        [TestMethod]
        public void TestProcessInputDown()
        {
            Assert.AreEqual(Direction.None, player.GetDirection());
            labController.ProcessInput(null, GenerateKeyEvent(Keys.Down), true, null);
            labController.handlePlayerInput(player, labController.slimusDirectionInput);
            Assert.AreEqual(DirectionInput.Down, labController.slimusDirectionInput);
            player.UpdatePosition(loopTime);
            labController.ProcessInput(null, GenerateKeyEvent(Keys.Down), false, null);
            labController.handlePlayerInput(player, labController.slimusDirectionInput);
            Assert.AreEqual(DirectionInput.None, labController.slimusDirectionInput);
            // when running, the loop sometimes takes 16 or 17 ms
            player.UpdatePosition(13 * loopTime);
            // this simulates an instance of a 17 ms loop, with only even numbers it does not work
            player.UpdatePosition(loopTime + 1);
            Assert.AreEqual(originalX, player.x);
            Assert.AreEqual(originalY + Constants.unit * 2, player.y);
        }

        [TestMethod]
        public void TestProcessInputUp()
        {
            Assert.AreEqual(Direction.None, player.GetDirection());
            labController.ProcessInput(null, GenerateKeyEvent(Keys.Up), true, null);
            labController.handlePlayerInput(player, labController.slimusDirectionInput);
            Assert.AreEqual(DirectionInput.Up, labController.slimusDirectionInput);
            player.UpdatePosition(loopTime);
            labController.ProcessInput(null, GenerateKeyEvent(Keys.Up), false, null);
            labController.handlePlayerInput(player, labController.slimusDirectionInput);
            Assert.AreEqual(DirectionInput.None, labController.slimusDirectionInput);
            // when running, the loop sometimes takes 16 or 17 ms
            player.UpdatePosition(13 * loopTime);
            // this simulates an instance of a 17 ms loop, with only even numbers it does not work
            player.UpdatePosition(loopTime + 1);
            Assert.AreEqual(DirectionInput.None, labController.slimusDirectionInput);
            Assert.AreEqual(originalX, player.x);
            Assert.AreEqual(originalY - Constants.unit * 2, player.y);
        }

        [TestMethod]
        public void TestProcessInputLeft()
        {
            Assert.AreEqual(Direction.None, player.GetDirection());
            labController.ProcessInput(null, GenerateKeyEvent(Keys.Left), true, null);
            labController.handlePlayerInput(player, labController.slimusDirectionInput);
            Assert.AreEqual(DirectionInput.Left, labController.slimusDirectionInput);
            player.UpdatePosition(loopTime);
            labController.ProcessInput(null, GenerateKeyEvent(Keys.Left), false, null);
            labController.handlePlayerInput(player, labController.slimusDirectionInput);
            Assert.AreEqual(DirectionInput.None, labController.slimusDirectionInput);
            // when running, the loop sometimes takes 16 or 17 ms
            player.UpdatePosition(13 * loopTime);
            // this simulates an instance of a 17 ms loop, with only even numbers it does not work
            player.UpdatePosition(loopTime + 1);
            Assert.AreEqual(DirectionInput.None, labController.slimusDirectionInput);
            Assert.AreEqual(originalX - Constants.unit * 2, player.x);
            Assert.AreEqual(originalY, player.y);
        }

        [TestMethod]
        public void TestProcessInputRight()
        {
            Assert.AreEqual(Direction.None, player.GetDirection());
            labController.ProcessInput(null, GenerateKeyEvent(Keys.Right), true, null);
            labController.handlePlayerInput(player, labController.slimusDirectionInput);
            Assert.AreEqual(DirectionInput.Right, labController.slimusDirectionInput);
            player.UpdatePosition(loopTime);
            labController.ProcessInput(null, GenerateKeyEvent(Keys.Right), false, null);
            labController.handlePlayerInput(player, labController.slimusDirectionInput);
            Assert.AreEqual(DirectionInput.None, labController.slimusDirectionInput);
            // when running, the loop sometimes takes 16 or 17 ms
            player.UpdatePosition(13 * loopTime);
            // this simulates an instance of a 17 ms loop, with only even numbers it does not work
            player.UpdatePosition(loopTime + 1);
            Assert.AreEqual(DirectionInput.None, labController.slimusDirectionInput);
            if(assert)
            {
                Assert.AreEqual(originalX + Constants.unit * 2, player.x);
                Assert.AreEqual(originalY, player.y);
            }
        }

        [TestMethod]
        public void TestCollideWithWall()
        {
            TestProcessInputUp();
            TestProcessInputUp();
            PrivateObject prPhysTh = new PrivateObject(physicsThread);
            var collision = prPhysTh.Invoke("CheckForCollision", player);
            Assert.IsFalse((bool)collision);
        }

        [TestMethod]
        public void TestCollideWithToxicSlime()
        {
            assert = false;
            TestProcessInputRight();
            TestProcessInputRight();
            PrivateObject prPhysTh = new PrivateObject(physicsThread);
            var collision = prPhysTh.Invoke("CheckForCollision", player);
            Assert.IsFalse((bool)collision);
            Assert.AreEqual(5, player.hearts.acquired);
        }

        [TestMethod]
        public void TestCollideWithGem()
        {
            TestProcessInputDown();
            PrivateObject prPhysTh = new PrivateObject(physicsThread);
            prPhysTh.Invoke("CheckForCollision", player);
            Assert.AreEqual(1, player.gems.acquired);
        }

        [TestMethod]
        public void TestShootBubble()
        {
            Assert.AreEqual(6, player.bubbles.acquired);
            labController.ProcessInput(null, GenerateKeyEvent(Keys.Space), true, null);
            Assert.AreEqual(1, labController.lab.GetComponentListCopy().OfType<Bubble>().ToList().Count());
        }

        private KeyEventArgs GenerateKeyEvent(Keys key)
        {
            KeyEventArgs keyEventArgs = new KeyEventArgs(key);
            return keyEventArgs;
        }
    }
}
