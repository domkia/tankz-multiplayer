using Microsoft.VisualStudio.TestTools.UnitTesting;
using TankzClient.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TankzClient.Framework.Tests
{
    [TestClass()]
    public class ParticleEmitterTests
    {
        [TestMethod()]
        public void RenderTest()
        {
            string path = "../../res/particles/";
            ParticleEmitter emitter = new ParticleEmitter(10, new Sprite(Image.FromFile(path + "shield_particle.png"), new Vector2(6, 5), new Vector2(100, 100)),
                new ParticleProperties(new Range(1f, 2f), new Range(1f, 2f), new Range(1f, 2f), new Range(1f, 2f), new Range(1f, 2f), 5f));
            Bitmap map = new Bitmap(path + "shield_particle.png");
            emitter.IsEmitting = false;
            emitter.Render(Graphics.FromImage(map));
            emitter.Emit();
            Assert.IsTrue(emitter.IsEmitting);
        }

        [TestMethod()]
        public void RenderParticleGraphicsTest()
        {
            Particle particle = new Particle(5f, 10f);
            particle.position = new Vector2(10f, 10f);
            string path = "../../res/particles/";
            ParticleEmitter emitter = new ParticleEmitter(10, new Sprite(Image.FromFile(path + "shield_particle.png"), new Vector2(6, 5), new Vector2(100, 100)),
                new ParticleProperties(new Range(1f, 2f), new Range(1f, 2f), new Range(1f, 2f), new Range(1f, 2f), new Range(1f, 2f), 5f));
            Bitmap map = new Bitmap(path + "shield_particle.png");
            emitter.RenderParticleGraphics(particle, Graphics.FromImage(map));
            Sprite sprite = emitter.getSprite();
            Assert.AreEqual(particle.position, sprite.transform.position, "bloga pozicija");
        }
    }
}