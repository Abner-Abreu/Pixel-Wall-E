using Parsing;
using System;
public static class ASTPrinter
{
    public static void PrintAST(AST? ast, int deep)
    {
        PrintDeep(deep);
        if (ast is null)
        {
            Console.WriteLine("Null");
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
        Console.WriteLine(node.Identifier);
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
                Console.WriteLine("Add");
                break;
            case Subs:
                Console.WriteLine("Subs");
                break;
            case Mult:
                Console.WriteLine("Mult");
                break;
            case Div:
                Console.WriteLine("Div");
                break;
            case Pow:
                Console.WriteLine("Pow");
                break;
            case Mod:
                Console.WriteLine("Mod");
                break;
            //Boolean
            case Equal:
                Console.WriteLine("Equal");
                break;
            case Less:
                Console.WriteLine("Less");
                break;
            case Less_Equal:
                Console.WriteLine("Less_Equal");
                break;
            case More:
                Console.WriteLine("More");
                break;
            case More_Equal:
                Console.WriteLine("More_Equal");
                break;
            case Or:
                Console.WriteLine("Or");
                break;
            case And:
                Console.WriteLine("And");
                break;
            case Assing:
                Console.WriteLine("Assign");
                break;
            case GoTo:
                Console.WriteLine("GoTo");
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
                Console.WriteLine("Num");
                break;
            case Color:
                Console.WriteLine("Color");
                break;
            case Variable:
                Console.WriteLine("Var");
                break;
            case Label:
                Console.WriteLine(((Label)node).Identifier);
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