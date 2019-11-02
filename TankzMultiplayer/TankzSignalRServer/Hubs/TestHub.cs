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
        private static List<Player> currentPlayers;
        private static int currentTurn;
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
            var existingPerson2 = _context.Players.SingleOrDefault(p => p.ConnectionId == Context.ConnectionId);
            existingPerson2.ReadyState = !existingPerson2.ReadyState;
            _context.SaveChanges();
            bool allTrue = _context.Players.All(i => i.ReadyState == true);
            if (allTrue)
            {
                gameStart();
            }
            return ConnectedPeople();
        }
        public Task gameStart()
        {
            Clients.All.SendAsync("GameStart","");
            currentTurn = 0;
            string conn = currentPlayers[currentTurn].ConnectionId;
            return Clients.All.SendAsync("Turn", conn);
        }
        private Vector2 calculatePos(float speed, float gravity, float angle, Vector2 currentPos, float time)
        {
            float x = currentPos.x;
            float y = currentPos.y;
            x = (float)(x + speed * time * Math.Cos(angle));
            y = (float)(y - (speed * time * Math.Sin(angle)) - (0.5f * gravity * Math.Pow(time, 2)));
            return new Vector2(x, y);
        }
        [HubMethodName("EndTurn")]
        public Task Turn(float angle, float power)
        {
            if (currentTurn + 1 >= currentPlayers.Count)
                currentTurn = 0;
            else
                currentTurn++;
            Clients.All.SendAsync("Turn", currentPlayers[currentTurn].ConnectionId);
            return Shoot(power, angle);
        }
        public Task Shoot(float power, float angle)
        {
            Vector2 startPos = new Vector2(currentPlayers[currentTurn].TankState.Pos_X, currentPlayers[currentTurn].TankState.Pos_Y);
            Vector2 newPos = new Vector2(0f, 0f);
            float time = 0;
            float angleDeg = (float)(angle * (Math.PI / 180.0));
            while (newPos.y <= 300f)
            {
                newPos = calculatePos(power, -9.8f, angleDeg, startPos, time);
                time++;
                Clients.All.SendAsync("ReceiveMessage", newPos.x + " " +newPos.y);
            }
            return Clients.All.SendAsync("ReceiveMessage", "Done shooting");
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
            currentPlayers = _context.Players.Include(p => p.Tank).Include(s => s.TankState).ToList();
            string json = JsonConvert.SerializeObject(currentPlayers);
            return Clients.All.SendAsync("Players", json);  
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        //On client connect task
        //Add connection to list and calls for connectedPeople method
        public override Task OnConnectedAsync()
        {
            Random rand = new Random();
            Tank tank = new Tank { Color_id = 0, Chasis_id = 0, Trucks_id = 0, Turret_id = 0 };
            TankState state = new TankState { Health = 100, Fuel = 100, Pos_X = rand.Next(100,500), Pos_Y = 300/*rand.Next(300,400)*/ };
            Player player = new Player { ConnectionId = Context.ConnectionId, Name = "", Icon = "", ReadyState = false, Tank = tank, TankState =state};
            _context.Players.Add(player);
            _context.SaveChanges();
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            ConnectedPeople();
            return base.OnConnectedAsync();
        }

        //On disconnected task
        //Removes player and related elements from database and calls for connectedPeople method
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Player player = GetPlayerById(Context.ConnectionId);
            _context.Players.Remove(player);
            _context.SaveChanges();
            _context.Tanks.Remove(player.Tank);
            _context.SaveChanges();
            _context.TankStates.Remove(player.TankState);            
            _context.SaveChanges();
            ConnectedPeople();
            Clients.All.SendAsync("Disconnected", Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
        public Player GetPlayerById(string ConnId)
        {
            return _context.Players.FirstOrDefault(c => c.ConnectionId == ConnId);
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
