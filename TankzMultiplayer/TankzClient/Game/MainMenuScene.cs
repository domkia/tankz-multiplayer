using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class MainMenuScene : Scene
    {
        Tank tank;
        private Button startButton;
        private InputField inputField;
        private ProgressBar progressBar;

        private void StartButton_OnClickCallback()
        {
            NetworkManager.Instance.SendRequest("join;" + inputField.Text);
        }
        public MainMenuScene()
        {
            tank = CreateEntity(new Tank()) as Tank;
            inputField = CreateEntity(new InputField(20, 20, 120, 20)) as InputField;
            startButton = CreateEntity(new Button(100, 100, 50, 50, null, "test")) as Button;
            startButton.OnClickCallback += StartButton_OnClickCallback;

            progressBar = CreateEntity(new ProgressBar(new Rectangle(170, 100, 64, 8), Color.LightGreen, Color.DarkGreen)) as ProgressBar;
            progressBar.SetProgress(0.4f);
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.DarkBlue);
            base.Render(context);
            context.DrawString("Scene: Main Menu", SystemFonts.MenuFont, Brushes.Red, new Point(0, 0));
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
    }
}
