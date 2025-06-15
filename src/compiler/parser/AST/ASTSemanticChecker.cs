using Parsing;
using Errors;
using Boolean = Parsing.Boolean;
using System.Collections.Generic;
public class SementicChecker
{
    public Context Context { private set; get; }
    public List<Error> SemanticErrors { private set; get; }

    public SementicChecker(List<AST?> program)
    {
        Context = new Context();
        SemanticErrors = new List<Error>();

        foreach (AST? node in program)
        {
            CheckSemantic(node, Context, SemanticErrors);
        }
    }
    public bool CheckSemantic(AST? node, Context context, List<Error> semanticErrors)
    {
        switch (node)
        {
            case Atom:
                return CheckAtom((Atom)node, context, semanticErrors);
            case Arithmethic:
                return CheckArithmethic((Arithmethic)node, context, semanticErrors);
            case Boolean:
                return CheckBoolean((Boolean)node, context, semanticErrors);
            case Function:
                return CheckFunction((Function)node, context, semanticErrors);
            case GoTo:
                return CheckGoTo((GoTo)node, context, semanticErrors);
            case Assing:
                return CheckAssign((Assing)node, context, semanticErrors);
        }
        return false;
    }
    private bool CheckAtom(Atom? atom, Context context, List<Error> sementicErrors)
    {
        switch (atom)
        {
            case Color:
                atom.Type = ExpressionType.COLOR;
                return true;
            case Number:
                atom.Type = ExpressionType.NUM;
                return true;
            case Variable:
                //Check variable declaration
                if (context.Vars.ContainsKey(((Variable)atom).VarName) == false)
                {
                    sementicErrors.Add(new Error(ErrorType.Semantic, $@"Variable ""{((Variable)atom).VarName}"" does not exist in the current context", atom.Line, atom.Position));
                    return false;
                }
                else
                {
                    atom.Type = context.Vars[((Variable)atom).VarName].type;
                    return true;
                }
            case Label:
                //Check label declaration
                if (context.Labels.ContainsKey(((Label)atom).Identifier) == true)
                {
                    sementicErrors.Add(new Error(ErrorType.Semantic, "Label declaration can not be duplicated", atom.Line, atom.Position));
                    return false;
                }
                if (context.Vars.ContainsKey(((Label)atom).Identifier) == true)
                {
                    sementicErrors.Add(new Error(ErrorType.Semantic, "Label and Variables can not have the same Identifier", atom.Line, atom.Position));
                    return false;
                }
                context.Labels.Add(((Label)atom).Identifier, atom.Line);
                return true;
            default:
                return false;
        }
    }

