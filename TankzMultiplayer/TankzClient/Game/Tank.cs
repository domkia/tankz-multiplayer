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

        private Sprite barrel;

        public Tank() : base(Image.FromFile(chassisSpritePath), new Vector2(50, 50), new Vector2(64, 48))
        {
            barrel = new Sprite(Image.FromFile(barrrelSpritePath), new Vector2(50, 50), new Vector2(32, 8));
            Vector2 chassisPosition = GetComponent<TransformComponent>().position;
            barrel.GetComponent<TransformComponent>().SetPosition(chassisPosition - new Vector2(0, 0));
        }

        public override void Render(Graphics context)
        {
            barrel.Render(context);
            base.Render(context);
        }

        float angle = 0;
        public override void Update(float deltaTime)
        {
            barrel.GetComponent<TransformComponent>().SetAngle(angle);
            barrel.GetComponent<TransformComponent>().SetPosition(new Vector2(angle, angle));
            base.Update(deltaTime);
            angle += deltaTime * 60;
        }
    }
}
