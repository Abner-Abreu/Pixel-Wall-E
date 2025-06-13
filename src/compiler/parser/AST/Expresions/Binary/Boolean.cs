using Errors;

namespace Parsing;

public abstract class Boolean : Binary
{
    public Boolean(int line, int position) : base(line, position)
    {
        Type = ExpressionType.BOOLEAN;
    }
}