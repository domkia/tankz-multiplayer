using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class ParticlesScene : Scene
    {
        private ParticleEmitter explosion;
        private ParticleEmitter shield;
        private ParticleEmitter health;

        public override void Load()
        {
            explosion = new ParticleFactory().Create("explosion") as ParticleEmitter;
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
                explosion.transform.SetPosition(Input.MousePosition);
                explosion.Emit();
            }
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.Gray);
            base.Render(context);
        }
    }
}
