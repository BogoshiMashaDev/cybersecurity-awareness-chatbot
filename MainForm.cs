using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberSecurityBotPart2
{
    public partial class MainForm : Form
    {
        private readonly UserMemory memory;
        private readonly TaskService taskService;
        private readonly QuizService quizService;
        private readonly ActivityLogService activityLogService;
        private readonly ChatbotEngine chatbot;

        public MainForm()
        {
            InitializeComponent();

            memory = new UserMemory();
            taskService = new TaskService();
            quizService = new QuizService();
            activityLogService = new ActivityLogService();

            chatbot = new ChatbotEngine(memory, taskService, quizService, activityLogService);

            AudioPlayer.PlayGreeting();

            activityLogService.AddActivity("Chatbot application started.");

            AddBotMessage("Hello! Welcome to the Cybersecurity Awareness Bot.");
            AddBotMessage("You can ask me about passwords, phishing, scams, privacy, malware, safe browsing, tasks, reminders, or the quiz.");
            AddBotMessage("Examples: Add a task to enable two-factor authentication | Start quiz | Show activity log");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            ProcessInput("Start quiz", true);
        }

        private void btnShowTasks_Click(object sender, EventArgs e)
        {
            ProcessInput("Show my tasks", true);
        }

        private void btnActivityLog_Click(object sender, EventArgs e)
        {
            ProcessInput("Show activity log", true);
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
                e.SuppressKeyPress = true;
            }
        }

        private void SendMessage()
        {
            string userInput = txtInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                AddBotMessage("Please type something before pressing Send.");
                return;
            }

            ProcessInput(userInput, true);

            txtInput.Clear();
            txtInput.Focus();
        }

        private void ProcessInput(string userInput, bool showUserMessage)
        {
            if (showUserMessage)
            {
                AddUserMessage(userInput);
            }

            string response = chatbot.GetResponse(userInput);
            AddBotMessage(response);
        }

        private void AddUserMessage(string message)
        {
            rtbChat.SelectionColor = Color.DodgerBlue;
            rtbChat.AppendText($"You [{DateTime.Now:HH:mm}]: {message}" + Environment.NewLine + Environment.NewLine);
            rtbChat.ScrollToCaret();
        }

        private void AddBotMessage(string message)
        {
            rtbChat.SelectionColor = Color.DarkGreen;
            rtbChat.AppendText($"Bot [{DateTime.Now:HH:mm}]: {message}" + Environment.NewLine + Environment.NewLine);
            rtbChat.ScrollToCaret();
        }
    }
}