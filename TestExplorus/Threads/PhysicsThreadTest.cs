using Explorus.Models;
using Explorus.Threads;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System;
using Moq;
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
            Sprites[,] map = { { Sprites.toxicSlimeDownLarge, Sprites.slimusDownLarge } };
            Labyrinth lab = new Labyrinth(map);
            Assert.IsTrue(lab.toxicSlimes.Count > 0);
            GameState gameState = new GameState();
            gameState.Play();
            Assert.AreEqual(gameState.state, GameStates.Play);
            PhysicsThread physics = new PhysicsThread(lab, gameState);
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
            Assert.IsNotNull(lab.playerCharacter);
            GameState gameState = new GameState();
            gameState.Play();
            Assert.AreEqual(gameState.state, GameStates.Play);
            PhysicsThread physics = new PhysicsThread(lab, gameState);
            Assert.IsNotNull(physics);
            int x = lab.playerCharacter.x;
            int y = lab.playerCharacter.y;
            Thread.Sleep(200);
            Assert.AreEqual(x, lab.playerCharacter.x);
            Assert.AreEqual(y, lab.playerCharacter.y);
            lab.playerCharacter.Move(Explorus.Controllers.Direction.Right);
            physics.Start();
            Thread.Sleep(200);
            Assert.AreEqual(y, lab.playerCharacter.y);
            Assert.AreNotEqual(x, lab.playerCharacter.x);
        }

        [TestMethod]
        public void TestBubble()
        {
            Sprites[,] map = { { Sprites.slimusDownLarge, Sprites.empty, Sprites.empty, Sprites.wall } };
            Labyrinth lab = new Labyrinth(map);
            Assert.IsNotNull(lab.playerCharacter);
            GameState gameState = new GameState();
            gameState.Play();
            Assert.AreEqual(gameState.state, GameStates.Play);
            PhysicsThread physics = new PhysicsThread(lab, gameState);
            Assert.IsNotNull(physics);
            int x = lab.playerCharacter.x;
            int y = lab.playerCharacter.y;
            Thread.Sleep(200);
            Assert.AreEqual(x, lab.playerCharacter.x);
            Assert.AreEqual(y, lab.playerCharacter.y);
            lab.playerCharacter.ChangeDirection(Explorus.Controllers.Direction.Right);
            physics.Start();
            int count = lab.labyrinthComponentList.Count;
            lab.CreateBubble();
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
