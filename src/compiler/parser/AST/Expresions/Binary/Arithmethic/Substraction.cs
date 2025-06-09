namespace Parsing;

public class Subs: Arithmethic
{
    public Subs(int line, int position) : base(line, position) { }

    public override void Evaluate()
    {
        Right.Evaluate();
        Left.Evaluate();
        Value = (int)Right.Value - (int)Left.Value;
    }
}