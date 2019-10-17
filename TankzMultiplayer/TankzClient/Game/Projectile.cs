using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class Projectile : Sprite
    {
        const string projectileSpritePath = "../../res/projectile_1.png";
        int projectileRadius;
        int explosionRadius;
        int speed;
        public Projectile() : base(Image.FromFile(projectileSpritePath), new Vector2(200, 200), new Vector2(20,20))
        {
            
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
    }
}
