using System;
using System.Windows.Forms;

namespace CyberAwarenessBotGUI
{
    public partial class FormChat : Form
    {
        private readonly CyberAwarenessBotGUI bot;

        public FormChat()
        {
            InitializeComponent();
            bot = new CyberAwarenessBotGUI(AppendBot, AskUser);
            AppendBot("Welcome to the Cybersecurity Awareness Bot!");
        }

        private void sendBtn_Click(object? s, EventArgs e)
        {
            var input = inputBox.Text.Trim();
            if (input.Length == 0) return;
            chatLog.AppendText($"You: {input}\r\n");
            bot.ProcessInput(input);
            inputBox.Clear();
        }

        private void AppendBot(string msg) => chatLog.AppendText($"Bot: {msg}\r\n");
        private string? AskUser(string prompt)
        {
            using var dlg = new InputDialog(prompt);
            return dlg.ShowDialog() == DialogResult.OK ? dlg.Answer : null;
        }
    }
}



