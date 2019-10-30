using Microsoft.VisualStudio.TestTools.UnitTesting;
using TankzClient.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TankzClient.Framework.Tests
{
    [TestClass()]
    public class ProgressBarTests
    {
        [TestMethod()]
        public void SetProgressTestIfInbetweenLimits()
        {
            Rectangle rect = new Rectangle(0,0,0,0);
            ProgressBar progressBar = new ProgressBar(rect, Color.White, Color.White);
            progressBar.SetProgress(0.5f);
            float actual = progressBar.Progress;
            float expected = 0.5f;

            Assert.AreEqual(expected, actual, "Not equal progress");
        }

        [TestMethod()]
        public void SetProgressTestIfHigherThanOne()
        {
            Rectangle rect = new Rectangle(0, 0, 0, 0);
            ProgressBar progressBar = new ProgressBar(rect, Color.White, Color.White);
            progressBar.SetProgress(10.2f);
            float actual = progressBar.Progress;
            float expected = 1.0f;

            Assert.AreEqual(expected, actual, "Not equal progress");
        }
        [TestMethod()]
        public void SetProgressTestIfLowerThanZero()
        {
            Rectangle rect = new Rectangle(0, 0, 0, 0);
            ProgressBar progressBar = new ProgressBar(rect, Color.White, Color.White);
            progressBar.SetProgress(-10.0f);
            float actual = progressBar.Progress;
            float expected = 0.0f;

            Assert.AreEqual(expected, actual, "Not equal progress");
        }
    }
}