namespace Lexical;
using Errors;
public class Lexer
{
    public List<List<Token>> Tokens { private set; get; }
    public List<Error> LexicalErrors { private set; get; }
    public Lexer(string input)
    {
        List<List<Token>> tokens = new List<List<Token>>();
        List<Token> lineTokens = new List<Token>();
        List<Error> errors = new List<Error>();

        input += '$'; //End Of File Sign
        int currentLine = 1;
        int currentPosition = 0;
        for (int i = 0; i < input.Length; i++)
        {
            currentPosition++;
            if (input[i] == ' ') continue;

            if (input[i] == '\n' || input[i] == '\r' || (input[i] == '$' && i + 1 == input.Length))
            {
                if (lineTokens.Count() != 0)
                {
                    tokens.Add(lineTokens);
                    lineTokens = new List<Token>();
                }
                currentPosition = 0;
                currentLine++;
                continue;
            }

            //Sings
            if (input[i] == '(')
            {
                lineTokens.Add(new Token(TokenType.OPENPAR, "(", currentLine, currentPosition));
                continue;
            }
            if (input[i] == ')')
            {
                lineTokens.Add(new Token(TokenType.CLOSEPAR, ")", currentLine, currentPosition));
                continue;
            }
            if (input[i] == '[')
            {
                lineTokens.Add(new Token(TokenType.OPENCOR, "[", currentLine, currentPosition));
                continue;
            }
            if (input[i] == ']')
            {
                lineTokens.Add(new Token(TokenType.CLOSECOR, "]", currentLine, currentPosition));
                continue;
            }
            if (input[i] == ',')
            {
                lineTokens.Add(new Token(TokenType.COMMA, ",", currentLine, currentPosition));
                continue;
            }
            if (input[i] == '"')
            {
                string posibleColor = "";
                int advance = 0;
                for (int j = i + 1; j < input.Length && input[j] != '\n'; j++)
                {
                    advance++;
                    currentPosition++;
                    if (input[j] == '"') break;
                    posibleColor += input[j];
                }
                i += advance;
                if (input[i] == '"')
                {
                    if (IsColor(posibleColor))
                    {
                        lineTokens.Add(new Token(TokenType.COLOR, posibleColor, currentLine, currentPosition));
                    }
                    else
                    {
                        errors.Add(new Error(ErrorType.Lexical, "Invalid Color", currentLine, currentPosition - advance));
                    }
                }
                else
                {
                    errors.Add(new Error(ErrorType.Lexical, "Missing String Closer", currentLine, currentPosition));
                }

                continue;
            }


            //Boolean
            if (input[i] == '&')
            {
                if (i + 1 < input.Length && input[i + 1] == '&')
                {
                    i++;
                    currentPosition++;
                    lineTokens.Add(new Token(TokenType.AND, "&&", currentLine, currentPosition));
                }
                else
                {
                    errors.Add(new Error(ErrorType.Lexical, "Missing &", currentLine, currentPosition));
                }
                continue;
            }
            if (input[i] == '|')
            {
                if (i + 1 < input.Length && input[i + 1] == '|')
                {
                    i++;
                    currentPosition++;
                    lineTokens.Add(new Token(TokenType.OR, "||", currentLine, currentPosition));
                }
                else
                {
                    errors.Add(new Error(ErrorType.Lexical, "Missing |", currentLine, currentPosition));
                }
                continue;
            }
            if (input[i] == '=')
            {
                if (i + 1 < input.Length && input[i + 1] == '=')
                {
                    i++;
                    currentPosition++;
                    lineTokens.Add(new Token(TokenType.EQUAL, "==", currentLine, currentPosition));
                }
                else
                {
                    errors.Add(new Error(ErrorType.Lexical, "Missing =", currentLine, currentPosition));
                }
                continue;
            }
            if (input[i] == '<')
            {
                if (i + 1 < input.Length && input[i + 1] == '=')
                {
                    i++;
                    currentPosition++;
                    lineTokens.Add(new Token(TokenType.LESS_EQUAL, "<=", currentLine, currentPosition));
                }
                else if (i + 1 < input.Length && input[i + 1] == '-')
                {
                    i++;
                    currentPosition++;
                    lineTokens.Add(new Token(TokenType.ASSING, "<-", currentLine, currentPosition));
                }
                else
                {
                    lineTokens.Add(new Token(TokenType.LESS, "<", currentLine, currentPosition));
                }
                continue;
            }
            if (input[i] == '>')
            {
                if (i + 1 < input.Length && input[i + 1] == '=')
                {
                    i++;
                    currentPosition++;
                    lineTokens.Add(new Token(TokenType.MORE_EQUAL, ">=", currentLine, currentPosition));
                }
                else
                {
                    lineTokens.Add(new Token(TokenType.MORE, ">", currentLine, currentPosition));
                }
                continue;
            }

            //Aritmethic
            if (input[i] == '+')
            {
                lineTokens.Add(new Token(TokenType.PLUS, "+", currentLine, currentPosition));
                continue;
            }
            if (input[i] == '-')
            {
                lineTokens.Add(new Token(TokenType.MINUS, "-", currentLine, currentPosition));
                continue;
            }
            if (input[i] == '*')
            {
                if (i + 1 < input.Length && input[i + 1] == '*')
                {
                    i++;
                    currentPosition++;
                    lineTokens.Add(new Token(TokenType.POW, "**", currentLine, currentPosition));
                }
                else
                {
                    lineTokens.Add(new Token(TokenType.MULT, "*", currentLine, currentPosition));
                }
                continue;
            }
            if (input[i] == '/')
            {
                lineTokens.Add(new Token(TokenType.DIV, "/", currentLine, currentPosition));
                continue;
            }
            if (input[i] == '%')
            {
                lineTokens.Add(new Token(TokenType.MOD, "%", currentLine, currentPosition));
                continue;
            }


            if (IsAlphaNum(input[i]) || input[i] == '_')
            {
                string label = "" + input[i];
                int advance = 0;
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (!IsAlphaNum(input[j]) && input[j] != '_' || input[j] == ' ' || input[j] == '\n')
                    {
                        break;
                    }
                    currentPosition++;
                    advance++;
                    label += input[j];
                }
                i += advance;
                if (IsMultidigitNum(label))
                {
                    lineTokens.Add(new Token(TokenType.NUM, label, currentLine, currentPosition - advance));
                    continue;
                }
                if (IsFunction(label))
                {
                    lineTokens.Add(new Token(TokenType.FUNCTION, label, currentLine, currentPosition - advance));
                    continue;
                }
                if (IsInstruction(label))
                {
                    lineTokens.Add(new Token(TokenType.INSTRUCTION, label, currentLine, currentPosition - advance));
                    continue;
                }
                if (label == "GoTo")
                {
                    lineTokens.Add(new Token(TokenType.GOTO, label, currentLine, currentPosition - advance));
                    continue;
                }

                if (IsValidLabel(label))
                {
                    lineTokens.Add(new Token(TokenType.LABEL, label, currentLine, currentPosition - advance));
                    continue;
                }
                else
                {
                    errors.Add(new Error(ErrorType.Lexical, $"Labels can't begin with {input[i - advance]}", currentLine, currentPosition - advance));
                    continue;
                }
            }
            else
            {
                errors.Add(new Error(ErrorType.Lexical, $@"Invalid Character ""{input[i]}""", currentLine, currentPosition));
            }
        }
        Tokens = tokens;
        LexicalErrors = errors;
    }

    private bool IsFunction(string text)
    {
        return Utils.Functions.Contains(text);
    }

    private bool IsAlpha(char character)
    {
        return (character >= 'A' && character <= 'Z') || (character >= 'a' && character <= 'z') || character == 'Ñ' || character == 'ñ';
    }

    private bool IsNum(char character)
    {
        return char.IsDigit(character);
    }

    private bool IsMultidigitNum(string text)
    {
        foreach (char character in text)
        {
            if (!IsNum(character)) return false;
        }
        return true;
    }
    private bool IsAlphaNum(char character)
    {
        return IsAlpha(character) || IsNum(character);
    }

    private bool IsColor(string text)
    {
        return Utils.Colors.Contains(text);
    }

    private bool IsValidLabel(string text)
    {
        return !(IsNum(text[0]) || text[0] == '_');
    }

    private bool IsInstruction(string text)
    {
        return Utils.Instructions.Contains(text);
    }
}