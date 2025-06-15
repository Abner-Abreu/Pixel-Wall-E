using Lexical;
using Errors;
using Parsing;
using Interpret;
using System.Collections.Generic;
using System.Linq;

namespace Compilation;
public class Compiler
{
    public List<Error> errors { private set; get; }

    public Compiler(string code, ref Canvas canvas)
    {
        Lexer lexer = new Lexer(code);
        errors = lexer.LexicalErrors;
        if (errors.Count > 0)
        {
            return;
        }

        Parser parser = new Parser(lexer.Tokens);
        errors = parser.SintaxErrors;
        if (errors.Count > 0)
        {
            return;
        }

        SementicChecker sementicChecker = new SementicChecker(parser.Program);
        errors = sementicChecker.SemanticErrors;
        if (errors.Count > 0)
        {
            return;
        }

        Interpreter interpreter = new Interpreter(parser.Program, ref canvas, sementicChecker.Context);
        errors = interpreter.RuntimeErrors;
    }
}