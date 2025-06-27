using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CyberAwarenessBotGUI
{
    public class ResponseGenerator
    {
        // ── dependencies ───────────────────────────────────────────────
        private readonly MemoryHandler _mem;
        private readonly TaskAssistant _tasks;
        private readonly QuizManager _quiz;
        private readonly ActivityLog _log;
        private readonly Random _rng = new();

        // ── keyword dictionaries & generic tips ───────────────────────
        private readonly Dictionary<string, List<string>> _kw = new();
        private readonly List<string> _tips = new()
        {
            "Remember to keep your software up to date.",
            "Enable two-factor authentication wherever you can.",
            "Use a password manager to create strong, unique passwords.",
            "Back up important data regularly in case of ransomware.",
            "Be cautious when clicking links in unexpected emails."
        };

        public ResponseGenerator(
            MemoryHandler m,
            TaskAssistant t,
            QuizManager q,
            ActivityLog l)
        {
            _mem = m;
            _tasks = t;
            _quiz = q;
            _log = l;
            InitKeywords();
        }

        // ───────────────────────────────────────────────────────────────
        //  MAIN ENTRY
        // ───────────────────────────────────────────────────────────────
        public string Generate(
            string input,
            Func<string, string?> ask,
            Action<string> tell)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "I didn’t understand that.";

            //------------------------------------------------------------
            // 1) REMEMBER user interest
            //------------------------------------------------------------
            var remember = Regex.Match(
                input, @"topic i am most interested about is (.*)",
                RegexOptions.IgnoreCase);

            if (remember.Success)
            {
                var topic = remember.Groups[1].Value.Trim();
                _mem.Remember("interest", topic);
                _log.Add($"Interest saved: {topic}");
                return $"Got it! I'll remember your interest in {topic}.";
            }

            //------------------------------------------------------------
            // 2) RECALL interest  (return immediately!)
            //------------------------------------------------------------
            if (Regex.IsMatch(input, @"remind me what i am interested in",
                              RegexOptions.IgnoreCase))
            {
                var interest = _mem.Recall("interest");
                return interest is null
                    ? "I don’t recall your interests yet."
                    : $"Your favourite topic was “{interest}”.";
            }

            //------------------------------------------------------------
            // 3) TASK commands
            //------------------------------------------------------------
            if (Regex.IsMatch(input, @"add task", RegexOptions.IgnoreCase))
                return AddTask(input);

            if (Regex.IsMatch(input, @"list tasks", RegexOptions.IgnoreCase))
                return _tasks.List();

            if (Regex.IsMatch(input, @"complete task", RegexOptions.IgnoreCase))
                return TaskNum(input, complete: true);

            if (Regex.IsMatch(input, @"delete task", RegexOptions.IgnoreCase))
                return TaskNum(input, complete: false);

            //------------------------------------------------------------
            // 4) QUIZ
            //------------------------------------------------------------
            if (Regex.IsMatch(input, @"\b(start|take)?\s*quiz\b",
                              RegexOptions.IgnoreCase))
            {
                _log.Add("Quiz started");
                string summary = _quiz.Run(ask, tell);   // returns summary with praise
                _log.Add(summary);
                return summary;
            }

            //------------------------------------------------------------
            // 5) ACTIVITY LOG
            //------------------------------------------------------------
            if (Regex.IsMatch(input, @"show log", RegexOptions.IgnoreCase))
                return _log.Show();

            //------------------------------------------------------------
            // 6) SENTIMENT + KEYWORD
            //------------------------------------------------------------
            string sentiment = DetectSentiment(input);
            string keyword = KeywordTip(input);

            if (!string.IsNullOrEmpty(sentiment))
            {
                if (string.IsNullOrEmpty(keyword))
                    keyword = _tips[_rng.Next(_tips.Count)];
                return sentiment + keyword;
            }

            if (!string.IsNullOrEmpty(keyword))
                return keyword;

            return "I'm not sure I understand.";
        }

        // ── helper: add task ───────────────────────────────────────────
        private string AddTask(string text)
        {
            var m = Regex.Match(text,
                @"add task\s*-\s*(.+?)(?:;\s*remind\s+(.*))?$",
                RegexOptions.IgnoreCase);

            if (!m.Success)
                return "Format: add task - Title ; remind in 3 days";

            string title = m.Groups[1].Value.Trim();
            string rawDate = m.Groups[2].Value.Trim();
            DateTime? when = null;

            if (rawDate.Length > 0)
            {
                var rel = Regex.Match(rawDate, @"in (\d+) days?", RegexOptions.IgnoreCase);
                if (rel.Success)
                    when = DateTime.Today.AddDays(int.Parse(rel.Groups[1].Value));
                else if (DateTime.TryParseExact(
                             rawDate,
                             new[] { "yyyy-MM-dd", "dd/MM/yyyy" },
                             CultureInfo.InvariantCulture,
                             DateTimeStyles.None,
                             out var dt))
                    when = dt;
            }

            _log.Add($"Task added: {title}");
            return _tasks.Add(title, when);
        }

        // ── helper: complete/delete task ───────────────────────────────
        private string TaskNum(string text, bool complete)
        {
            var m = Regex.Match(text, @"(complete|delete) task (\d+)",
                                RegexOptions.IgnoreCase);
            if (!m.Success)
                return "Specify task number.";

            int n = int.Parse(m.Groups[2].Value);
            _log.Add($"{(complete ? "Completed" : "Deleted")} task {n}");
            return complete ? _tasks.Complete(n) : _tasks.Delete(n);
        }

        // ── helper: sentiment detection ───────────────────────────────
        private static string DetectSentiment(string s)
        {
            s = s.ToLower();
            if (s.Contains("worried") || s.Contains("stressed"))
                return "It's completely understandable to feel that way. ";
            if (s.Contains("curious") || s.Contains("interested"))
                return "That’s great! Curiosity keeps you safe. ";
            if (s.Contains("confused") || s.Contains("uncertain"))
                return "No worries! Let’s break it down. ";
            if (s.Contains("excited") || s.Contains("happy"))
                return "Awesome! Your enthusiasm is contagious. ";
            return "";
        }

        // ── helper: keyword tips ───────────────────────────────────────
        private string KeywordTip(string s)
        {
            foreach (var k in _kw.Keys)
                if (s.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0)
                    return _kw[k][_rng.Next(_kw[k].Count)];
            return "";
        }

        // ── initialise keyword dictionary ─────────────────────────────
        private void InitKeywords()
        {
            _kw["password"] = new()
            {
                "Use unique passwords for every account.",
                "Consider a password manager."
            };
            _kw["phishing"] = new()
            {
                "Beware of unexpected links or attachments.",
                "Verify sender addresses before responding."
            };
            _kw["privacy"] = new()
            {
                "Review your privacy settings regularly."
            };
            _kw["firewall"] = new()
            {
                "Firewalls block unauthorised network access."
            };
            _kw["hackers"] = new()
            {
                "Hackers exploit weak passwords—stay strong!"
            };
            _kw["authentication"] = new()
            {
                "Enable multi-factor authentication whenever possible."
            };
            // NEW 2FA KEYWORD
            _kw["2fa"] = new()
            {
                "Enable two-factor authentication for an extra layer of security.",
                "2FA protects you even if a password leaks—turn it on wherever possible.",
                "Authenticator apps are safer than SMS: consider Google Authenticator or Authy."
            };
        }
    }
}

