using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class LoginScene : Scene
    {
        private Button loginButton;
        private Button suicideButton;
        private Button createAccButton;
        private InputField usernameField;
        private InputField passwordField;
        private Sprite logo;
        bool error = false;
        private string msg = "";
        private Image[] icons;

        private void LoginButton_OnClickCallback()
        {
            string name = usernameField.Text.ToString();
            string password = passwordField.Text.ToString();
            string pw = Cipher.Encrypt(passwordField.Text.ToString(), "TankZPasswordCypher");
            NetworkManager.Instance.Login(usernameField.Text.ToString(), pw);
        }
        private void Instance_LoginSuccess(object sender, string e)
        {
            SceneManager.Instance.LoadScene<MainMenuScene>();
        }

        private void Instance_LoginErrorGot(object sender, string e)
        {
            error = true;
            msg = e;
        }

        private void SuicideButton_OnClickCallback()
        {
            SceneManager.Instance.LoadScene<SuicideScene>();
        }
        public override void Load()
        {
            if (!NetworkManager.Instance.IsConnected)
            {
                NetworkManager.Instance.Start();
                Thread networkThread = new Thread(NetworkManager.Instance.Connect);
                networkThread.IsBackground = true;
                networkThread.Start();
            }

            logo = CreateEntity(new Sprite(Image.FromFile("../../res/logo.png"), new Vector2(380,80), new Vector2(500,250)))as Sprite;
            usernameField = CreateEntity(new InputField(140, 200, 120, 20)) as InputField;
            usernameField.SetBackgroundColor(Color.Brown);

            passwordField = CreateEntity(new InputField(140, 225, 120, 20)) as InputField;
            passwordField.SetBackgroundColor(Color.Brown);
            loginButton = CreateEntity(new Button(270, 200, 50, 20, null, "Log In")) as Button;
            loginButton.OnClickCallback += LoginButton_OnClickCallback;
            loginButton.SetTextColor(Color.White);
            loginButton.SetColor(Color.Brown);

            createAccButton = CreateEntity(new Button(20, 250, 120, 20, null, "Create Account")) as Button;
            createAccButton.OnClickCallback += CreateAcc_OnClickCallback;
            createAccButton.SetTextColor(Color.White);
            createAccButton.SetColor(Color.Brown);

            NetworkManager.Instance.LoginErrorGot += Instance_LoginErrorGot;
            NetworkManager.Instance.LoginSuccess += Instance_LoginSuccess;

            suicideButton = CreateEntity(new Button(650, 400, 50, 50, null, "Suicide Scene")) as Button;
            suicideButton.OnClickCallback += SuicideButton_OnClickCallback;

            icons = new Image[]
            {
                HttpImageDownloader.GetBitmapFromURL("https://avatars2.githubusercontent.com/u/36890057?s=460&v=4", new Size(32, 32)),
                HttpImageDownloader.GetBitmapFromURL("https://avatars3.githubusercontent.com/u/36762328?s=460&v=4", new Size(32, 32)),
                HttpImageDownloader.GetBitmapFromURL("https://cdn2.iconfinder.com/data/icons/ninja-turtles-filledoutline/64/donatello-avatar-people-super_hero-ninja-ninja_turtles-warrior-cultures-japanese-oriental-512.png", new Size(32, 32))
            };
        }


        private void CreateAcc_OnClickCallback()
        {
            SceneManager.Instance.LoadScene<RegisterScene>(); ;
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.FromArgb(77,120,78));
            base.Render(context);
            context.DrawString("USERNAME:", new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Black, new Point(10, 200));
            context.DrawString("PASSWORD:", new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Black, new Point(10, 225));
            if (error)
            {
                context.DrawString(msg, new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Red, new Point(10, 270));
            }
            for (int i = 0; i < icons.Length; i++)
            {
                context.DrawImage(icons[i], new Point(16, 400 + 36 * i));
            }
        }
    }
}
