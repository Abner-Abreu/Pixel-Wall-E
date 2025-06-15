using Godot;
using System;
using Interpret;
using Lexical;
using Compilation;
public partial class MainWindow : Node2D
{
    private CodeEdit codeEdit;
    private Canvas canvas;
    private RichTextLabel console;
    private SpinBox canvasSize;
    private FileDialog fileLoad;
    private FileDialog fileSave;
    private Compiler compiler;

    private string currentFilePath = "";

    public override void _Ready()
    {
        codeEdit = GetNode<CodeEdit>("UI/VSplitContainer/HSplitContainer/Editor/CodeEdit");
        canvas = GetNode<Canvas>("UI/VSplitContainer/HSplitContainer/Canvas/SubViewportContainer/SubViewport/Node2D");
        console = GetNode<RichTextLabel>("UI/VSplitContainer/HSplitContainer/Canvas/Console");
        canvasSize = GetNode<SpinBox>("UI/VSplitContainer/ToolBar/CanvasSize");
        fileLoad = GetNode<FileDialog>("FileLoad");
        fileSave = GetNode<FileDialog>("FileSave");

        canvasSize.Value = 20;
        ResizeCanvas();

        SetupSytaxHighLighting();
    }

    private void SetupSytaxHighLighting()
    {
        var highlighther = new CodeHighlighter();

        //Functions
        string[] functions =
        {
            "Spawn", "Color", "Size", "DrawLine", "DrawRectangle",
            "DrawCircle", "Fill", "GetActualX", "GetActualY",
            "GetCanvasSize", "GetColorCount", "IsBrushColor",
            "IsBrushSize", "IsCanvasColor"
        };
        foreach (var word in functions)
        {
            highlighther.AddKeywordColor(word, new Color(0.863f, 0.808f, 0.667f));
        }

        //Numbers
        highlighther.NumberColor = new Color(0.710f, 0.808f, 0.659f);

        codeEdit.SyntaxHighlighter = highlighther;
    }

    private void ResizeCanvas()
    {
        int size = (int)canvasSize.Value;
        canvas.Initialize(size);
    }

    public void on_run_button_pressed()
    {
        compiler = new Compiler(codeEdit.Text, ref canvas);
    }


}
