using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberAwarenessBotGUI
{
    public partial class HomeForm : Form
    {
        private readonly GreetingManager _greetingManager = new();

        public HomeForm()
        {
            InitializeComponent();
        }

        // ───────────────────────────────────────────────────────────────
        // Runs when the form loads (event wired in Designer)
        // ───────────────────────────────────────────────────────────────
        private async void HomeForm_Load(object sender, EventArgs e)
        {
            // 1) Show ASCII immediately
            txtOutput.Text = _greetingManager.GetAsciiGreeting();

            // 2) Force the UI to paint now so the text is visible
            Application.DoEvents();

            // 3) Small async delay (optional but keeps things smooth)
            await Task.Delay(200);

            // 4) Play WAV greeting (only once)
            _greetingManager.PlayVoiceGreeting();
        }

        // ───────────────────────────────────────────────────────────────
        // Start Chat button
        // ───────────────────────────────────────────────────────────────
        private void startBtn_Click(object sender, EventArgs e)
        {
            Hide();
            using (var chatForm = new FormChat())
            {
                chatForm.ShowDialog();
            }
            Close();
        }
    }
}







