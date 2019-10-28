using System;
using System.Linq;
using TankzSignalRServer.Models;

namespace TankzSignalRServer.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TankzContext context)
        {
            context.Database.EnsureCreated();

            if (context.Players.Any())
            {
                return;
            }
            var Players = new Player[]
            {
                new Player{ Name = "Test1",Icon ="testIcon"},
                new Player{Name = "test2", Icon="testIcon2"}
            };
            foreach(Player player in Players)
            {
                context.Players.Add(player);
            }
            context.SaveChanges();
        }
    }
}
