using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class SoundsScene : Scene
    {
        private Button mp3Button;
        private Button wavButton;

        public override void Load()
        {
            SoundsPlayer.Instance.AddSound("background", "../../res/sounds/background.mp3");
            SoundsPlayer.Instance.AddSound("shoot_1", "../../res/sounds/shoot_1.wav");
            mp3Button = CreateEntity(new Button(100, 100, 100, 50, null, "MP3")) as Button;
            mp3Button.OnClickCallback += MP3Button_OnClickCallback;
            wavButton = CreateEntity(new Button(100, 200, 100, 50, null, "WAV")) as Button;
            wavButton.OnClickCallback += WAVButton_OnClickCallback;
        }

        private void MP3Button_OnClickCallback()
        {
            SoundsPlayer.Instance.PlaySound("background");
        }

        private void WAVButton_OnClickCallback()
        {
            SoundsPlayer.Instance.PlaySound("shoot_1");
        }


    }
}
