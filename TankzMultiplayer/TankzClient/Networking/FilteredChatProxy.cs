using System.Collections.Generic;
using System.Text;

namespace TankzClient.Networking
{
    /// <summary>
    /// This chat service turns all bad words into ******
    /// </summary>
    public class FilteredChatProxy : IChatService
    {
        private IChatService realChat;

        public FilteredChatProxy(IChatService realChat)
        {
            this.realChat = realChat;
        }

        private readonly List<string> badwords = new List<string>()
        {
            "shit", "fuck", "ass", "retard", "noob", "nigga"
        };

        public void SendMessage(string author, string message)
        {
            string filteredMessage = FilterMessage(message);
            realChat.SendMessage(author, filteredMessage);
        }

        private string FilterMessage(string message)
        {
            StringBuilder filteredMessage = new StringBuilder();
            string[] words = message.Split(' ');
            foreach (string word in words)
            {
                bool contains = false;
                string lower = word.ToLower();
                foreach (string cword in badwords)
                {
                    if (lower.Contains(cword))
                    {
                        contains = true;
                        break;
                    }
                }
                if (contains)
                {
                    filteredMessage.Append(new string('*', word.Length) + ' ');
                }
                else
                {
                    filteredMessage.Append(word + ' ');
                }
            }
            return filteredMessage.ToString();
        }
    }
}
