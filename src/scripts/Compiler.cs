using Lexical;
using Parsing;
using Interpret;
using System.Collections.Generic;
using Godot;

namespace Compilation;
public class Compiler
{
    public List<Errors.Error> errors { private set; get; }

    public Compiler(string code, Canvas canvas)
    {
        errors = new List<Errors.Error>();
        Lexer lexer = new Lexer(code);
        errors = lexer.LexicalErrors;
        if (errors.Count > 0)
        {
            return;
        }
        GD.Print("Lexical Analisis Successfull");

        Parser parser = new Parser(lexer.Tokens);
        errors = parser.SintaxErrors;
        if (errors.Count > 0)
        {
            return;
        }
        GD.Print("Parsing Analisis Successfull");

        SementicChecker sementicChecker = new SementicChecker(parser.Program);
        errors = sementicChecker.SemanticErrors;
        if (errors.Count > 0)
        {
            return;
        }
        GD.Print("Semantic Analisis Successfull");
        
        Interpreter interpreter = new Interpreter(parser.Program, canvas, sementicChecker.Context);


        if (interpreter.RuntimeErrors.Count > 0)
        {
            return;
        }
        GD.Print("Execution Successfull");
    }
}