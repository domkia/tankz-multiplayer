using System.Collections.Generic;

namespace TankzClient.Framework
{
    /// <summary>
    /// Bridge pattern for partcile emitters
    /// </summary>
    public interface IParticleEmitMode : ICloneable<IParticleEmitMode>
    {
        bool Update(ParticleEmitter emitter, float deltaTime);
    }
}
