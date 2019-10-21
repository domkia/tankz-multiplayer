using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public abstract class Projectile : Sprite
    {
        public float radius { get; protected set; }
        public float explosionRadius { get; protected set; }
        public float speed { get; protected set; }

        public Projectile(Image image, Vector2 position, float radius, float explosionRadius, float speed) 
            : base(image, position, new Vector2(radius / 2f, radius / 2f))
        {
            this.radius = radius;
            this.explosionRadius = explosionRadius;
            this.speed = speed;
        }

        public abstract void Explode();
    }
}
