namespace Parsing;

public abstract class Atom : Expression
{
    public Atom(int line,int position) : base(line,position){}
}