using System.Collections.Generic;

public class MemoryHandler
{
    private Dictionary<string, string> memory = new Dictionary<string, string>();

    public void Remember(string key, string value)
    {
        memory[key] = value;
    }

    public string Recall(string key)
    {
        return memory.ContainsKey(key) ? memory[key] : null;
    }

    public bool HasMemory(string key)
    {
        return memory.ContainsKey(key);
    }
}
