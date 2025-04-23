
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
        GreetUser();
        RunInteractionLoop();
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
        try
        {
            using (SoundPlayer player = new SoundPlayer(filePath))
            {
                player.PlaySync(); // Use Play() for async playback
            }
        }
        catch (FileNotFoundException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Voice greeting file not found: " + filePath);
            Console.ResetColor();
        }
        catch (InvalidOperationException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Could not play the greeting sound. Reason: " + ex.Message);
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("An unexpected error occurred while playing the greeting sound: " + ex.Message);
            Console.ResetColor();
        }
    }


    private void GreetUser()
    {
        Console.Write("What's your name? ");
        userName = Console.ReadLine();
        Console.WriteLine($"Nice to meet you, {userName}!");
    }

    private void RunInteractionLoop()
    {
        string input;

        do
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nAsk a cybersecurity question or type 'exit' to quit:");
            Console.ResetColor();
            Console.Write("> ");
            input = Console.ReadLine()?.ToLower();

            string response = GenerateResponse(input);
            Console.WriteLine(response);

        } while (input != "exit");
    }

    private string GenerateResponse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "I didn’t quite understand that. Could you rephrase?";

        switch (input)
        {
            case "how are you":
                return "I'm doing well and yourself, but I’m here to help you stay cyber safe!";
            case "what’s your purpose?":
            case "what's your purpose":
                return "I’m here to teach you about cybersecurity basics!";
            case "password safety":
                return "Safe browsing is the habit of using the internet carefully to avoid security threats." +
                    "Use strong passwords and never reuse them across platforms.";
            case "phishing":
                return "Phishing is a cyberattack where attackers trick people into revealing sensitive information." +
                    "Be cautious of suspicious emails with links or attachments.";
            case "safe browsing":
                return "Safe browsing is the practice of using the internet in a way that protects your personal information and devices. " +
                    "Avoid clicking unknown links, and keep your browser updated.";
            default:
                return "I didn’t quite understand that. Could you rephrase?";
        }
    }
}