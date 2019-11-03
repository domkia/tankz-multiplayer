using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class Crate : Sprite
    {
        public Projectile weapon;

        public Crate(Image image, Vector2 position, Projectile weapon, Vector2 size)
            : base (image, position, size)
        {
            this.weapon = weapon;
        }

        public override void Update(float deltaTime)
        {
            
        }

    }
}
