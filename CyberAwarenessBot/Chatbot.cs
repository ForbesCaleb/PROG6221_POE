using System;

public class Chatbot
{
    //----------------------------------------------------------
    // Fields
    //----------------------------------------------------------
    private string userName;
    private readonly MemoryHandler memory = new();
    private readonly TaskAssistant tasks = new();
    private readonly QuizManager quiz = new();
    private readonly ActivityLog log = new();
    private readonly AppearanceManager ui = new();
    private readonly GreetingManager greeting = new();
    private readonly ResponseGenerator responder;

    //----------------------------------------------------------
    public Chatbot()
    {
        responder = new ResponseGenerator(memory, tasks, quiz, log, ui);
    }

    //----------------------------------------------------------
    public void Start()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        greeting.DisplayAsciiArt("files/ascii.txt");
        greeting.PlayVoiceGreeting();
        GreetUser();
        RunLoop();
    }

    private void GreetUser()
    {
        Console.Write("What's your name? ");
        userName = Console.ReadLine();
        Console.WriteLine($"Nice to meet you, {userName}!");
        if (!string.IsNullOrWhiteSpace(userName))
            memory.Remember("name", userName);
    }
    private void RunLoop()
    {
        string input;
        do
        {
            //--------------------------------------------------
            // Check for due reminders each turn
            //--------------------------------------------------
            foreach (var t in tasks.DueToday())
                ui.ShowBotMessage($"🔔 Reminder: {t.Title} is due today!");

            ui.ShowDivider();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Ask a cybersecurity question or type 'exit' to quit:");
            Console.ResetColor();
            ui.ShowUserPrompt();
            input = Console.ReadLine()?.Trim();

            if (input?.ToLower() == "exit") break;

            var reply = responder.GenerateResponse(input);
            if (!string.IsNullOrEmpty(reply))
                ui.ShowBotMessage(reply);

        } while (true);

        ui.ShowBotMessage($"Goodbye {userName}, and stay safe online!");
    }
}


