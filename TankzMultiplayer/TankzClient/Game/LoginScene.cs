using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class LoginScene : Scene
    {
        private Button loginButton;
        private InputField inputField;
        bool nameError = false;

        private void LoginButton_OnClickCallback()
        {
            if (inputField.Text.ToString().Length != 0)
            {
                NetworkManager.Instance.SetName(inputField.Text.ToString());
                SceneManager.Instance.LoadScene<IngobbyScene>();
            }
            else
            {
                nameError = true;
            }
        }
        public override void Load()
        {
            inputField = CreateEntity(new InputField(140, 13, 120, 20)) as InputField;
            inputField.SetBackgroundColor(Color.Brown);
            loginButton = CreateEntity(new Button(270, 13, 50, 20, null, "Log In")) as Button;
            loginButton.OnClickCallback += LoginButton_OnClickCallback;
            loginButton.SetTextColor(Color.White);
            loginButton.SetColor(Color.Brown);
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.Bisque);
            base.Render(context);
            context.DrawString("USERNAME:", new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Black, new Point(10, 10));
            if (nameError)
            {
                context.DrawString("Username can't be empty", new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Red, new Point(10, 30));
            }
        }
    }
}
