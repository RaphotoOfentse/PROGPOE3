// CyberChatbot.cs - A refactored non-console chatbot class for GUI use
using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberChatbotLib
{
    public static class CyberChatbot
    {
        private static string currentTopic = null;
        private static readonly HashSet<string> rememberedTopics = new HashSet<string>();

        public static Dictionary<string, string[]> GeneralResponses = new Dictionary<string, string[]>
        {
            {"how are you", new[]
                {
                    "I am fantastic. Thank you for asking 😊.",
                    "I'm great! I hope you're doing well too!",
                    "Feeling awesome today! I hope you are too 😄!"
                }
            },
            {"am", new[]
                {
                    "I'm overjoyed that you're doing well.",
                    "That's wonderful to hear. Let's dive into some cybersecurity topics.",
                    "Yay! That is great to hear."
                }
            },
            {"topics", new[]
                {
                    "I'm a cybersecurity bot! 🔐 Ask me about phishing, password safety, suspicious links, or safe browsing.",
                    "Cybersecurity is my specialty! From password tips to avoiding scams—ask away!",
                    "I can help with phishing, scams, passwords, and safe internet habits. What would you like to know?"
                }
            },
            {"purpose", new[]
                {
                    "I'm your cybersecurity buddy - here to guide you through the online safety jungle!",
                    "I help users understand threats like phishing and suspicious links.",
                    "My mission is to make the digital world safer by educating users like you!"
                }
            }
        };

        public static Dictionary<string, Func<string>> CyberTopics = new Dictionary<string, Func<string>>
        {
            { "phishing", GetPhishingInfo },
            { "protection", GetPhishingTips },
            { "suspicious", GetSuspiciousLinkTips },
            { "cybersecurity tips", GetCybersecurityTips },
            { "password", GetPasswordSafetyTips },
            { "privacy", GetPrivacyAdvice },
            { "scam", GetScamAdvice }
        };

        public static string GetResponse(string input, string name)
        {
            string lowerInput = input.ToLower();
            string response = "";

            // Sentiment detection
            string sentiment = DetectSentiment(lowerInput);
            if (sentiment != "neutral")
            {
                response += sentiment switch
                {
                    "worried" => $"It's okay to feel worried, {name}. I'm here to help you step-by-step.",
                    "curious" => $"I love curious minds, {name}! Let's explore more together.",
                    "frustrated" => $"I'm sorry you're feeling frustrated, {name}. Let's take it slowly.",
                    _ => ""
                } + "\n";
            }

            // Topic memory
            if (lowerInput.StartsWith("i am interested in") ||
                lowerInput.StartsWith("i'm interested in") ||
                lowerInput.StartsWith("i have an interest in"))
            {
                string topic = input.Substring(input.IndexOf("in") + 2).Trim();
                if (!string.IsNullOrEmpty(topic))
                {
                    if (rememberedTopics.Add(topic))
                        response += $"Great! I'll remember that you're interested in {topic}, {name}.\n";
                    else
                        response += $"You've already told me you're interested in {topic}, {name}.\n";
                }
                else
                    response += "Oops! Please specify the topic you're interested in.\n";

                return response;
            }

            // Follow-up logic
            if (!string.IsNullOrEmpty(currentTopic) && IsFollowUp(lowerInput))
            {
                return response + GetFollowUp(currentTopic, name);
            }

            // Cyber topics
            foreach (var pair in CyberTopics)
            {
                if (lowerInput.Contains(pair.Key))
                {
                    currentTopic = pair.Key;
                    return response + pair.Value();
                }
            }

            // General small talk
            Random rnd = new Random();
            foreach (var pair in GeneralResponses)
            {
                if (lowerInput.Contains(pair.Key))
                {
                    string[] responses = pair.Value;
                    return response + responses[rnd.Next(responses.Length)];
                }
            }

            // Memory recall
            if (lowerInput.Contains("remind me") || lowerInput.Contains("what did i say"))
            {
                if (!string.IsNullOrEmpty(currentTopic))
                    return response + $"{name}, you told me you're interested in {currentTopic}. Want to learn more?";
                else
                    return response + "You haven't told me your favourite topic yet. Try saying 'I'm interested in privacy' etc.";
            }

            // Default fallback
            return response + $"I'm sorry {name}, I didn't quite understand that. Try asking about phishing, scams, passwords, etc.";
        }

        private static bool IsFollowUp(string input)
        {
            string[] followUps = { "more", "details", "enlighten", "explain", "tell me more" };
            return followUps.Any(keyword => input.Contains(keyword));
        }

        private static string GetFollowUp(string topic, string name)
        {
            return topic switch
            {
                "phishing" => $"Here's more about phishing, {name}:\n" + GetPhishingTips(),
                "password" => $"Password safety tips continued, {name}:\n" + GetPasswordSafetyTips(),
                "privacy" => $"More on privacy, {name}:\n" + GetPrivacyAdvice(),
                "scam" => $"Extra scam awareness advice for you, {name}:\n" + GetScamAdvice(),
                _ => "I don't have more info on that topic."
            };
        }

        public static string DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("anxious")) return "worried";
            if (input.Contains("curious") || input.Contains("interested") || input.Contains("excited")) return "curious";
            if (input.Contains("frustrated") || input.Contains("angry") || input.Contains("annoyed")) return "frustrated";
            return "neutral";
        }

        // Static info responses
        public static string GetPhishingInfo() =>
            "Phishing is a type of cyberattack using fake emails or links to trick you into giving up sensitive info.";

        public static string GetPhishingTips() =>
            "To protect against phishing:\n- Don't click suspicious links\n- Check sender email\n- Use 2FA\n- Stay alert!";

        public static string GetPasswordSafetyTips() =>
            "Password tips:\n1. Use 12+ characters\n2. Don't reuse passwords\n3. Use a password manager\n4. Enable 2FA";

        public static string GetPrivacyAdvice() =>
            "Privacy tips:\n1. Limit personal info shared online\n2. Disable location tracking\n3. Use encrypted apps";

        public static string GetScamAdvice() =>
            "Scam prevention:\n1. Don't send money to unknowns\n2. Be cautious of urgent messages\n3. Verify sources first";

        public static string GetSuspiciousLinkTips() =>
            "Suspicious links often have odd spellings, unknown senders, or urgency. Hover over links to preview.";

        public static string GetCybersecurityTips() =>
            "Cybersecurity tips:\n- Avoid clicking unknown links\n- Hover to verify URLs\n- Use antivirus\n- Keep software updated";
    }
}
