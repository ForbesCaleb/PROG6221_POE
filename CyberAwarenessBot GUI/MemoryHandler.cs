using System.Collections.Generic;

namespace CyberAwarenessBotGUI
{
    public class MemoryHandler
    {
        private readonly Dictionary<string, string> _mem = new();

        public void Remember(string key, string value) => _mem[key] = value;
        public string? Recall(string key) => _mem.TryGetValue(key, out var v) ? v : null;
    }
}



