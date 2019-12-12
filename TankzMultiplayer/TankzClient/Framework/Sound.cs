using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Game;

namespace TankzClient.Framework
{
    class Sound
    {
        string name { get; set; }
        ISounds sounds { get; set; }

        public Sound (string name, ISounds sounds)
        {
            this.name = name;
            this.sounds = sounds;
        }

        public string getName()
        {
            return name;
        }

        public ISounds getISounds()
        {
            return sounds;
        }
    }
}
