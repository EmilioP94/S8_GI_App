using Explorus.Models;
using Explorus.Threads;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System;
using TestExplorus.Models;
using System.Linq;

namespace TestExplorus.Threads
{
    [TestClass]
    public class PhysicsThreadTest
    {
        [TestMethod]
        public void TestToxicSlime()
        {
            Sprites[,] map = { { Sprites.toxicSlimeFollow, Sprites.slimusDownLarge } };
            Labyrinth lab = new Labyrinth(map);
            Assert.IsTrue(lab.toxicSlimes.Count > 0);
            GameState.GetInstance().Play();
            Assert.AreEqual(GameState.GetInstance().state, GameStates.Play);
            PhysicsThread physics = new PhysicsThread(lab, GameState.GetInstance());
            Assert.IsNotNull(physics);
            int x = lab.toxicSlimes[0].x;
            int y = lab.toxicSlimes[0].y;
            Thread.Sleep(200);
            Assert.AreEqual(x, lab.toxicSlimes[0].x);
            Assert.AreEqual(y, lab.toxicSlimes[0].y);
            physics.Start();
            Thread.Sleep(200);
            Assert.IsTrue(y != lab.toxicSlimes[0].y || x != lab.toxicSlimes[0].x);
            physics.Stop();
            x = lab.toxicSlimes[0].x;
            y = lab.toxicSlimes[0].y;
            Thread.Sleep(200);
            Assert.AreEqual(x, lab.toxicSlimes[0].x);
            Assert.AreEqual(y, lab.toxicSlimes[0].y);
        }

        [TestMethod]
        public void TestSlimus()
        {
            Sprites[,] map = { { Sprites.slimusDownLarge } };
            Labyrinth lab = new Labyrinth(map);
            Assert.IsNotNull(lab.players[0]);
            GameState.GetInstance().Play();
            Assert.AreEqual(GameState.GetInstance().state, GameStates.Play);
            PhysicsThread physics = new PhysicsThread(lab, GameState.GetInstance());
            Assert.IsNotNull(physics);
            int x = lab.players[0].x;
            int y = lab.players[0].y;
            Thread.Sleep(200);
            Assert.AreEqual(x, lab.players[0].x);
            Assert.AreEqual(y, lab.players[0].y);
            lab.players[0].Move(Explorus.Controllers.Direction.Right);
            physics.Start();
            Thread.Sleep(200);
            Assert.AreEqual(y, lab.players[0].y);
            Assert.AreNotEqual(x, lab.players[0].x);
        }

        [TestMethod]
        public void TestBubble()
        {
            Sprites[,] map = { { Sprites.slimusDownLarge, Sprites.empty, Sprites.empty, Sprites.wall } };
            Labyrinth lab = new Labyrinth(map);
            Assert.IsNotNull(lab.players[0]);
            GameState.GetInstance().Play();
            Assert.AreEqual(GameState.GetInstance().state, GameStates.Play);
            PhysicsThread physics = new PhysicsThread(lab, GameState.GetInstance());
            Assert.IsNotNull(physics);
            int x = lab.players[0].x;
            int y = lab.players[0].y;
            Thread.Sleep(200);
            Assert.AreEqual(x, lab.players[0].x);
            Assert.AreEqual(y, lab.players[0].y);
            lab.players[0].ChangeDirection(Explorus.Controllers.Direction.Right);
            physics.Start();
            int count = lab.labyrinthComponentList.Count;
            lab.CreateBubble(lab.players[0]);
            Assert.AreNotEqual(count, lab.labyrinthComponentList.Count);
            Bubble bubble = null;
            foreach (Bubble _bubble in lab.labyrinthComponentList.OfType<Bubble>())
            {
                bubble = _bubble;
            }
            Assert.IsNotNull(bubble);
            int bubblex = bubble.x;
            Thread.Sleep(200);
            Assert.AreNotEqual(bubblex, bubble.x);
        }
    }
}
