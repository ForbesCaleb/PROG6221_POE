using System;

public class Task
{
    public string Title { get; }
    public string Description { get; }
    public DateTime? ReminderDate { get; }
    public bool IsCompleted { get; private set; }

    public Task(string title, string description, DateTime? reminderDate)
    {
        Title = title;
        Description = description;
        ReminderDate = reminderDate;
        IsCompleted = false;
    }

    public void MarkComplete() => IsCompleted = true;

    public override string ToString()
    {
        var status = IsCompleted ? "✅ Done " : "⏳ Pending ";
        var remind = ReminderDate is null ? ""
                     : $"(remind on {ReminderDate:yyyy-MM-dd})";
        return $"{status}- {Title} {remind}";
    }
}
