using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class MainMenuScene : Scene
    {
        Tank tank;

        public MainMenuScene()
        {
            tank = CreateEntity(new Tank()) as Tank;
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.DarkBlue);
            base.Render(context);
            context.DrawString("Scene: Main Menu", SystemFonts.MenuFont, Brushes.Red, new Point(0, 0));
        }

        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(System.Windows.Forms.Keys.Space))
            {
                bool a = false;
            }
            base.Update(deltaTime);
        }
    }
}
