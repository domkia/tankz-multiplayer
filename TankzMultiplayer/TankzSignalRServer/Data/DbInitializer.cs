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
            context.SaveChanges();
        }
    }
}
