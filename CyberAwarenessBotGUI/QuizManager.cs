using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberAwarenessBotGUI
{
    public class QuizQuestion
    {
        public string QuestionText { get; set; }
        public List<string> Choices { get; set; } = new List<string>(); // for MCQs
        public string CorrectAnswer { get; set; }
        public bool IsTrueFalse { get; set; }
    }
    public class QuizManager
    {
        private List<QuizQuestion> questions;
        private int currentQuestionIndex = 0;
        public int Score { get; private set; } = 0;

        public QuizManager()
        {
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            questions = new List<QuizQuestion>
        {
            new QuizQuestion { QuestionText = "True or False: You should reuse the same password across multiple sites.", IsTrueFalse = true, CorrectAnswer = "False" },
            new QuizQuestion { QuestionText = "What is phishing?", Choices = new List<string> { "A cyberattack to steal information", "A method of fishing", "A password manager" }, CorrectAnswer = "A cyberattack to steal information" },
            // Add 8 more questions here...
        };
        }

        public QuizQuestion? GetNextQuestion()
        {
            if (currentQuestionIndex < questions.Count)
                return questions[currentQuestionIndex++];
            return null;
        }

        public string SubmitAnswer(string answer)
        {
            var question = questions[currentQuestionIndex - 1];
            if (answer.Trim().ToLower() == question.CorrectAnswer.Trim().ToLower())
            {
                Score++;
                return "✅ Correct! Good job.";
            }
            else
            {
                return $"❌ Incorrect. The right answer was: {question.CorrectAnswer}.";
            }
        }

        public string GetFinalFeedback()
        {
            if (Score >= 8)
                return $"🎉 Great job! You're a cybersecurity pro. Final score: {Score}/10";
            else if (Score >= 5)
                return $"👍 Not bad! Keep learning to stay safe. Final score: {Score}/10";
            else
                return $"📘 Keep learning! Cybersecurity is important. Final score: {Score}/10";
        }
    }


}
