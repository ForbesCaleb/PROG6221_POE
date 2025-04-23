# PROG6221_POE
# Cybersecurity Awareness Bot

# Cybersecurity Awareness Bot

A simple C# console application that acts as a chatbot for basic cybersecurity awareness. The bot greets the user with ASCII art and a voice message (`greeting.wav`), asks for the user’s name, and answers basic cybersecurity questions in a loop until the user types "exit".

---

## Features

- **Voice Greeting:** Plays a WAV greeting audio file (`greeting.wav`) at startup.
- **ASCII Art:** Shows an ASCII banner from a file (`ascii.txt`).
- **Personalized Greeting:** Greets the user by name.
- **Chatbot:** Answers questions about common cybersecurity topics (e.g., phishing, password safety, safe browsing).
- **Interactive Loop:** Responds to user input until "exit" is typed.
- **Error Handling:** Alerts if required files (`ascii.txt` or `greeting.wav`) are missing or invalid.

---

## Folder Structure


---

## Setup & Usage

1. **Clone or download the project folder.**
2. **Add your files:**
   - `files/ascii.txt`: Place ASCII art here (plain text file).
   - `files/greeting.wav`: Place a short WAV audio greeting here.
     - Make sure your audio file is in **WAV format** (not MP3 or other formats).
3. **In Visual Studio:**
   - Right-click both `ascii.txt` and `greeting.wav` in Solution Explorer.
   - Go to **Properties** > **Copy to Output Directory** > Set to **Copy always**.
4. **Build and run the solution.**
5. **Follow on-screen prompts and interact with the bot.**

---

## Supported Questions

- `how are you`
- `what’s your purpose?` or `what is your purpose`
- `password safety`
- `phishing`
- `safe browsing`

If the question is not recognized, the bot will prompt you to rephrase.

---

## WAV File Notes

- **Required:** The chatbot will attempt to play `files/greeting.wav` at startup.
- **If the file is missing or not a valid WAV:**  
  You will see a red error message in the console.
- **WAV Format Only:**  
  Ensure the file is `.wav` format. MP3 files are not supported by `System.Media.SoundPlayer`.

---

## Troubleshooting

- **ASCII file not found:**  
  Make sure `ascii.txt` exists in the `files` folder.
- **WAV file not found or incorrect format:**  
  Make sure `greeting.wav` is in the `files` folder and is a valid WAV file.
- **Sound not playing:**  
  Sound playback only works on Windows. Ensure your file is in `.wav` format.
- **To convert MP3 to WAV:**  
  Use any free online converter, or in Windows, open with Audacity and export as WAV.

---

## License

MIT License (or add your own).

---

## Example



