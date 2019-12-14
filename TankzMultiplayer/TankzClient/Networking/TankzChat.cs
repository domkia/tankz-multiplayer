using System.Collections.Generic;
using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Networking
{
    public class TankzChat : UIElement, IChatService
    {
        private List<string> history = new List<string>();

        public TankzChat() : base(new Rectangle(0, 0, 800, 600))
        {
            NetworkManager.Instance.OnChatMessageReceived += ChatMessageReceived;
        }

        // TODO: split into author and message
        private void ChatMessageReceived(string message)
        {
            SendMessage("player", message);
        }

        public void SendMessage(string author, string message)
        {
            string formatted = string.Format($"{author}: {message}");
            history.Add(formatted);
        }

        public override void Render(Graphics context)
        {
            for (int i = 0; i < history.Count; i++)
            {
                string message = history[history.Count - 1 - i];
                int rowCount = message.Length / 24;
                if (rowCount <= 0)
                    rowCount = 1;
                context.DrawString(message, SystemFonts.MessageBoxFont, Brushes.Blue, new RectangleF(600f, 500 - (i + (rowCount - 1)) * 24, 168f, 540f));
            }
        }

        public override void Click(Point mousePos) { }
    }
}
