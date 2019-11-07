using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TankzClient.Models
{
    public class Crate
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }
}
