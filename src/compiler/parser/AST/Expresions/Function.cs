using Errors;

namespace Parsing;

public class Function : Expression
{
    public string Identifier { private set; get; }
    public ReturnType Return { private set; get; }
    public List<Expression?> Parameters { set; get; }

    public Function(string identifier, int line, int position) : base(line, position)
    {
        Identifier = identifier;
        Return = Utils.Functions[identifier].returnType;
        Parameters = new List<Expression?>();
    }

    public override bool CheckSemantic(Context context, List<Error> semanticErrors)
    {
        bool check = true;
        if (Parameters.Count != Utils.Functions[Identifier].paramType.Length)
        {
            semanticErrors.Add(new Error(ErrorType.Semantic, $"Expected {Utils.Functions[Identifier].paramType.Length} parameters", Line, Position));
            return false;
        }
        for (int i = 0; i < Parameters.Count; i++)
        {
            if (Parameters[i].CheckSemantic(context, semanticErrors) == false)
            {
                check = false;
            }
            if (Parameters[i].Type != Utils.Functions[Identifier].paramType[i])
            {
                semanticErrors.Add(new Error(ErrorType.Semantic, $"Expected {Utils.Functions[Identifier].paramType[i]} expresion type as parameter", Line, Position));
                check = false;
            }
        }
        return check;
    }

    public override void Evaluate()
    {
        throw new NotImplementedException();
    }
}