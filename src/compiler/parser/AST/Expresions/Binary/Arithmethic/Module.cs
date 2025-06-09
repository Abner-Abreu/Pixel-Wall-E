namespace Parsing;

public class Mod: Arithmethic
{
    public Mod(int line, int position) : base(line, position) { }

    public override void Evaluate()
    {
        Right.Evaluate();
        Left.Evaluate();
        Value = (int)Right.Value % (int)Left.Value;
        Value = (int)Value;
    }
}