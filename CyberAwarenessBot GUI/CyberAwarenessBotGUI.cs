using System;
using CyberAwarenessBotGUI;

namespace CyberAwarenessBotGUI
{
    /// <summary>
    /// Chat engine wrapper — connects UI to ResponseGenerator.
    /// </summary>
    public class CyberAwarenessBotGUI
    {
        private string? _userName;

        private readonly MemoryHandler _mem = new();
        private readonly TaskAssistant _tasks = new();
        private readonly QuizManager _quiz = new();
        private readonly ActivityLog _log = new();
        private readonly ResponseGenerator _engine;

        private readonly Action<string> _print;
        private readonly Func<string, string?> _ask;

        public CyberAwarenessBotGUI(Action<string> printer,
                                    Func<string, string?> asker)
        {
            _print = printer;
            _ask = asker;

            _engine = new ResponseGenerator(_mem, _tasks, _quiz, _log);

            new GreetingManager().PlayVoiceGreeting();
            _print("Hello! What's your name?");
        }

        public void ProcessInput(string input)
        {
            if (_userName is null)
            {
                _userName = input.Trim();
                _mem.Remember("name", _userName);
                _print($"Nice to meet you, {_userName}!");
                return;
            }

            // reminder check
            foreach (var t in _tasks.DueToday())
                _print($"🔔 Reminder: {t.Title} is due today!");

            // exit command
            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                _print($"Goodbye {_userName}, and stay safe online!");
                return;
            }

            // delegate to the brain
            var reply = _engine.Generate(input, _ask, _print);
            if (!string.IsNullOrEmpty(reply))
                _print(reply);
        }
    }
}

