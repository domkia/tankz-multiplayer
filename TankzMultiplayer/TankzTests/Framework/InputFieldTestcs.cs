using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TankzClient.Game;
using TankzTests.Mocks;

namespace TankzClient.Framework.Tests
{
    [TestClass()]
    public class InputFieldTestcs
    {
        [TestMethod()]
        public void ClickTestOnButton()
        {
            InputField field = new InputField(0, 0, 10, 10);
            System.Drawing.Point point = new System.Drawing.Point(2, 2);
            field.Click(point);
            Assert.IsTrue(field.IsFocused, "paspausta ne ant mygtuko");
        }
        [TestMethod()]
        public void ClickTestOutsideOfButton()
        {
            InputField field = new InputField(0, 0, 10, 10);
            System.Drawing.Point point = new System.Drawing.Point(20, 25);
            field.Click(point);
            Assert.IsFalse(field.IsFocused, "paspausta ant mygtuko");
        }
    }
}
