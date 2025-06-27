using System;
using System.Collections.Generic;
using System.Linq;

public class TaskAssistant
{
    private readonly List<Task> tasks = new();
    public IReadOnlyList<Task> Tasks => tasks;

    //------------------------------------------------------------------
    // CRUD operations
    //------------------------------------------------------------------
    public string Add(string title, string description, DateTime? reminder)
    {
        tasks.Add(new Task(title, description, reminder));
        return $"Task “{title}” added{(reminder is null ? "" : $" – I’ll remind you on {reminder:yyyy-MM-dd}")}.";
    }

    public string Complete(int index)
    {
        if (index < 1 || index > tasks.Count) return "No task at that number.";
        tasks[index - 1].MarkComplete();
        return $"Marked task {index} as complete.";
    }

    public string Delete(int index)
    {
        if (index < 1 || index > tasks.Count) return "No task at that number.";
        var removed = tasks[index - 1].Title;
        tasks.RemoveAt(index - 1);
        return $"Deleted “{removed}”.";
    }

    public string List()
    {
        if (tasks.Count == 0) return "You have no tasks yet.";
        return string.Join(Environment.NewLine,
            tasks.Select((t, i) => $"{i + 1}. {t}"));
    }

    //------------------------------------------------------------------
    // Reminder helper – called once per loop from Chatbot
    //------------------------------------------------------------------
    public IEnumerable<Task> DueToday() =>
        tasks.Where(t => !t.IsCompleted &&
                         t.ReminderDate is not null &&
                         t.ReminderDate.Value.Date <= DateTime.Today);
}

