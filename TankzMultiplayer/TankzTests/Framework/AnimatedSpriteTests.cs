using Microsoft.VisualStudio.TestTools.UnitTesting;
using TankzClient.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankzClient.Framework.Tests
{
    [TestClass()]
    public class AnimatedSpriteTests
    {
        [TestMethod()]
        public void SetFrameIndexTestNegative()
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile("..//..//..//..//TankzClient//res//crater.bmp");
            AnimatedSprite animatedSprite = new AnimatedSprite(image, new Vector2(10f, 10f), new Vector2(10f, 10f), 2, 2);
            Assert.ThrowsException<IndexOutOfRangeException>(() => animatedSprite.SetFrame(-5),"Negaunamas exception kai indeksas neigiamas");
        }
        [TestMethod()]
        public void SetFrameIndexTestTooBig()
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile("..//..//..//..//TankzClient//res//crater.bmp");
            AnimatedSprite animatedSprite = new AnimatedSprite(image, new Vector2(10f, 10f), new Vector2(10f, 10f), 2, 2);
            Assert.ThrowsException<IndexOutOfRangeException>(() => animatedSprite.SetFrame(5),"Negaunamas exception kai per didelis indeksas");
        }
    }
}