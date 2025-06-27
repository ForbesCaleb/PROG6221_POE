// ResponseGenerator.cs
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

public class ResponseGenerator
{
    //───────────────────────────────────────────────────────────
    // Dependencies
    //───────────────────────────────────────────────────────────
    private readonly MemoryHandler memory;
    private readonly TaskAssistant tasks;
    private readonly QuizManager quiz;
    private readonly ActivityLog log;
    private readonly AppearanceManager ui;

    // Existing fields
    private Dictionary<string, List<string>> keywordResponses;
    private readonly Random random = new();

    //───────────────────────────────────────────────────────────
    // Constructor
    //───────────────────────────────────────────────────────────
    public ResponseGenerator(
        MemoryHandler memory,
        TaskAssistant tasks,
        QuizManager quiz,
        ActivityLog log,
        AppearanceManager ui)
    {
        this.memory = memory;
        this.tasks = tasks;
        this.quiz = quiz;
        this.log = log;
        this.ui = ui;

        InitializeResponses();
    }

    //───────────────────────────────────────────────────────────
    // Main entry
    //───────────────────────────────────────────────────────────
    public string GenerateResponse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "I didn’t quite understand that. Could you rephrase?";

        //------------------------------------------------------------------
        // (1) MEMORY  ➜  store / recall user's interest
        //------------------------------------------------------------------
        var rememberMatch = Regex.Match(
            input,
            @"topic\s+i\s+am\s+most\s+interested\s+about\s+is\s+(.*)",
            RegexOptions.IgnoreCase);

        if (rememberMatch.Success)
        {
            string topic = rememberMatch.Groups[1].Value.Trim();
            memory.Remember("interest", topic);
            log.Add($"Remembered interest: {topic}");
            return $"Great! I'll remember that you're interested in {topic}. It's a crucial part of staying safe online.";
        }

        if (Regex.IsMatch(input, @"remind\s+me\s+what\s+i\s+am\s+interested\s+in",
                          RegexOptions.IgnoreCase))
        {
            var interest = memory.Recall("interest");
            return interest is null
                ? "I don’t recall your interests yet. Let me know what you're interested in!"
                : $"As someone interested in {interest}, you might want to review the security settings on your accounts.";
        }

        //------------------------------------------------------------------
        // (2) TASK ASSISTANT  ➜  add / list / complete / delete
        //------------------------------------------------------------------
        if (TryHandleTaskCommands(input, out var taskReply))
            return taskReply;

        //------------------------------------------------------------------
        // (3) QUIZ
        //------------------------------------------------------------------
        if (Regex.IsMatch(input, @"\b(start|take)\s+(the\s+)?quiz\b", RegexOptions.IgnoreCase) ||
            input.Equals("quiz", StringComparison.OrdinalIgnoreCase))
        {
            log.Add("Started quiz");
            quiz.RunQuiz(ui, log);        // interactive loop manages its own I/O
            return null;                  // nothing more to say here
        }

        //------------------------------------------------------------------
        // (4) ACTIVITY LOG
        //------------------------------------------------------------------
        if (Regex.IsMatch(input, @"\b(show|view)\s+(activity\s+)?log\b", RegexOptions.IgnoreCase))
        {
            log.Add("Viewed activity log");
            return log.Show();            // last 10 entries
        }

        //------------------------------------------------------------------
        // (5) SENTIMENT + KEYWORD RESPONSES
        //------------------------------------------------------------------
        string sentiment = DetectSentiment(input);
        string keyword = DetectKeywordResponse(input);

        if (!string.IsNullOrEmpty(sentiment) || !string.IsNullOrEmpty(keyword))
            return $"{sentiment}{keyword}";

