using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TankzSignalRServer.Data;
using TankzSignalRServer.Models;
using Newtonsoft.Json;
using TankzSignalRServer.Controllers;
using Microsoft.EntityFrameworkCore;
using TankzSignalRServer.Utilites;

namespace TankzSignalRServer.Hubs
{
    public class TestHub : Hub
    {
        private readonly TankzContext _context;

        private static int currentTurn;
        private static int turnsToNextCrate;
        Random rand = new Random();
        //PlayersController pct;
        public TestHub(TankzContext context)
        {
            _context = context;
            //pct = new PlayersController(context);
        }
        #region Connection
        //On client connect task
        //Add connection to list and calls for connectedPeople method
        public override Task OnConnectedAsync()
        {
            //Tank tank = new Tank { Color_id = 0, Chasis_id = 0, Trucks_id = 0, Turret_id = 0 };
            //Player player = new Player { ConnectionId = Context.ConnectionId, Name = "", Icon = "", ReadyState = false, Tank = tank, TankState =state};
            //_context.Players.Add(player);
            //_context.SaveChanges();

            Clients.Caller.SendAsync("Connected", Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        //On disconnected task
        //Removes player and related elements from database and calls for connectedPeople method
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Player player = GetPlayerById(Context.ConnectionId);
            _context.Players.Remove(player);
            //_context.Tanks.Remove(player.Tank);
            //_context.TankStates.Remove(player.TankState);            
            _context.SaveChanges();
            Groups.RemoveFromGroupAsync(Context.ConnectionId, "Lobby");
            Clients.All.SendAsync("Disconnected", Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
        #endregion
        #region Authorisation
        [HubMethodName("Register")]
        public Task Register(string name, string password)
        {
            if (name == "" || password == "")
            {
                return Clients.Caller.SendAsync("RegisterError", "Fields can't be empty");
            }
           
            if (_context.Users.Where(c => c.Username == name).Count() == 0)
            {
                TankState tankstate = GetRandomTankState();
                Tank tank = new Tank { Chasis_id = 1, Color_id = 1, Trucks_id = 1, Turret_id = 1 };
                Player player = new Player { ConnectionId = "", Icon = "", Name = name, ReadyState = false, Tank = tank, TankState = tankstate };
                User user = new User { Username = name, Password = password, Player = player };
                _context.Users.Add(user);
                _context.SaveChanges();
                return Clients.Caller.SendAsync("Registered", "Successful registration");
            }
            else
            {
                return Clients.Caller.SendAsync("RegisterError", "User already exists");
            }
        }
        [HubMethodName("Login")]
        public Task Login(string name, string password)
        {
            if (name == "" || password == "")
            {
                return Clients.Caller.SendAsync("LoginError", "Fields can't be empty");
            }
            else if (_context.Users.Where(c => c.Username == name).Count() == 0)
            {
                return Clients.Caller.SendAsync("LoginError", "User Does not exist");
            }
            else
            {
                if (_context.Users.Where(c => c.Username == name).First().Password == password)
                {
                    //After loggin in sets id as current connId
                    _context.Players.Where(c => c.Name == name).FirstOrDefault().ConnectionId = Context.ConnectionId;
                    return Clients.Caller.SendAsync("LoginSuccess", "User successfully logged in");
                }
                else
                {
                    return Clients.Caller.SendAsync("LoginError", "Wrong password");
                }
            }
        }
        #endregion
        #region Lobby Methods
        //Gets connected people and returns to all clients that are listening
        [HubMethodName("GetConnected")]
        public Task ConnectedPeople()
        {
            List<Player> connectedPlayers = _context.Players.ToList();
            string json = JsonConvert.SerializeObject(connectedPlayers);
            return Clients.Group("Lobby").SendAsync("Players", json);
        }
        [HubMethodName("ChangeReadyState")]
        public Task ChangeState()
        {
            string clientId = Context.ConnectionId;
            var player = GetPlayerById(clientId);

            // Toggle player ready state
            player.ReadyState = !player.ReadyState;
            _context.SaveChanges();
            Clients.Group("Lobby").SendAsync("PlayerReadyStateChanged", player.ConnectionId, player.ReadyState);

            // Check whether all players are ready
            // If so, start the game
            bool allTrue = _context.Players.All(i => i.ReadyState == true);
            if (allTrue)
            {
                GameStart();
            }

            return Task.CompletedTask;

        }
        [HubMethodName("JoinLobby")]
        public Task JoinLobby(string name, string iconUrl)
        {
            string clientId = Context.ConnectionId;

            // Create new player with a given name
            Player newPlayer = new Player()
            {
                ConnectionId = clientId,
                Name = name,
                Icon = iconUrl,
                ReadyState = false,
                TankState = GetRandomTankState(),
                Tank = new Tank()
            };
            Groups.AddToGroupAsync(clientId, "Lobby").Wait();
            // Insert newly created player into database
            _context.Players.Add(newPlayer);
            _context.SaveChanges();
            // Notify caller about players already in the lobby
            ConnectedPeople().Wait();



            // Notify all clients about new player
            string json = JsonConvert.SerializeObject(newPlayer);

            return Clients.Group("Lobby").SendAsync("PlayerJoinedLobby", json);
        }
        #endregion
        #region Turns
        public Task GameStart()
        {
            Random rand = new Random();

            currentTurn = 0;
            turnsToNextCrate = rand.Next(1, 4);
            Clients.All.SendAsync("ReceiveMessage", "Turns until next crate spawn: " + turnsToNextCrate);
            //string conn = currentPlayers[currentTurn].ConnectionId;
            //Weapon weapon = new Weapon { ID = 0, Name = "Grenade", Explosion_Radius = 10, Radius = 2 };
            //_context.Weapons.Add(weapon);
            //_context.SaveChanges();
            //Clients.All.SendAsync("Turn", conn);

            // Get players with references to tank configs and states
            List<Player> playersAndTanks = _context
                .Players
                .Include(p => p.Tank)
                .Include(p => p.TankState)
                .ToList();

            // Tell everyone to start the gameplay scene
            string json = JsonConvert.SerializeObject(playersAndTanks);
            Clients.All.SendAsync("GameStart", json).Wait();

            // Tell who starts the turn
            Player startsFirst = SelectRandomPlayerToStart();
            return Clients.Group("Lobby").SendAsync("Turn", startsFirst.ConnectionId);
        }

        private Player SelectRandomPlayerToStart()
        {
            int index = rand.Next(0, _context.Players.Count());
            Player startsFirst = _context.Players.Skip(index).First();
            currentTurn = index;
            return startsFirst;
        }
        private Task EndTurn()
        {
            if (currentTurn + 1 >= _context.Players.Count())
                currentTurn = 0;
            else
                currentTurn++;

            if (turnsToNextCrate <= 0)
            {
                Crate crate = new Crate { ID = 0, Weapon = GetWeaponById(1), PositionX = rand.Next(100, 400), PositionY = 300 };
                crate.Name = "Crate of " + crate.Weapon.Name;
                _context.Crates.Add(crate);
                _context.SaveChanges();

                Clients.All.SendAsync("ReceiveMessage", "created Crate: " + crate.Name + ", ID: " + crate.ID + ", Weapon name: " + GetWeaponById(1).Name + ", Position: (" + crate.PositionX + "," + crate.PositionY + ")");
                turnsToNextCrate = rand.Next(1, 4);
                Clients.All.SendAsync("ReceiveMessage", "Turns until next crate: " + turnsToNextCrate);
            }

            // Advance to the next turn
            Player currentPlayer = _context.Players.Skip(currentTurn).First();
            Clients.Group("Lobby").SendAsync("Turn", currentPlayer.ConnectionId);

            turnsToNextCrate--;
            return Task.CompletedTask;
        }
        #endregion
        #region Ingame Methods
        [HubMethodName("Shoot")]
        public Task Shoot(float power, float angle)
        {
            //Vector2 startPos = new Vector2(currentPlayers[currentTurn].TankState.Pos_X, currentPlayers[currentTurn].TankState.Pos_Y);
            //Vector2 newPos = new Vector2(0f, 0f);
            //float time = 0;
            //float angleDeg = (float)(angle * (Math.PI / 180.0));
            //while (newPos.y <= 300f)
            //{
            //    newPos = calculatePos(power, -9.8f, angleDeg, startPos, time);
            //    time++;
            //    Clients.All.SendAsync("ReceiveMessage", newPos.x + " " + newPos.y);
            //}
            Clients.Group("Lobby").SendAsync("ReceiveMessage", "Done shooting").Wait();
            return EndTurn();
        }
        [HubMethodName("SetPos")]
        public Task SetTankPos(float x, float y)
        {
            return Clients.GroupExcept("Lobby", Context.ConnectionId).SendAsync("PosChange", x, y, Context.ConnectionId);
        }
        [HubMethodName("SetAngle")]
        public Task SetTankPos(float angle)
        {
            return Clients.GroupExcept("Lobby", Context.ConnectionId).SendAsync("AngleChange", angle, Context.ConnectionId);
        }
        #endregion
        #region Crate Methods
        //Gets crate if it was created
        [HubMethodName("GetCrate")]
        public Task Crate()
        {
            return Clients.All.SendAsync("Crate", "");
        }
        public int GetCrateCount()
        {
            return _context.Crates.Count();
        }
        #endregion
        #region Utilities
        [HubMethodName("Cleanup")]
        public void ClearDatabase()
        {
            _context.Players.RemoveRange(_context.Players);
            _context.TankStates.RemoveRange(_context.TankStates);
            _context.Tanks.RemoveRange(_context.Tanks);
            _context.SaveChanges();
        }
        private Vector2 calculatePos(float speed, float gravity, float angle, Vector2 currentPos, float time)
        {
            float x = currentPos.x;
            float y = currentPos.y;
            x = (float)(x + speed * time * Math.Cos(angle));
            y = (float)(y - (speed * time * Math.Sin(angle)) - (0.5f * gravity * Math.Pow(time, 2)));
            return new Vector2(x, y);
        }
        #endregion
        #region DB getters
        public Weapon GetWeaponById(int ID)
        {
            return _context.Weapons.FirstOrDefault(i => i.ID == ID);
        }
        public Player GetPlayerById(string ConnId)
        {
            return _context.Players.FirstOrDefault(c => c.ConnectionId == ConnId);
        }
        #endregion

        //Testing method (not used in game)
        [HubMethodName("SendMessage")]
        public Task Message()
        {
            List<Player> players = _context.Players.ToList();
            string json = JsonConvert.SerializeObject(players);
            return Clients.Group("Lobby").SendAsync("ReceiveMessage", json);
        }
        [HubMethodName("GetMyId")]
        public Task GetMyId()
        {
            return Clients.Caller.SendAsync("ConnectionId", Context.ConnectionId);
        }
        
        /// <summary>
        /// Set name for player when joining from lobby
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Gražina pakeistą žaidėjų sąrašą</returns>
        [HubMethodName("SetName")]
        public Task SetName(string name)
        {
            Player player = _context.Players.SingleOrDefault(p => p.ConnectionId == Context.ConnectionId);
            player.Name = name;
            _context.SaveChanges();
            return ConnectedPeople();
        }
        [HubMethodName("GetPlayer")]
        public Task getPlayer(string connId)
        {
            string playerData = JsonConvert.SerializeObject(GetPlayerById(connId));
            return Clients.All.SendAsync("GotPlayer", playerData);
        }
        private TankState GetRandomTankState()
        {
            Random rand = new Random();
            TankState state = new TankState
            {
                Health = 100,
                Fuel = 100,
                Pos_X = rand.Next(100,500),
                Pos_Y = 300/*rand.Next(300,400)*/ ,
                Angle = 90f,
                Power = 0f
            };
            return state;
        }
        #region SignalR overrides
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion
    }
}
