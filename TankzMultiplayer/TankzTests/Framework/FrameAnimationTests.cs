using Microsoft.VisualStudio.TestTools.UnitTesting;
using TankzClient.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankzClient.Framework.Tests
{
    [TestClass()]
    public class FrameAnimationTests
    {
        [TestMethod()]
        public void GetFrameTest()
        {
            FrameAnimation animation = new FrameAnimation(40.0f, true, 5, 10);
            int actual = animation.GetFrame(10f);
            int expected = 7;

            Assert.AreEqual(expected, actual, 0, "Nevienodi gauti frame");
        }
    }
}