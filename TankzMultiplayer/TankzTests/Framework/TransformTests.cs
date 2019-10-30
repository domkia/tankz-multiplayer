using Microsoft.VisualStudio.TestTools.UnitTesting;
using TankzClient.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TankzTests.Mocks;

namespace TankzClient.Framework.Tests
{
    [TestClass()]
    public class TransformTests
    {
        [TestMethod()]
        public void SetPositionTestNoParent()
        {
            EntityMock entity = new EntityMock();
            Transform transform = new Transform(entity);
            Vector2 newPos = new Vector2(10f, 10f);
            transform.SetPosition(newPos);
            Vector2 expected = newPos;
            Vector2 actual = transform.position;
            Assert.AreEqual(expected, actual, "Nesutampa norima gauti pozicija su gauta");
        }

        [TestMethod()]
        public void SetPositionTestWithParent()
        {
            EntityMock entity = new EntityMock();
            EntityMock entity2 = new EntityMock();
            entity.SetParent(entity2);
            Vector2 parentPos = new Vector2(2f, 2f);
            entity2.transform.SetPosition(parentPos);
            Vector2 newPos = new Vector2(10f, 10f);
            entity.transform.SetPosition(newPos);
            Vector2 expected = new Vector2(12f, 12f);
            Vector2 actual = entity.transform.position;
            Assert.AreEqual(expected, actual, "Nesutampa norima gauti pozicija su gauta esant tėvinei esybei");
        }

        [TestMethod()]
        public void TranslateTestNoParent()
        {
            EntityMock entity = new EntityMock();
            Transform transform = new Transform(entity);
            Vector2 newPos = new Vector2(10f, 10f);
            transform.SetPosition(newPos);
            Vector2 offset = new Vector2(2f, 2f);
            transform.Translate(offset);
            Vector2 expected = new Vector2(12f, 12f);
            Vector2 actual = transform.position;
            Assert.AreEqual(expected, actual, "Nesutampa norima gauti pozicija su gauta po pastūmimo");
        }
        [TestMethod()]
        public void TranslateTestWithParent()
        {
            EntityMock entity = new EntityMock();
            EntityMock entity2 = new EntityMock();
            entity.SetParent(entity2);
            Vector2 parentPos = new Vector2(2f, 2f);
            entity2.transform.SetPosition(parentPos);
            Vector2 newPos = new Vector2(10f, 10f);
            entity.transform.SetPosition(newPos);
            entity.transform.Translate(parentPos);
            Vector2 expected = new Vector2(14f, 14f);
            Vector2 actual = entity.transform.position;
            Assert.AreEqual(expected, actual, "Nesutampa norima gauti pozicija su gauta esant tėvinei esybei po pastūmimo");
        }

        [TestMethod()]
        public void GetParentWorldPosTest()
        {
            EntityMock entity = new EntityMock();
            EntityMock entity2 = new EntityMock();
            entity.SetParent(entity2);
            Vector2 parentPos = new Vector2(2f, 2f);
            entity2.transform.SetPosition(parentPos);
            Vector2 newPos = new Vector2(10f, 10f);
            entity.transform.SetPosition(newPos);
            Vector2 expected = new Vector2(2f, 2f);
            Vector2 actual = entity.transform.GetParentWorldPos();
            Assert.AreEqual(expected, actual, "Gauta tėvinės esybės pozicija nesutampa su tokia kokia turi būti");
        }
        [TestMethod()]
        public void GetParentWorldPosTestNoParent()
        {
            EntityMock entity = new EntityMock();
            Vector2 newPos = new Vector2(10f, 10f);
            entity.transform.SetPosition(newPos);
            Vector2 expected = new Vector2(10f, 10f);
            Vector2 actual = entity.transform.GetParentWorldPos();
            Assert.AreEqual(expected, actual, "Gauta bloga pozicija nesant tėvinei esybei");
        }
    }
}