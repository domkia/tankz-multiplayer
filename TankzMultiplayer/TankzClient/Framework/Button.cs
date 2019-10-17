using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using TankzClient.Framework.Components;

namespace TankzClient.Framework
{ 
    public class Button: UIElement
    {
        public readonly string Text;
        public readonly Image Image;

        private Brush brush = Brushes.Gray;
        private Brush textBrush = Brushes.White;
        private Font font;

        public event Action OnClickCallback;

        public Button(int x, int y, int width, int height, Image image, string text) : base(new Rectangle(x, y, width, height))
        {
            this.rect = new Rectangle(x, y, width, height);
            this.Text = text;
            this.Image = image;

            this.font = new Font("Arial", 8f);
        }

        public override void Render(Graphics context)
        {
            if (Image == null)
            {
                context.FillRectangle(brush, Rect);
            }
            else
            {
                context.DrawImage(Image, this.rect);
            }
            context.DrawString(this.Text, this.font, textBrush, rect);
        }

        public override void Click(Point point)
        {
            if(rect.Contains(point))
            if (OnClickCallback != null)
                OnClickCallback.Invoke();
        }

        public override void SetColor(Color color)
        {
            this.brush = new SolidBrush(color);
            base.SetColor(color);
        }

        public void SetTextSize(float size)
        {
            this.font = new Font("Arial", size);
        }

        public void SetTextColor(Color color)
        {
            this.textBrush = new SolidBrush(color);
        }

    }
}
