using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class ParticlesScene : Scene
    {
        private ParticleEmitter explosion;
        private ParticleEmitter explosionClone;
        private ParticleEmitter shield;
        private ParticleEmitter health;

        public override void Load()
        {
            explosion = new ParticleFactory().Create("explosion") as ParticleEmitter;
            int hash1 = explosion.GetHashCode();

            explosionClone = new ParticleFactory().Create("explosion") as ParticleEmitter;
            int hash2 = explosionClone.GetHashCode();

            shield = new ParticleFactory().Create("shield") as ParticleEmitter;
            health = new ParticleFactory().Create("health") as ParticleEmitter;

            shield.transform.SetPosition(new Vector2(200, 300));
            shield.Emit();
            health.transform.SetPosition(new Vector2(400, 300));
            health.Emit();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (Input.MouseButtonDown)
            {
                Vector2 mousePosition = Input.MousePosition;

                explosion.transform.SetPosition(mousePosition);
                explosion.Emit();

                explosionClone.transform.SetPosition(mousePosition + Vector2.left * 300f);
                explosionClone.Emit();
            }
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.Gray);
            base.Render(context);
        }
    }
}
