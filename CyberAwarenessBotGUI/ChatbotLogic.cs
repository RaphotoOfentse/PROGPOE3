using CyberAwarenessBotGUI;
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

                        if (score >= 8)
                            response += "Great job! You're a cybersecurity pro. 🔐";
                        else
                            response += "Keep learning to stay safe online! 🛡️";

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
                }
                else
                {
                    response = "Please enter the number corresponding to your answer (e.g., 1, 2, 3...).";
                }
            }
            else if (input.Contains("quiz") || input.Contains("play game"))
            {
                inQuizMode = true;
                quiz.ResetQuiz();
                var q = quiz.GetCurrentQuestion();
                response = $"🧠 Cybersecurity Quiz Started!\n\n{q.Text}\n";
                for (int i = 0; i < q.Options.Length; i++)
                    response += $"{i + 1}. {q.Options[i]}\n";
            }
            else if (awaitingReminderConfirmation && input.Contains("remind"))
            {
                int days = ExtractDaysFromInput(input);
                DateTime reminderDate = DateTime.Now.AddDays(days);

                taskManager.AddTask(pendingTaskTitle, pendingTaskDescription, reminderDate);
                response = $"Got it! I'll remind you in {days} day(s) 📅";

                awaitingReminderConfirmation = false;
                pendingTaskTitle = null;
                pendingTaskDescription = null;
            }
            else if (input.Contains("add task") || input.Contains("review privacy"))
            {
                pendingTaskTitle = "Review Privacy Settings";
                pendingTaskDescription = "Review account privacy settings to ensure your data is protected.";
                awaitingReminderConfirmation = true;

                response = $"Task added with the description \"{pendingTaskDescription}\". Would you like a reminder?";
            }
            else if (input.Contains("recommend"))
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
                response = "I'm not sure how to respond to that. Try asking me about scams, passwords, or tasks!";
            }

            return response;
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

        private string ShowScamAdvice() => "Scam tip: Never click suspicious links or share personal info online.";
        private string ShowPasswordSafety() => "Use a strong password with uppercase, lowercase, numbers, and symbols.";
        private string ShowPrivacyAdvice() => "Adjust your account privacy settings and review app permissions regularly.";

        private string RecommendCyberTasks()
        {
            return "Try these: 🔒 Update your password, 📧 Review your inbox for phishing emails, 🔍 Check browser settings.";
        }
    }
}