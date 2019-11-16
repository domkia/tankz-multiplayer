using System.Threading;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class IntroScene : Scene
    {
        public override void Load()
        {
            // Load sounds
            SoundsPlayer.Instance.AddSound("background", "../../res/sounds/background.mp3");
            SoundsPlayer.Instance.AddSound("shoot_1", "../../res/sounds/shoot_1.wav");

            // Initialize server connection
            if (!NetworkManager.Instance.IsConnected)
            {
                NetworkManager.Instance.Start();
                Thread networkThread = new Thread(NetworkManager.Instance.Connect);
                networkThread.IsBackground = true;
                networkThread.Start();
            }

            // Load login scene
            SceneManager.Instance.LoadScene<LoginScene>();
        }
    }
}
