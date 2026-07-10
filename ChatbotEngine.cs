using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CyberSecurityBotPart2
{
    public class ChatbotEngine
    {
        private readonly UserMemory memory;
        private readonly Random random;
        private readonly Dictionary<string, List<string>> responses;
        private readonly TaskService taskService;
        private readonly QuizService quizService;
        private readonly ActivityLogService activityLogService;

        public ChatbotEngine(
            UserMemory userMemory,
            TaskService taskService,
            QuizService quizService,
            ActivityLogService activityLogService)
        {
            memory = userMemory;
            this.taskService = taskService;
            this.quizService = quizService;
            this.activityLogService = activityLogService;

            random = new Random();

            responses = new Dictionary<string, List<string>>
            {
                {
                    "password",
                    new List<string>
                    {
                        "Use strong passwords with at least 12 characters, including uppercase letters, lowercase letters, numbers, and symbols.",
                        "Avoid using personal details such as your name, birthday, or phone number in your passwords.",
                        "Use a different password for every account. A password manager can help you store them safely.",
                        "Never share your passwords with anyone. Your password protects your personal information."
                    }
                },
                {
                    "phishing",
                    new List<string>
                    {
                        "Be careful of emails or SMS messages asking for your banking details or passwords.",
                        "Check the sender's email address carefully before clicking any links.",
                        "Do not open attachments from unknown senders because they may contain malware.",
                        "Phishing messages often create fear or urgency, such as saying your account will be blocked."
                    }
                },
                {
                    "scam",
                    new List<string>
                    {
                        "Online scams often create urgency, such as saying your account will be blocked immediately.",
                        "If an offer looks too good to be true, it is probably a scam.",
                        "Never send money or personal details to someone you have not verified.",
                        "Always confirm suspicious messages directly with the company using official contact details."
                    }
                },
                {
                    "privacy",
                    new List<string>
                    {
                        "Review your privacy settings on social media and limit what strangers can see.",
                        "Avoid sharing sensitive information such as your ID number, home address, or banking details online.",
                        "Think carefully before posting personal information because it can be copied or misused.",
                        "Use two-factor authentication to add another layer of protection to your private accounts."
                    }
                },
                {
                    "malware",
                    new List<string>
                    {
                        "Malware is harmful software that can damage your device or steal your information.",
                        "Only download apps and files from trusted websites or official app stores.",
                        "Keep your antivirus software and operating system updated.",
                        "Avoid clicking pop-up ads because some can lead to unsafe downloads."
                    }
                },
                {
                    "browsing",
                    new List<string>
                    {
                        "Use secure websites that begin with HTTPS when entering personal information.",
                        "Avoid using public Wi-Fi for online banking or sensitive accounts.",
                        "Keep your browser updated to reduce security risks.",
                        "Do not save passwords on public or shared computers."
                    }
                },
                {
                    "cyberbullying",
                    new List<string>
                    {
                        "Cyberbullying can happen through messages, social media, or online comments. Save evidence and report harmful behaviour.",
                        "Do not respond aggressively to cyberbullying. Block the person, report the account, and speak to someone you trust.",
                        "Protect your privacy online and avoid sharing personal details with people who may misuse them."
                    }
                }
            };
        }

        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Please type a message so I can help you.";
            }

            string lowerInput = input.ToLower().Trim();

            DetectUserName(lowerInput);
            DetectFavouriteTopic(lowerInput);

            string sentimentMessage = DetectSentiment(lowerInput);

            if (quizService.IsQuizActive)
            {
                if (lowerInput.Contains("stop quiz") || lowerInput.Contains("exit quiz"))
                {
                    activityLogService.AddActivity("Quiz stopped by user.");
                    return quizService.StopQuiz();
                }

                activityLogService.AddActivity("Quiz answer submitted.");
                return quizService.SubmitAnswer(input);
            }

            if (IsActivityLogRequest(lowerInput))
            {
                return activityLogService.ShowRecentActivities();
            }

            if (IsQuizStartRequest(lowerInput))
            {
                activityLogService.AddActivity("Quiz started.");
                return quizService.StartQuiz();
            }

            if (IsViewTasksRequest(lowerInput))
            {
                activityLogService.AddActivity("Viewed cybersecurity tasks.");
                return taskService.ViewTasks();
            }

            if (IsCompleteTaskRequest(lowerInput))
            {
                int taskId = ExtractTaskId(lowerInput);

                if (taskId <= 0)
                {
                    return "Please include the task number. Example: Mark task 1 as complete.";
                }

                string result = taskService.MarkTaskAsComplete(taskId);
                activityLogService.AddActivity($"Marked task {taskId} as complete.");
                return result;
            }

            if (IsDeleteTaskRequest(lowerInput))
            {
                int taskId = ExtractTaskId(lowerInput);

                if (taskId <= 0)
                {
                    return "Please include the task number. Example: Delete task 1.";
                }

                string result = taskService.DeleteTask(taskId);
                activityLogService.AddActivity($"Deleted task {taskId}.");
                return result;
            }

            if (IsTaskAddRequest(lowerInput))
            {
                string title = ExtractTaskTitle(input);
                DateTime? reminderDate = ExtractReminderDate(lowerInput);
                string description = GenerateTaskDescription(title);

                string result = taskService.AddTask(title, description, reminderDate);

                if (reminderDate.HasValue)
                {
                    activityLogService.AddActivity($"Task added: '{title}' with reminder for {reminderDate.Value:yyyy-MM-dd}.");
                }
                else
                {
                    activityLogService.AddActivity($"Task added: '{title}' with no reminder.");
                }

                return sentimentMessage + result;
            }

            if (IsFollowUp(lowerInput))
            {
                return sentimentMessage + GetFollowUpResponse();
            }

            foreach (string topic in responses.Keys)
            {
                if (lowerInput.Contains(topic))
                {
                    memory.LastTopic = topic;
                    string response = GetRandomResponse(topic);

                    if (!string.IsNullOrWhiteSpace(memory.UserName))
                    {
                        response = $"{memory.UserName}, {response}";
                    }

                    activityLogService.AddActivity($"Provided cybersecurity guidance about {topic}.");
                    return sentimentMessage + response;
                }
            }

            if (lowerInput.Contains("hello") || lowerInput.Contains("hi") || lowerInput.Contains("hey"))
            {
                return $"Hello {GetDisplayName()}! You can ask me about passwords, phishing, scams, privacy, malware, safe browsing, tasks, reminders, or the quiz.";
            }

            if (lowerInput.Contains("help"))
            {
                return "You can ask about password safety, phishing, scams, privacy, malware, safe browsing, add tasks, view tasks, start quiz, or show activity log.";
            }

            if (lowerInput.Contains("how are you"))
            {
                return "I'm doing well and ready to help you learn how to stay safe online.";
            }

            if (lowerInput.Contains("purpose"))
            {
                return "My purpose is to teach users about cybersecurity awareness and help them practise safer online behaviour.";
            }

            if (lowerInput.Contains("what can i ask"))
            {
                return "You can ask about password safety, phishing, scams, privacy, malware, safe browsing, tasks, reminders, quiz questions, and your activity log.";
            }

            if (lowerInput.Contains("remember") || lowerInput.Contains("recall"))
            {
                return RecallMemory();
            }

            return "I'm not sure I understand. Try asking about passwords, phishing, privacy, adding a task, starting the quiz, or showing the activity log.";
        }

        private bool IsTaskAddRequest(string input)
        {
            return input.Contains("add task") ||
                   input.Contains("add a task") ||
                   input.Contains("remind me to") ||
                   input.Contains("set reminder") ||
                   input.Contains("create task");
        }

        private bool IsViewTasksRequest(string input)
        {
            return input.Contains("show my tasks") ||
                   input.Contains("view tasks") ||
                   input.Contains("list tasks") ||
                   input.Contains("show tasks");
        }

        private bool IsCompleteTaskRequest(string input)
        {
            return input.Contains("mark task") &&
                   (input.Contains("complete") || input.Contains("done") || input.Contains("completed"));
        }

        private bool IsDeleteTaskRequest(string input)
        {
            return input.Contains("delete task") ||
                   input.Contains("remove task");
        }

        private bool IsQuizStartRequest(string input)
        {
            return input.Contains("start quiz") ||
                   input.Contains("begin quiz") ||
                   input.Contains("play quiz") ||
                   input.Contains("start game") ||
                   input.Contains("mini game");
        }

        private bool IsActivityLogRequest(string input)
        {
            return input.Contains("show activity log") ||
                   input.Contains("activity log") ||
                   input.Contains("what have you done") ||
                   input.Contains("recent actions");
        }

        private string ExtractTaskTitle(string input)
        {
            string title = input.Trim();
            string lowerInput = input.ToLower();

            string[] prefixes =
            {
                "add a task to ",
                "add task to ",
                "add task - ",
                "add task ",
                "create task to ",
                "create task ",
                "remind me to ",
                "set a reminder to ",
                "set reminder to "
            };

            foreach (string prefix in prefixes)
            {
                if (lowerInput.StartsWith(prefix))
                {
                    title = input.Substring(prefix.Length).Trim();
                    break;
                }
            }

            title = Regex.Replace(title, @"\bin\s+\d+\s+days?\b", "", RegexOptions.IgnoreCase).Trim();
            title = Regex.Replace(title, @"\btomorrow\b", "", RegexOptions.IgnoreCase).Trim();
            title = Regex.Replace(title, @"\btoday\b", "", RegexOptions.IgnoreCase).Trim();
            title = Regex.Replace(title, @"\bnext week\b", "", RegexOptions.IgnoreCase).Trim();
            title = Regex.Replace(title, @"\bon\s+\d{4}-\d{2}-\d{2}\b", "", RegexOptions.IgnoreCase).Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                title = "Cybersecurity task";
            }

            return char.ToUpper(title[0]) + title.Substring(1);
        }

        private DateTime? ExtractReminderDate(string input)
        {
            if (input.Contains("tomorrow"))
            {
                return DateTime.Today.AddDays(1).AddHours(9);
            }

            if (input.Contains("today"))
            {
                return DateTime.Now.AddHours(1);
            }

            if (input.Contains("next week"))
            {
                return DateTime.Today.AddDays(7).AddHours(9);
            }

            Match daysMatch = Regex.Match(input, @"in\s+(\d+)\s+days?");

            if (daysMatch.Success)
            {
                int days = int.Parse(daysMatch.Groups[1].Value);
                return DateTime.Today.AddDays(days).AddHours(9);
            }

            Match dateMatch = Regex.Match(input, @"\d{4}-\d{2}-\d{2}");

            if (dateMatch.Success && DateTime.TryParse(dateMatch.Value, out DateTime parsedDate))
            {
                return parsedDate.AddHours(9);
            }

            return null;
        }

        private string GenerateTaskDescription(string title)
        {
            string lowerTitle = title.ToLower();

            if (lowerTitle.Contains("two-factor") || lowerTitle.Contains("2fa") || lowerTitle.Contains("authentication"))
            {
                return "Enable two-factor authentication to add an extra layer of protection to your online accounts.";
            }

            if (lowerTitle.Contains("password"))
            {
                return "Review and update passwords to ensure they are strong, unique, and not reused across accounts.";
            }

            if (lowerTitle.Contains("privacy"))
            {
                return "Review account privacy settings to ensure personal information is protected.";
            }

            if (lowerTitle.Contains("phishing") || lowerTitle.Contains("scam"))
            {
                return "Review suspicious messages and report possible phishing or scam attempts.";
            }

            return "Cybersecurity task created to help improve online safety.";
        }

        private int ExtractTaskId(string input)
        {
            Match match = Regex.Match(input, @"\d+");

            if (match.Success && int.TryParse(match.Value, out int id))
            {
                return id;
            }

            return 0;
        }

        private string GetRandomResponse(string topic)
        {
            List<string> topicResponses = responses[topic];
            int index = random.Next(topicResponses.Count);
            return topicResponses[index];
        }

        private bool IsFollowUp(string input)
        {
            return input.Contains("tell me more") ||
                   input.Contains("explain more") ||
                   input.Contains("another tip") ||
                   input.Contains("more info") ||
                   input.Contains("continue") ||
                   input.Contains("give me more");
        }

        private string GetFollowUpResponse()
        {
            if (string.IsNullOrWhiteSpace(memory.LastTopic))
            {
                return "Tell me which cybersecurity topic you want to learn more about, such as passwords, phishing, scams, privacy, malware, or safe browsing.";
            }

            return GetRandomResponse(memory.LastTopic);
        }

        private void DetectUserName(string input)
        {
            if (input.StartsWith("my name is "))
            {
                memory.UserName = input.Replace("my name is ", "").Trim();
            }
            else if (input.StartsWith("i am "))
            {
                string possibleName = input.Replace("i am ", "").Trim();

                if (!possibleName.Contains("worried") &&
                    !possibleName.Contains("curious") &&
                    !possibleName.Contains("frustrated") &&
                    !possibleName.Contains("confused") &&
                    !possibleName.Contains("scared"))
                {
                    memory.UserName = possibleName;
                }
            }
        }

        private void DetectFavouriteTopic(string input)
        {
            if (input.Contains("interested in") ||
                input.Contains("favourite topic is") ||
                input.Contains("favorite topic is"))
            {
                foreach (string topic in responses.Keys)
                {
                    if (input.Contains(topic))
                    {
                        memory.FavouriteTopic = topic;
                        memory.LastTopic = topic;
                    }
                }
            }
        }

        private string DetectSentiment(string input)
        {
            if (input.Contains("worried") ||
                input.Contains("scared") ||
                input.Contains("afraid") ||
                input.Contains("stressed") ||
                input.Contains("overwhelmed"))
            {
                memory.LastSentiment = "worried";
                return "It's completely understandable to feel worried. Cybersecurity can seem stressful, but small steps can protect you. ";
            }

            if (input.Contains("curious") ||
                input.Contains("interested"))
            {
                memory.LastSentiment = "curious";
                return "I like that you're curious. Learning more is one of the best ways to stay safe. ";
            }

            if (input.Contains("frustrated") ||
                input.Contains("confused") ||
                input.Contains("annoyed"))
            {
                memory.LastSentiment = "frustrated";
                return "I understand that this can feel confusing. I'll keep the explanation simple and practical. ";
            }

            return "";
        }

        private string RecallMemory()
        {
            string message = "";

            if (!string.IsNullOrWhiteSpace(memory.UserName))
            {
                message += $"I remember your name is {memory.UserName}. ";
            }

            if (!string.IsNullOrWhiteSpace(memory.FavouriteTopic))
            {
                message += $"I also remember that you are interested in {memory.FavouriteTopic}. ";
                message += $"As someone interested in {memory.FavouriteTopic}, you should keep learning practical ways to protect your information online.";
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                message = "I do not have any saved details yet. You can tell me your name or say something like: I am interested in privacy.";
            }

            return message;
        }

        private string GetDisplayName()
        {
            if (string.IsNullOrWhiteSpace(memory.UserName))
            {
                return "there";
            }

            return memory.UserName;
        }
    }
}