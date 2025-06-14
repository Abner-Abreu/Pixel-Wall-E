using Lexical;
using Errors;
using Parsing;
public class Program
{
    public static void Main()
    {
        string testInput = @"Spawn(1,2)
Color(""Black"")
n <- 5
k <- 3 + 3 * 10
n <- k * 2
f <- 4 >= 2 && 4 == 4 || 4 <= 2
actual_x <- GetActualX()
i <- 3 % 2 + 4 ** 2 - 10 / 2
IsBrushSize(GetActualY())
loop1
DrawLine(1,0,1)
i <- i + 1
Malanga
is_brush_color_blue <- IsBrushColor(""Blue"")
GoTo[Malanga](1 == 1)
Color(""Blue"")

        
loop_ends_here";
        Lexer lexer = new Lexer(testInput);
        Parser parser = new Parser(lexer.Tokens);
        foreach (var erro in lexer.LexicalErrors)
        {
            Console.WriteLine(erro.GetError());
        }
        foreach (var item in parser.Program)
        {
            ASTPrinter.PrintAST(item, 0);
        }
        foreach (var erro in parser.SintaxErrors)
        {
            Console.WriteLine(erro.GetError());
        }
        SementicChecker checker = new SementicChecker(parser.Program);
        foreach (var erro in checker.SemanticErrors)
        {
            Console.WriteLine(erro.GetError());
        }
    }
}