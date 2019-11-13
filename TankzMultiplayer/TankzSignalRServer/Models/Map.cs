using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TankzSignalRServer.Models
{
    public class Map
    {
        int ID { get; set; }
        string name { get; set; }
        int max_players { get; set; }
    }
}
