using Parsing;
using System;
using Malanga = Godot.GD;
public static class ASTPrinter
{
    public static void PrintAST(AST? ast, int deep)
    {
        PrintDeep(deep);
        if (ast is null)
        {
            Malanga.Print("Null");
        }
        if (ast is Binary)
        {
            PrintBinary((Binary)ast, deep + 1);
        }
        if (ast is Atom)
        {
            PrintAtom((Atom)ast);
        }
        if (ast is Function)
        {
            PrintFunction((Function)ast, deep + 1);
        }
    }

    private static void PrintFunction(Function node, int deep)
    {
        foreach (AST? ast in node.Parameters)
        {
            PrintAST(ast, deep + 1);
        }
    }

    private static void PrintBinary(Binary node, int deep)
    {
        switch (node)
        {
            //Arithmethic
            case Add:
                Malanga.Print("Add");
                break;
            case Subs:
                Malanga.Print("Subs");
                break;
            case Mult:
                Malanga.Print("Mult");
                break;
            case Div:
                Malanga.Print("Div");
                break;
            case Pow:
                Malanga.Print("Pow");
                break;
            case Mod:
                Malanga.Print("Mod");
                break;
            //Boolean
            case Equal:
                Malanga.Print("Equal");
                break;
            case Less:
                Malanga.Print("Less");
                break;
            case Less_Equal:
                Malanga.Print("Less_Equal");
                break;
            case More:
                Malanga.Print("More");
                break;
            case More_Equal:
                Malanga.Print("More_Equal");
                break;
            case Or:
                Malanga.Print("Or");
                break;
            case And:
                Malanga.Print("And");
                break;
            case Assing:
                Malanga.Print("Assign");
                break;
            case GoTo:
                Malanga.Print("GoTo");
                break;
        }
        PrintAST(node.Left, deep + 1);
        PrintAST(node.Right, deep + 1);
    }
    private static void PrintAtom(Atom node)
    {
        switch (node)
        {
            case Number:
                Malanga.Print("Num");
                break;
            case Color:
                Malanga.Print("Color");
                break;
            case Variable:
                Malanga.Print("Var");
                break;
            case Label:
                Malanga.Print(((Label)node).Identifier);
                break;
        }
    }
    private static void PrintDeep(int deep)
    {
        for (int i = 0; i < deep; i++)
        {
            Console.Write("  ");
        }
    }
}