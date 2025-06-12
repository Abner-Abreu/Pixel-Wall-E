namespace Parsing;

using Lexical;
using Errors;
public class Parser
{
    public Context Context { private set; get; }
    public List<Expression?> Program { private set; get; }

    public List<Error> SintaxErrors { private set; get; }
    public List<Error> SemanticErrors { private set; get; }
    public Parser(List<List<Token>> tokens)
    {
        Context = new Context();
        Program = new List<Expression?>();
        SintaxErrors = new List<Error>();
        SemanticErrors = new List<Error>();
        foreach (List<Token> tokenLine in tokens)
        {
            if (tokenLine[0].Type == TokenType.LABEL)
            {
                ParseLabelDeclaration(tokenLine);
                continue;
            }
            if (tokenLine[0].Type == TokenType.VAR)
            {
                ParseVarAssign(tokenLine);
                continue;
            }
            if (tokenLine[0].Type == TokenType.GOTO)
            {
                ParseGoToExpression(tokenLine);
                continue;
            }
            if (tokenLine[0].Type == TokenType.FUNCTION)
            {
                Program.Add(ParseFunctionExpression(tokenLine, 0, tokenLine.Count - 1));
                continue;
            }
            SintaxErrors.Add(new Error(ErrorType.Syntax, $"Invalid expression {tokenLine[0].Content}", tokenLine[0].Line, tokenLine[0].Position));
        }
    }

    private Expression? ParseExpression(List<Token> tokens, int first, int last)
    {
        if (IsBooleanExpression(tokens, first, last)) return ParseBooleanExpression(tokens, first, last);
        if (IsArithmethicExpression(tokens, first, last)) return ParseArithmethicExpression(tokens, first, last);
        if (tokens[first].Type == TokenType.FUNCTION) return ParseFunctionExpression(tokens, first, last);
        if (first == last) return ParseAtomExpression(tokens[first]);

        SintaxErrors.Add(new Error(ErrorType.Syntax, "Invalid Expresion", tokens[first].Line, tokens[first].Position));
        return null;
    }

    private Expression? ParseAtomExpression(Token token)
    {
        switch (token.Type)
        {
            case TokenType.VAR:
                Variable variable = new Variable(token.Content, token.Line, token.Position);
                return variable;
            case TokenType.NUM:
                Number number = new Number(token.Line, token.Position, token);
                return number;
            case TokenType.COLOR:
                Color color = new Color(token.Line, token.Position, token);
                return color;
            default: return null;    
        }
    }
    private bool IsBooleanExpression(List<Token> tokens, int first, int last)
    {
        for (int i = first; i <= last; i++)
        {
            if (tokens[i].Type == TokenType.AND
            || tokens[i].Type == TokenType.OR
            || tokens[i].Type == TokenType.LESS
            || tokens[i].Type == TokenType.LESS_EQUAL
            || tokens[i].Type == TokenType.MORE
            || tokens[i].Type == TokenType.MORE_EQUAL
            || tokens[i].Type == TokenType.EQUAL)
                return true;
        }
        return false;
    }

    private bool IsArithmethicExpression(List<Token> tokens, int first, int last)
    {
        for (int i = first; i <= last; i++)
        {
            if (tokens[i].Type == TokenType.PLUS
            || tokens[i].Type == TokenType.DIV
            || tokens[i].Type == TokenType.MULT
            || tokens[i].Type == TokenType.MOD
            || tokens[i].Type == TokenType.MINUS)
                return true;
        }
        return false;
    }

