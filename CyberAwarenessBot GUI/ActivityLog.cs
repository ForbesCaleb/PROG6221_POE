using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberAwarenessBotGUI
{
    public class ActivityLog
    {
        private readonly LinkedList<string> _log = new();
        private const int Max = 50;

        public void Add(string entry)
        {
            _log.AddFirst($"{DateTime.Now:HH:mm} – {entry}");
            while (_log.Count > Max) _log.RemoveLast();
        }

        public string Show(int n = 10) =>
            _log.Count == 0 ? "Log empty." : string.Join(Environment.NewLine, _log.Take(n));
    }
}
