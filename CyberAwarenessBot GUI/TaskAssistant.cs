using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberAwarenessBotGUI
{
    public class TaskAssistant
    {
        private readonly List<Task> _tasks = new();

        // ─────────────────────────────────────────────
        // ADD
        // ─────────────────────────────────────────────
        public string Add(string title, DateTime? reminder)
        {
            _tasks.Add(new Task(title, reminder));
            return $"Task “{title}” added" +
                   (reminder is null ? "" : $" – I’ll remind you on {reminder:yyyy-MM-dd}.");
        }

        // ─────────────────────────────────────────────
        // LIST
        // ─────────────────────────────────────────────
        public string List() =>
            _tasks.Count == 0
                ? "No tasks yet."
                : string.Join(Environment.NewLine,
                              _tasks.Select((t, i) => $"{i + 1}. {t}"));

        // ─────────────────────────────────────────────
        // COMPLETE n
        // ─────────────────────────────────────────────
        public string Complete(int n)
        {
            if (n < 1 || n > _tasks.Count)
                return "No such task.";

            _tasks[n - 1].MarkComplete();
            return $"Marked task {n} as complete.";
        }

        // ─────────────────────────────────────────────
        // DELETE n
        // ─────────────────────────────────────────────
        public string Delete(int n)
        {
            if (n < 1 || n > _tasks.Count)
                return "No such task.";

            string title = _tasks[n - 1].Title;
            _tasks.RemoveAt(n - 1);
            return $"Deleted “{title}”.";
        }

        // ─────────────────────────────────────────────
        // REMINDERS DUE TODAY
        // ─────────────────────────────────────────────
        public IEnumerable<Task> DueToday() =>
            _tasks.Where(t => !t.IsComplete &&
                              t.Reminder?.Date <= DateTime.Today);
    }
}

