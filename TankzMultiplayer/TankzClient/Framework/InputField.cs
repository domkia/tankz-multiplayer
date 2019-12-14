using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TankzClient.Framework
{
    class InputField : UIElement
    {
        public bool IsFocused { get; private set; }
        public StringBuilder Text { get; set; }

        const int textMargin = 2;
        private Rectangle textRect;
        private float blinkSpeed = 0.25f;
        private bool cursorVisible;
        private int cursorOffsetX = 0;
        private Font font;
        private Brush background;
        private Pen foreground;

        public InputField(int x, int y, int width, int height)
            : base(new Rectangle(x, y, width, height))
        {
            Text = new StringBuilder();
            textRect = new Rectangle(
                Rect.X + textMargin,
                Rect.Y + textMargin,
                Rect.Width - textMargin * 2,
                Rect.Height - textMargin * 2);
            font = new Font(FontFamily.GenericMonospace, 8f, FontStyle.Bold);
            background = new SolidBrush(tintColor);
            foreground = new Pen(Color.White);
        }

        public override void Click(Point mousePos)
        {
            if (IsMouseOver(mousePos))
                IsFocused = true;
            else
                IsFocused = false;
        }

        public void SetBackgroundColor(Color color)
        {
            background = new SolidBrush(color);
        }

        public override void Render(Graphics context)
        {
            cursorOffsetX = (int)context.MeasureString(Text.ToString(), font).Width;
            if (IsFocused)
            {
                context.FillRectangle(background, base.Rect);
                context.DrawRectangle(foreground, base.Rect);
                if (cursorVisible)
                    context.DrawLine(foreground, textRect.X + cursorOffsetX, textRect.Y, textRect.X + cursorOffsetX, textRect.Y + textRect.Height);
            }
            else
                context.FillRectangle(background, base.Rect);
            if (Text.Length > 0)
                context.DrawString(Text.ToString(), font, Brushes.White, textRect);
        }

        private float timer = 0.0f;
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (!IsFocused)
                return;
            GetKeyboardInput();
            timer += deltaTime;
            if (timer >= blinkSpeed)
            {
                cursorVisible = !cursorVisible;
                timer = 0.0f;
            }
        }

        private void GetKeyboardInput()
        {
            var keys = Input.inputQueue;
            foreach (Keys key in keys)
            {
                if (key == Keys.Back)
                {
                    if (Text.Length > 0)
                        Text.Remove(Text.Length - 1, 1);
                }
                else if (key == Keys.Enter)
                {
                    IsFocused = false;
                }
                else
                {
                    char character = (char)key;
                    // TODO: read all keys
                    //if (char.IsLetterOrDigit(character))
                    //{
                        Text.Append(character);
                    //}
                }
            }
        }
    }
}
