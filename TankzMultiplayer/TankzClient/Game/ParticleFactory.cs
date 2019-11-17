using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class ParticleFactory : EntityFactory
    {
        public override Entity Create(EntityCreateArgs args)
        {
            ParticleEmitter particleEmitter = null;
            string path = "../../res/particles/";
            Image image;
            switch (args.type.ToLower())
            {
                case "explosion":
                    image = Image.FromFile(path + "explosion_spritesheet.png");
                    AnimatedSprite explosionSprite = new AnimatedSprite(image, new Vector2(), new Vector2(100, 100), 4, 4);
                    FrameAnimation animation = new FrameAnimation(1f, false, 0, 12);
                    particleEmitter = new AnimatedParticleEmitter(16, explosionSprite, animation, 
                        new ParticleProperties()
                        {
                            startSize = new Range(48, 84),
                            startAngle = new Range(225f, 315f),
                            startLifetime = new Range(0.75f, 1.5f),
                            startSpeed = new Range(100f, 400f),
                            sizeGrow = -32,
                        }, 
                    new ParticlesOneShotMode(),
                    true);
                    break;
                case "shield":
                    image = Image.FromFile(path + "shield_particle.png");
                    Sprite shieldSprite = new Sprite(image, new Vector2(), new Vector2(100, 100));
                    particleEmitter = new ParticleEmitter(5, shieldSprite, new ParticleProperties()
                    {
                        startSize = 128f,
                        sizeGrow = -128f,
                        spawnRate = 0.4f,
                        startLifetime = 1f
                    }, new ParticlesContinuousMode());
                    break;
                case "health":
                    image = Image.FromFile(path + "health_particle.png");
                    Sprite healthSprite = new Sprite(image, new Vector2(), new Vector2(100, 100));
                    particleEmitter = new ParticleEmitter(8, healthSprite, new ParticleProperties()
                    {
                        startSpeed = 700f,
                        speedDamping = 0.7f,
                        startOffset = new Vector2(20f, 0f),
                        startSize = 64f,
                        sizeGrow = -64f,
                        spawnRate = 0.2f,
                        startAngle = 270f,
                        startLifetime = 0.5f
                    }, new ParticlesContinuousMode());
                    break;
                default:
                    return null;
            }
            return SceneManager.Instance.CurrentScene.CreateEntity(particleEmitter);
        }
    }
}
