using System.Drawing;

namespace TankzClient.Framework.Components
{
    public class TransformComponent : IComponent
    {
        public Vector2 position { get; private set; }
        public float angle { get; private set; }
        public Vector2 size { get; private set; }
        
        public Rectangle Rect
        {
            get
            {
                return new Rectangle(
                    (int)(position.x - 0.5f * size.x),
                    (int)(position.y - 0.5f * size.y),
                    (int)size.x, 
                    (int)size.y
                );
            }
        }

        public TransformComponent()
        {
            this.position = new Vector2(0, 0);
            this.size = new Vector2(100, 100);
        }

        public void SetPosition(Vector2 newPosition) => this.position = newPosition;

        public void SetAngle(float newAngle) => this.angle = newAngle;

        public void SetSize(Vector2 newSize) => this.size = newSize;

        public void Update(float deltaTime, Entity entity) { }
    }
}
