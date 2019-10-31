using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using TankzClient.Game;
using TankzClient.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankzTests.Framework
{
    [TestClass]
    public class TankTests
    {
        [Ignore]
        [TestMethod]
        public void ApplyCamouflageIsCreatedObjectNotEqual()
        {
            SceneManager.Instance.LoadScene<TestScene>();
            Tank usaTank =
                new TankBuilder()
                .SetChassis(1, 1)
                .SetTurret(1)
                .SetTracks(0)
                .Build();
            SceneManager.Instance.CurrentScene.CreateEntity(usaTank);
            usaTank.transform.SetPosition(new Vector2(500, 100));



            // Apply customiztion
            //usaTank.ApplyCamouflage(0);
            //usaTank.ApplySideskirt(0);
            //usaTank.ApplyAccessory(1);
            TankChassis chassis = usaTank.FindChild<TankChassis>();
            //new TankChassis(Image.FromFile($"../../res/tanks/chassis_0_.png"), new Vector2(64f, 48f), new Vector2(64f, 64f));
            SceneManager.Instance.CurrentScene.CreateEntity(chassis);
            TankAccessoriesDecorator accessoryChassis = new TankAccessoriesDecorator(3, chassis);
            accessoryChassis.SetParent(usaTank);
            SceneManager.Instance.CurrentScene.DestroyEntity(chassis);
            SceneManager.Instance.CurrentScene.CreateEntity(accessoryChassis);
            Assert.AreEqual(true, usaTank.children.Contains(accessoryChassis));
        }
    }
}