    private bool CheckArithmethic(Arithmethic node, Context context, List<Error> semanticErrors)
    {
        bool right = CheckSemantic(node.Right, context, semanticErrors);
        bool left = CheckSemantic(node.Left, context, semanticErrors);

        //Check types
        if (node.Right.Type != ExpressionType.NUM)
        {
            semanticErrors.Add(new Error(ErrorType.Semantic, "Invalid type, expected numerical expression type", node.Right.Line, node.Right.Position));
            right = false;
        }
        if (node.Left.Type != ExpressionType.NUM)
        {
            semanticErrors.Add(new Error(ErrorType.Semantic, "Invalid type, expected numerical expression type", node.Left.Line, node.Left.Position));
            left = false;
        }

        if (right && left)
        {
            node.Type = ExpressionType.NUM;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckGoTo(GoTo goTo, Context context, List<Error> semanticErrors)
    {
        bool label = true;
        if (context.Labels.ContainsKey(((Label)goTo.Left).Identifier) == false)
        {
            semanticErrors.Add(new Error(ErrorType.Semantic, $@"Label ""{((Label)goTo.Left).Identifier}"" does not exist in the current context", goTo.Line, goTo.Position));
            label = false;
        }

        bool condition = CheckSemantic(goTo.Right, context, semanticErrors);
        if (goTo.Right.Type != ExpressionType.BOOLEAN)
        {
            semanticErrors.Add(new Error(ErrorType.Semantic, "Invalid type of expression, most be a Boolean Expression", goTo.Line, goTo.Position));
            condition = false;
        }
        return label && condition;
    }

    private bool CheckBoolean(Boolean node, Context context, List<Error> semanticErrors)
    {
        bool right = CheckSemantic(node.Right, context, semanticErrors);
        bool left = CheckSemantic(node.Left, context, semanticErrors);

        //Check Types
        if (node is Or || node is And)
        {
            //And and Or expressions
            if (node.Right.Type != ExpressionType.BOOLEAN)
            {
                semanticErrors.Add(new Error(ErrorType.Semantic, "Invalid type, expected boolean expression type", node.Right.Line, node.Right.Position));
                right = false;
            }
            if (node.Left.Type != ExpressionType.BOOLEAN)
            {
                semanticErrors.Add(new Error(ErrorType.Semantic, "Invalid type, expected boolean expression type", node.Left.Line, node.Left.Position));
                left = false;
            }
        }
        else
        {
            //Equal, More, Less, More_Equal, Less_Equal expressions
            if (node.Right.Type != ExpressionType.NUM)
            {
                semanticErrors.Add(new Error(ErrorType.Semantic, "Invalid type, expected numerical expression type", node.Right.Line, node.Right.Position));
                right = false;
            }
            if (node.Left.Type != ExpressionType.NUM)
            {
                semanticErrors.Add(new Error(ErrorType.Semantic, "Invalid type, expected numerical expression type", node.Left.Line, node.Left.Position));
                left = false;
            }
        }

        if (right && left)
        {
            node.Type = ExpressionType.BOOLEAN;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckAssign(Assing node, Context context, List<Error> semanticErrors)
    {
        bool right = CheckSemantic(node.Right, context, semanticErrors);
        bool var = true;
        //Check duplicate with Vars
        if (context.Vars.ContainsKey(((Variable)node.Left).VarName) == true)
        {
            //Check types
            if (node.Right.Type != context.Vars[((Variable)node.Left).VarName].type)
            {
                semanticErrors.Add(new Error(ErrorType.Semantic, $"{node.Right.Type} type expression can not be assigned to {context.Vars[((Variable)node.Left).VarName].type} variable types", node.Right.Line, node.Right.Position));
                var = false;
            }
        }
        else
        {
            //Check duplicate with Labels
            if (context.Labels.ContainsKey(((Variable)node.Left).VarName) == true)
            {
                semanticErrors.Add(new Error(ErrorType.Semantic, "Variables and Labels can not have the same identifier", node.Left.Line, node.Left.Position));
                var = false;
            }

            object valueType = node.Right.Type == ExpressionType.NUM ? new bool() : new int();
            context.Vars.Add(((Variable)node.Left).VarName, (node.Right.Type, valueType));
        }
        return var && right;
    }

    private bool CheckFunction(Function function, Context context, List<Error> semanticErrors)
    {
        if (function.Parameters.Count != Utils.Functions[function.Identifier].paramType.Length)
        {
            semanticErrors.Add(new Error(ErrorType.Semantic, $"Wrong number of parameters {function.Parameters.Count}, expected {Utils.Functions[function.Identifier].paramType.Length}", function.Line, function.Position));
            return false;
        }
        List<bool> paramsCheck = new List<bool>();
        for (int i = 0; i < function.Parameters.Count; i++)
        {
            paramsCheck.Add(CheckSemantic(function.Parameters[i], context, semanticErrors));
            if (function.Parameters[i].Type != Utils.Functions[function.Identifier].paramType[i])
            {
                semanticErrors.Add(new Error(ErrorType.Semantic, $"Invalid parameter type, expected {Utils.Functions[function.Identifier].paramType[i]} type", function.Parameters[i].Line, function.Parameters[i].Position));
                paramsCheck[i] = false;
            }
        }

        foreach (var check in paramsCheck)
        {
            if (check == false) return false;
        }

        function.Type = Utils.Functions[function.Identifier].returnType;
        return true;
    }
}