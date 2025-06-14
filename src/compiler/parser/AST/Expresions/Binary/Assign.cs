using Errors;

namespace Parsing;

public class Assing : Binary
{
    public Assing(int line, int position) : base(line, position)
    {
        Type = ExpressionType.ASSING;
    }
}