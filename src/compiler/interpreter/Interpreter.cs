namespace Interpret;

using Parsing;
using Boolean = Parsing.Boolean;
using Errors;
using System.Collections.Generic;
using System;
public class Interpreter
{
    public List<Error> RuntimeErrors { private set; get; }
    public Context Context { private set; get; }
    public ICanvas Canva { private set; get; }

    private bool SpawnFound;
    public Interpreter(List<AST> nodes, Canvas canvas, Context context)
    {
        RuntimeErrors = new List<Error>();
        Canva = canvas;
        Context = context;
        Evaluate(nodes, 0);
    }

    private void Evaluate(List<AST> nodes, int index)
    {
        //Check if first line is Spawn
        if (nodes[0] is not Function)
        {
            RuntimeErrors.Add(new Error(ErrorType.Runtime, "First line most be a Spawn", nodes[0].Line, nodes[0].Position));
            return;
        }
        else
        {
            if (((Function)nodes[0]).Identifier != "Spawn")
            {
                RuntimeErrors.Add(new Error(ErrorType.Runtime, "First line most be a Spawn", nodes[0].Line, nodes[0].Position));
                return;
            }
        }

        for (int i = index; i < nodes.Count; i++)
        {
            if (RuntimeErrors.Count > 0)
            {
                break;
            }
            if (nodes[i] is GoTo)
            {
                i = EvaluateGoTo((GoTo)nodes[i], nodes, i);
            }
            if (nodes[i] is Label)
            {
                continue;
            }
            Evaluate(nodes[i]);
        }
    }
    private object? Evaluate(AST node)
    {
        if (CheckErrors() == true)
        {
            return null;
        }
        switch (node)
        {
            case Boolean:
                return EvaluateBoolean((Boolean)node);
            case Arithmethic:
                return EvaluateArithmethic((Arithmethic)node);
            case Assing:
                EvaluateAssign((Assing)node);
                return null;
            case Atom:
                return EvaluateAtom((Atom)node);
            case Function:
                return EvaluateFunction((Function)node);
        }
        return null;
    }

    private bool CheckErrors()
    {
        if (Canva.GetErrors() != null)
        {
            RuntimeErrors.Add(Canva.GetErrors());
        }
        return RuntimeErrors.Count > 0;
    }

