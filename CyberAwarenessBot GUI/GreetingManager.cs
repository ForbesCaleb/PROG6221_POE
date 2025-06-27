using System.IO;
using System.Media;
using System.Windows.Forms;

namespace CyberAwarenessBotGUI
{
    public class GreetingManager
    {
        public void PlayVoiceGreeting()
        {
            string filePath = "files\\greeting.wav";
            if (File.Exists(filePath))
            {
                try
                {
                    using (var player = new SoundPlayer(filePath))
                    {
                        player.PlaySync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error playing greeting: " + ex.Message);
                }
            }
        }

        public string GetAsciiGreeting()
        {
            string filePath = "files\\ascii.txt";
            return File.Exists(filePath) ? File.ReadAllText(filePath) : "CyberAwarenessBot";
        }
    }
}

