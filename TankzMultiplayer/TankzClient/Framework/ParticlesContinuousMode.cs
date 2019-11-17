using System;

namespace TankzClient.Framework
{
    public class ParticlesContinuousMode : IParticleEmitMode
    {
        private Random random = new Random();
        private float timer = float.MaxValue;

        public bool Update(ParticleEmitter emitter, float deltaTime)
        {
            // Simulate any alive particles
            for (int i = 0; i < emitter.particleCount; i++)
            {
                if (emitter.particles[i].IsAlive)
                {
                    emitter.Simulate(emitter.particles[i], deltaTime);
                }
            }

            // 'Spawn' new particle
            timer += deltaTime;
            if (timer >= emitter.props.spawnRate)
            {
                for (int i = 0; i < emitter.particles.Length; i++)
                {
                    Particle p = emitter.particles[i];
                    if (p.IsAlive == false)
                    {
                        p.position = emitter.transform.position + emitter.props.startOffset * ((float)random.NextDouble() * 2f - 1f);
                        p.size = p.startSize;
                        p.speed = p.startSpeed;
                        p.timer = float.Epsilon;
                        break;
                    }
                }
                timer %= emitter.props.spawnRate;
            }

            return emitter.IsEmitting;
        }
    }
}
