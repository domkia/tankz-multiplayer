using System;
using System.Drawing;

namespace TankzClient.Framework
{
    public class NullUIElement : UIElement
    {
        public NullUIElement(Rectangle rect) : base(rect) { }

        public override void Click(Point mousePos)
        {
            Console.WriteLine("This UI Element is NULL");
        }

        public override void Render(Graphics context)
        {
            base.Render(context);
            context.DrawLine(
                Pens.Red,
                new Point(rect.Left, rect.Bottom),
                new Point(rect.Right, rect.Top));
            context.DrawLine(
                Pens.Red,
                new Point(rect.Left, rect.Top),
                new Point(rect.Right, rect.Bottom));
        }
    }
}
