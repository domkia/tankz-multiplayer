using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TankzClient.Models;
using TankzClient.Game;

namespace TankzClient.Framework
{
    public class NetworkManager
    {
        private Player[] Players;
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
        public void ChangeReadyState()
        {
            _connection.InvokeAsync("ChangeReadyState");
        }
        /// <summary>
        /// Gets localy saved player list
        /// </summary>
        /// <returns> Player list</returns>
        public Player[] GetPlayerList()
        {
            if(Players == null)
            {
                return new Player[] { };
            }
            return Players;
        }

        /// <summary>
        /// Sends set name method request for server
        /// </summary>
        /// <param name="name">wanted name</param>
        public void SetName(string name)
        {
            _connection.InvokeAsync("SetName", name);
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
                .WithUrl("https://tankzsignalrserver.azurewebsites.net//TestHub")
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
                                           Console.WriteLine(value);
                                           Players = JsonConvert.DeserializeObject<Player[]>(value);
                                       });
                _connection.On<string>("GameStart",
                                       (value) =>
                                       {
                                           Console.WriteLine("Game Starting");
                                           SceneManager.Instance.LoadScene<GameplayScene>();
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
