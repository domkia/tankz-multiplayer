using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TankzSignalRServer.Models
{
    public class MatchPlayer
    {
        public int ID { get; set; }
        public Match Match { get; set; }
        public Player Player { get; set; }
        public Tank Tank { get; set; }
        public bool ReadyState { get; set; }

    }
}
