using System.Collections.Generic;
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
    VAR,
    ASSING,
    LABEL,
    NONE
}

public static class Utils
{

    public static Dictionary<string, (ExpressionType returnType, ExpressionType[]  paramType)> Functions = new Dictionary<string, (ExpressionType, ExpressionType[])>
    {
        //Instructions
        { "Spawn", (ExpressionType.NONE,new ExpressionType[] {ExpressionType.NUM , ExpressionType.NUM})},
        {"Color", (ExpressionType.NONE,new ExpressionType[] {ExpressionType.COLOR})},
        {"Size", (ExpressionType.NONE,new ExpressionType[] {ExpressionType.NUM})},
        {"DrawLine", (ExpressionType.NONE,new ExpressionType[] {ExpressionType.NUM , ExpressionType.NUM, ExpressionType.NUM})},
        {"DrawRectangle", (ExpressionType.NONE,new ExpressionType[] {ExpressionType.NUM , ExpressionType.NUM, ExpressionType.NUM, ExpressionType.NUM, ExpressionType.NUM})},
        {"DrawCircle", (ExpressionType.NONE,new ExpressionType[]{ExpressionType.NUM,ExpressionType.NUM, ExpressionType.NUM})},
        {"Fill", (ExpressionType.NONE,new ExpressionType[] {})},

        //Functions
        {"GetActualX",(ExpressionType.NUM, new ExpressionType[]{})},
        {"GetActualY",(ExpressionType.NUM, new ExpressionType[]{})},
        {"GetCanvasSize",(ExpressionType.NUM, new ExpressionType[]{})},
        {"GetColorCount",(ExpressionType.NUM, new ExpressionType[]{ExpressionType.COLOR,ExpressionType.NUM,ExpressionType.NUM,ExpressionType.NUM,ExpressionType.NUM})},
        {"IsBrushColor", (ExpressionType.NUM, new ExpressionType[]{ExpressionType.COLOR})},
        {"IsBrushSize", (ExpressionType.NUM, new ExpressionType[]{ExpressionType.NUM})},
        {"IsCanvasColor", (ExpressionType.NUM, new ExpressionType[]{ExpressionType.COLOR})},
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