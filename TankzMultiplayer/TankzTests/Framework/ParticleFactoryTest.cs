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
    public class ParticleFactoryTest
    {
        [TestMethod()]
        public void ParticleFactoryCreateTest()
        {
            EntityCreateArgs args = new EntityCreateArgs("explosion");
            SceneManager.Instance.LoadScene<MockScene>();
            ParticleFactory factory = new ParticleFactory();
            Entity element = factory.Create(args);
            bool deleted = SceneManager.Instance.CurrentScene.DestroyEntity(element);
            Assert.IsTrue(deleted, "Scenoje nerastas objektas");
        }
        [TestMethod()]
        public void ParticleFactoryCreateTestNotExisting()
        {
            EntityCreateArgs args = new EntityCreateArgs("random");
            SceneManager.Instance.LoadScene<MockScene>();
            ParticleFactory factory = new ParticleFactory();
            Assert.IsNull( factory.Create(args), "Sukurtas objektas kai turėjo gražinti null");
        }
    }
}
