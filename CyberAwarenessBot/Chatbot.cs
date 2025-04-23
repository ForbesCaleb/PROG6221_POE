
using System.Media;

public class Chatbot
{
    private string userName;

    public void Start()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        DisplayAsciiArt("files/ascii.txt");
        PlayVoiceGreeting();
    }

    static void DisplayAsciiArt(string filePath)
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

    static void PlayVoiceGreeting()
    {
        string filePath = "files/greeting.wav"; // Ensure the file is in the correct directory  
        using (SoundPlayer player = new SoundPlayer(filePath))
        {
            player.PlaySync(); // Use Play() for async playback  
        }
    }
}