# CyberAwarenessBotGUI - ReadMe

📌 Project Overview

CyberAwarenessBotGUI is a C# Windows Forms chatbot application designed to educate users on cybersecurity. It features personalized interactions, quizzes, task management, sentiment detection, and activity logging.

---

✅ Features

💬 Chatbot Core

* ASCII text art + voice greeting on launch
* Personalized welcome using user's name
* Cybersecurity advice based on keywords (e.g., "firewall", "password")
* Sentiment-aware responses (e.g., stress, curiosity, happiness)
* Goodbye message includes the user’s name

🧠 NLP Capabilities

* Input normalization: `.ToLower().Trim()`
* Keyword matching via `input.Contains(...)`
* Sentiment detection via fixed emotion keyword lists
* Contextual memory using `MemoryHandler` (e.g., remembers user interests)
* Intent recognition using simple rule-based checks

🎯 Quiz System

* Ask single questions (`ask me a question`)
* Start full quiz (`start quiz`, `cybersecurity quiz`)
* Gives feedback on answers like "Great job!" or "Try again."

✅ Task Management

* Add task: `add task - description ; remind in X days`
* Complete task: `complete task - task name`
* Delete task: `delete task - task name`
* View all tasks: `show tasks`

📜 Activity Log

* Tracks major interactions and commands (e.g., startup, questions, tasks)
* Command: `show log`

---

🛠️ How to Use the App

1. **Start the Application**

   * HomeForm launches with ASCII text + voice greeting
   * Click “Start Chat” to open the chatbot window

2. **Interact Using These Commands**

   * Ask questions like `what is phishing?`
   * Trigger the quiz: `start quiz` or `ask me a question`
   * Set interest: `the topic I am most interested about is social engineering`
   * Recall interest: `remind me what I like`
   * Manage tasks: `add task`, `complete task`, `delete task`, `show tasks`
   * View log: `show log`
   * Exit chatbot: `exit`

3. **Quiz Feedback**

   * If you answer correctly: response includes praise (e.g., "Correct! Well done!")
   * If you answer incorrectly: response includes guidance

---

🔄 NLP Summary

| Feature                 | Status |
| ----------------------- | ------ |
| Text Normalization      | ✅      |
| Keyword Matching        | ✅      |
| Sentiment Detection     | ✅      |
| Memory Recall (Context) | ✅      |
| Intent Recognition      | ✅      |
| Machine Learning NLP    | ❌      |
| Synonym/Fuzzy Matching  | ❌      |

---

📁 File Structure

```
CyberAwarenessBotGUI/
├── Forms/
│   ├── HomeForm.cs
│   ├── FormChat.cs
│   ├── InputDialog.cs
├── Logic/
│   ├── AppearanceManager.cs
│   ├── Chatbot.cs
│   ├── GreetingManager.cs
│   ├── MemoryHandler.cs
│   ├── QuizManager.cs
│   ├── ResponseGenerator.cs
│   ├── TaskManager.cs
├── Resources/
│   ├── ascii.txt
│   └── greeting.wav
├── Program.cs
└── README.md
```

---

📌 Known Issues & Fixes

* If audio plays twice: make sure `PlayVoiceGreeting()` runs **only once** in `HomeForm`
* If `ask me a question` doesn't work: ensure the `ResponseGenerator` has proper conditionals
* If scroll not working during quiz: check `RichTextBox` scroll settings (`ScrollBars = Vertical`)


---

## 🔐 Cyber Tip

Always enable Two-Factor Authentication (2FA) on all important accounts and be cautious of unexpected emails and links.







