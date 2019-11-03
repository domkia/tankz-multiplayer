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
        public event EventHandler DataGained;
        public event EventHandler ConnectedToServer;
        private string currentTurn = "";
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

        public void EndTurn(float angle, float power)
        {

            _connection.InvokeAsync("EndTurn", angle, power);
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
                //.WithUrl("https://localhost:44311/TestHub")
                .WithUrl("https://tankzsignalrserver.azurewebsites.net/TestHub")
                .Build();

            _connection.Closed += async (error) =>
            {   
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };
        }
        protected virtual void OnPlayersGot(EventArgs e)
        {
            DataGained?.Invoke(this, e);
        }
        protected virtual void OnPlayerConnected(EventArgs e)
        {
            ConnectedToServer?.Invoke(this, e);
        }
        public string getCurrentPlayer()
        {
            return currentTurn;
        }
        public string myConnId()
        {
            return _connection.ConnectionId;
        }
        /// <summary>
        /// Async method with listeners
        /// </summary>
        public async void connect()
        {
            _connection.On<string>("Connected",
                (connectionid) =>
                {
                    Console.WriteLine(connectionid);
                });
            _connection.On<string>("Disconnected",
                (value) =>
                {
                    Console.WriteLine("Player " + value + " disconnected");
                });
            //Gets connected player list
            _connection.On<string>("Players",
                (value) =>
                {
                    Console.WriteLine(value);
                    Players = JsonConvert.DeserializeObject<Player[]>(value);
                    OnPlayersGot(EventArgs.Empty);
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
            _connection.On<string>("Turn",
                (value) =>
                {
                    if(value == _connection.ConnectionId)
                    {
                        currentTurn = "YOU";
                    }
                    else
                    currentTurn = value;
                });
                try
                {
                    await _connection.StartAsync();
                    OnPlayerConnected(EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            
        }
    }
}
