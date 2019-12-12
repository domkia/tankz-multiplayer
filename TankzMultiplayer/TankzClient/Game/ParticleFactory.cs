using System.Collections.Generic;
using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class ParticleFactory : EntityFactory
    {
        private static Dictionary<string, ParticleEmitter> cache = new Dictionary<string, ParticleEmitter>();

        private ParticleEmitter Clone(string particleName)
        {
            System.Console.WriteLine("PROTOTYPE ParticleFactory: Clone()");

            ParticleEmitter original = cache[particleName];
            ParticleEmitter newEmitter = original.Clone();
            return newEmitter;
        }

        public override Entity Create(EntityCreateArgs args)
        {
            System.Console.WriteLine($"FACTORY ParticleFactory: Create()");
            ParticleEmitter particleEmitter = null;
            string path = "../../res/particles/";
            Image image;

            string particleType = args.type.ToLower();
            if (cache.ContainsKey(particleType))
            {
                particleEmitter = Clone(particleType);
            }
            else
            {
                switch (args.type.ToLower())
                {
                    case "explosion":
                        System.Console.WriteLine($"\tCreating Explosion");
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
                        System.Console.WriteLine($"\tCreating Shield");
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
                        System.Console.WriteLine($"\tCreating Health");
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
                cache.Add(particleType, particleEmitter);
            }

            return SceneManager.Instance.CurrentScene.CreateEntity(particleEmitter);
        }
    }
}
