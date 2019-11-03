using System.Drawing;
using System.Drawing.Drawing2D;

namespace TankzClient.Framework
{
    public class Sprite : Entity, IRenderable, ICloneable<Sprite>
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
            if(image != null)
                context.DrawImage(image, rect.X, rect.Y, rect.Size.Width, rect.Size.Height);
        }

        public Sprite Clone()
        {
            Sprite sprite = new Sprite(
                image,
                this.transform.position,
                this.transform.size);
            return sprite;
        }
    }
}
