namespace Lexical;

public enum TokenType
{
    LABEL,
    VAR,
    FUNCTION,
    NUM,
    COLOR,

    ASSING,
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
    QUOTE,
}

public static class Utils
{
    public static HashSet<string> Functions = new HashSet<string>
    {
        "Spawn",
        "Color",
        "Size",
        "DrawLine",
        "DrawCircle",
        "DrawRectangle",
        "Fill",
        "GetActualX",
        "GetActualY",
        "GetCanvasSize",
        "GetColorCount",
        "IsBrushColor",
        "IsBrushSize",
        "IsCanvasColor",
        "GoTo"
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