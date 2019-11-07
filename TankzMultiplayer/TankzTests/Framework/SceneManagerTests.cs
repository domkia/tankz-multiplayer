using Microsoft.VisualStudio.TestTools.UnitTesting;
using TankzClient.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TankzTests.Mocks;

namespace TankzClient.Framework.Tests
{
    [TestClass()]
    public class SceneManagerTests
    {
        [TestMethod()]
        public void LoadSceneTest()
        {
            SceneManager.Instance.LoadScene<MockScene>();
            Assert.IsInstanceOfType(SceneManager.Instance.CurrentScene, typeof(MockScene), "wrong scenes");
        }
    }
}