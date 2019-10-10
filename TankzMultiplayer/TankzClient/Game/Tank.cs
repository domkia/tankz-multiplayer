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

        public Sprite barrel;
        private TankPhase currentPhase = null;
        private float startAngle = -90;

        public Tank() : base(Image.FromFile(chassisSpritePath), new Vector2(128, 128), new Vector2(64, 48))
        {
            Vector2 chassisPosition = GetComponent<TransformComponent>().position;

            // Setup barrel
            barrel = new Sprite(Image.FromFile(barrrelSpritePath), new Vector2(50, 50), new Vector2(64, 8));
            barrel.GetComponent<TransformComponent>().SetPosition(chassisPosition + new Vector2(0, -20));
            barrel.GetComponent<TransformComponent>().SetAngle(startAngle);
        }

        

        

        public override void Render(Graphics context)
        {
            context.Transform = barrel.OrientationMatrix;
            barrel.Render(context);
            context.ResetTransform();

            base.Render(context);
        }

        public override void Update(float deltaTime)
        {
            if (currentPhase != null)
                currentPhase.Update(deltaTime);
            base.Update(deltaTime);
        }
    }
}
