using System;
using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class TankBarrel : Sprite
    {
        const float barrelLength = 30.0f;

        public TankBarrel(Tank tank, Image image, Vector2 position, Vector2 size) : base(image, position, size)
        {
            float angle = -tank.Angle;
            transform.SetAngle(angle);
        }

        public Vector2 GetReleasePosition()
        {
            ITank parentTank = parent as ITank;
            float angle = -parentTank.Angle;
            double rad = Utils.Deg2Rad(angle);
            Vector2 aimDirection = new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad));
            Vector2 spawnPoint = transform.position + aimDirection * barrelLength;
            return spawnPoint;
        }

        // TODO: remove this redundant code
        public override void Render(Graphics context)
        {
            //context.Transform = OrientationMatrix;
            base.Render(context);
            //context.ResetTransform();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            //transform.Rotate(-angle);
            //transform.SetPosition(tank.transform.position + new Vector2(0, -20));
            //transform.Rotate(angle);
        }
    }
}
