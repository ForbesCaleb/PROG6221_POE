namespace CyberAwarenessBotGUI
{
    partial class HomeForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.Button startBtn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            txtOutput = new RichTextBox();
            startBtn = new Button();
            SuspendLayout();
            // 
            // txtOutput
            // 
            txtOutput.BorderStyle = BorderStyle.None;
            txtOutput.Dock = DockStyle.Top;
            txtOutput.Font = new Font("Consolas", 12F);
            txtOutput.Location = new Point(0, 0);
            txtOutput.Name = "txtOutput";
            txtOutput.ReadOnly = true;
            txtOutput.Size = new Size(800, 380);
            txtOutput.TabIndex = 0;
            txtOutput.Text = "";
            // 
            // startBtn
            // 
            startBtn.Dock = DockStyle.Bottom;
            startBtn.Location = new Point(0, 410);
            startBtn.Name = "startBtn";
            startBtn.Size = new Size(800, 40);
            startBtn.TabIndex = 1;
            startBtn.Text = "Start Chat";
            startBtn.Click += startBtn_Click;
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtOutput);
            Controls.Add(startBtn);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HomeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CyberAwarenessBot – Welcome";
            Load += HomeForm_Load;
            ResumeLayout(false);
        }
        #endregion
    }
}
