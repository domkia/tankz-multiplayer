using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TankzSignalRServer.Models
{
    public class Tank
    {
        public int ID { get; set; }
        public int Chasis_id { get; set; }
        public int Color_id { get; set; }
        public int Turret_id { get; set; }
        public int Trucks_id { get; set; }
    }
}
