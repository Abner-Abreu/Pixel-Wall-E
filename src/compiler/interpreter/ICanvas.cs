using Errors;
using Parsing;

public interface ICanvas
{
    abstract Error GetErrors();
    abstract void SetExecutePosition(AST node);
    abstract void Spawn(int x, int y);
    abstract void Color(string color);
    abstract void Size(int size);
    abstract void DrawLine(int dirX, int dirY, int distance);
    abstract void DrawCircle(int dirX, int dirY, int radius);
    abstract void DrawRectangle(int dirX, int dirY, int distance, int width, int height);
    abstract void Fill();
    abstract int GetActualX();
    abstract int GetActualY();
    abstract int GetCanvasSize();
    abstract int GetColorCount(string color, int x1, int y1, int x2, int y2);
    abstract int IsBrushColor(string color);
    abstract int IsBrushSize(int size);
    abstract int IsCanvasColor(string color, int vertical, int hotizontal);
}