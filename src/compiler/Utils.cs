
public enum TokenType
{
    LABEL,
    VAR,
    FUNCTION,
    INSTRUCTION,
    NUM,
    COLOR,
    ASSING,

    GOTO,

    //Boolean
    AND,
    OR,
    EQUAL,
    LESS,
    MORE,
    LESS_EQUAL,
    MORE_EQUAL,

    //Aritmethic
    PLUS,
    MINUS,
    MULT,
    DIV,
    POW,
    MOD,

    //Signs
    OPENPAR,
    CLOSEPAR,
    OPENCOR,
    CLOSECOR,
    COMMA,
}

public enum ExpressionType
{
    NUM,
    COLOR,
    ARITHMETHIC,
    BOOLEAN,
}
public static class Utils
{
    public static HashSet<string> Functions = new HashSet<string>
    {
        "GetActualX",
        "GetActualY",
        "GetCanvasSize",
        "GetColorCount",
        "IsBrushColor",
        "IsBrushSize",
        "IsCanvasColor",
    };

    public static HashSet<string> Instructions = new HashSet<string>
    {
        "Spawn",
        "Color",
        "Size",
        "DrawLine",
        "DrawCircle",
        "DrawRectangle",
        "Fill",
    };

    public static HashSet<string> Colors = new HashSet<string>
    {
        "Red",
        "Blue",
        "Green",
        "Yellow",
        "Orange",
        "Purple",
        "Black",
        "White",
        "Transparent",
    };
}