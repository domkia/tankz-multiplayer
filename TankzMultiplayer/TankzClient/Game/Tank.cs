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

        public Tank() : base(Image.FromFile(chassisSpritePath), new Vector2(128, 128), new Vector2(64, 48))
        {
            Vector2 chassisPosition = GetComponent<TransformComponent>().position;

            // Setup barrel
            barrel = new Sprite(Image.FromFile(barrrelSpritePath), new Vector2(50, 50), new Vector2(64, 8));
            barrel.GetComponent<TransformComponent>().SetPosition(chassisPosition + new Vector2(0, -20));
            barrel.GetComponent<TransformComponent>().SetAngle(angle);

            Input.OnKeyDown += RotateBarrel;
        }

        float angle = -90;
        private void RotateBarrel(object sender, System.Windows.Forms.Keys e)
        {
            if (e == System.Windows.Forms.Keys.Right)
                angle += 5f;
            else if (e == System.Windows.Forms.Keys.Left)
                angle -= 5f;
            if (angle < -180)
                angle = -180;
            else if (angle > 0)
                angle = 0;
            TransformComponent t = barrel.GetComponent<TransformComponent>();
            t.SetAngle(angle);
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
            base.Update(deltaTime);
        }
    }
}
