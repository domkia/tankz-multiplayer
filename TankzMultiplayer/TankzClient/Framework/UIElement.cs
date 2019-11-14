using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TankzClient.Framework
{
    public abstract class UIElement : Entity, IRenderable
    {
        protected Rectangle rect;
        protected Color tintColor;

        public virtual int SortingLayer => 10;

        protected UIElement(Rectangle rect)
        {
            this.rect = rect;
            this.tintColor = Color.Gray;
        }

        public Size Size
        {
            get { return rect.Size; }
            set { rect.Size = value; }
        }

        public Rectangle Rect => this.rect;

        public Matrix OrientationMatrix => transform.OrientationMatrix;

        public bool IsVisible { get; set; }

        public virtual void Render(Graphics context)
        {
            context.DrawRectangle(Pens.Red, rect);
        }

        public virtual void SetColor(Color color)
        {
            this.tintColor = color;
        }

        public virtual bool IsMouseOver(Point mousePos)
        {
            return rect.Contains(mousePos);
        }
        public abstract void Click(Point mousePos);

        public override void Update(float deltaTime)
        {
            if (Input.MouseButtonDown)
            {
                Click(Input.MousePosition);
            }
        }
    }
}