    private Expression? ParseBooleanExpression(List<Token> tokens, int first, int last)
    {
        //Search OR expressions
        for (int i = first; i < last; i++)
        {
            if (tokens[i].Type == TokenType.OR)
            {
                Or or = new Or(tokens[i].Line, tokens[i].Position);
                or.Left = ParseExpression(tokens, first, i - 1);
                or.Right = ParseExpression(tokens, i + 1, last);
                return or;
            }
        }
        //Search AND expressions
        for (int i = first; i < last; i++)
        {
            if (tokens[i].Type == TokenType.AND)
            {
                And and = new And(tokens[i].Line, tokens[i].Position);
                and.Left = ParseExpression(tokens, first, i - 1);
                and.Right = ParseExpression(tokens, i + 1, last);
                return and;
            }
        }
        //Search EQUAL, LESS, MORE, LESS_EQUAL, MORE_EQUAL expressions
        for (int i = first; i < last; i++)
        {
            switch (tokens[i].Type)
            {
                case TokenType.EQUAL:
                    Equal equal = new Equal(tokens[i].Line, tokens[i].Position);
                    equal.Left = ParseExpression(tokens, first, i - 1);
                    equal.Right = ParseExpression(tokens, i + 1, last);
                    return equal;
                case TokenType.LESS:
                    Less less = new Less(tokens[i].Line, tokens[i].Position);
                    less.Left = ParseExpression(tokens, first, i - 1);
                    less.Right = ParseExpression(tokens, i + 1, last);
                    return less;
                case TokenType.MORE:
                    More more = new More(tokens[i].Line, tokens[i].Position);
                    more.Left = ParseExpression(tokens, first, i - 1);
                    more.Right = ParseExpression(tokens, i + 1, last);
                    return more;
                case TokenType.LESS_EQUAL:
                    Less_Equal less_Equal = new Less_Equal(tokens[i].Line, tokens[i].Position);
                    less_Equal.Left = ParseExpression(tokens, first, i - 1);
                    less_Equal.Right = ParseExpression(tokens, i + 1, last);
                    return less_Equal;
                case TokenType.MORE_EQUAL:
                    More_Equal more_Equal = new More_Equal(tokens[i].Line, tokens[i].Position);
                    more_Equal.Left = ParseExpression(tokens, first, i - 1);
                    more_Equal.Right = ParseExpression(tokens, i + 1, last);
                    return more_Equal;
                default: continue;
            }
        }
        SintaxErrors.Add(new Error(ErrorType.Syntax, "Invalid Expression", tokens[first].Line, tokens[first].Position));
        return null;
    }

    private Expression? ParseArithmethicExpression(List<Token> tokens, int first, int last)
    {
        //Search POW expressions
        for (int i = first; i < last; i++)
        {
            switch (tokens[i].Type)
            {
                case TokenType.POW:
                    Pow pow = new Pow(tokens[i].Line, tokens[i].Position);
                    pow.Left = ParseExpression(tokens, first, i - 1);
                    pow.Right = ParseExpression(tokens, i + 1, last);
                    return pow;
            }
        }
        //Search MULT, DIV, MOD expressions
        for (int i = first; i < last; i++)
        {
            switch (tokens[i].Type)
            {
                case TokenType.MULT:
                    Mult mult = new Mult(tokens[i].Line, tokens[i].Position);
                    mult.Left = ParseExpression(tokens, first, i - 1);
                    mult.Right = ParseExpression(tokens, i + 1, last);
                    return mult;
                case TokenType.DIV:
                    Div div = new Div(tokens[i].Line, tokens[i].Position);
                    div.Left = ParseExpression(tokens, first, i - 1);
                    div.Right = ParseExpression(tokens, i + 1, last);
                    return div;
                case TokenType.MOD:
                    Mod mod = new Mod(tokens[i].Line, tokens[i].Position);
                    mod.Left = ParseExpression(tokens, first, i - 1);
                    mod.Right = ParseExpression(tokens, i + 1, last);
                    return mod;
            }
        }
        //Search ADD, SUBS expressions
        for (int i = first; i < last; i++)
        {
            switch (tokens[i].Type)
            {
                case TokenType.PLUS:
                    Add add = new Add(tokens[i].Line, tokens[i].Position);
                    add.Left = ParseExpression(tokens, first, i - 1);
                    add.Right = ParseExpression(tokens, i + 1, last);
                    return add;
                case TokenType.MINUS:
                    Subs subs = new Subs(tokens[i].Line, tokens[i].Position);
                    subs.Left = ParseExpression(tokens, first, i - 1);
                    subs.Right = ParseExpression(tokens, i + 1, last);
                    return subs;
            }
        }
        SintaxErrors.Add(new Error(ErrorType.Syntax, "Invalid Expression", tokens[first].Line, tokens[first].Position));
        return null;
    }
    private void ParseLabelDeclaration(List<Token> tokens)
    {
        if (tokens.Count != 1)
        {
            SintaxErrors.Add(new Error(ErrorType.Syntax, "Label most be the single element in the line", tokens[0].Line, tokens[0].Position));
        }
        else if (Context.Labels.ContainsKey(tokens[0].Content))
        {
            SemanticErrors.Add(new Error(ErrorType.Semantic, "Labels can not be duplicated", tokens[0].Line, tokens[0].Position));
        }
        else if (Context.Vars.ContainsKey(tokens[0].Content))
        {
            SemanticErrors.Add(new Error(ErrorType.Semantic, "Labels and Variables can not have the same identifier", tokens[0].Line, tokens[0].Position));
        }
        else
        {
            Context.Labels.Add(tokens[0].Content, tokens[0].Line);
        }
    }

