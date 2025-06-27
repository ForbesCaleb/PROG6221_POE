namespace CyberAwarenessBotGUI
{
    partial class FormChat
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox chatLog;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Button sendBtn;

        protected override void Dispose(bool disposing)
        { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            chatLog = new System.Windows.Forms.RichTextBox();
            inputBox = new System.Windows.Forms.TextBox();
            sendBtn = new System.Windows.Forms.Button();
            SuspendLayout();
            // chatLog
            chatLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top |
                                  System.Windows.Forms.AnchorStyles.Left) |
                                  System.Windows.Forms.AnchorStyles.Right)));
            chatLog.Location = new System.Drawing.Point(12, 12);
            chatLog.Size = new System.Drawing.Size(760, 380);
            chatLog.ReadOnly = true;
            chatLog.Font = new System.Drawing.Font("Consolas", 10F);
            // inputBox
            inputBox.Location = new System.Drawing.Point(12, 405);
            inputBox.Size = new System.Drawing.Size(660, 23);
            // sendBtn
            sendBtn.Location = new System.Drawing.Point(690, 405);
            sendBtn.Size = new System.Drawing.Size(82, 23);
            sendBtn.Text = "Send";
            sendBtn.Click += sendBtn_Click;
            // FormChat
            ClientSize = new System.Drawing.Size(784, 441);
            Controls.Add(chatLog);
            Controls.Add(inputBox);
            Controls.Add(sendBtn);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "CyberAwarenessBot – Chat";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
