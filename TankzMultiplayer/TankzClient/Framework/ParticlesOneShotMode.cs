namespace TankzClient.Framework
{
    public class ParticlesOneShotMode : IParticleEmitMode
    {
        public bool Update(ParticleEmitter emitter, float deltaTime)
        {
            bool alive = false;
            for (int i = 0; i < emitter.particleCount; i++)
            {
                emitter.Simulate(emitter.particles[i], deltaTime);
                if (emitter.particles[i].IsAlive == false)
                    continue;
                else alive = true;
            }
            return alive;
        }
    }
}