    private void ParseVarAssign(List<Token> tokens)
    {
        if (tokens[1].Type != TokenType.ASSING)
        {
            SintaxErrors.Add(new Error(ErrorType.Syntax, @"Expected assing (""<-"") expresiom", tokens[1].Line, tokens[0].Position));
        }
        else if (Context.Labels.ContainsKey(tokens[0].Content))
        {
            SemanticErrors.Add(new Error(ErrorType.Semantic, "Labels and Variables can not have the same identifier", tokens[0].Line, tokens[0].Position));
        }
        else
        {
            Variable variable = new Variable(tokens[0].Content, tokens[0].Line, tokens[0].Position);
            Assing assing = new Assing(tokens[1].Line, tokens[1].Position);
            assing.Left = variable;
            assing.Right = ParseExpression(tokens, 2, tokens.Count - 1);
            Program.Add(assing);
        }
    }

    private void ParseGoToExpression(List<Token> tokens)
    {
        if (tokens[1].Type != TokenType.OPENCOR)
        {
            SintaxErrors.Add(new Error(ErrorType.Syntax, @"Invalid expression, ""["" expected", tokens[1].Line, tokens[1].Position));
            return;
        }
        if (tokens[2].Type != TokenType.LABEL)
        {
            SemanticErrors.Add(new Error(ErrorType.Semantic, @"Invalid expression, label expected", tokens[2].Line, tokens[2].Position));
            return;
        }
        if (tokens[3].Type != TokenType.CLOSECOR)
        {
            SintaxErrors.Add(new Error(ErrorType.Syntax, @"Invalid expression, ""]"" expected", tokens[3].Line, tokens[3].Position));
            return;
        }
        if (tokens[4].Type != TokenType.OPENPAR)
        {
            SintaxErrors.Add(new Error(ErrorType.Syntax, @"Invalid expression, ""("" expected", tokens[4].Line, tokens[4].Position));
            return;
        }
        if (tokens[^1].Type != TokenType.CLOSEPAR)
        {
            SintaxErrors.Add(new Error(ErrorType.Syntax, @"Invalid expression, "")"" expected", tokens[^1].Line, tokens[^1].Position));
            return;
        }
        GoTo goTo = new GoTo(tokens[0].Line, tokens[0].Position);
        goTo.Label = tokens[2].Content;
        goTo.Condition = ParseExpression(tokens, 5, tokens.Count - 2);
        Program.Add(goTo);
    }

    private Expression? ParseFunctionExpression(List<Token> tokens, int first, int last)
    {
        if (tokens[first + 1].Type != TokenType.OPENPAR)
        {
            SintaxErrors.Add(new Error(ErrorType.Syntax, @"Invalid expression, ""("" expected", tokens[first + 1].Line, tokens[first + 1].Position));
            return null;
        }
        bool closeParFinded = false;
        for (int i = last; i >= first+2; i--)
        {
            if (tokens[i].Type == TokenType.CLOSEPAR)
            {
                closeParFinded = true;
                if (i != last)
                {
                    SintaxErrors.Add(new Error(ErrorType.Syntax, @"Invalid expression, last token most be a "")""", tokens[last].Line, tokens[last].Line));
                    return null;
                }
                else
                {
                    break;
                }
            }
        }
        if (closeParFinded == false)
        {
            SintaxErrors.Add(new Error(ErrorType.Syntax, @"Invalid expression, "")"" expected", tokens[last].Line, tokens[last].Position));
            return null;
        }
        if (tokens[last].Type != TokenType.CLOSEPAR)
            {

                return null;
            }
        Function function = new Function(tokens[first].Content, tokens[first].Line, tokens[first].Position);
        int checkPoint = first + 2;
        if (tokens[checkPoint].Type == TokenType.CLOSEPAR) return function;
        for (int i = first + 2; i <= last; i++)
            {
                if (tokens[i].Type == TokenType.COMMA || i == last)
                {
                    function.Parameters.Add(ParseExpression(tokens, checkPoint, i - 1));
                    checkPoint = i + 1;
                }
            }
        return function;
    }
}