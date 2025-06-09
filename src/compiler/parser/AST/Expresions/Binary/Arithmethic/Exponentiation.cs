namespace Parsing;

public class Pow: Arithmethic
{
    public Pow (int line, int position) : base(line, position) { }

    public override void Evaluate()
    {
        Right.Evaluate();
        Left.Evaluate();
        Value = Math.Pow((int)Right.Value, (int)Left.Value);
    }
}