using System.Drawing;

namespace TankzClient.Framework
{
    /// <summary>
    /// Particle emitter which draws animted sprites
    /// </summary>
    public class AnimatedParticleEmitter : ParticleEmitter
    {
        private IAnimationData animation;

        public AnimatedParticleEmitter(int particleCount, AnimatedSprite sprite, IAnimationData animation, ParticleProperties props, IParticleEmitMode emitMode, bool useGravity = false, Entity parent = null) 
            : base(particleCount, sprite, props, emitMode, useGravity, parent)
        {
            this.sprite = sprite;
            this.animation = animation;
        }

        public override void RenderParticleGraphics(Particle particle, Graphics context)
        {
            int currentFrame = animation.GetFrame(particle.Progress);
            ((AnimatedSprite)sprite).SetFrame(currentFrame);
            sprite.transform.SetPosition(particle.position);
            sprite.transform.SetSize(Vector2.one * particle.size);
            sprite.Render(context);
        }
    }
}
