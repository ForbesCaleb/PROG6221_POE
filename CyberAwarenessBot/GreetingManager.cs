using System;
using System.IO;
using System.Media;

public class GreetingManager
{
    public void DisplayAsciiArt(string filePath)
    {
        if (File.Exists(filePath))
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(File.ReadAllText(filePath));
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: ASCII file not found at path: {filePath}");
            Console.ResetColor();
        }
    }

    public void PlayVoiceGreeting()
    {
        string filePath = "files/greeting.wav";
        try
        {
            using (SoundPlayer player = new SoundPlayer(filePath))
            {
                player.PlaySync();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Could not play the greeting sound: " + ex.Message);
            Console.ResetColor();
        }
    }
}

