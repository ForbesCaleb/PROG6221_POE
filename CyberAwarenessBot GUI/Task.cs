using System;

namespace CyberAwarenessBotGUI
{
    public class Task
    {
        public string Title { get; }
        public DateTime? Reminder { get; }
        public bool IsComplete { get; private set; }

        public Task(string title, DateTime? reminder)
        {
            Title = title;
            Reminder = reminder;
        }

        public void MarkComplete() => IsComplete = true;

        public override string ToString() =>
            $"{(IsComplete ? "✅" : "⏳")} {Title}" +
            (Reminder is null ? "" : $" (remind {Reminder:yyyy-MM-dd})");

        internal static async System.Threading.Tasks.Task Delay(int v)
        {
           
        }
    }
}


