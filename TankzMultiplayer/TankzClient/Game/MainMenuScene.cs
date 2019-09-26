using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class MainMenuScene : Scene
    {
        public MainMenuScene()
        {
            Vector2 a = new Vector2(-1, 0);
            Vector2 b = new Vector2(0, 1);
            float angle = Vector2.Angle(a, b);

            Vector2 c = new Vector2(1f, 0f);
            Vector2 d = new Vector2(-0.7f, 0.7f);
            float dotProduct = Vector2.Dot(c, d);

            Vector2 e = new Vector2(0, 0);
            Vector2 f = new Vector2(5, 5);
            float distance = Vector2.Distance(e, f);

            angle = angle;
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.DarkBlue);
            base.Render(context);
            context.DrawString("Scene: Main Menu", SystemFonts.MenuFont, Brushes.Red, new Point(0, 0));
        }
    }
}
