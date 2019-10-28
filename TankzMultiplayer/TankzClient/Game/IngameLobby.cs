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
    class IngobbyScene : Scene
    {
        string[] Players;
        public override void Load()
        {
            
            NetworkManager.Instance.GetConnected();
        }

        public override void Render(Graphics context)
        {
            
            Players = NetworkManager.Instance.GetPlayerList();
            context.Clear(Color.Bisque);
            base.Render(context);
            context.DrawString("GAME LOBBY", new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Black, new Point(100, 10));
            for(int i = 0; i< Players.Length;i++)
            {
                context.DrawString(((i+1) + " " +Players[i]), new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Black, new Point(0, 30 + i * 20));
            }
        }
    }
}
