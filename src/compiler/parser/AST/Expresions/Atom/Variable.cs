namespace Parsing;
public class Variable : Atom
{

    public string VarName { private set; get; }
    public Variable(string varName, int line, int position) : base(line, position)
    {
        VarName = varName;
        Type = ExpressionType.VAR;
    }

}