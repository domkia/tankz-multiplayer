using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using TankzClient.Framework.Components;

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
        }

        public Point Position()
        {
            Point point = new Point((int)GetComponent<TransformComponent>().position.x, (int)GetComponent<TransformComponent>().position.x);
            return point;
        }

        public Size Size
        {
            get { return rect.Size; }
            set { rect.Size = value; }
        }

        public Rectangle Rect => this.rect;

        public Matrix OrientationMatrix => GetComponent<TransformComponent>().OrientationMatrix;

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
    }
}
