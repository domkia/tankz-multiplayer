using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TankzSignalRServer.Models
{
    public class Map
    {
        public int ID { get; set; }
        public string name { get; set; }
        public int max_players { get; set; }
    }
}
