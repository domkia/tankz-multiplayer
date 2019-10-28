using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TankzSignalRServer.Data;
using TankzSignalRServer.Models;
using Newtonsoft.Json;

namespace TankzSignalRServer.Hubs
{
    public class TestHub : Hub
    {
        private readonly TankzContext _context;
        private static HashSet<Player> LobbyPlayers = new HashSet<Player>();

        public TestHub(TankzContext context)
        {
           _context = context;
        }
        
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        [HubMethodName("ChangeReadyState")]
        public Task ChangeState()
        {
            var existingPerson2 = LobbyPlayers.SingleOrDefault(p => p.ConnectionId == Context.ConnectionId);
            existingPerson2.ReadyState = !existingPerson2.ReadyState;
            bool allTrue = LobbyPlayers.All(i => i.ReadyState == true);
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
            var existingPerson2 = LobbyPlayers.SingleOrDefault(p => p.ConnectionId == Context.ConnectionId);
            existingPerson2.Name = name;
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
            string json = JsonConvert.SerializeObject(LobbyPlayers.ToList());
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
            Player player = new Player { ConnectionId = Context.ConnectionId, Name = "", Icon = "", ReadyState = false};
            LobbyPlayers.Add(player);
            ConnectedPeople();
            return base.OnConnectedAsync();
        }

        //On disconnected task
        //Removes player form list and calls for connectedPeople method
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var existingPerson2 = LobbyPlayers.SingleOrDefault(p => p.ConnectionId == Context.ConnectionId);
            LobbyPlayers.Remove(existingPerson2);
            ConnectedPeople();
            return base.OnDisconnectedAsync(exception);
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
