using Lexical;

namespace Parsing;

public class Color : Atom
{
    public string Content { private set; get; }
    public Color(int line, int position, Token color) : base(line, position)
    {
        Content = color.Content;
        Type = ExpressionType.COLOR;
    }

}