# Cybersecurity Awareness Chatbot

## Project Overview

This is a console-based Cybersecurity Awareness Chatbot written in C#. It educates users on basic cybersecurity concepts through a dynamic and interactive conversation. The chatbot supports sentiment detection, remembers user preferences, and enhances engagement with styled terminal output.

---

## Features

### 1. **Voice and Visual Greeting**

* Loads ASCII art from a file (`ascii.txt`)
* Plays a greeting audio file (`greeting.wav`)

### 2. **Personalized Greeting**

* Asks for the user’s name and remembers it using an in-memory dictionary.

### 3. **Cybersecurity Keyword Recognition**

* Detects keywords like `password`, `phishing`, `firewall`, etc.
* Returns a random tip for each keyword using a `Dictionary<string, List<string>>`.

### 4. **Sentiment Detection**

* Responds empathetically to emotional input (e.g. “worried”, “curious”).
* Combines sentiment and keyword responses in a single reply.

### 5. **Memory & Recall**

* Stores and recalls user preferences (e.g. “my favorite topic is privacy”).
* Handles alternate phrasing like “the topic I am most interested about is”.

### 6. **Formatted Terminal Output** (via `AppearanceManager`)

* Colors and sections:

  * Headers and section dividers
  * Bot messages and user input prompts
  * Errors and success messages

---

## Project Structure

```
CyberSecurityChatbot/
│
├── Program.cs               # Entry point
├── Chatbot.cs               # Chatbot flow and interaction loop
├── ResponseGenerator.cs     # Handles keyword and sentiment analysis
├── MemoryHandler.cs         # Stores and retrieves user data
├── GreetingManager.cs       # ASCII art and sound playback
├── AppearanceManager.cs     # Console styling for enhanced UI
├── files/
│   ├── ascii.txt            # ASCII art file
│   └── greeting.wav         # Voice greeting
└── README.md                # Project description
```

---

## ▶ How to Run

1. Open the project in **Visual Studio** or your favorite IDE.
2. Ensure your project structure includes a `files` folder with:

   * `ascii.txt` for the welcome ASCII art
   * `greeting.wav` for the audio greeting
3. Build and run the project.

---

##  Requirements

* .NET 6.0 SDK or later
* Console application project template
* Windows OS (for `System.Media.SoundPlayer` to work)

---

##  Example Conversation

```
=== CYBERSECURITY AWARENESS BOT ===

What's your name? Caleb
Nice to meet you, Caleb!

----------------------------------------
Ask a cybersecurity question or type 'exit' to quit:
You : I'm worried about my password

Bot : It's completely understandable to feel that way. Scammers can be very convincing. Make sure to use strong, unique passwords for each account.
----------------------------------------
```

---

## Educational Goal

The chatbot helps users:

* Understand and recognize cybersecurity risks
* Learn best practices in an engaging way
* Get real-time feedback and support

---

## Authors

* Caleb Forbes

Feel free to extend the bot with more topics, a GUI version, or even machine learning-based sentiment detection!



