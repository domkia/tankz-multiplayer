using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TankzSignalRServer.Data;
using TankzSignalRServer.Models;
using Newtonsoft.Json;
using TankzSignalRServer.Controllers;

namespace TankzSignalRServer.Hubs
{
    public class TestHub : Hub
    {
        private readonly TankzContext _context;
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
            return Clients.All.SendAsync("GameStart","");
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
            string json = JsonConvert.SerializeObject(_context.Players.ToList());
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
            Player player = new Player { ConnectionId = Context.ConnectionId, Name = "", Icon = "", ReadyState = false, Tank = null, TankState = null};
            _context.Players.AddAsync(player);
            _context.SaveChangesAsync();
            ConnectedPeople();
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        //On disconnected task
        //Removes player form list and calls for connectedPeople method
        public override Task OnDisconnectedAsync(Exception exception)
        {
            _context.Players.Remove(GetPlayerById(Context.ConnectionId));
            _context.SaveChanges();
            ConnectedPeople();
            Clients.Caller.SendAsync("Disconnected", Context.ConnectionId);
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
