using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace TankzClient.Framework
{
    public class NetworkManager
    {
        private string[] Players;
        private static HubConnection _connection;
        #region Singleton

        private static NetworkManager instance = null;
        public static NetworkManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NetworkManager();
                }
                return instance;
            }
        }
        #endregion
        /// <summary>
        /// Gets localy saved player list
        /// </summary>
        /// <returns> Player list</returns>
        public string[] GetPlayerList()
        {
            if(Players == null)
            {
                return new string[] { };
            }
            return Players;
        }
        /// <summary>
        /// Asks server for players
        /// </summary>
        public void GetPlayer()
        { 
        _connection.InvokeAsync("GetPlayers");
        }
        /// <summary>
        /// Ask server for connected users
        /// </summary>
        public void GetConnected()
        {
            _connection.InvokeAsync("GetConnected");
        }
        /// <summary>
        /// Connection to server start
        /// </summary>
        public void start()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44311/TestHub")
                .Build();

            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };
        }
        /// <summary>
        /// Async method with listeners
        /// </summary>
        public async void connect()
        {
                _connection.On<string>("Connected",
                                       (connectionid) =>
                                       {
                                           //Debug stuff
                                           Console.WriteLine(connectionid);
                                       });
                _connection.On<string>("Disconnected",
                                       (value) =>
                                       {
                                           //Debug stuff
                                           Console.WriteLine("Player " + value + " disconnected");
                                       });
                //Gets connected player list
                _connection.On<string>("Players",
                                       (value) =>
                                       {
                                           var objects = JsonConvert.DeserializeObject<List<String>>(value);
                                           Players = objects.Select(obj => JsonConvert.SerializeObject(obj)).ToArray();
                                       });
                _connection.On<string>("ReceiveMessage",
                                       (value) =>
                                       {
                                           //Debug, not used anymore
                                           Console.WriteLine("Received " + value);
                                       });
            
                try
                {
                    await _connection.StartAsync();
                    Console.WriteLine("connection started");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            
        }
    }
}
