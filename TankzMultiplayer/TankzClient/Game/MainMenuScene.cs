using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class MainMenuScene : Scene
    {
        Tank tank;
        private Button startButton;

        private void StartButton_OnClickCallback()
        {
            System.Console.WriteLine("Clicked");
        }
        public MainMenuScene()
        {
            tank = CreateEntity(new Tank()) as Tank;
            startButton = CreateEntity(new Button(100, 100, 50, 50, null, "test")) as Button;
            startButton.OnClickCallback += StartButton_OnClickCallback;
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
