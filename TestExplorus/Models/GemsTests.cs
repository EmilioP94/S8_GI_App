﻿using Explorus.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestExplorus.Models
{
    [TestClass]
    public class GemsTests
    {
        [TestMethod]
        public void TestMethod()
        {
            Gems gem1 = new Gems(0, 0, null);
            Gems gem2 = new Gems(0, 1, null);

            Slimus slimus = new Slimus(0, 0);

            Assert.AreEqual(6, slimus.gems.total);
            Assert.AreEqual(0, slimus.gems.acquired);

            gem1.Collide(slimus);
            Assert.AreEqual(1, slimus.gems.acquired);

            gem2.Collide(slimus);
            Assert.AreEqual(2, slimus.gems.acquired);
        }
    }
}