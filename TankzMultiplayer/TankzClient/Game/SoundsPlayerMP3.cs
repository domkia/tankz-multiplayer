using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace TankzClient.Game
{
    public class SoundsPlayerMP3 : ISounds
    {
        WindowsMediaPlayer mp3player;

        public SoundsPlayerMP3(string URL, bool autoplay = true)
        {
            Console.WriteLine("ADAPTER new SoundsPlayerMP3()");
            this.mp3player = new WindowsMediaPlayer();
            if (autoplay == false)
            {
                this.mp3player.PlayStateChange += PauseOnLoaded;
            }
            string url = GetAbsolutePath(URL);
            this.mp3player.URL = url;
        }

        /// Use event to make sure mp3 does not auto play
        private void PauseOnLoaded(int NewState)
        {
            if ((WMPPlayState)NewState == WMPPlayState.wmppsPlaying)
            {
                mp3player.controls.pause();
                mp3player.PlayStateChange -= PauseOnLoaded;
            }
        }

        /// <summary>
        /// Combine relative project path with res/sounds folder
        /// </summary>
        /// <param name="URL">relative to res</param>
        /// <returns>Aboslute path</returns>
        private string GetAbsolutePath(string URL)
        {
            int index = URL.IndexOf("res/sounds/");
            string res = URL.Substring(index, URL.Length - index);
            res = res.Replace("/", "\\");
            string projectPath = Environment.CurrentDirectory;
            projectPath = projectPath.Substring(0, projectPath.IndexOf("bin"));
            return Path.Combine(projectPath, res);
        }

        public void Play()
        {
            Console.WriteLine("ADAPTER SoundsPlayerMP3: Play()");
            mp3player.controls.play();
        }
    }
}
