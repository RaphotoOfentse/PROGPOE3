using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberAwarenessBotGUI
{
    public class CyberQuiz
    {
        private int currentQuestionIndex;
        private int score;

        public List<Question> Questions { get; private set; }

        public CyberQuiz()
        {
            Questions = new List<Question>
            {
                new Question("1.What is phishing?", new[] {
                    "A method of catching fish online",
                    "A cyberattack that tricks you into giving personal information",
                    "A way to speed up your internet",
                    "A security software"
                }, 1),

                new Question("2. True or False: Using '123456' as a password is safe.", new[] {
                    "True", "False"
                }, 1),

                new Question("3. What should you do if you receive a suspicious email?", new[] {
                    "Click all links to check them",
                    "Reply with your personal details",
                    "Delete it or report it as spam",
                    "Forward it to a friend"
                }, 2),

                new Question("4. True or False: Public Wi-Fi is always secure.", new[] {
                    "True", "False"
                }, 1),

                new Question("5. What is two-factor authentication (2FA)?", new[] {
                    "A way to double your internet speed",
                    "A method of verifying your identity using two methods",
                    "A password reset tool",
                    "A virus removal technique"
                }, 1),

                new Question("6. What’s the best way to create a strong password?", new[] {
                    "Use your birthdate",
                    "Use 'password123'",
                    "Use a mix of upper/lowercase letters, numbers, and symbols",
                    "Use your pet's name"
                }, 2),

                new Question("7. True or False: You should share your passwords with trusted friends.", new[] {
                    "True", "False"
                }, 1),

                new Question("8. Which of these is a social engineering tactic?", new[] {
                    "Brute force attack",
                    "Firewall",
                    "Impersonation",
                    "Antivirus scan"
                }, 2),

                new Question("9. Why are software updates important?", new[] {
                    "They take up space",
                    "They add new viruses",
                    "They patch security vulnerabilities",
                    "They delete your data"
                }, 2),

                new Question("10. What is the purpose of a firewall?", new[] {
                    "To block unauthorized access to your network",
                    "To make your PC run slower",
                    "To change your wallpaper",
                    "To allow hackers in"
                }, 0)
            };
        }

        public Question GetCurrentQuestion() => Questions[currentQuestionIndex];

        public bool AnswerCurrentQuestion(int answerIndex)
        {
            bool isCorrect = GetCurrentQuestion().CorrectAnswerIndex == answerIndex;
            if (isCorrect) score++;
            currentQuestionIndex++;
            return isCorrect;
        }

        public bool IsQuizOver() => currentQuestionIndex >= Questions.Count;

        public int GetScore() => score;

        public int GetTotalQuestions() => Questions.Count;

        public void ResetQuiz()
        {
            currentQuestionIndex = 0;
            score = 0;
        }
    }

    public class Question
    {
        public string Text { get; }
        public string[] Options { get; }
        public int CorrectAnswerIndex { get; }

        public Question(string text, string[] options, int correctAnswerIndex)
        {
            Text = text;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
        }
    }
}
