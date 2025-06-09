namespace Parsing;

public abstract class Expression : AST
{
    public abstract void Evaluate();

    public object? Value { set; get; }
    public ExpressionType Type { set; get; }
    public Expression(int line, int position) : base (line,position){}
}