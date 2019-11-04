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
        private InputField inputField;
        bool nameError = false;
        bool waiting = false;

        private Image[] icons;

        private bool joining = false;

        private void LoginButton_OnClickCallback()
        {
            if (joining == true)
                return;
            if (inputField.Text.ToString().Length != 0)
            {
                waiting = true;
                NetworkManager.Instance.Start();

                //TODO: move this out of here
                Thread networkThread = new Thread(NetworkManager.Instance.Connect);
                networkThread.IsBackground = true;
                networkThread.Start();

                string name = inputField.Text.ToString();
                joining = true;
                NetworkManager.Instance.JoinLobby(name);
            }
            else
            {
                nameError = true;
            }
        }

        private void SuicideButton_OnClickCallback()
        {

            SceneManager.Instance.LoadScene<SuicideScene>();
        }

        private void Instance_ConnectedToServer(object sender, EventArgs e)
        {
            //NetworkManager.Instance.SetName(inputField.Text.ToString());
            //NetworkManager.Instance.DataGained += Instance_DataGained;
            //NetworkManager.Instance.ConnectedToServer -= Instance_ConnectedToServer;
        }

        private void Instance_DataGained(object sender, System.EventArgs e)
        {
            //SceneManager.Instance.LoadScene<IngobbyScene>();
            //NetworkManager.Instance.DataGained -= Instance_DataGained;
        }
        public override void Load()
        {
            inputField = CreateEntity(new InputField(140, 13, 120, 20)) as InputField;
            inputField.SetBackgroundColor(Color.Brown);
            loginButton = CreateEntity(new Button(270, 13, 50, 20, null, "Log In")) as Button;
            loginButton.OnClickCallback += LoginButton_OnClickCallback;
            loginButton.SetTextColor(Color.White);
            loginButton.SetColor(Color.Brown);

            suicideButton = CreateEntity(new Button(10, 10, 50, 50, null, "Suicide Scene")) as Button;
            suicideButton.OnClickCallback += SuicideButton_OnClickCallback;

            icons = new Image[]
            {
                HttpImageDownloader.GetBitmapFromURL("https://avatars2.githubusercontent.com/u/36890057?s=460&v=4", new Size(32, 32)),
                HttpImageDownloader.GetBitmapFromURL("https://avatars3.githubusercontent.com/u/36762328?s=460&v=4", new Size(32, 32)),
                HttpImageDownloader.GetBitmapFromURL("https://avatars2.githubusercontent.com/u/36892013?s=460&v=4", new Size(32, 32))
            };
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.Bisque);
            base.Render(context);
            if (!joining)
            {
                context.DrawString("USERNAME:", new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Black, new Point(10, 10));
                if (nameError)
                {
                    context.DrawString("Username can't be empty", new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Red, new Point(10, 30));
                }
                if (waiting)
                {
                    context.DrawString("Waiting for server...", new Font(FontFamily.GenericMonospace, 30f, FontStyle.Bold), Brushes.Black, new Point(400, 600));
                }
            }
            else
            {
                context.DrawString("Joining... Please wait", new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Black, new Point(10, 10));
            }
            for (int i = 0; i < icons.Length; i++)
            {
                context.DrawImage(icons[i], new Point(16, 100 + 36 * i));
            }
        }
    }
}
