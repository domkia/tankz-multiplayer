using System.Drawing;
using System.Drawing.Drawing2D;
using TankzClient.Framework.Components;

namespace TankzClient.Framework
{
    public class Sprite : Entity, IRenderable
    {
        public Image image { get; protected set; }
        public int SortingLayer => 0;
        public Matrix OrientationMatrix => GetComponent<TransformComponent>().OrientationMatrix;

        public Sprite(Image image, Vector2 position, Vector2 size)
        {
            this.image = image;
            base.GetComponent<TransformComponent>().SetPosition(position);
            base.GetComponent<TransformComponent>().SetSize(size);
        }

        public virtual void Render(Graphics context)
        {
            TransformComponent transform = base.GetComponent<TransformComponent>();
            context.DrawImage(image, transform.position.x, transform.position.y, transform.size.x, transform.size.y);
        }
    }
}
