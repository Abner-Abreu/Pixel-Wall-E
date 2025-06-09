using Errors;

namespace Parsing;

public class Assing : Binary
{
    public string VarName { private set; get; }
    public Assing(int line, int position) : base(line, position){ }

    public override bool CheckSemantic(Context context, List<Error> semanticErrors)
    {
        if (Left.Type != ExpressionType.VAR)
        {
            semanticErrors.Add(new Error(ErrorType.Semantic, "Left operand most be a variable name", Line, Position));
            return false;
        }
        else if (Right.CheckSemantic(context, semanticErrors) == false)
        {
            return false;
        }
        Type = Right.Type;
        return true;
    }

    public override void Evaluate()
    {
        Right.Evaluate();
        Value = Right.Value;
    }
}