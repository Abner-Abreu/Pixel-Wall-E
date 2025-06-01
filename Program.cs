using Lexical;
using Errors;
public class Program
{
    public static void Main()
    {
        string testInput = @"Spawn(1,2)
Color(""Black"")
n <- 5
k <- 3 + 3 * 10
n <- k * 2
actual_x <- GetActualX()
i <- 0
$
loop1
DrawLine(1,0,1)
i <- i + 1
is_brush_color_blue <- IsBrushColor(""Blue"")
GoTo [loop_ends_here] (is_brush_color_blue == 1)
GoTo [loop1] (i < 10)
Color(""Blue"")
GoTo [loop1] (1 == 1)
        
loop_ends_here";
        Lexer lexer = new Lexer(testInput);

        foreach (List<Token> tokens in lexer.Tokens)
        {
            foreach (Token token in tokens)
            {
                Console.Write(token.Type + " ");
            }
            Console.WriteLine();
        }
        foreach (Error error in lexer.LexicalErrors)
        {
            Console.WriteLine(error.GetError());
        }
    }
    
}