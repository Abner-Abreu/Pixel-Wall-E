using Lexical;

namespace Parsing;

public class Number : Atom
{
    public int Value { private set; get; }
    public Number(int line, int position, Token number) : base(line, position)
    {
        Value = int.Parse(number.Content);
        Type = ExpressionType.NUM;
    }

}