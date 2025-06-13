namespace Parsing;

public abstract class Expression : AST
{
    public object? Value { set; get; }
    public ExpressionType Type = ExpressionType.NONE;
    public Expression(int line, int position) : base (line,position){}
}