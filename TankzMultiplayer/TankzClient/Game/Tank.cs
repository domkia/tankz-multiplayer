using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;
using TankzClient.Framework.Components;

namespace TankzClient.Game
{
    class Tank : Sprite
    {
        const string chassisSpritePath = "../../res/tank_chassis_1.png";
        const string barrrelSpritePath = "../../res/tank_barrel.png";

        public TankBarrel barrel;
        private TankPhase currentPhase = null;

        public Tank() : base(Image.FromFile(chassisSpritePath), new Vector2(128, 128), new Vector2(64, 48))
        {
            // Setup barrel
            barrel = new TankBarrel(this, Image.FromFile(barrrelSpritePath), new Vector2(50, 50), new Vector2(64, 8));

            currentPhase = new TankAiming(this);
        }

        public override void Render(Graphics context)
        {
            barrel.Render(context);
            base.Render(context);
        }

        public override void Update(float deltaTime)
        {
            if (currentPhase != null)
                currentPhase.Update(deltaTime);
            base.Update(deltaTime);
            barrel.Update(deltaTime);
        }
    }
}
