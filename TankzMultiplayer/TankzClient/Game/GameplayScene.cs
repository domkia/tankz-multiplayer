using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class GameplayScene : Scene
    {
        GameplayUI gameplayUI;
        
        public override void Load()
        {
            gameplayUI = CreateEntity(new GameplayUI()) as GameplayUI;

            Tank usaTank = new TankBuilder()
                .SetChassis(1, 1)
                .SetTurret(1)
                .SetTracks(0)
                .Build();
            CreateEntity(usaTank);
            usaTank.transform.SetPosition(new Vector2(500, 100));

        }
        public override void Render(Graphics context)
        {
            base.Render(context);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
    }
}
