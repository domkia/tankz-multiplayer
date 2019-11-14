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
        public Player MyData { get; set; }
        public List<Player> Players { get; private set; }
        public List<Player> LobbyPlayers = new List<Player>();
        public Player Me => Players.FirstOrDefault(p => p.ConnectionId == _connection.ConnectionId);
        public bool IsMyTurn => CurrentTurn == _connection.ConnectionId;
        private bool connected = false;
        public bool IsConnected =>connected;
        public Player CurrentPlayer => GetPlayerById(CurrentTurn);
        public string CurrentTurn { get; private set; } 
        public int CurrentLobby { get; private set; }

        private static HubConnection _connection;

        // Events
        public event Action<List<Player>> OnTankConfigsReceived;
        public event EventHandler OnTurnStarted;
        public event EventHandler<string> RegisterErrorGot;
        public event EventHandler<string> RegisterSuccess;
        public event EventHandler<string> LoginErrorGot;
        public event EventHandler<string> LoginSuccess;
        public event EventHandler<string> LobbyErrorGot;
        public event EventHandler<MoveEventArtgs> PlayerMoved;
        public event EventHandler<RotateEventArgs> BarrelRotate;
        public event Action<int> OnCrateDestroyed;
        public event Action<Models.Crate> OnCrateSpawned;


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
            OnTurnStarted?.Invoke(this, e);
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
            _connection.On<string, int>("PlayerJoinedLobby", PlayerJoinedLobby);
            _connection.On<string, bool>("PlayerReadyStateChanged", PlayerReadyStateChanged);
            _connection.On<string>("Registered", Registered);
            _connection.On<string>("RegisterError", RegisterError);
            _connection.On<string>("LoginSuccess", LoggedIn);
            _connection.On<string>("LoginError", LoginError);
            _connection.On<int>("CrateDestroyed", CrateDestroyed);
            _connection.On<string>("CrateSpawned", CrateSpawned);
            _connection.On<string>("LobbyError", LobbyError);
            // Connect to the server
            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Player GetPlayerById(string playerId)
        {
            Player player = Players.FirstOrDefault(p => p.ConnectionId == playerId);
            if (player == null)
            {
                //return null;
                throw new Exception($"Player with id:{playerId} does not exist");
            }
            return player;
        }
    }
}
