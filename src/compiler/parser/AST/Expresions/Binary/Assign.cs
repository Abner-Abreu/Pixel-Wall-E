using Errors;

namespace Parsing;

public class Assing : Binary
{
    public string VarName { private set; get; }
    public Assing(int line, int position) : base(line, position)
    {
        Type = ExpressionType.ASSING;
    }
}