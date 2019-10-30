using Microsoft.VisualStudio.TestTools.UnitTesting;
using TankzClient.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TankzTests.Mocks;

namespace TankzClient.Framework.Tests
{
    [TestClass()]
    public class EntityTests
    {
        [TestMethod()]
        public void SetParentTest()
        {
            EntityMock mockEntity = new EntityMock();
            EntityMock parentEntity = new EntityMock();
            mockEntity.SetParent(parentEntity);
            Assert.AreEqual(parentEntity, mockEntity.parent, "Parent objects are not equal");
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception),
        "Can't set child as a parent. Possible loop")]
        public void SetParentTestHierarchyLoop()
        {
            EntityMock mockEntity = new EntityMock();
            EntityMock parentEntity = new EntityMock();
            parentEntity.SetParent(mockEntity);
            mockEntity.SetParent(parentEntity);
        }
        [TestMethod()]
        public void FindChildTest()
        {
            EntityMock parentEntity = new EntityMock();
            EntityMock childEntity1 = new EntityMock();
            EntityMock childEntity2 = new EntityMock();
            childEntity1.SetParent(parentEntity);
            childEntity2.SetParent(parentEntity);
            EntityMock child = parentEntity.FindChild<EntityMock>();
            Assert.AreEqual(child, childEntity1, "Got wrong child object");
        }
    }
}