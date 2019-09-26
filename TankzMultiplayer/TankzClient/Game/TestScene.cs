using System;
using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class TestScene : Scene
    {
        private float loadMainMenuAfter = 5.0f;

        public override void Render(Graphics context)
        {
            Random rand = new Random();
            context.Clear(Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)));
            context.DrawString("Scene: TEST", SystemFonts.MenuFont, Brushes.Red, new Point(0, 0));
            context.DrawString($"Loading Main Menu in {loadMainMenuAfter - timer}", SystemFonts.MenuFont, Brushes.Red, new Point(0, 20));
        }

        private float timer = 0;
        public override void Update(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= loadMainMenuAfter)
                SceneManager.Instance.LoadScene<MainMenuScene>();
        }
    }
}
