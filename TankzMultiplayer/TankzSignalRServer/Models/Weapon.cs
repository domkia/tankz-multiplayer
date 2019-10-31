using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TankzSignalRServer.Models
{
    public class Weapon
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Radius { get; set; }
        public float Explosion_Radius { get; set; }
    }
}
