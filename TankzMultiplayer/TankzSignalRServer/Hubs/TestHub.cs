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
using System.Threading;

namespace TankzSignalRServer.Hubs
{
    public class TestHub : Hub
    {
        private readonly TankzContext _context;

        private static int currentTurn;
        private static int turnsToNextCrate;
        Random rand = new Random();
        Timer timer;
        //private ShootArgs args;
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
            int lobbyId = _context.Lobbies.Include(l => l.Players)
                .Where(p => p.Players
                .Where(c => c.ConnectionId == Context.ConnectionId).FirstOrDefault() != null)
                .FirstOrDefault().ID;
            string group = "Lobby" + lobbyId;
            Player player = GetPlayerById(Context.ConnectionId);
            int curr = _context.Lobbies.Include(l => l.Players).Where(p => p.Players.Where(pp => pp.Name == player.Name) != null).FirstOrDefault().CurrPlayers;
            curr--;
            _context.Lobbies.Include(l => l.Players).Where(p => p.Players.Where(pp => pp.Name == player.Name) != null).FirstOrDefault().CurrPlayers = curr;
            _context.Lobbies.Include(l => l.Players).Where(p => p.Players.Where(pp => pp.Name==player.Name) != null).FirstOrDefault().Players.Remove(player);
            //_context.Tanks.Remove(player.Tank);
            //_context.TankStates.Remove(player.TankState);      
            _context.Players.Where(p => p.Name == player.Name).FirstOrDefault().ReadyState = false;
            _context.SaveChanges();
            Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
            ConnectedPeople(lobbyId).Wait();
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
                _context.TankStates.Add(tankstate);
                Tank tank = new Tank { Chasis_id = 1, Color_id = 1, Trucks_id = 1, Turret_id = 1 };
                _context.Tanks.Add(tank);
                Player player = new Player { ConnectionId = "", Icon = "", Name = name, ReadyState = false, Tank = tank, TankState = tankstate };
                _context.Players.Add(player);
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
                    Player player = _context.Users.Include(p => p.Player).ThenInclude(t => t.Tank).Include(p => p.Player).ThenInclude(t => t.TankState).
                        Where(c => c.Username == name).First().Player;
                    

