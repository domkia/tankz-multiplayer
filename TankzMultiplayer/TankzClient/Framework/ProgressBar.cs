using System.Drawing;

namespace TankzClient.Framework
{
    /// <summary>
    /// Progress bar UI element
    /// Progress range 0.0 - 1.0
    /// </summary>
    public class ProgressBar : UIElement
    {
        public float Progress { get; protected set; }
        public Color FillColor { get; protected set; }
        protected int Margin { get; set; }

        private Brush background;
        private Brush fill;

        public ProgressBar(Rectangle rect, Color fillColor, Color backColor) 
            : base(rect)
        {
            base.tintColor = backColor;
            this.FillColor = fillColor;
            this.Margin = 2;

            this.background = new SolidBrush(backColor);
            this.fill = new SolidBrush(fillColor);
        }

        public void SetProgress(float progress)
        {
            if (progress < 0.0f)
            {
                Progress = 0.0f;
            }
            else if (progress > 1.0f)
            {
                Progress = 1.0f;
            }else
            Progress = progress;
        }

        public override void Render(Graphics context)
        {
            context.FillRectangle(background, rect);
            Rectangle fillRect = new Rectangle(
                rect.X + Margin,
                rect.Y + Margin,
                (int)(rect.Width * Progress) - Margin * 2,
                rect.Height - Margin * 2);
            context.FillRectangle(fill, fillRect);
        }

        public override void Click(Point mousePos) { }
    }
}
