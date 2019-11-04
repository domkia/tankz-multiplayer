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
        private Button readyCheckButton;
        private bool readyState = false;
        private Font font;

        public override void Load()
        {
            readyCheckButton = CreateEntity(new Button(650, 520, 150, 60, null, "X ready")) as Button;
            readyCheckButton.SetColor(Color.Red);
            readyCheckButton.SetTextSize(20);
            readyCheckButton.OnClickCallback += readyCheckButton_OnClickCallback;
            this.font = new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold);
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
            var players = NetworkManager.Instance.Players;
            context.Clear(Color.Bisque);
            base.Render(context);
            context.DrawString("GAME LOBBY", font, Brushes.Black, new Point(100, 10));
            for(int i = 0; i < players.Count; i++)
            {
                context.DrawString(((i+1) + " " + players[i].Name + " " + ((players[i].ReadyState)?"true":"false")), font, Brushes.Black, new Point(0, 30 + i * 20));
            }
            
        }
    }
}
