using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Media;

namespace CyberSecurityBotPart2
{
    public class AudioPlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "welcome.wav");

                if (File.Exists(path))
                {
                    SoundPlayer player = new SoundPlayer(path);
                    player.Play();
                }
            }
            catch
            {
                // The application must continue running even if the audio fails.
            }
        }
    }
}
