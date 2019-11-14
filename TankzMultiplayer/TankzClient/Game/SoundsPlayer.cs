using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;
using System.Media;

namespace TankzClient.Game
{
    class SoundsPlayer
    {
        #region Singleton

        private static SoundsPlayer instance = null;
        public static SoundsPlayer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundsPlayer();
                }
                return instance;
            }
        }
        #endregion

        Dictionary<string, ISounds> sounds = new Dictionary<string, ISounds>();

        public void PlaySound(string soundName)
        {
            if (!sounds.ContainsKey(soundName))
            {
                throw new Exception("nera tokio garso");
            }
            sounds[soundName].Play();
        }

        public void AddSound(string soundName, string URL)
        {
            string extention = URL.Substring(URL.Length - 3, 3);
            if (sounds.ContainsKey(soundName))
            {
                throw new Exception("toks garsas jau pridetas dictionary");

            }
            switch (extention.ToLower())
            {
                case "mp3":
                    sounds.Add(soundName, new SoundsPlayerMP3(URL));
                    break;
                case "wav":
                    sounds.Add(soundName, new SoundsPlayerWAV(URL));
                    break;
            }
        }
    }
}