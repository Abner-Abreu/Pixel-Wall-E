
public enum TokenType
{
    LABEL,
    VAR,
    FUNCTION,
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
    VAR
}

public enum ReturnType
{
    NONE,
    NUMBER,
    BOOLEAN,
}
public static class Utils
{

    public static Dictionary<string, (ReturnType returnType, ExpressionType[]  paramType)> Functions = new Dictionary<string, (ReturnType, ExpressionType[])>
    {
        //Instructions
        { "Spawn", (ReturnType.NONE,new ExpressionType[] {ExpressionType.NUM , ExpressionType.NUM})},
        {"Color", (ReturnType.NONE,new ExpressionType[] {ExpressionType.COLOR})},
        {"Size", (ReturnType.NONE,new ExpressionType[] {ExpressionType.NUM})},
        {"DrawLine", (ReturnType.NONE,new ExpressionType[] {ExpressionType.NUM , ExpressionType.NUM, ExpressionType.NUM})},
        {"DrawRectangle", (ReturnType.NONE,new ExpressionType[] {ExpressionType.NUM , ExpressionType.NUM, ExpressionType.NUM, ExpressionType.NUM, ExpressionType.NUM})},
        {"DrawCircle", (ReturnType.NONE,new ExpressionType[]{ExpressionType.NUM,ExpressionType.NUM, ExpressionType.NUM})},
        {"Fill", (ReturnType.NONE,new ExpressionType[] {})},

        //Functions
        {"GetActualX",(ReturnType.NUMBER, new ExpressionType[]{})},
        {"GetActualY",(ReturnType.NUMBER, new ExpressionType[]{})},
        {"GetCanvasSize",(ReturnType.NUMBER, new ExpressionType[]{})},
        {"GetColorCount",(ReturnType.NUMBER, new ExpressionType[]{ExpressionType.COLOR,ExpressionType.NUM,ExpressionType.NUM,ExpressionType.NUM,ExpressionType.NUM})},
        {"IsBrushColor", (ReturnType.BOOLEAN, new ExpressionType[]{ExpressionType.COLOR})},
        {"IsBrushSize", (ReturnType.BOOLEAN, new ExpressionType[]{ExpressionType.NUM})},
        {"IsCanvasColor", (ReturnType.BOOLEAN, new ExpressionType[]{ExpressionType.COLOR})},
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