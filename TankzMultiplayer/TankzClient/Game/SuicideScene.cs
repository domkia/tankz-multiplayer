using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class SuicideScene : Scene
    {
        private ParticleEmitter explosion;
        private ParticleEmitter explosion2;

        public override void Load()
        {
            int n = 10000;
            Console.WriteLine("-------------Greitaveikos tyrimas-----------------------");
            Console.WriteLine(n + " objektu");
            Console.WriteLine("--------------------------------------------------------");
            
            Stopwatch stopwatch = new Stopwatch();
            ParticleFactory factory = new ParticleFactory();
            stopwatch.Start();
            for (int i = 0; i < n; i++)
            {
                ParticleEmitter particleEmitter = null;
                particleEmitter = factory.Create("explosion") as ParticleEmitter;
            }
            stopwatch.Stop();
            Console.WriteLine("Su flyweight: " + stopwatch.ElapsedMilliseconds + " ms");
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(true));

            Stopwatch stopwatchNF = new Stopwatch();
            stopwatchNF.Start();
            for (int i = 0; i < n; i++)
            {
                ParticleEmitter particleEmitter = null;
                string path = "../../res/particles/";
                Image image;
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
            }
            stopwatchNF.Stop();
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Be flyweight: " + stopwatchNF.ElapsedMilliseconds + " ms");
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(true));
            Console.WriteLine("--------------------------------------------------------");

            //TankBuilder builder = new UsaTankBuilder(false);
            //Tank usaTank = builder.Build();
            //    /*new TankBuilder(true)
            //    .SetChassis(1, 1)
            //    .SetTurret(1)
            //    .SetTracks(0)
            //    .Build();*/
            //CreateEntity(usaTank);
            //usaTank.transform.SetPosition(new Vector2(500, 100));

            //usaTank.ApplyCamouflage(0);
            //usaTank.ApplySideskirt(0);
            //usaTank.ApplyAccessory(1);



            //explosion = new ParticleFactory().Create("explosion") as ParticleEmitter;
            //explosion.transform.SetPosition(usaTank.transform.position);
            //explosion2 = explosion.Clone();
            //explosion2.transform.SetPosition(usaTank.transform.position + new Vector2(200, 200));

        }
        public override void Render(Graphics context)
        {
            base.Render(context);
            context.DrawString("Scene: Suicide scene", SystemFonts.MenuFont, Brushes.Red, new Point(0, 0));
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (Input.IsKeyDown(System.Windows.Forms.Keys.E))
            {
                //explosion.Emit();
                /*var explosion2 = explosion.Clone() as ParticleEmitter;
                ParticleEmitter explosion = new ParticleFactory().Create("explosion") as ParticleEmitter;
                explosion.transform.SetPosition(new Vector2(500, 100));

                explosion2.transform.SetPosition(explosion2.transform.position + new Vector2(100, 100));
                explosion2.Emit();
                explosion.Emit();*/



            }
        }

    }
}
