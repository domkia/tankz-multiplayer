using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    /// <summary>
    /// Grenade class
    /// </summary>
    public class Grenade : Projectile
    {
        public float delayTime { get; protected set; }

        public Grenade(Image image, Vector2 position, float radius, float explosionRadius, float speed, float delayTime)
            : base(image, position, radius, explosionRadius, speed)
        {
            this.delayTime = delayTime;
        }

        private float timer = 0f;

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (timer > delayTime)
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
