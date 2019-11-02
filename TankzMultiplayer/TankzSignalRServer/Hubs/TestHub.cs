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
        [HubMethodName("EndTurn")]
        public Task Turn()
        {
            if (currentTurn + 1 >= currentPlayers.Count)
                currentTurn = 0;
            else
                currentTurn++;
            return Clients.All.SendAsync("Turn", currentPlayers[currentTurn].ConnectionId);
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
