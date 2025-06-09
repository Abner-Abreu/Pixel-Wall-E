namespace Parsing;

using Errors;
public class More_Equal : Boolean
{
    public More_Equal(int line, int position) : base(line, position){}
    public override void Evaluate()
    {
        Right.Evaluate();
        Left.Evaluate();

        if ((int)Right.Value >= (int)Left.Value)
        {
            Value = 1;
        }
        else
        {
            Value = 0;
        }
    }
}