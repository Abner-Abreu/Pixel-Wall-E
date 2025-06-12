namespace Lexical;

public class Token
{
    public TokenType Type { private set; get; }
    public string Content { private set; get; }
    public int Line { private set; get; }
    public int Position { private set; get; }

    public Token(TokenType type, string content, int line, int position)
    {
        Type = type;
        Content = content;
        Line = line;
        Position = position;
    }

    public Token(TokenType type , Token token)
    {
        Type = type;
        Content = token.Content;
        Line = token.Line;
    }
}