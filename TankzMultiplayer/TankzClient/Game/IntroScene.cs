using System.Threading;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class IntroScene : Scene
    {
        public override void Load()
        {
            SoundsPlayer.Instance.AddSoundIter("background", "../../res/sounds/background.mp3", "background");
            SoundsPlayer.Instance.AddSoundIter("shoot_0", "../../res/sounds/shoot_0.wav", "shooting");
            SoundsPlayer.Instance.AddSoundIter("shoot_1", "../../res/sounds/shoot_1.wav", "shooting");
            SoundsPlayer.Instance.AddSoundIter("shoot_2", "../../res/sounds/shoot_2.wav", "shooting");
            SoundsPlayer.Instance.CreateEnumerators();

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
