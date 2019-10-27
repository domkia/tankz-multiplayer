using System.Drawing;
using System.Drawing.Drawing2D;

namespace TankzClient.Framework
{
    public class Sprite : Entity, IRenderable
    {
        public Image image { get; protected set; }
        public virtual int SortingLayer => 0;
        public Matrix OrientationMatrix => transform.OrientationMatrix;

        public Sprite(Image image, Vector2 position, Vector2 size)
        {
            this.image = image;
            transform.SetPosition(position);
            transform.SetSize(size);
        }

        public virtual void Render(Graphics context)
        {
            Rectangle rect = transform.Rect;
            context.DrawImage(image, rect.X, rect.Y, rect.Size.Width, rect.Size.Height);
        }
    }
}
