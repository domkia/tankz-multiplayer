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
            Console.WriteLine("ADAPTER new SoundsPlayerWAV()");
            this.player = new SoundPlayer(URL);
        }

        public void Play()
        {
            Console.WriteLine("ADAPTER SoundsPlayerWAV: Play()");
            player.Play();
        }
    }
}
