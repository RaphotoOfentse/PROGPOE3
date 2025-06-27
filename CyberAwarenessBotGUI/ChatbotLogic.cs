using CyberAwarenessBotGUI;
using CyberChatbotLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace CyberAwarenessBotGUI
{
    public class ChatbotLogic
    {
        private string name;
        private TaskManager taskManager;
        private Dictionary<string, Func<string>> cyberTopics;
        private string currentTopic;

        private bool awaitingReminderConfirmation = false;
        private string pendingTaskTitle = null;
        private string pendingTaskDescription = null;

        private CyberQuiz quiz = new CyberQuiz();
        private bool inQuizMode = false;
        private List<ChatAction> chatHistory = new List<ChatAction>();

        Dictionary<string, string[]> intentKeywords = new Dictionary<string, string[]>()
{
    { "add_task", new string[] { "add task", "new task", "remind me", "set reminder", "schedule", "create task" } },
    { "start_quiz", new string[] { "start quiz", "quiz time", "ask me questions", "test my knowledge" } },
    { "cyber_password", new string[] { "password", "change password", "strong password", "update password" } },
    { "cyber_privacy", new string[] { "privacy", "check privacy", "privacy settings", "data protection" } },
    { "cyber_phishing", new string[] { "phishing", "suspicious link", "fake email", "avoid scam" } },
      { "show_summary", new string[] {
        "summary",
        "recent actions",
        "what have you done",
        "what have you done for me",
        "show history",
        "what did i ask",
        "recap"
    } }
};
        public ChatbotLogic(string userName, TaskManager taskManager)
        {
            this.name = userName;
            this.taskManager = taskManager;

            cyberTopics = new Dictionary<string, Func<string>> {
            { "scam", ShowScamAdvice },
            { "password", ShowPasswordSafety },
            { "privacy", ShowPrivacyAdvice }
        };
        }
         
        public string ProcessInput(string input)
        {
            input = NormalizeInput(input);
            string intent = DetectIntent(input);
            input = input.ToLower().Trim();
            string response = "";

            if (inQuizMode)
            {
                if (int.TryParse(input, out int answerIndex))
                {
                    var isCorrect = quiz.AnswerCurrentQuestion(answerIndex - 1);
                    response = isCorrect ? "✅ Correct!\n" : "❌ Incorrect.\n";

                    if (quiz.IsQuizOver())
                    {
                        int score = quiz.GetScore();
                        int total = quiz.GetTotalQuestions();
                        response += $"Quiz Over! 🎉 You scored {score}/{total}.\n";
                        response += score >= 8 ? "Great job! You're a cybersecurity pro. 🔐" : "Keep learning to stay safe online! 🛡️";

                        inQuizMode = false;
                        quiz.ResetQuiz();
                    }
                    else
                    {
                        var q = quiz.GetCurrentQuestion();
                        response += $"\n{q.Text}\n";
                        for (int i = 0; i < q.Options.Length; i++)
                            response += $"{i + 1}. {q.Options[i]}\n";
                    }
                    return response;
                }
                else
                {
                    response = "Please enter the number corresponding to your answer (e.g., 1, 2, 3...).";
                }
            }
            switch (intent)
            {
                case "add_task":
                    string task = ExtractAfter(input, "add task");
                    if (string.IsNullOrEmpty(task)) task = ExtractAfter(input, "add a task to");
                    if (string.IsNullOrEmpty(task)) task = ExtractAfter(input, "remind me to");
                    if (string.IsNullOrEmpty(task)) task = ExtractAfter(input, "note to");
                    if (string.IsNullOrEmpty(task)) task = input; // fallback

                    pendingTaskTitle = ToTitleCase(task);
                    pendingTaskDescription = task;
                    awaitingReminderConfirmation = true;

                    taskManager.AddTask(pendingTaskTitle, pendingTaskDescription, null);
                    chatHistory.Add(new ChatAction { Description = pendingTaskTitle });

                    response = $"Task added: \"{pendingTaskTitle}\". Would you like to set a reminder for this task?";
                    break;

                case "start_quiz":
                    inQuizMode = true;
                    quiz.ResetQuiz();
                    var q = quiz.GetCurrentQuestion();
                    response = $"🧠 Cybersecurity Quiz Started!\n\n{q.Text}\n";
                    for (int i = 0; i < q.Options.Length; i++)
                        response += $"{i + 1}. {q.Options[i]}\n";
                    break;

                case "cyber_password":
                    response = CyberChatbot.GetPasswordSafetyTips();
                    break;

                case "cyber_privacy":
                    response = CyberChatbot.GetPrivacyAdvice();
                    break;

                case "cyber_phishing":
                    response = CyberChatbot.GetScamAdvice();
                    break;

                case "show_summary":
                    if (chatHistory.Count == 0)
                    {
                        response = "I haven't done anything yet! Try asking me to add a task or set a reminder. 📋";
                    }
                    else
                    {
                        response = "Here's a summary of recent actions:\n";
                        for (int i = 0; i < chatHistory.Count; i++)
                        {
                            response += $"{i + 1}. {chatHistory[i]}\n";
                        }
                    }
                    break;

                default:
                    response = null;
                    break;
            }


                if (awaitingReminderConfirmation && input.Contains("remind"))
            {
                int days = ExtractDaysFromInput(input);
                DateTime reminderDate = DateTime.Now.AddDays(days);

                taskManager.AddTask(pendingTaskTitle, pendingTaskDescription, reminderDate);
                chatHistory.Add(new ChatAction { Description = pendingTaskTitle, ReminderDate = reminderDate });

                response = $"Got it! I'll remind you in {days} day(s) 📅";

                awaitingReminderConfirmation = false;
                pendingTaskTitle = null;
                pendingTaskDescription = null;

                return response;
            }

            // Handle reminder confirmation
            if (awaitingReminderConfirmation && input.Contains("remind"))
            {
                int days = ExtractDaysFromInput(input);
                DateTime reminderDate = DateTime.Now.AddDays(days);

                taskManager.AddTask(pendingTaskTitle, pendingTaskDescription, reminderDate);
                chatHistory.Add(new ChatAction { Description = pendingTaskTitle, ReminderDate = reminderDate });

                response = $"Got it! I'll remind you in {days} day(s) 📅";

                awaitingReminderConfirmation = false;
                pendingTaskTitle = null;
                pendingTaskDescription = null;

                return response;
            }

            // Catch any leftover fallback logic if no intent was matched
            if (string.IsNullOrEmpty(response))
            {
                if (input.Contains("recommend"))
                {
                    response = RecommendCyberTasks();
                }
                else if (input.Contains("complete"))
                {
                    var completed = taskManager.MarkRandomTaskComplete();
                    response = completed != null
                        ? $"Nice work, {name}! ✅ You've completed: {completed.Title}"
                        : $"You don't have any tasks to complete right now.";
                }
                else if (cyberTopics.Any(kvp => input.Contains(kvp.Key)))
                {
                    currentTopic = cyberTopics.First(kvp => input.Contains(kvp.Key)).Key;
                    response = cyberTopics[currentTopic]();
                }
                else
                {
                    response = CyberChatbot.GetResponse(input, name);
                }
            }

            return response;
        }
        public string ExtractAfter(string input, string phrase)
        {
            int index = input.IndexOf(phrase);
            if(index != -1)
            {
                string result = input.Substring(index + phrase.Length).Trim();
                return result.TrimStart('-', ':').Trim();
            }
            return "";
        }

        public string ToTitleCase(string input)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }

        private string NormalizeInput(string input)
        {
            return new string(input
        .Where(c => !char.IsPunctuation(c))
        .ToArray())
        .ToLower()
        .Trim();
        }
        private int ExtractDaysFromInput(string input)
        {
            var parts = input.Split(' ');
            foreach (var part in parts)
            {
                if (int.TryParse(part, out int days))
                    return days;
            }
            return 1;
        }

        private string DetectIntent(string input)
        {
            input = input.ToLower();

            // PRIORITIZE task-related keywords first
            string[] taskKeywords = { "add task", "add a task to", "remind me", "remind me to", "note to", "set reminder", "schedule", "create task" };
            foreach (string keyword in taskKeywords)
            {
                if (input.Contains(keyword))
                    return "add_task";
            }
            //Checks other intents
            foreach (var intent in intentKeywords)
            {
                if(intent.Key == "add task") continue; //Already checked
                foreach(var phrase in intent.Value)
                {
                    if (input.Contains(phrase))
                        return intent.Key;
                }
            }
            return "unknown";
        }

        private string ShowScamAdvice() => "Scam tip: Never click suspicious links or share personal info online.";
        private string ShowPasswordSafety() => "Use a strong password with uppercase, lowercase, numbers, and symbols.";
        private string ShowPrivacyAdvice() => "Adjust your account privacy settings and review app permissions regularly.";

        private string RecommendCyberTasks()
        {
            return "Try these: 🔒 Update your password, 📧 Review your inbox for phishing emails, 🔍 Check browser settings.";
        }
    }
}