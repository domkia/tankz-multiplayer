using Microsoft.VisualStudio.TestTools.UnitTesting;
using TankzClient.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TankzTests.Mocks;

namespace TankzClient.Framework.Tests
{
    [TestClass()]
    public class SceneTests
    {
        [TestMethod()]
        public void CreateEntityTest()
        {
            MockScene scene = new MockScene();
            EntityMock entity = new EntityMock();
            EntityMock created = scene.CreateEntity(entity) as EntityMock;
            Assert.AreEqual(created, entity, "created entity is not same as mocked entity");
        }
        [TestMethod()]
        public void CreateEntityTestAddMultipleTimes()
        {
            MockScene scene = new MockScene();
            EntityMock entity = new EntityMock();
            EntityMock created = scene.CreateEntity(entity) as EntityMock;
            EntityMock created2 = scene.CreateEntity(entity) as EntityMock;
            Assert.AreEqual(created2, null, "Same entity got created");
        }

        [TestMethod()]
        public void DestroyEntityTestSuccess()
        {
            MockScene scene = new MockScene();
            EntityMock entity = new EntityMock();
            EntityMock created = scene.CreateEntity(entity) as EntityMock;
            Assert.IsTrue(scene.DestroyEntity(entity), "object was not existing");
        }
        [TestMethod()]
        public void DestroyEntityNotExistingObject()
        {
            MockScene scene = new MockScene();
            EntityMock entity = new EntityMock();
            EntityMock entity2 = new EntityMock();
            EntityMock created = scene.CreateEntity(entity) as EntityMock;
            Assert.IsFalse(scene.DestroyEntity(entity2));
        }
    }
}