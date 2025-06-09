namespace Parsing;

using Errors;
public class Or : Boolean
{
    public Or(int line, int position) : base(line, position) { }

    public override bool CheckSemantic(Context context, List<Error> semanticErrors)
    {
        bool right = Right.CheckSemantic(context, semanticErrors);
        bool left = Left.CheckSemantic(context, semanticErrors);

        if (Right.Type != ExpressionType.BOOLEAN || Left.Type != ExpressionType.BOOLEAN)
        {
            semanticErrors.Add(new Error(ErrorType.Semantic,"Both members of the expression most be boolean expresions",Line,Position));
            return false;
        }

        Type = ExpressionType.BOOLEAN;
        return right && left;
    }
    public override void Evaluate()
    {
        Right.Evaluate();
        Left.Evaluate();

        if ((int)Right.Value == 1 || (int)Left.Value == 1)
        {
            Value = 1;
        }
        else
        {
            Value = 0;
        }
    }
}