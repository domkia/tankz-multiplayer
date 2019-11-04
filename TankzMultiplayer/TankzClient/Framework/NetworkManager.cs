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
    public partial class NetworkManager
    {
        private Player[] Players;
        private static HubConnection _connection;
        public event EventHandler DataGained;
        public event EventHandler ConnectedToServer;
        public event EventHandler PlayerChanged;
        public event EventHandler<MoveEventArtgs> PlayerMoved;
        public event EventHandler<RotateEventArgs> BarrelRotate;
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
        /// Connection to server start
        /// </summary>
        public void Start()
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
        protected virtual void OnPlayerChanged(EventArgs e)
        {
            PlayerChanged?.Invoke(this, e);
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
        public async void Connect()
        {
            // Setup client callbacks
            _connection.On<string>("Connected", Connected);
            _connection.On<string>("Disconnected", Disconnected);
            _connection.On<string>("Players", PlayerListReceived);
            _connection.On<string>("GameStart", GameStarted);
            _connection.On<string>("ReceiveMessage", MessageReceived);
            _connection.On<string>("Turn", TurnStarted);
            _connection.On<float, float, string>("PosChange", TankPosChanged);
            _connection.On<float, string>("AngleChange", TankAngleChanged);

            // Connect to the server
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
