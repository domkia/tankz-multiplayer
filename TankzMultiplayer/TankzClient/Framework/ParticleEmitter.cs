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
        public Particle[] particles { get; protected set; }
        public int particleCount { get; protected set; }
        public IParticleEmitMode emitMode { get; protected set; }
        public Sprite sprite { get; protected set; }
        public ParticleProperties props { get; protected set; }
        public bool useGravity { get; protected set; }

        public bool IsEmitting { get; set; }

        public ParticleEmitter(int particleCount, Sprite sprite, ParticleProperties props, IParticleEmitMode emitMode, bool useGravity = false, Entity parent = null) 
            : base(parent)
        {
            this.particleCount = particleCount;
            this.particles = new Particle[particleCount];
            this.sprite = sprite;
            this.emitMode = emitMode;
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
            {
                return;
            }
            if (emitMode == null)
            {
                throw new Exception("Particle emitting mode was not selected");
            }
            IsEmitting = emitMode.Update(this, deltaTime);
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
            Console.WriteLine("PROTOYPE ParticleEmitter: Clone()");

            ParticleEmitter emitter = new ParticleEmitter(
                particleCount,
                sprite, 
                props.Clone(),
                emitMode.Clone(),
                useGravity
                );
            emitter.transform.SetPosition(transform.position);
            return emitter;
        }
    }
}
