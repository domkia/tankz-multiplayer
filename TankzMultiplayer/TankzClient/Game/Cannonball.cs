using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class Cannonball : Projectile
    {
        public Cannonball(Image image, Vector2 position, float radius, float explosionRadius, float speed)
            : base(image, position, radius, explosionRadius, speed)
        {
            
        }

        private float timer = 0f;

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            //if (collided == true)
            {
                Explode();
            }
            timer += deltaTime;
        }

        public override void Explode()
        {
            //TODO: BOOM goes here
        }
    }
}
