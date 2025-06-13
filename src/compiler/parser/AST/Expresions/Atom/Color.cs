using Errors;
using Lexical;

namespace Parsing;

public class Color : Atom
{
    public Color(int line, int position, Token color) : base(line, position)
    {
        Value = color.Content;
        Type = ExpressionType.COLOR;
    }

}