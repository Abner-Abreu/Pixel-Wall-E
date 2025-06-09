namespace Parsing;

public class More : Boolean
{
    public More(int line, int position) : base(line, position){}
    public override void Evaluate()
    {
        Right.Evaluate();
        Left.Evaluate();

        if ((int)Right.Value > (int)Left.Value)
        {
            Value = 1;
        }
        else
        {
            Value = 0;
        }
    }
}