    private int EvaluateGoTo(GoTo goTo, List<AST> nodes, int index)
    {
        // Validar Left
        object cond = Evaluate(goTo.Right);
        if (cond is not int condInt)
        {
            RuntimeErrors.Add(new Error(ErrorType.Runtime, "Invalid condition for GoTo", goTo.Line, goTo.Position));
            return index;
        }

        if (condInt == 1)
        {
            if (goTo.Left is not Label lbl)
            {
                RuntimeErrors.Add(new Error(ErrorType.Runtime, "GoTo must reference a Label", goTo.Line, goTo.Position));
                return index;
            }

            if (!Context.Labels.ContainsKey(lbl.Identifier))
            {
                RuntimeErrors.Add(new Error(ErrorType.Runtime, $"Label '{lbl.Identifier}' not found", goTo.Line, goTo.Position));
                return index;
            }
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Line >= Context.Labels[lbl.Identifier])
                {
                    return i;
                }
            }
        }

        return index;
    }


    private object EvaluateAtom(Atom node)
    {
        switch (node)
        {
            case Variable:
                if (Context.Vars.ContainsKey(((Variable)node).VarName))
                {
                    return Context.Vars[((Variable)node).VarName].value;
                }
                else
                {
                    RuntimeErrors.Add(new Error(ErrorType.Runtime, "Unnasigned variable use", node.Line, node.Position));
                    return null;
                }
            case Color:
                return ((Color)node).Content;
            case Number:
                return ((Number)node).Value;
        }
        return null;
    }
    private int? EvaluateBoolean(Boolean node)
    {
        switch (node)
        {
            case And:
                return (int)Evaluate(node.Left) == 1 && (int)Evaluate(node.Right) == 1 ? 1 : 0;
            case Or:
                return (int)Evaluate(node.Left) == 1 || (int)Evaluate(node.Right) == 1 ? 1 : 0;
            case Equal:
                return (int)Evaluate(node.Left) == (int)Evaluate(node.Right) ? 1 : 0;
            case Less:
                return (int)Evaluate(node.Left) < (int)Evaluate(node.Right) ? 1 : 0;
            case Less_Equal:
                return (int)Evaluate(node.Left) <= (int)Evaluate(node.Right) ? 1 : 0;
            case More:
                return (int)Evaluate(node.Left) > (int)Evaluate(node.Right) ? 1 : 0;
            case More_Equal:
                return (int)Evaluate(node.Left) >= (int)Evaluate(node.Right) ? 1 : 0;
        }
        return null;
    }

    private int? EvaluateArithmethic(Arithmethic node)
    {
        switch (node)
        {
            case Add:
                return (int)Evaluate(node.Left) + (int)Evaluate(node.Right);
            case Subs:
                return (int)Evaluate(node.Left) - (int)Evaluate(node.Right);
            case Mult:
                return (int)Evaluate(node.Left) * (int)Evaluate(node.Right);
            case Div:
                if ((int)Evaluate(node.Left) == 0 || (int)Evaluate(node.Right) == 0)
                {
                    RuntimeErrors.Add(new Error(ErrorType.Runtime, "Division by zero is not defined", node.Line, node.Position));
                    return null;
                }
                else
                {
                    return (int)Evaluate(node.Left) / (int)Evaluate(node.Right);
                }
            case Mod:
                if ((int)Evaluate(node.Left) == 0 || (int)Evaluate(node.Right) == 0)
                {
                    RuntimeErrors.Add(new Error(ErrorType.Runtime, "Division by zero is not defined", node.Line, node.Position));
                    return null;
                }
                else
                {
                    return (int)Evaluate(node.Left) % (int)Evaluate(node.Right);
                }
            case Pow:
                return (int)Math.Pow((double)Evaluate(node.Left), (double)Evaluate(node.Right));
        }
        return null;
    }

    private void EvaluateAssign(Assing node)
    {
        Context.Vars[((Variable)node.Left).VarName] = (node.Type, Evaluate(node.Right));
    }

    private int? EvaluateFunction(Function function)
    {
        Canva.SetExecutePosition(function);
        switch (function.Identifier)
        {
            case "Spawn":
                if (SpawnFound)
                {
                    RuntimeErrors.Add(new Error(ErrorType.Runtime, "Spawn instruction can not be duplicated", function.Line, function.Position));
                    return null;
                }
                int x = (int)Evaluate(function.Parameters[0]);
                int y = (int)Evaluate(function.Parameters[0]);

                if (CheckErrors()) return null;

                Canva.Spawn(x, y);
                SpawnFound = true;
                return null;
            case "Color":
                string color = (string)Evaluate(function.Parameters[0]);

                if (CheckErrors()) return null;

                Canva.Color(color);
                return null;
            case "Size":
                int size = (int)Evaluate(function.Parameters[0]);

                if (CheckErrors()) return null;
                if (size % 2 == 0)
                {
                    size -= 1;
                }

                Canva.Size(size);
                return null;
            case "DrawLine":
                int LineX = (int)Evaluate(function.Parameters[0]);
                int LineY = (int)Evaluate(function.Parameters[1]);
                int distance = (int)Evaluate(function.Parameters[2]);

                if (CheckErrors()) return null;

                Canva.DrawLine(LineX, LineY, distance);

                return null;

            case "DrawCircle":
                int CircleX = (int)Evaluate(function.Parameters[0]);
                int CircleY = (int)Evaluate(function.Parameters[1]);
                int radius = (int)Evaluate(function.Parameters[2]);

                if (CheckErrors()) return null;

                Canva.DrawCircle(CircleX, CircleY, radius);
                return null;
            case "DrawRectangle":
                int RecX = (int)Evaluate(function.Parameters[0]);
                int RecY = (int)Evaluate(function.Parameters[1]);
                int RecDis = (int)Evaluate(function.Parameters[2]);
                int RecW = (int)Evaluate(function.Parameters[3]);
                int RecH = (int)Evaluate(function.Parameters[4]);

                if (CheckErrors()) return null;
                Canva.DrawRectangle(RecX, RecY, RecDis, RecW, RecH);
                return null;
            case "Fill":
                Canva.Fill();
                return null;
            case "GetActualX":
                return Canva.GetActualX();
            case "GetActualY":
                return Canva.GetActualY();
            case "GetCanvaSize":
                return Canva.GetCanvasSize();
            case "GetColorCount":
                string GetColor = (string)Evaluate(function.Parameters[0]);
                int GetX1 = (int)Evaluate(function.Parameters[1]);
                int GetY1 = (int)Evaluate(function.Parameters[2]);
                int GetX2 = (int)Evaluate(function.Parameters[3]);
                int GetY2 = (int)Evaluate(function.Parameters[4]);

                if (CheckErrors()) return null;

                return Canva.GetColorCount(GetColor, GetX1, GetY1, GetX2, GetY2);
            case "IsBrushColor":
                string BrushColor = (string)Evaluate(function.Parameters[0]);

                if (CheckErrors()) return null;

                return Canva.IsBrushColor(BrushColor);
            case "IsBrushSize":
                int BrushSize = (int)Evaluate(function.Parameters[0]);

                if (CheckErrors()) return null;

                return Canva.IsBrushSize(BrushSize);
            case "IsCanvaColor":
                string CanvaColor = (string)Evaluate(function.Parameters[0]);
                int Horizontal = (int)Evaluate(function.Parameters[1]);
                int Vertical = (int)Evaluate(function.Parameters[2]);

                if (CheckErrors()) return null;

                return Canva.IsCanvasColor(CanvaColor, Horizontal, Vertical);
        }
        return null;
    }
}