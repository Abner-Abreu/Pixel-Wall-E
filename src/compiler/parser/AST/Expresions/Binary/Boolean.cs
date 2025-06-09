using Errors;

namespace Parsing;

public abstract class Boolean : Binary
{
    public Boolean(int line, int position) : base(line, position)
    {
        Type = ExpressionType.BOOLEAN;
    }

    public override bool CheckSemantic(Context context, List<Error> semanticErrors)
    {
        bool right = Right.CheckSemantic(context, semanticErrors);
        bool left = Left.CheckSemantic(context, semanticErrors);

        if (Right.Type != ExpressionType.NUM || Left.Type != ExpressionType.NUM)
        {
            semanticErrors.Add(new Error(ErrorType.Semantic,"Both members of the expression most be numbers",Line,Position));
            return false;
        }

        Type = ExpressionType.BOOLEAN;
        return right && left;
    }
}