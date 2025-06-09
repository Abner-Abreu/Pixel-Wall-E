namespace Parsing;

public class Div : Arithmethic
{
    public Div(int line, int position) : base(line, position) { }

    public override void Evaluate()
    {
        Right.Evaluate();
        Left.Evaluate();
        Value = (int)Right.Value / (int)Left.Value;
        Value = (int)Value;
    }
}