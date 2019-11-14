using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TankzSignalRServer.Models
{
    public class Lobby
    {
        public int ID { get; set; }
        public LobbyState state { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public string LobbyName { get; set; }
        public int MaxPlayers { get; set; }
        public int CurrPlayers { get; set; }
        public string Password { get; set; }
        public Map map { get; set; }
    }
    public enum LobbyState { waiting, full, ingame, finished };
}
