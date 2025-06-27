using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberAwarenessBot
{
    public record QuizQuestion(string Text, string[] Options, int CorrectIndex, string Explanation);
}

