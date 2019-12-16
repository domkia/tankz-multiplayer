using System.Drawing;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class MainMenuScene : Scene
    {
        private Sprite logo;
        private Button start;
        
        bool error = false;
        string msg;
        public override void Load()
        {
            NetworkManager.Instance.LobbyErrorGot += Instance_LobbyErrorGot;
            logo = CreateEntity(new Sprite(Image.FromFile("../../res/logo.png"), new Vector2(380, 80), new Vector2(500, 250))) as Sprite;
            start = CreateEntity(new Button(350, 250, 100, 20, null, "START")) as Button;
           
            start.OnClickCallback += Start_OnClickCallback;
            
            start.SetTextColor(Color.White);
            start.SetColor(Color.Brown);
            
        }

        private void Instance_LobbyErrorGot(object sender, string e)
        {
            error = true;
            msg = e;
        }

        

        private void Start_OnClickCallback()
        {
            NetworkManager.Instance.JoinLobby(1);
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.FromArgb(77, 120, 78));
            base.Render(context);
            context.DrawString("Connected as:"+NetworkManager.Instance.MyData.Name, new Font(FontFamily.GenericMonospace, 15f, FontStyle.Bold), Brushes.Black, new Point(5, 300));
            if(error)
                context.DrawString("Lobby is full", new Font(FontFamily.GenericMonospace, 15f, FontStyle.Bold), Brushes.Red, new Point(350, 300));
        }
    }
}
