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
        public override void Load()
        {
            Tank usaTank =
                new TankBuilder()
                .SetChassis(1, 1)
                .SetTurret(1)
                .SetTracks(0)
                .Build();
            CreateEntity(usaTank);
            usaTank.transform.SetPosition(new Vector2(500, 100));

            usaTank.ApplyCamouflage(0);
            usaTank.ApplySideskirt(0);
            usaTank.ApplyAccessory(1);
        }
        public override void Render(Graphics context)
        {
            base.Render(context);
            context.DrawString("Scene: Suicide scene", SystemFonts.MenuFont, Brushes.Red, new Point(0, 0));
        }
    }
}