                    _context.Lobbies.Include(l => l.Players).Where(p => p.Players.Where(pp => pp.Name == player.Name) != null).FirstOrDefault().Players.Remove(player);
                    _context.Players.Where(c => c.Name == name).FirstOrDefault().ConnectionId = Context.ConnectionId;
                    _context.Players.Where(c => c.Name == name).FirstOrDefault().ReadyState = false;
                    TankState state = player.TankState;
                    state.Pos_X = rand.Next(100, 700);
                    _context.SaveChanges();
                    string playerData = JsonConvert.SerializeObject(_context.Players.Where(c => c.Name == name).FirstOrDefault());
                    return Clients.Caller.SendAsync("LoginSuccess", playerData);
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
        public Task ConnectedPeople(int lobbyId)
        {
            string lobbyName = "Lobby" + lobbyId;
            List<Player> connectedPlayers = _context.Lobbies.Include(p => p.Players).Where(l => l.ID == lobbyId).FirstOrDefault().Players.ToList();
            string json = JsonConvert.SerializeObject(connectedPlayers);
            return Clients.Group(lobbyName).SendAsync("Players", json);
        }
        [HubMethodName("ChangeReadyState")]
        public Task ChangeState(int lobbyId)
        {
            string lobbyName = "Lobby" + lobbyId;
            string clientId = Context.ConnectionId;
            var player = GetPlayerById(clientId);

            // Toggle player ready state
            player.ReadyState = !player.ReadyState;
            _context.SaveChanges();
            Clients.Group(lobbyName).SendAsync("PlayerReadyStateChanged", player.ConnectionId, player.ReadyState);

            // Check whether all players are ready
            // If so, start the game
            bool allTrue = _context.Lobbies.Include(c => c.Players).Where(l => l.ID == lobbyId).FirstOrDefault().Players.All(i => i.ReadyState == true);
            if (allTrue)
            {
                GameStart(lobbyId);
            }

            return Task.CompletedTask;

        }
        [HubMethodName("JoinLobby")]
        public Task JoinLobby(int lobbyId)
        {
            Lobby lobby = _context.Lobbies.Where(c => c.ID == lobbyId).FirstOrDefault();
            if (lobby.CurrPlayers < lobby.MaxPlayers)
            {
                string clientId = Context.ConnectionId;
                string lobbyName = "Lobby" + _context.Lobbies.Where(c => c.ID == lobbyId).FirstOrDefault().ID;
                Groups.AddToGroupAsync(clientId, lobbyName).Wait();
                Player newPlayer = _context.Players
                    .Include(t => t.Tank)
                    .Include(t => t.TankState)
                    .Where(c => c.ConnectionId == clientId)
                    .FirstOrDefault();
                // Notify all clients about new player
                string json = JsonConvert.SerializeObject(newPlayer);
                //_context.Lobbies.Include(l => l.Players).Where(c => c.ID == lobbyId).FirstOrDefault().Players.Add(newPlayer);
                int playerCount = _context.Lobbies.Include(l => l.Players).Where(c => c.ID == lobbyId).FirstOrDefault().CurrPlayers;
                playerCount++;
                _context.Lobbies.Where(c => c.ID == lobbyId).FirstOrDefault().CurrPlayers = playerCount;
                List<Player> player = _context.Lobbies.Include(l => l.Players).Where(c => c.ID == lobbyId).FirstOrDefault().Players.ToList();
                TankState state = newPlayer.TankState;
                state.Health = 100;
                player.Add(newPlayer);
                _context.Lobbies.Include(l => l.Players).Where(c => c.ID == lobbyId).FirstOrDefault().Players = player;
                _context.SaveChanges();

                // Notify caller about players already in the lobby
                ConnectedPeople(lobbyId).Wait();
                return Clients.Group(lobbyName).SendAsync("PlayerJoinedLobby", json, lobbyId);
            }
            else
            {
                return Clients.Caller.SendAsync("LobbyError", "LobbyIsFull");
            }
        }
        #endregion
        #region Turns
        public Task GameStart(int lobby)
        {
            string lobbyName = "Lobby" + lobby;
            Random rand = new Random();

            currentTurn = 0;
            turnsToNextCrate = rand.Next(1, 4);
            Clients.Group(lobbyName).SendAsync("ReceiveMessage", "Turns until next crate spawn: " + turnsToNextCrate);
            //string conn = currentPlayers[currentTurn].ConnectionId;
            //Weapon weapon = new Weapon { ID = 0, Name = "Grenade", Explosion_Radius = 10, Radius = 2 };
            //_context.Weapons.Add(weapon);
            //_context.SaveChanges();
            //Clients.All.SendAsync("Turn", conn);

            // Get players with references to tank configs and states
            List<Player> playersAndTanks = _context
                .Lobbies.Include(p => p.Players)
                .ThenInclude(p => p.Tank)
                .Include(p => p.Players)
                .ThenInclude(p => p.TankState).Where(l => l.ID == lobby).FirstOrDefault().Players
                .ToList();

            // Tell everyone to start the gameplay scene
            string json = JsonConvert.SerializeObject(playersAndTanks);
            Clients.Group(lobbyName).SendAsync("GameStart", json).Wait();

            // Tell who starts the turn
            Player startsFirst = SelectRandomPlayerToStart(lobby);
            return Clients.Group(lobbyName).SendAsync("Turn", startsFirst.ConnectionId);
        }

        private Player SelectRandomPlayerToStart(int lobbyId)
        {
            int index = rand.Next(0, _context.Lobbies.Include(l => l.Players).Where(l => l.ID == lobbyId).FirstOrDefault().Players.Count());
            Player startsFirst = _context.Lobbies.Include(l => l.Players).Where(l => l.ID == lobbyId).FirstOrDefault().Players.Skip(index).First();
            currentTurn = index;
            return startsFirst;
        }
        private Task EndTurn(int lobbyId)
        {
            if (currentTurn + 1 >= _context.Lobbies.Include(l => l.Players).Where(l => l.ID == lobbyId).FirstOrDefault().Players.Count())
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
            string lobbyName = "Lobby" + lobbyId;
            // Advance to the next turn
            Player currentPlayer = _context.Lobbies.Include(l => l.Players).ThenInclude(s => s.TankState).Where(l => l.ID == lobbyId).FirstOrDefault().Players.Skip(currentTurn).First();
            Clients.Group(lobbyName).SendAsync("Turn", currentPlayer.ConnectionId);

            turnsToNextCrate--;
            return Task.CompletedTask;
        }
        #endregion
        #region Ingame Methods
        [HubMethodName("Shoot")]
        public async Task Shoot(float angle, float power,int lobbyId)
        {
            CancellationTokenSource cancelSource = new CancellationTokenSource();
            Player currentPlayer = _context.Lobbies.Include(l => l.Players).ThenInclude(s => s.TankState).Where(l => l.ID == lobbyId).FirstOrDefault().Players.Skip(currentTurn).First();
            Vector2 startPos = new Vector2(currentPlayer.TankState.Pos_X, currentPlayer.TankState.Pos_Y - 20);
            Vector2 newPos = startPos;
            float angleDeg = (float)(angle * (Math.PI / 180.0));
            string lobbyName = "Lobby" + lobbyId;
            Clients.Group(lobbyName).SendAsync("ShootingStart", startPos.x, startPos.y).Wait();
            float time = 0f;
            while (newPos.y <= 300f)
                {
                    newPos = calculatePos(power * 50f, -9.8f, angleDeg, startPos, time);
                    Clients.Group(lobbyName).SendAsync("ProjectileMove", newPos.x, newPos.y).Wait();
                time += 0.1f;
                await Task.Delay(100);
            }
            //cancelSource.Cancel();
            Explode(newPos.x, newPos.y, lobbyName).Wait();
            Clients.Group(lobbyName).SendAsync("ReceiveMessage", "Done shooting").Wait();
            await EndTurn(lobbyId);
        }

