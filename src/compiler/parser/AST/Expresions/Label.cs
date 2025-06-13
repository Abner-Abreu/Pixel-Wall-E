namespace Parsing;

public class Label : Atom
{
    public string Identifier { private set; get; }
    public Label(string label, int line, int position) : base(line, position)
    {
        Identifier = label;
    }
}