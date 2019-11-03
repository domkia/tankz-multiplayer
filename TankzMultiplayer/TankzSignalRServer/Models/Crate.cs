using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TankzSignalRServer.Interfaces;
using TankzSignalRServer.Utilites;

namespace TankzSignalRServer.Models
{
    public class Crate : ICloneable<Crate>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Weapon Weapon { get; set; }

        public Crate Clone()
        {
            Crate crate = new Crate {
                ID = this.ID, 
                Name = this.Name, 
                PositionX = this.PositionX, 
                PositionY = this.PositionY, 
                Weapon = this.Weapon
            };
            return crate;
        }
    }
}
