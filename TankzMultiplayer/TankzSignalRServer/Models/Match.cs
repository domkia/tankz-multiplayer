using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TankzSignalRServer.Models
{
    public class Match
    {
        public int ID { get; set; }
        public DateTime start_datetime { get; set; }
        public ICollection<Player> Players { get; set; }

    }
}
