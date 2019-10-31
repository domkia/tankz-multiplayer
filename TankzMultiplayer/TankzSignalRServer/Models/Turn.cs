using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TankzSignalRServer.Models
{
    public class Turn
    {
        public int ID { get; set; }
        public Player Player { get; set; }
        public float Angle { get; set; }
        public float Power { get; set; }
        public Weapon Weapon { get; set; }
    }
}
