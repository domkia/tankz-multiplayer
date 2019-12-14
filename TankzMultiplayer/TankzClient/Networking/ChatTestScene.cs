using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Networking
{
    public class ChatTestScene : Scene
    {
        private IChatService chat;
        private InputField inputField;

        public override void Load()
        {
            TankzChat tankzchat = CreateEntity(new TankzChat()) as TankzChat;
            chat = new FilteredChatProxy(tankzchat);
            inputField = new InputField(600, 530, 200, 24);
            CreateEntity(inputField);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (Input.IsKeyDown(System.Windows.Forms.Keys.Enter))
            {
                if (inputField.Text.Length > 0)
                {
                    chat.SendMessage("player", inputField.Text.ToString());
                    inputField.Text.Clear();
                }
            }
        }
    }
}
