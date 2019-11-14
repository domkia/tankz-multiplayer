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
            Map avg = new Map { name = "Small", max_players = 4 };
            if (context.Maps.Count() == 0)
            {
                context.Maps.Add(new Map { name = "Tiny", max_players = 2 });
                context.Maps.Add(avg);
                context.Maps.Add(new Map { name = "Average", max_players = 6 });
            }
            if (context.Weapons.Count() == 0)
            {
                context.Weapons.Add(new Weapon { Name = "Cannonball", Description = "Simple cannonball", Radius = 1f, Explosion_Radius = 2f, rarity = Weapon.Rarity.Common });
                context.Weapons.Add(new Weapon { Name = "Big Cannonball", Description = "Bigger cannonball", Radius = 2f, Explosion_Radius = 2f, rarity = Weapon.Rarity.Uncommon });
                context.Weapons.Add(new Weapon { Name = "Big Boom", Description = "Projectile with big explosion", Radius = 2f, Explosion_Radius = 3f, rarity = Weapon.Rarity.Rare });
                context.Weapons.Add(new Weapon { Name = "Nuke", Description = "Most dangerous explosive", Radius = 2f, Explosion_Radius = 6f, rarity = Weapon.Rarity.Legendary });
                context.Lobbies.Add(new Lobby { LobbyName = "TestLobby", MaxPlayers = 4, CurrPlayers = 0, Password = "", map = avg, state = LobbyState.waiting });
            }
            context.SaveChanges();
        }
    }
}
