namespace CyberAwarenessBotGUI
{
    partial class InputDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.TextBox txtAnswer;
        private System.Windows.Forms.Button btnOK;

        protected override void Dispose(bool disposing)
        { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            lblPrompt = new System.Windows.Forms.Label();
            txtAnswer = new System.Windows.Forms.TextBox();
            btnOK = new System.Windows.Forms.Button();
            SuspendLayout();
            // lblPrompt
            lblPrompt.AutoSize = true;
            lblPrompt.Location = new System.Drawing.Point(12, 9);
            // txtAnswer
            txtAnswer.Location = new System.Drawing.Point(15, 35);
            txtAnswer.Width = 250;
            // btnOK
            btnOK.Location = new System.Drawing.Point(190, 70);
            btnOK.Text = "OK";
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Click += btnOK_Click;
            // InputDialog
            AcceptButton = btnOK;
            ClientSize = new System.Drawing.Size(280, 110);
            Controls.Add(lblPrompt);
            Controls.Add(txtAnswer);
            Controls.Add(btnOK);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Input";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

