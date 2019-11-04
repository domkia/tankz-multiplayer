using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using TankzClient.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TankzClient.Models;
using Newtonsoft.Json;

namespace TankzClient.Framework.Tests
{
    [Ignore]
    [TestClass()]
    public class NetworkManagerTests
    {
        Thread networkThread = new Thread(NetworkManager.Instance.connect);
        
        [TestInitialize]
        public void testInit()
        {
            NetworkManager.Instance.start();
            networkThread.IsBackground = true;
            networkThread.Start();
        }

        [TestCleanup]
        public void testClean()
        {
            networkThread.Abort();
        }

        [TestMethod()]
        public void SetNameTest()
        {
            NetworkManager.Instance.SetName("Rokas");
            NetworkManager.Instance.GetConnectedPlayerList();
            Player[] player = NetworkManager.Instance.GetPlayerList();
            Assert.AreEqual(player[0].Name, "Rokas", "Vardai nesutampa");
        }
    }
}