using Lexical;
using Parsing;
public class Context
{
    public Dictionary<string, int> Labels { private set; get; }
    public Dictionary<string, int> Vars { private set; get; }

    public Context()
    {
        Labels = new Dictionary<string, int>();
        Vars = new Dictionary<string, int>();
    }

    private void AddLabel(string label, int line)
    {
        Labels[label] = line;
    }

    private void AddVar(string varName, int value)
    {
        Labels[varName] = value;
    }
}