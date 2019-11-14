using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace TankzClient.Game
{
    public class SoundsPlayerWAV : ISounds
    {
        SoundPlayer player;
        public SoundsPlayerWAV(string URL)
        {
            this.player = new SoundPlayer(URL);
        }
        public void Play()
        {
            player.Play();
        }
    }
}
