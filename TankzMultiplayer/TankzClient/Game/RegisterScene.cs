using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class RegisterScene : Scene
    {
        private InputField usernameField;
        private InputField passwordField;
        private Sprite logo;
        private Button registerButton;
        private Button backButton;
        private string errorMsg;
        bool error = false;
        bool registered = false;
        public override void Load()
        {
            logo = CreateEntity(new Sprite(Image.FromFile("../../res/logo.png"), new Vector2(380, 80), new Vector2(500, 250))) as Sprite;
            usernameField = CreateEntity(new InputField(140, 200, 120, 20)) as InputField;
            usernameField.SetBackgroundColor(Color.Brown);
            passwordField = CreateEntity(new InputField(140, 230, 120, 20)) as InputField;
            passwordField.SetBackgroundColor(Color.Brown);
            registerButton = CreateEntity(new Button(270, 200, 50, 20, null, "Register")) as Button;
            registerButton.OnClickCallback += RegisterButton_OnClickCallback;
            NetworkManager.Instance.RegisterErrorGot += Instance_RegisterErrorGot;
            NetworkManager.Instance.RegisterSuccess += Instance_RegisterSuccess;
            backButton = CreateEntity(new Button(20, 20, 30, 20, null, "Back")) as Button;
            backButton.OnClickCallback += backButton_OnClickCallback;
        }

        private void backButton_OnClickCallback()
        {
            SceneManager.Instance.LoadScene<LoginScene>();
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.FromArgb(77, 120, 78));
            base.Render(context);
            context.DrawString("USERNAME:", new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Black, new Point(10, 200));
            context.DrawString("PASSWORD:", new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Black, new Point(10, 230));
            
            if (error)
            {
                context.DrawString("Error: " + errorMsg, new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Red, new Point(10, 250));
            }
            if (registered)
            {
                context.DrawString("Successfully registered ", new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Green, new Point(10, 250));
            }
        }

        private void Instance_RegisterErrorGot(object sender, string e)
        {
            errorMsg = e;
            error = true;
            registered = false;
        }
        private void Instance_RegisterSuccess(object sender, string e)
        {
            registered = true;
            errorMsg = e;
            error = false;
        }

        private void RegisterButton_OnClickCallback()
        {
            string pw = Cipher.Encrypt(passwordField.Text.ToString(), "TankZPasswordCypher");
            NetworkManager.Instance.Register(usernameField.Text.ToString(), pw);
        }
    }
}
