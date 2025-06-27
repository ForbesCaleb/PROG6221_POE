using System.Collections.Generic;
using System.Linq;

public class ActivityLog
{
    private readonly LinkedList<string> log = new();
    private const int MaxEntries = 50;

    public void Add(string entry)
    {
        log.AddFirst($"{System.DateTime.Now:HH:mm} – {entry}");
        while (log.Count > MaxEntries) log.RemoveLast();
    }

    public string Show(int count = 10) =>
        log.Count == 0
        ? "Log is empty."
        : string.Join(System.Environment.NewLine, log.Take(count));
}

