using System;
using System.Collections.Generic;
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
            TankBuilder builder = new UsaTankBuilder(false);
            Tank usaTank = builder.Build();
                /*new TankBuilder(true)
                .SetChassis(1, 1)
                .SetTurret(1)
                .SetTracks(0)
                .Build();*/
            CreateEntity(usaTank);
            usaTank.transform.SetPosition(new Vector2(500, 100));

            usaTank.ApplyCamouflage(0);
            usaTank.ApplySideskirt(0);
            usaTank.ApplyAccessory(1);


            
            explosion = new ParticleFactory().Create("explosion") as ParticleEmitter;
            explosion.transform.SetPosition(usaTank.transform.position);
            explosion2 = explosion.Clone();
            explosion2.transform.SetPosition(usaTank.transform.position + new Vector2(200, 200));
            
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
                explosion2.Emit();
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
