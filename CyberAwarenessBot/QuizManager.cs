using System;
using System.Collections.Generic;
using CyberAwarenessBot;

public class QuizManager
{
    private readonly List<QuizQuestion> questions = new();
    private readonly Random rng = new();

    public QuizManager()
    {
        //------------------- 10 sample Qs ------------------------------
        questions.Add(new("Which of these is a form of two-factor authentication?",
            new[] { "Password", "Captcha", "SMS code", "Username" }, 2,
            "A temporary SMS code is a second factor (something you have)."));

        questions.Add(new("True or False: All HTTPS websites are guaranteed safe.",
            new[] { "True", "False" }, 1,
            "HTTPS only secures the connection; it doesn’t guarantee the site’s intent."));

        // …add at least 8 more as required by the rubric
        //---------------------------------------------------------------
    }

    public string RunQuiz(AppearanceManager ui, ActivityLog log)
    {
        int score = 0;
        ui.ShowBotMessage($"📚 Starting a {questions.Count}-question cybersecurity quiz!");
        for (int i = 0; i < questions.Count; i++)
        {
            var q = questions[i];
            ui.ShowBotMessage($"\nQ{i + 1}: {q.Text}");
            for (int j = 0; j < q.Options.Length; j++)
                Console.WriteLine($"   {Convert.ToChar(65 + j)}) {q.Options[j]}");

            ui.ShowUserPrompt();
            var ans = Console.ReadLine()?.Trim().ToUpper();
            int idx = ans.Length > 0 ? ans[0] - 'A' : -1;

            if (idx == q.CorrectIndex)
            {
                ui.ShowSuccess("Correct!");
                score++;
            }
            else
            {
                ui.ShowError($"Incorrect. {q.Explanation}");
            }
        }

        string summary = $"You scored {score}/{questions.Count}.";
        ui.ShowBotMessage(summary);
        log.Add($"Finished quiz – {summary}");
        return summary;
    }
}

