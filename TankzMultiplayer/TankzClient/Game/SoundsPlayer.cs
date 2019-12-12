using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;
using System.Media;
using TankzClient.Framework;

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
        List<Sound>.Enumerator shootingEnum;
        HashSet<Sound>.Enumerator backgroundEnum;
        Dictionary<string, ISounds>.Enumerator environmentEnum;

        List<Sound> shootingSounds = new List<Sound>();
        HashSet<Sound> backgroundSounds = new HashSet<Sound>();
        Dictionary<string, ISounds> environmentSounds = new Dictionary<string, ISounds>();

        Dictionary<string, ISounds> sounds = new Dictionary<string, ISounds>();

        public void PlaySound(string soundName)
        {
            if (!sounds.ContainsKey(soundName))
            {
                throw new Exception("nera tokio garso");
            }
            sounds[soundName].Play();
        }

        public void PlayNextSound(string type)
        {
            switch (type)
            {
                case "shooting":
                    if (!shootingEnum.MoveNext()) //jeigu pasieke pabaiga, pradeda is naujo
                    {
                        shootingEnum = shootingSounds.GetEnumerator();
                        shootingEnum.MoveNext();
                    }
                    shootingEnum.Current.getISounds().Play();
                    break;

                case "background":
                    if (!backgroundEnum.MoveNext()) //jeigu pasieke pabaiga, pradeda is naujo
                    {
                        backgroundEnum = backgroundSounds.GetEnumerator();
                        backgroundEnum.MoveNext();
                    }
                    backgroundEnum.Current.getISounds().Play();
                    break;

                case "environment":
                    if (!environmentEnum.MoveNext()) //jeigu pasieke pabaiga, pradeda is naujo
                    {
                        environmentEnum = environmentSounds.GetEnumerator();
                        environmentEnum.MoveNext();
                    }
                    environmentEnum.Current.Value.Play();
                    break;

                default:
                    break;
            }
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

        public void AddSoundIter(string name, string URL, string type)
        {
            string extention = URL.Substring(URL.Length - 3, 3);

            switch (type)
            {
                case "environment":
                    if (environmentSounds.ContainsKey(name))
                    {
                        throw new Exception("toks garsas jau yra environment garsu sarase");
                    }
                    switch (extention.ToLower())
                    {
                        case "mp3":
                            environmentSounds.Add(name, new SoundsPlayerMP3(URL));
                            break;
                        case "wav":
                            environmentSounds.Add(name, new SoundsPlayerWAV(URL));
                            break;
                    }
                    break;
                case "shooting":
                    if (shootingSounds.Any(x => x.getName() == name))
                    {
                        throw new Exception("toks garsas jau yra shooting garsu sarase");
                    }
                    switch (extention.ToLower())
                    {
                        case "mp3":
                            shootingSounds.Add(new Sound(name, new SoundsPlayerMP3(URL)));
                            break;
                        case "wav":
                            shootingSounds.Add(new Sound(name, new SoundsPlayerWAV(URL)));
                            break;
                    }
                    break;
                case "background":
                    if (backgroundSounds.Any(x => x.getName() == name))
                    {
                        throw new Exception("toks garsas jau yra background garsu sarase");
                    }
                    switch (extention.ToLower())
                    {
                        case "mp3":
                            backgroundSounds.Add(new Sound(name, new SoundsPlayerMP3(URL)));
                            break;
                        case "wav":
                            backgroundSounds.Add(new Sound(name, new SoundsPlayerWAV(URL)));
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        public void CreateEnumerators()
        {
            shootingEnum = shootingSounds.GetEnumerator();
            backgroundEnum = backgroundSounds.GetEnumerator();
            environmentEnum = environmentSounds.GetEnumerator();
        }
    }
}