using System;
using System.Windows.Forms;

namespace CyberAwarenessBotGUI
{
    public partial class InputDialog : Form
    {
        public string Answer => txtAnswer.Text.Trim();

        public InputDialog(string prompt)
        {
            InitializeComponent();
            lblPrompt.Text = prompt;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAnswer.Text))
            {
                MessageBox.Show("Please enter something.", "Input Needed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
