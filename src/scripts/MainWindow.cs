using Godot;
using System;
using Compilation;
using System.IO;
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
        var highlighter = new CodeHighlighter();


        highlighter.NumberColor = new Color(0.710f, 0.808f, 0.659f);
        highlighter.SymbolColor = new Color(0.831f, 0.455f, 0.553f);
        highlighter.FunctionColor = new Color(0.529f, 0.737f, 0.992f);

        codeEdit.SyntaxHighlighter = highlighter;
    }

    private void ResizeCanvas()
    {
        int size = (int)canvasSize.Value;
        canvas.Initialize(size);
    }

    public void _on_run_pressed()
    {
        try
        {
            compiler = new Compiler(codeEdit.Text + "\n ", canvas);
        }
        catch (Exception ex)
        {
            console.Text = $"Error al crear Compiler: {ex.Message}";
            return;
        }
        console.Text = "";
        if (compiler.errors.Count > 0)
        {

            foreach (var error in compiler.errors)
            {
                console.AppendText($"[color=red]{error.GetError()}[/color]\n");
            }
        }
        else
        {
            console.AppendText($"[color=green]Compilation Successfull[/color]\n");
        }
    }

    public void _on_load_pressed()
    {
        fileLoad.PopupCentered(new Vector2I(600, 400));
    }

    public void _on_save_pressed()
    {
        if (string.IsNullOrEmpty(currentFilePath))
        {
            fileSave.PopupCentered(new Vector2I(600, 400));
        }
        else
        {
            SaveFile(currentFilePath);
        }
    }

    public void _on_load_file_selected(string path)
    {
        string sistemPath = ProjectSettings.GlobalizePath(path);
        try
        {
            codeEdit.Text = File.ReadAllText(sistemPath);
            currentFilePath = sistemPath;
            console.Text = $"File load success\n";
        }
        catch (Exception ex)
        {
            console.Text = $"Error when loading file: {ex}\n";
        }
    }

    public void _on_save_file_selected(string path)
    {
        SaveFile(path);
    }

    private void SaveFile(string path)
    {
        try
        {
            string sistemPath = ProjectSettings.GlobalizePath(path);
            
            var dir = Path.GetDirectoryName(sistemPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(sistemPath, codeEdit.Text);
            currentFilePath = sistemPath;
            console.Text = $"File saved successfully\n";
        }
        catch (Exception ex)
        {
            console.Text = $"Error when saving file: {ex.Message}\n";
        }
    }
    public void _on_resize_pressed()
    {
        ResizeCanvas();
    }


}
