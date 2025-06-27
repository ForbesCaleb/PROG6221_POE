using System;
using System.Collections.Generic;

namespace CyberAwarenessBotGUI
{
    public class QuizManager
    {
        private readonly List<QuizQuestion> _qs = new();
        private readonly Random _rng = new();

        public QuizManager()
        {
            // ❶ 2FA
            _qs.Add(new QuizQuestion(
                "Which of these is a form of two-factor authentication?",
                new[] { "Password", "Captcha", "SMS code", "Username" }, 2,
                "A time-based SMS (or authenticator app) code is a second factor: something you have."));

            // ❷ HTTPS ≠ guaranteed safe
            _qs.Add(new QuizQuestion(
                "True or False: All websites that use HTTPS are guaranteed to be safe.",
                new[] { "True", "False" }, 1,
                "HTTPS secures the connection, but it doesn’t prove the site itself is trustworthy."));

            // ❸ Phishing email red flag
            _qs.Add(new QuizQuestion(
                "Which is a common sign of a phishing email?",
                new[] { "Personalised greeting", "Perfect grammar",
                        "Urgent demand for personal info", "Email from coworker" }, 2,
                "A sense of urgency or request for personal data is a classic phishing trait."));

            // ❹ Password length
            _qs.Add(new QuizQuestion(
                "Security experts recommend passwords be at least how many characters?",
                new[] { "4", "8", "12", "20" }, 2,
                "12+ characters makes brute-force attacks dramatically harder."));

            // ❺ Software updates
            _qs.Add(new QuizQuestion(
                "True or False: Delaying software updates can expose you to known vulnerabilities.",
                new[] { "True", "False" }, 0,
                "Updates patch security flaws; postponing them keeps the door open to attackers."));

            // ❻ Public Wi-Fi
            _qs.Add(new QuizQuestion(
                "What’s the safest way to use public Wi-Fi?",
                new[] { "Use any site - Wi-Fi is safe",
                        "Only social media sites",
                        "Use a VPN connection",
                        "Disable your firewall" }, 2,
                "A VPN encrypts your traffic so eavesdroppers on public Wi-Fi can’t read it."));

            // ❼ Social-engineering phone call
            _qs.Add(new QuizQuestion(
                "A stranger calls claiming to be IT support and asks for your login. You should…",
                new[] { "Give the password", "Hang up and call the official help-desk",
                        "Press * to mute phone audio", "Ignore and hope they don’t call again" }, 1,
                "Always verify via official channels before sharing credentials."));

            // ❽ Ransomware best practice
            _qs.Add(new QuizQuestion(
                "The best defence against ransomware is:",
                new[] { "Hourly password changes", "Regular off-site backups",
                        "Turning off the firewall", "Using the same password everywhere" }, 1,
                "Backups let you restore data without paying ransom."));

            // ❾ Browser padlock icon
            _qs.Add(new QuizQuestion(
                "True or False: The padlock icon in a browser bar means the site is legitimate.",
                new[] { "True", "False" }, 1,
                "It only means the connection is encrypted, not that the site is trustworthy."));

            // ❿ Strongest password
            _qs.Add(new QuizQuestion(
                "Which password is strongest?",
                new[] { "P@ssw0rd", "Pass1234!", "T0t@lLyR@nd0m#987", "Football2024" }, 2,
                "Long, random, and mixed-case with symbols and numbers beats predictable patterns."));
        }

        public int Count => _qs.Count;

        /// <summary>
        /// Runs the quiz and returns a summary string with praise.
        /// </summary>
        public string Run(Func<string, string?> ask, Action<string> tell)
        {
            int score = 0;

            for (int i = 0; i < _qs.Count; i++)
            {
                var q = _qs[i];
                tell($"\nQ{i + 1}: {q.Text}");
                for (int j = 0; j < q.Options.Length; j++)
                    tell($"   {(char)('A' + j)}) {q.Options[j]}");

                string? ans = ask("Your answer (A-D):")?.Trim().ToUpper();
                int idx = ans?.Length > 0 ? ans[0] - 'A' : -1;

                if (idx == q.CorrectIndex) { tell("✔ Correct!"); score++; }
                else tell($"✘ Incorrect. {q.Explanation}");
            }

            double ratio = (double)score / _qs.Count;
            string summary = $"You scored {score}/{_qs.Count}.";

            summary += ratio >= 0.8 ? " 🎉 Great job!"
                     : ratio >= 0.5 ? " 👍 Well done!"
                     : " Keep practising!";

            return summary;
        }
    }
}

