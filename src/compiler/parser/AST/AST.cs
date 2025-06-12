namespace Parsing;

using Errors;

public abstract class AST
{
    public int Line { private set; get; }
    public int Position { private set; get; }
    public abstract bool CheckSemantic(Context context, List<Error> semanticErrors);
    public AST(int line, int position)
    {
        Line = line;
        Position = position;
    }
}