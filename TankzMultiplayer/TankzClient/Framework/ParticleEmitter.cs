using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TankzClient.Framework
{
    /// <summary>
    /// Base particle emitter renders sprites
    /// </summary>
    public class ParticleEmitter : Entity, IRenderable, ICloneable<ParticleEmitter>
    {
        protected readonly float gravityMultiplier = 1.5f;
        protected Particle[] particles;
        protected int particleCount;
        protected ParticleEmitMode mode;
        protected Sprite sprite; //galbut reikia irgi klonuoti
        protected ParticleProperties props;
        protected bool useGravity;

        public bool IsEmitting { get; set; }

        public ParticleEmitter(int particleCount, Sprite sprite, ParticleProperties props, bool useGravity = false, ParticleEmitMode mode = ParticleEmitMode.Continuous, Entity parent = null) 
            : base(parent)
        {
            this.particleCount = particleCount;
            this.particles = new Particle[particleCount];
            this.sprite = sprite;
            this.mode = mode;
            this.props = props;
            this.useGravity = useGravity;
        }
        public Sprite getSprite()
        {
            return sprite;
        }

        public void Emit()
        {
            Init();
            IsEmitting = true;
        }

        public override void Update(float deltaTime)
        {
            if (IsEmitting == false)
                return;
            switch (mode)
            {
                case ParticleEmitMode.Continuous:
                    Continuous(deltaTime);
                    break;
                case ParticleEmitMode.OneShot:
                    if (OneShot(deltaTime) == false)
                        IsEmitting = false;
                    break;
            }
        }

        public void Simulate(Particle p, float deltaTime)
        {
            float progress = p.timer / p.lifetime;
            p.size = p.startSize + p.sizeGrow * progress * p.lifetime;
            if (useGravity)
                p.direction += Vector2.up * gravityMultiplier * deltaTime;
            p.speed *= p.speedDamping;
            p.position += p.direction * p.speed * deltaTime;
            p.timer += deltaTime;
        }

        private Random random = new Random();

        public void Init()
        {
            for (int i = 0; i < particleCount; i++)
            {
                float rad = Utils.Lerp(props.startAngle.min, props.startAngle.max, (float)random.NextDouble()) / 180f * (float)Math.PI;
                particles[i] = new Particle
                (
                    Utils.Lerp(props.startSize.min, props.startSize.max, (float)random.NextDouble()),
                    Utils.Lerp(props.startSpeed.min, props.startSpeed.max, (float)random.NextDouble())
                )
                {
                    position = transform.position + props.startOffset * ((float)random.NextDouble() * 2f - 1f),
                    direction = new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad)),
                    lifetime = Utils.Lerp(props.startLifetime.min, props.startLifetime.max, (float)random.NextDouble()),
                    sizeGrow = Utils.Lerp(props.sizeGrow.min, props.sizeGrow.max, (float)random.NextDouble()),
                    speedDamping = props.speedDamping
                };
            }
        }

        private bool OneShot(float deltaTime)
        {
            bool alive = false;
            for (int i = 0; i < particleCount; i++)
            {
                Simulate(particles[i], deltaTime);
                if (particles[i].IsAlive == false)
                    continue;
                else alive = true;
            }
            return alive;
        }

        float timer = float.MaxValue;
        private void Continuous(float deltaTime)
        {
            // Simulate any alive particles
            for (int i = 0; i < particleCount; i++)
            {
                if (particles[i].IsAlive)
                {
                    Simulate(particles[i], deltaTime);
                }
            }

            // 'Spawn' new particle
            timer += deltaTime;
            if (timer >= props.spawnRate)
            {
                for (int i = 0; i < particleCount; i++)
                {
                    if (particles[i].IsAlive == false)
                    {
                        particles[i].position = transform.position + props.startOffset * ((float)random.NextDouble() * 2f - 1f);
                        particles[i].size = particles[i].startSize;
                        particles[i].speed = particles[i].startSpeed;
                        particles[i].timer = float.Epsilon;
                        break;
                    }
                }
                timer %= props.spawnRate;
            }
        }

        public int SortingLayer => 10;
        public Matrix OrientationMatrix => transform.OrientationMatrix;

        public bool IsVisible { get => sprite.IsVisible; set => sprite.IsVisible = value; }

        public void Render(Graphics context)
        {
            if (IsEmitting == false)
                return;
            for(int i = 0; i < particleCount; i++)
            {
                if (particles[i].IsAlive)
                {
                    RenderParticleGraphics(particles[i], context);
                }
            }
        }

        public virtual void RenderParticleGraphics(Particle particle, Graphics context)
        {
            sprite.transform.SetPosition(particle.position);
            sprite.transform.SetSize(Vector2.one * particle.size);
            sprite.Render(context);
        }

        public ParticleEmitter Clone()
        {
            ParticleEmitter emitter = new ParticleEmitter(
                particleCount,
                sprite, 
                props.Clone(),
                useGravity,
                mode
                );
            return emitter;
        }
    }
}
