namespace Parsing;

using System.Collections.Generic;
public class Function : Expression
{
    public string Identifier { private set; get; }
    public List<Expression?> Parameters { set; get; }

    public Function(string identifier, int line, int position) : base(line, position)
    {
        Identifier = identifier;
        Parameters = new List<Expression?>();
    }
}