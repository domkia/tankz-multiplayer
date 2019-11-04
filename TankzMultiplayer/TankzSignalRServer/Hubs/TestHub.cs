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
        //private static int turnsToNextCrate;
        //PlayersController pct;
        public TestHub(TankzContext context)
        {
            _context = context;
            //pct = new PlayersController(context);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        [HubMethodName("ChangeReadyState")]
        public Task ChangeState()
        {
            string clientId = Context.ConnectionId;
            var player = GetPlayerById(clientId);

            // Toggle player ready state
            player.ReadyState = !player.ReadyState;
            _context.SaveChanges();
            Clients.All.SendAsync("PlayerReadyStateChanged", player.ConnectionId, player.ReadyState);

            // Check whether all players are ready
            // If so, start the game
            bool allTrue = _context.Players.All(i => i.ReadyState == true);
            if (allTrue)
            {
                GameStart();
            }

            return Task.CompletedTask;
        }

        public Task GameStart()
        {
            //Random rand = new Random();

            //currentTurn = 0;
            //turnsToNextCrate = rand.Next(1, 4);
            //Clients.All.SendAsync("ReceiveMessage", "Turns until next crate spawn: " + turnsToNextCrate);
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
            return Clients.All.SendAsync("TurnStarted", startsFirst.ConnectionId);
        }

        private Player SelectRandomPlayerToStart()
        {
            Random rand = new Random();
            int index = rand.Next(0, _context.Players.Local.Count);
            Player startsFirst = _context.Players.ElementAt(index);
            currentTurn = index;
            return startsFirst;
        }

        private Vector2 calculatePos(float speed, float gravity, float angle, Vector2 currentPos, float time)
        {
            float x = currentPos.x;
            float y = currentPos.y;
            x = (float)(x + speed * time * Math.Cos(angle));
            y = (float)(y - (speed * time * Math.Sin(angle)) - (0.5f * gravity * Math.Pow(time, 2)));
            return new Vector2(x, y);
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
            
            // Insert newly created player into database
            _context.Players.Add(newPlayer);
            _context.SaveChanges();

            // Notify caller about players already in the lobby
            ConnectedPeople();

            // Notify all clients about new player
            string json = JsonConvert.SerializeObject(newPlayer);
            return Clients.All.SendAsync("PlayerJoinedLobby", json);
        }

        private Task EndTurn()
        {
            if (currentTurn + 1 >= _context.Players.Local.Count)
                currentTurn = 0;
            else
                currentTurn++;

            //if (turnsToNextCrate <= 0)
            //{
            //    Crate crate = new Crate { ID = 0, Weapon = GetWeaponById(1), PositionX = rand.Next(100, 400), PositionY = 300 };
            //    crate.Name = "Crate of " + crate.Weapon.Name;
            //    _context.Crates.Add(crate);
            //    _context.SaveChanges();
            //    Clients.All.SendAsync("ReceiveMessage", "created Crate: " + crate.Name + ", ID: " + crate.ID + ", Weapon name: " + GetWeaponById(1).Name + ", Position: (" + crate.PositionX + "," + crate.PositionY + ")");
            //    turnsToNextCrate = rand.Next(1, 4);
            //    Clients.All.SendAsync("ReceiveMessage", "Turns until next crate: " + turnsToNextCrate);
            //}

            // Advance to the next turn
            Player currentPlayer = _context.Players.ElementAt(currentTurn);
            Clients.All.SendAsync("Turn", currentPlayer.ConnectionId);

            //turnsToNextCrate--;
            return Task.CompletedTask;
        }

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
            Clients.All.SendAsync("ReceiveMessage", "Done shooting").Wait();
            return EndTurn();
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
        [HubMethodName("SetPos")]
        public Task SetTankPos(float x, float y)
        {
            return Clients.Others.SendAsync("PosChange", x, y, Context.ConnectionId);
        }
        [HubMethodName("SetAngle")]
        public Task SetTankPos(float angle)
        {
            return Clients.Others.SendAsync("AngleChange", angle, Context.ConnectionId);
        }

        //Testing method (not used in game)
        [HubMethodName("SendMessage")]
        public Task Message()
        {
            List<Player> players = _context.Players.ToList();
            string json = JsonConvert.SerializeObject(players);
            return Clients.All.SendAsync("ReceiveMessage", json);
        }

        //Gets connected people and returns to all clients that are listening
        [HubMethodName("GetConnected")]
        public Task ConnectedPeople()
        {
            List<Player> connectedPlayers = _context.Players.ToList();
            string json = JsonConvert.SerializeObject(connectedPlayers);
            return Clients.All.SendAsync("Players", json);  
        }

        //Gets crate if it was created
        [HubMethodName("GetCrate")]
        public Task Crate()
        {
            return Clients.All.SendAsync("Crate", "");
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        //On client connect task
        //Add connection to list and calls for connectedPeople method
        public override Task OnConnectedAsync()
        {
            //Tank tank = new Tank { Color_id = 0, Chasis_id = 0, Trucks_id = 0, Turret_id = 0 };
            //Player player = new Player { ConnectionId = Context.ConnectionId, Name = "", Icon = "", ReadyState = false, Tank = tank, TankState =state};
            //_context.Players.Add(player);
            //_context.SaveChanges();
            //Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            //ConnectedPeople();

            return base.OnConnectedAsync();
        }

        //On disconnected task
        //Removes player and related elements from database and calls for connectedPeople method
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Player player = GetPlayerById(Context.ConnectionId);
            _context.Players.Remove(player);
            //_context.SaveChanges();
            //_context.Tanks.Remove(player.Tank);
            //_context.SaveChanges();
            //_context.TankStates.Remove(player.TankState);            
            //_context.SaveChanges();
            //ConnectedPeople();
            Clients.All.SendAsync("Disconnected", Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public Player GetPlayerById(string ConnId)
        {
            return _context.Players.FirstOrDefault(c => c.ConnectionId == ConnId);
        }

        public Weapon GetWeaponById(int ID)
        {
            return _context.Weapons.FirstOrDefault(i => i.ID == ID);
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

        public int GetCrateCount()
        {
            return _context.Crates.Count();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
