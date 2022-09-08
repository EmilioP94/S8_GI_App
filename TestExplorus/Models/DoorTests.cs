using Explorus.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestExplorus.Models
{
    [TestClass]
    public class DoorTests
    {
        [TestMethod]
        public void TestDoorCollisionWithoutAllCoinCollect()
        {
            Image2D image = new Image2D(null, ImageType.Wall);
            Door door = new Door(0, 0, image);
            
            Sprites[,] testMap = new Sprites[1, 1];
            testMap[0, 0] = Sprites.gem;

            Collection gems = new Collection(testMap, Sprites.gem, Bars.yellow, false);

            Slimus slimus = new Slimus(0, 0);
            slimus.SetCollections(gems, gems, gems);
            door.Collide(slimus);            
        }

        [TestMethod]
        public void TestDoorCollisionWithAllCoinCollect()
        {
            Image2D image = new Image2D(null, ImageType.Wall);
            Door door = new Door(0, 0, image);

            Sprites[,] testMap = new Sprites[1, 1];
            testMap[0, 0] = Sprites.gem;

            Collection gems = new Collection(testMap, Sprites.gem, Bars.yellow, true);

            Slimus slimus = new Slimus(0, 0);
            slimus.SetCollections(gems, gems, gems);            
            door.Collide(slimus);
            door.Collide(slimus);

            Assert.IsTrue(door.isSolid);
            Assert.IsNotNull(door.hitbox);
        }
    }
}