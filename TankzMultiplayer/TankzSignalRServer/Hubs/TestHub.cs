using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TankzSignalRServer.Controllers;
using TankzSignalRServer.Data;
using TankzSignalRServer.Models;
using Newtonsoft.Json;

namespace TankzSignalRServer.Hubs
{
    public class TestHub : Hub
    {
        private readonly TankzContext _context;
        public TestHub(TankzContext context)
        {
           _context = context;
        }
        static HashSet<string> CurrentConnections = new HashSet<string>();
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        [HubMethodName("SendMessage")]
        public Task Message()
        {
            List<Player> players = _context.Players.ToList();
            string json = JsonConvert.SerializeObject(players);
            return Clients.All.SendAsync("ReceiveMessage", json);
        }
        [HubMethodName("GetConnected")]
        public Task ConnectedPeople()
        {
            string json = JsonConvert.SerializeObject(CurrentConnections.ToList());
            return Clients.All.SendAsync("Players", json);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Task OnConnectedAsync()
        {
            CurrentConnections.Add(Context.ConnectionId);
            ConnectedPeople();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            CurrentConnections.Remove(Context.ConnectionId);
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
