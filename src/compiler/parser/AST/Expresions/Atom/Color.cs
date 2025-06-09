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

    public override bool CheckSemantic(Context context, List<Error> semanticErrors)
    {
        return true;
    }

    public override void Evaluate()
    {
        return;
    }

}