        public Task Explode(float x, float y, string lobbyName)
        {
            List<Player> connectedPlayers = _context.Lobbies.Include(p => p.Players).ThenInclude(s => s.TankState).Where(l => l.ID == 1).FirstOrDefault().Players.ToList();
            //var ts = _context.TankStates.Where(m => ids.Contains(m.ID));
            bool death = false;
            foreach (Player player in connectedPlayers)
            {
                TankState tank = player.TankState;
                int t = circle((int)tank.Pos_X, (int)tank.Pos_Y + 5, (int)x, (int)y, 50, 30);
                if (t >= 0)
                {
                    tank.Health = tank.Health - 50;
                    if (tank.Health <= 0)
                        death = true;
                    _context.SaveChanges();
                    Clients.Client(player.ConnectionId).SendAsync("HealthUpdate", tank.Health).Wait();
                }
            }
            if (death)
            {


                Clients.Group(lobbyName).SendAsync("ProjectileExplode").Wait();
                return GameOver(lobbyName);
            }
            else
            {
                return Clients.Group(lobbyName).SendAsync("ProjectileExplode");
            }
        }
        private Task GameOver(string lobbyName)
        {
            string winner = "draw";
            List<Player> connectedPlayers = _context.Lobbies.Include(p => p.Players).ThenInclude(s => s.TankState).Where(l => l.ID == 1).FirstOrDefault().Players.ToList();
            foreach(Player player in connectedPlayers)
            {
                TankState state = player.TankState;
                if (state.Health > 0)
                    winner = player.Name;
            }
            return Clients.Group(lobbyName).SendAsync("GameEnd", winner);
        }

        [HubMethodName("SetPos")]
        public Task SetTankPos(float x, float y, int lobbyId)
        {
            string lobbyName = "Lobby" + lobbyId;
            return Clients.GroupExcept(lobbyName, Context.ConnectionId).SendAsync("PosChange", x, y, Context.ConnectionId);
        }
        [HubMethodName("SavePos")]
        public Task SaveTankPos(float x, float y, int lobbyId)
        {
            string lobbyName = "Lobby" + lobbyId;
            TankState state = _context.Players.Include(t => t.TankState).Where(c => c.ConnectionId == Context.ConnectionId).FirstOrDefault().TankState;
            state.Pos_X = x;
            state.Pos_Y = y;
            _context.SaveChanges();
            return Clients.Group(lobbyName).SendAsync("ReceiveMessage", state.Pos_X.ToString());
        }
        [HubMethodName("SetAngle")]
        public Task SetTankPos(float angle, int lobbyId)
        {
            string lobbyName = "Lobby" + lobbyId;
            return Clients.GroupExcept(lobbyName, Context.ConnectionId).SendAsync("AngleChange", angle, Context.ConnectionId);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns>If < 0 then it doesn't contact</returns>
        static int circle(int x1, int y1, int x2,
                      int y2, int r1, int r2)
        {
            int distSq = (x1 - x2) * (x1 - x2) +
                         (y1 - y2) * (y1 - y2);
            int radSumSq = (r1 + r2) * (r1 + r2);
            if (distSq == radSumSq)
                return 1;
            else if (distSq > radSumSq)
                return -1;
            else
                return 0;
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

        ////Testing method (not used in game)
        //[HubMethodName("SendMessage")]
        //public Task Message()
        //{
        //    List<Player> players = _context.Players.ToList();
        //    string json = JsonConvert.SerializeObject(players);
        //    return Clients.Group("Lobby").SendAsync("ReceiveMessage", json);
        //}
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
        //[HubMethodName("SetName")]
        //public Task SetName(string name)
        //{
        //    Player player = _context.Players.SingleOrDefault(p => p.ConnectionId == Context.ConnectionId);
        //    player.Name = name;
        //    _context.SaveChanges();
        //    return ConnectedPeople();
        //}
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
