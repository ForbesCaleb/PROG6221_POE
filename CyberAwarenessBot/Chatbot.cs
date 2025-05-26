// Chatbot.cs
using System;

public class Chatbot
{
    private string userName;
    private MemoryHandler memoryHandler;
    private ResponseGenerator responseGenerator;
    private GreetingManager greetingManager;
    private AppearanceManager appearanceManager;

    public Chatbot()
    {
        memoryHandler = new MemoryHandler();
        responseGenerator = new ResponseGenerator(memoryHandler);
        greetingManager = new GreetingManager();
        appearanceManager = new AppearanceManager();
    }

    public void Start()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        greetingManager.DisplayAsciiArt("files/ascii.txt");
        greetingManager.PlayVoiceGreeting();
        GreetUser();
        RunInteractionLoop();
    }

    private void GreetUser()
    {
        Console.Write("What's your name? ");
        userName = Console.ReadLine();
        Console.WriteLine($"Nice to meet you, {userName}!");

        if (!string.IsNullOrWhiteSpace(userName))
        {
            memoryHandler.Remember("name", userName);
        }
    }

    private void RunInteractionLoop()
    {
        string input;

        do
        {
            appearanceManager.ShowDivider();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Ask a cybersecurity question or type 'exit' to quit:");
            Console.ResetColor();
            appearanceManager.ShowUserPrompt();
            input = Console.ReadLine()?.ToLower();

            string response = responseGenerator.GenerateResponse(input);
            appearanceManager.ShowBotMessage(response);

        } while (input != "exit");
    }
}
