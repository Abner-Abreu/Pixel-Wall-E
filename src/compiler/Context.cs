using System.Collections.Generic;
public class Context
{
    public Dictionary<string, int> Labels { set; get; }
    public Dictionary<string, (ExpressionType type , object value)> Vars { set; get; }

    public Context()
    {
        Labels = new Dictionary<string, int>();
        Vars = new Dictionary<string, (ExpressionType type, object value)>();
    }

}