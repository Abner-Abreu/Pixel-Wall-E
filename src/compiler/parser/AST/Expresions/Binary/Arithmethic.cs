using Errors;

namespace Parsing;

public abstract class Arithmethic : Binary
{
    public Arithmethic(int line, int position) : base(line, position)
    {
        Type = ExpressionType.ARITHMETHIC;
    }
}
