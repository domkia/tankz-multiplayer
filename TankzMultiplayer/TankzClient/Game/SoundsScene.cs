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
        private SoundsPlayer player;

        public override void Load()
        {
            player = new SoundsPlayer();
            player.AddSound("background", "C:/Users/justi/Documents/GitHub/tankz-multiplayer/TankzMultiplayer/TankzClient/res/sounds/background.mp3");
            player.AddSound("shoot_1", "../../res/sounds/shoot_1.wav");
            mp3Button = CreateEntity(new Button(100, 100, 100, 50, null, "MP3")) as Button;
            mp3Button.OnClickCallback += MP3Button_OnClickCallback;
            wavButton = CreateEntity(new Button(100, 200, 100, 50, null, "WAV")) as Button;
            wavButton.OnClickCallback += WAVButton_OnClickCallback;
        }

        private void MP3Button_OnClickCallback()
        {
            player.PlaySound("background");
        }

        private void WAVButton_OnClickCallback()
        {
            player.PlaySound("shoot_1");
        }


    }
}
