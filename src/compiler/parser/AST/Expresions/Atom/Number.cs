using Errors;
using Lexical;

namespace Parsing;

public class Number : Atom
{

    public Number(int line, int position, Token number) : base(line, position)
    {
        Value = int.Parse(number.Content);
        Type = ExpressionType.NUM;
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