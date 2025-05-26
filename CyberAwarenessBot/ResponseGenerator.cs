using System;
using System.Collections.Generic;

public class ResponseGenerator
{
    private Dictionary<string, List<string>> keywordResponses;
    private MemoryHandler memoryHandler;
    private Random random = new Random();

    public ResponseGenerator(MemoryHandler memoryHandler)
    {
        this.memoryHandler = memoryHandler;
        InitializeResponses();
    }

    private void InitializeResponses()
    {
        keywordResponses = new Dictionary<string, List<string>>
        {
            { "password", new List<string>
                {
                    "Make sure to use strong, unique passwords for each account.",
                    "Avoid using personal details in your passwords.",
                    "Consider using a password manager to generate and store passwords."
                }
            },
            { "scam", new List<string>
                {
                    "Scammers often disguise themselves as trusted organisations.",
                    "Be cautious of unsolicited messages asking for your personal information."
                }
            },
            { "privacy", new List<string>
                {
                    "Review the security settings on your accounts regularly.",
                    "Be careful what personal information you share online."
                }
            },
            { "phishing", new List<string>
                {
                    "Be cautious of emails asking for personal information.",
                    "Look for signs of phishing like poor grammar and suspicious links.",
                    "Never click on unknown links or download unexpected attachments."
                }
            },
            { "firewall", new List<string>
                {
                    "A firewall protects a computer network from unauthorized access.",
                    "It acts like a barrier between a trusted and untrusted network.",
                    "Firewalls can be hardware or software-based.",
                    "Firewalls help prevent hackers from entering a system.",
                }
            },
            { "hackers", new List<string>
                {
                    "A hacker is someone who breaks into computer systems.",
                    "Hackers can steal data or cause damage.",
                    "Some hackers help improve security.",
                    "Hackers use special tools and skills."
                }
            }
        };
    }

    public string GenerateResponse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "I didn’t quite understand that. Could you rephrase?";

        string sentimentResponse = "";
        string keywordResponse = "";

        // Sentiment detection
        if (input.Contains("worried") || input.Contains("frustrated"))
        {
            sentimentResponse = "It's completely understandable to feel that way. Scammers can be very convincing. ";
        }
        else if (input.Contains("curious"))
        {
            sentimentResponse = "That's great! Curiosity is key to cybersecurity awareness. ";
        }

        // Memory and recall
        if (input.Contains("my favorite topic is"))
        {
            var topic = input.Replace("my favorite topic is", "").Trim();
            memoryHandler.Remember("interest", topic);
            return $"Great! I'll remember that you're interested in {topic}. It's a crucial part of staying safe online.";
        }

        if (input.Contains("the topic i am most interested about is"))
        {
            var topic = input.Replace("the topic i am most interested about is", "").Trim();
            memoryHandler.Remember("interest", topic);
            return $"Great! I'll remember that you're interested in {topic}. It's a crucial part of staying safe online.";
        }

        if (input.Contains("remind me what i am interested in") || input.Contains("remind me what I like"))
        {
            var interest = memoryHandler.Recall("interest");
            return interest != null
                ? $"As someone interested in {interest}, you might want to review the security settings on your accounts."
                : "I don’t recall your interests yet. Let me know what you're interested in!";
        }

        // Keyword detection
        foreach (var keyword in keywordResponses.Keys)
        {
            if (input.Contains(keyword))
            {
                var responses = keywordResponses[keyword];
                keywordResponse = responses[random.Next(responses.Count)];
                break;
            }
        }

        // Final response combining sentiment + keyword
        if (!string.IsNullOrEmpty(sentimentResponse) || !string.IsNullOrEmpty(keywordResponse))
        {
            return $"{sentimentResponse}{keywordResponse}";
        }

        return "I'm not sure I understand. Can you try rephrasing?";
    }
}

