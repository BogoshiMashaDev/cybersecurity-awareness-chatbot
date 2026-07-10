using System.Drawing;
using System.Windows.Forms;

namespace CyberSecurityBotPart2
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblAscii;
        private RichTextBox rtbChat;
        private TextBox txtInput;
        private Button btnSend;
        private Button btnStartQuiz;
        private Button btnShowTasks;
        private Button btnActivityLog;
        private Panel panelBottom;
        private Panel panelQuickActions;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblAscii = new Label();
            rtbChat = new RichTextBox();
            txtInput = new TextBox();
            btnSend = new Button();
            btnStartQuiz = new Button();
            btnShowTasks = new Button();
            btnActivityLog = new Button();
            panelBottom = new Panel();
            panelQuickActions = new Panel();
            panelBottom.SuspendLayout();
            panelQuickActions.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.FromArgb(20, 52, 89);
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(950, 60);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "Cybersecurity Awareness Chatbot - POE";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblAscii
            // 
            lblAscii.BackColor = Color.FromArgb(34, 87, 122);
            lblAscii.Dock = DockStyle.Top;
            lblAscii.Font = new Font("Consolas", 10F, FontStyle.Bold);
            lblAscii.ForeColor = Color.White;
            lblAscii.Location = new Point(0, 60);
            lblAscii.Name = "lblAscii";
            lblAscii.Size = new Size(950, 150);
            lblAscii.TabIndex = 3;
            lblAscii.Text = "[LOCK] STAY SAFE ONLINE [LOCK]\r\n\r\n   ____   __   __  ____  \r\n  / ___| / _| / _|/ ___|\r\n | |    | |_ | |_| |    \r\n | |___ |  _||  _| |___ \r\n  \\____||_|  |_|  \\____|";
            lblAscii.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // rtbChat
            // 
            rtbChat.BackColor = Color.WhiteSmoke;
            rtbChat.BorderStyle = BorderStyle.None;
            rtbChat.Dock = DockStyle.Fill;
            rtbChat.Font = new Font("Segoe UI", 11F);
            rtbChat.Location = new Point(0, 265);
            rtbChat.Name = "rtbChat";
            rtbChat.ReadOnly = true;
            rtbChat.Size = new Size(950, 375);
            rtbChat.TabIndex = 0;
            rtbChat.Text = "";
            // 
            // txtInput
            // 
            txtInput.Dock = DockStyle.Fill;
            txtInput.Font = new Font("Segoe UI", 11F);
            txtInput.Location = new Point(10, 10);
            txtInput.Name = "txtInput";
            txtInput.PlaceholderText = "Type your cybersecurity question, task, reminder, or quiz answer here...";
            txtInput.Size = new Size(830, 32);
            txtInput.TabIndex = 0;
            txtInput.KeyDown += txtInput_KeyDown;
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.FromArgb(0, 120, 215);
            btnSend.Dock = DockStyle.Right;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSend.ForeColor = Color.White;
            btnSend.Location = new Point(840, 10);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(100, 40);
            btnSend.TabIndex = 1;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // btnStartQuiz
            // 
            btnStartQuiz.BackColor = Color.FromArgb(0, 120, 215);
            btnStartQuiz.Dock = DockStyle.Left;
            btnStartQuiz.FlatStyle = FlatStyle.Flat;
            btnStartQuiz.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStartQuiz.ForeColor = Color.White;
            btnStartQuiz.Location = new Point(10, 10);
            btnStartQuiz.Name = "btnStartQuiz";
            btnStartQuiz.Size = new Size(130, 35);
            btnStartQuiz.TabIndex = 2;
            btnStartQuiz.Text = "Start Quiz";
            btnStartQuiz.UseVisualStyleBackColor = false;
            btnStartQuiz.Click += btnStartQuiz_Click;
            // 
            // btnShowTasks
            // 
            btnShowTasks.BackColor = Color.FromArgb(34, 139, 34);
            btnShowTasks.Dock = DockStyle.Left;
            btnShowTasks.FlatStyle = FlatStyle.Flat;
            btnShowTasks.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnShowTasks.ForeColor = Color.White;
            btnShowTasks.Location = new Point(140, 10);
            btnShowTasks.Name = "btnShowTasks";
            btnShowTasks.Size = new Size(130, 35);
            btnShowTasks.TabIndex = 1;
            btnShowTasks.Text = "Show Tasks";
            btnShowTasks.UseVisualStyleBackColor = false;
            btnShowTasks.Click += btnShowTasks_Click;
            // 
            // btnActivityLog
            // 
            btnActivityLog.BackColor = Color.FromArgb(90, 90, 120);
            btnActivityLog.Dock = DockStyle.Left;
            btnActivityLog.FlatStyle = FlatStyle.Flat;
            btnActivityLog.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnActivityLog.ForeColor = Color.White;
            btnActivityLog.Location = new Point(270, 10);
            btnActivityLog.Name = "btnActivityLog";
            btnActivityLog.Size = new Size(130, 35);
            btnActivityLog.TabIndex = 0;
            btnActivityLog.Text = "Activity Log";
            btnActivityLog.UseVisualStyleBackColor = false;
            btnActivityLog.Click += btnActivityLog_Click;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(20, 52, 89);
            panelBottom.Controls.Add(txtInput);
            panelBottom.Controls.Add(btnSend);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 640);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(10);
            panelBottom.Size = new Size(950, 60);
            panelBottom.TabIndex = 1;
            // 
            // panelQuickActions
            // 
            panelQuickActions.BackColor = Color.FromArgb(232, 240, 248);
            panelQuickActions.Controls.Add(btnActivityLog);
            panelQuickActions.Controls.Add(btnShowTasks);
            panelQuickActions.Controls.Add(btnStartQuiz);
            panelQuickActions.Dock = DockStyle.Top;
            panelQuickActions.Location = new Point(0, 210);
            panelQuickActions.Name = "panelQuickActions";
            panelQuickActions.Padding = new Padding(10);
            panelQuickActions.Size = new Size(950, 55);
            panelQuickActions.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(950, 700);
            Controls.Add(rtbChat);
            Controls.Add(panelBottom);
            Controls.Add(panelQuickActions);
            Controls.Add(lblAscii);
            Controls.Add(lblTitle);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cybersecurity Awareness Chatbot POE";
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            panelQuickActions.ResumeLayout(false);
            ResumeLayout(false);
        }

    }
}