        return "I'm not sure I understand. Can you try rephrasing?";
    }

    //───────────────────────────────────────────────────────────
    // Task-command router
    //───────────────────────────────────────────────────────────
    private bool TryHandleTaskCommands(string input, out string reply)
    {
        // ADD TASK  ─────────────────────────────────────────────
        var add = Regex.Match(
            input,
            @"add\s+task\s*-\s*(.+?)(?:;\s*remind\s+(.+))?$",
            RegexOptions.IgnoreCase);

        if (add.Success)
        {
            string title = add.Groups[1].Value.Trim();
            string remindRaw = add.Groups[2].Value.Trim();
            DateTime? remind = ParseDateOrOffset(remindRaw);

            reply = tasks.Add(title, description: "", reminder: remind);
            log.Add($"Added task: {title}");
            return true;
        }

        // LIST TASKS ────────────────────────────────────────────
        if (Regex.IsMatch(input, @"\b(list|show)\s+tasks\b", RegexOptions.IgnoreCase))
        {
            reply = tasks.List();
            log.Add("Listed tasks");
            return true;
        }

        // COMPLETE TASK n ──────────────────────────────────────
        var comp = Regex.Match(input, @"complete\s+task\s+(\d+)", RegexOptions.IgnoreCase);
        if (comp.Success)
        {
            int idx = int.Parse(comp.Groups[1].Value);
            reply = tasks.Complete(idx);
            log.Add($"Completed task #{idx}");
            return true;
        }

        // DELETE TASK n ────────────────────────────────────────
        var del = Regex.Match(input, @"delete\s+task\s+(\d+)", RegexOptions.IgnoreCase);
        if (del.Success)
        {
            int idx = int.Parse(del.Groups[1].Value);
            reply = tasks.Delete(idx);
            log.Add($"Deleted task #{idx}");
            return true;
        }

        reply = null;
        return false;
    }

    //───────────────────────────────────────────────────────────
    // Helpers
    //───────────────────────────────────────────────────────────
    private static DateTime? ParseDateOrOffset(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return null;

        // "in 3 days"
        var rel = Regex.Match(raw, @"in\s+(\d+)\s+days?", RegexOptions.IgnoreCase);
        if (rel.Success)
            return DateTime.Today.AddDays(int.Parse(rel.Groups[1].Value));

        // direct date e.g. 2025-07-01 or 01/07/2025
        if (DateTime.TryParseExact(raw,
                                   new[] { "yyyy-MM-dd", "dd/MM/yyyy" },
                                   CultureInfo.InvariantCulture,
                                   DateTimeStyles.None,
                                   out var dt))
            return dt;

        return null;
    }

    private string DetectSentiment(string input)
    {
        input = input.ToLowerInvariant();
        if (input.Contains("worried") || input.Contains("anxious") || input.Contains("stressed"))
            return "It's completely understandable to feel that way. ";
        if (input.Contains("curious") || input.Contains("interested") || input.Contains("keen"))
            return "That's great! Curiosity is key to staying safe online. ";
        if (input.Contains("confused") || input.Contains("unsure") || input.Contains("uncertain"))
            return "No worries! Cybersecurity can be confusing at first. ";
        if (input.Contains("excited") || input.Contains("happy") || input.Contains("glad"))
            return "Awesome! It’s great to see your enthusiasm. ";
        return "";
    }

    private string DetectKeywordResponse(string input)
    {
        foreach (var keyword in keywordResponses.Keys)
            if (input.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                var list = keywordResponses[keyword];
                return list[random.Next(list.Count)];
            }
        return "";
    }

    //───────────────────────────────────────────────────────────
    // Populate canned keyword answers
    //───────────────────────────────────────────────────────────
    private void InitializeResponses()
    {
        keywordResponses = new Dictionary<string, List<string>>
        {
            ["password"] = new()
            {
                "Make sure to use strong, unique passwords for each account.",
                "Avoid using personal details in your passwords.",
                "Consider using a password manager to generate and store passwords."
            },
            ["phishing"] = new()
            {
                "Be cautious of emails asking for personal information.",
                "Look for signs of phishing like poor grammar and suspicious links.",
                "Never click on unknown links or download unexpected attachments."
            },
            ["privacy"] = new()
            {
                "Review the security settings on your accounts regularly.",
                "Be careful what personal information you share online."
            },
            ["firewall"] = new()
            {
                "A firewall protects a computer network from unauthorised access.",
                "It acts as a barrier between trusted and untrusted networks."
            },
            ["hackers"] = new()
            {
                "A hacker is someone who breaks into computer systems.",
                "Some hackers, known as white-hats, help improve security."
            },
            ["authentication"] = new()
            {
                "Authentication checks if someone is who they say they are.",
                "Multi-factor authentication adds an extra layer of security."
            }
        };
    }
}



