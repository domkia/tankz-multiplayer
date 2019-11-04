using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class LobbyScene : Scene
    {

        public override void Load()
        {
            NetworkManager.Instance.GetConnectedPlayerList();
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.Bisque);
            base.Render(context);
            context.DrawString("LOBBIES", new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold), Brushes.Black, new Point(100, 10));
        }
    }
}
