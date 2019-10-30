using Microsoft.VisualStudio.TestTools.UnitTesting;
using TankzClient.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TankzClient.Framework.Tests
{
    [TestClass()]
    public class SpriteAnimatorTests
    {
        [TestMethod()]
        public void AddAnimationTestFirstAsCurrentAnimation()
        {
            SpriteAnimator animator = new SpriteAnimator();
            FrameAnimation animation = new FrameAnimation(2f, false, 1, 10);
            animator.AddAnimation("test", animation);
            Assert.AreEqual(animation, animator.CurrentAnimation, "Animacija nepridėta arba sąrašas nebuvo tuščias");
        }
        [DataRow(2)]
        [DataRow(10)]
        [DataTestMethod]
        public void AddAnimationTestAddMultipleAnimationParametrized(int num)
        {
            SpriteAnimator animator = new SpriteAnimator();
            Dictionary<string, FrameAnimation> animDict = new Dictionary<string, FrameAnimation>();
            FrameAnimation[] animations = new FrameAnimation[num];
            for (int i = 0; i < num; i++)
            {
                string name = "name" + i;
                FrameAnimation anim = new FrameAnimation(10f, false, i, 20*i);
                animDict.Add(name, anim);
                animator.AddAnimation(name, anim);
            }
            Assert.IsTrue(animDict.All(x => animator.IsAnimationInList(x.Key) == true));
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception),
        "No such animation exists")]
        public void PlayAnimationTestNotExistant()
        {
            SpriteAnimator animator = new SpriteAnimator();
            FrameAnimation animation = new FrameAnimation(2f, false, 1, 10);
            animator.AddAnimation("test", animation);
            animator.PlayAnimation("wrongName");
        }

        [TestMethod()]
        public void PlayAnimationTestIfPlaying()
        {
            SpriteAnimator animator = new SpriteAnimator();
            FrameAnimation animation = new FrameAnimation(2f, false, 1, 10);
            animator.AddAnimation("test", animation);
            animator.PlayAnimation("test");
            Assert.IsTrue(animator.IsAnimationPlaying("test"));
        }

        [TestMethod()]
        public void PlayAnimationTestIfPlayingWrong()
        {
            SpriteAnimator animator = new SpriteAnimator();
            FrameAnimation animation = new FrameAnimation(2f, false, 1, 10);
            FrameAnimation animation2 = new FrameAnimation(5f, false, 1, 10);
            animator.AddAnimation("test", animation);
            animator.AddAnimation("testWrong", animation2);
            animator.PlayAnimation("test");
            Assert.IsFalse(animator.IsAnimationPlaying("testWrong"));
        }
    }
}