using Errors;

namespace Parsing;

public class GoTo : Expression
{
    public string? Label {set; get; }
    public Expression? Condition {set; get; }

    public GoTo(int line, int position) : base(line, position){}
    public override bool CheckSemantic(Context context, List<Error> semanticErrors)
    {
        throw new NotImplementedException();
    }

    public override void Evaluate()
    {
        throw new NotImplementedException();
    }
}