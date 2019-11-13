using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TankzSignalRServer.Interfaces;

namespace TankzSignalRServer.Models
{
    public class Weapon : ICloneable<Weapon>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Radius { get; set; }
        public float Explosion_Radius { get; set; }
        public Rarity rarity { get; set; }

        public enum Rarity { Common, Uncommon, Rare, Epic, Legendary}
        public Weapon Clone()
        {
            Weapon weapon = new Weapon{
                ID = this.ID,
                Description = this.Description,
                Name = this.Name,
                Radius = this.Radius,
                Explosion_Radius = this.Explosion_Radius,
                Rarity = this. Rarity};
            return weapon;
        }
    }
}
