using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class TankBarrel : Sprite
    {
        const float startAngle = -90;
        public float angle = startAngle;

        private Tank tank;

        public TankBarrel(Tank tank, Image image, Vector2 position, Vector2 size) : base(image, position, size)
        {
            this.tank = tank;
            Vector2 tankPosition = tank.transform.position;
            transform.SetPosition(tankPosition + new Vector2(0, -20));
            transform.SetAngle(startAngle);
        }

        public override void Render(Graphics context)
        {
            context.Transform = OrientationMatrix;
            base.Render(context);
            context.ResetTransform();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            transform.Rotate(-angle);
            transform.SetPosition(tank.transform.position + new Vector2(0, -20));
            transform.Rotate(angle);
        }
    }
}
