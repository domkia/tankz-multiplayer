using System.Drawing;
using TankzClient.Framework;
using TankzClient.Framework.Components;

namespace TankzClient.Game
{
    class TankBarrel : Sprite
    {
        const float startAngle = -90;
        public float angle = startAngle;

        private Tank parent;

        public TankBarrel(Tank tank, Image image, Vector2 position, Vector2 size) : base(image, position, size)
        {
            this.parent = tank;
            Vector2 tankPosition = tank.GetComponent<TransformComponent>().position;
            GetComponent<TransformComponent>().SetPosition(tankPosition + new Vector2(0, -20));
            GetComponent<TransformComponent>().SetAngle(startAngle);
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
            GetComponent<TransformComponent>().Rotate(-angle);
            GetComponent<TransformComponent>().SetPosition(parent.GetComponent<TransformComponent>().position + new Vector2(0, -20));
            GetComponent<TransformComponent>().Rotate(angle);
        }
    }
}
