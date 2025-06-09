namespace Parsing;

using Errors;
public class Variable : Atom
{

    public string VarName { private set; get; }
    public Variable(string varName, int line, int position) : base(line, position)
    {
        VarName = varName;
        Type = ExpressionType.VAR;
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