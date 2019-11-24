using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class EndGameScene : Scene
    {

        private Sprite logo;
        private string winner = null;

        public override void Load()
        {
            logo = CreateEntity(new Sprite(Image.FromFile("../../res/logo.png"), new Vector2(380, 80), new Vector2(500, 250))) as Sprite;
            NetworkManager.Instance.GameEnded += Instance_GameEnded;
        }

        private void Instance_GameEnded(string obj)
        {
            winner = obj;
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.FromArgb(77, 120, 78));
            base.Render(context);
            if(winner != null)
            {
                if(winner == "draw")
                {
                    context.DrawString("Game is draw", new Font(FontFamily.GenericMonospace, 15f, FontStyle.Bold), Brushes.Red, new Point(350, 300));
                }
                else
                context.DrawString(winner + " has won the Game!", new Font(FontFamily.GenericMonospace, 15f, FontStyle.Bold), Brushes.Red, new Point(350, 300));
            }
        }
    }
}
