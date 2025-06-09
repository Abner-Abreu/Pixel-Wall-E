namespace Parsing;

using Errors;
public class Less : Boolean
{
    public Less(int line, int position) : base(line, position){}
    public override void Evaluate()
    {
        Right.Evaluate();
        Left.Evaluate();

        if ((int)Right.Value < (int)Left.Value)
        {
            Value = 1;
        }
        else
        {
            Value = 0;
        }
    }
}