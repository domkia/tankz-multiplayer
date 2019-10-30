using Microsoft.VisualStudio.TestTools.UnitTesting;
using TankzClient.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TankzClient.Game;
using TankzTests.Mocks;

namespace TankzClient.Framework.Tests
{
    [TestClass()]
    public class UIFactoryTests
    {
        [TestMethod()]
        public void CreateTestButton()
        {
            MockScene scene = new MockScene();
            SceneManager.Instance.LoadScene<MockScene>();
            UICreateArgs args = new UICreateArgs("button", new Vector2(0, 0));
            UIFactory factory = new UIFactory();
            Button element = (Button)factory.Create(args);
            bool deleted = SceneManager.Instance.CurrentScene.DestroyEntity(element);
            Assert.IsTrue(deleted);
        }
        [TestMethod()]
        public void CreateTestWrong()
        {
            MockScene scene = new MockScene();
            SceneManager.Instance.LoadScene<MockScene>();
            UICreateArgs args = new UICreateArgs("abc", new Vector2(0, 0));
            UIFactory factory = new UIFactory();
            Assert.IsNull((Button)factory.Create(args));
        }
    }
}