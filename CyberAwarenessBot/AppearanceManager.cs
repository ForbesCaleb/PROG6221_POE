using System;

public class AppearanceManager
{
 public void ShowBotMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nBot 🤖: {message}");
        Console.ResetColor();
    }

    public void ShowUserPrompt()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\nYou 🧠: ");
        Console.ResetColor();
    }

    public void ShowDivider()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(new string('-', 40));
        Console.ResetColor();
    }

    public void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[Error]: {message}");
        Console.ResetColor();
    }

    public void ShowSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"✔ {message}");
        Console.ResetColor();
    }
}

