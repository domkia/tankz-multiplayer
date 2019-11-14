using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace TankzClient.Game
{
    public class SoundsPlayerMP3 : ISounds
    {
        WindowsMediaPlayer mp3player;

        public SoundsPlayerMP3(string URL)
        {

            this.mp3player = new WindowsMediaPlayer();
            this.mp3player.URL = URL;
        }

        public void Play()
        {
            mp3player.controls.play();
        }
    }
}
