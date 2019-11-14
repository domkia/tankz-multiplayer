using System.Drawing;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class MainMenuScene : Scene
    {
        private Sprite logo;
        private Button start;
        private Button editProfile;
        public override void Load()
        {
            logo = CreateEntity(new Sprite(Image.FromFile("../../res/logo.png"), new Vector2(380, 80), new Vector2(500, 250))) as Sprite;
            start = CreateEntity(new Button(350, 250, 100, 20, null, "START")) as Button;
            editProfile = CreateEntity(new Button(350, 280, 100, 20, null, "EDIT PROFILE")) as Button;
            start.OnClickCallback += Start_OnClickCallback;
            editProfile.OnClickCallback += EditProfile_OnClickCallback;
            start.SetTextColor(Color.White);
            start.SetColor(Color.Brown);
            editProfile.SetTextColor(Color.White);
            editProfile.SetColor(Color.Brown);
        }

        private void EditProfile_OnClickCallback()
        {
            throw new System.NotImplementedException();
        }

        private void Start_OnClickCallback()
        {
            NetworkManager.Instance.JoinLobby(1);
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.FromArgb(77, 120, 78));
            base.Render(context);
            //if(NetworkManager.Instance.MyData != null)
            context.DrawString("Connected as:"+NetworkManager.Instance.MyData.Name, new Font(FontFamily.GenericMonospace, 15f, FontStyle.Bold), Brushes.Black, new Point(5, 300));
            
            
        }
    }
}
