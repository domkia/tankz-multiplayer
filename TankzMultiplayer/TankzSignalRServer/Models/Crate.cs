using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TankzSignalRServer.Utilites;

namespace TankzSignalRServer.Models
{
    public class Crate
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Weapon Weapon { get; set; }
    }
}
