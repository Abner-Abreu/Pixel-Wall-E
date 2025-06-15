namespace Errors;

public enum ErrorType
{
    Lexical,
    Syntax,
    Semantic,
    Runtime
}
public class Error
{
    public readonly int Line;
    public readonly int Position;
    public readonly ErrorType ErrorType;
    public readonly string Message;

    public Error(ErrorType type,string message, int line, int position)
    {
        ErrorType = type;
        Line = line;
        Position = position;
        Message = message;
    }

    public string GetError()
    {
        return $"{ErrorType} Error: {Message} in {Line} line, {Position} position";
    }
}