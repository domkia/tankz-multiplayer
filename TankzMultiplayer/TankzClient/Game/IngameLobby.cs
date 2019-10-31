using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TankzClient.Framework;
using TankzClient.Models;

namespace TankzClient.Game
{
    class IngobbyScene : Scene
    {
        Player[] Players;
        private Button readyCheckButton;
        private bool readyState = false;
        public override void Load()
        {
            NetworkManager.Instance.GetConnected();
            readyCheckButton = CreateEntity(new Button(650, 520, 150, 60, null, "X ready")) as Button;
            readyCheckButton.SetColor(Color.Red);
            readyCheckButton.SetTextSize(20);
            readyCheckButton.OnClickCallback += readyCheckButton_OnClickCallback;

        }

        private void readyCheckButton_OnClickCallback()
        {
            if (!readyState)
            {
                readyCheckButton.setText("Ready");
                readyCheckButton.SetColor(Color.Green);
            }
            else
            {
                readyCheckButton.setText("X ready");
                readyCheckButton.SetColor(Color.Red);
            }
            NetworkManager.Instance.ChangeReadyState();
            readyState = !readyState;
            
        }

        public override void Render(Graphics context)
        {
            
            Players = NetworkManager.Instance.GetPlayerList();
            context.Clear(Color.Bisque);
            base.Render(context);
            context.DrawString("GAME LOBBY", new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Black, new Point(100, 10));
            for(int i = 0; i< Players.Length;i++)
            {
                context.DrawString(((i+1) + " " + Players[i].Name + " " + ((Players[i].ReadyState)?"true":"false")), new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Black, new Point(0, 30 + i * 20));
            }
            
        }
    }
}
