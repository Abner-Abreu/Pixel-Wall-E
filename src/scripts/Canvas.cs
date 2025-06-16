using Godot;
using System;
using Errors;
using System.Collections.Generic;
using Parsing;

public partial class Canvas : Node2D, ICanvas
{
    public int CanvasSize { private set; get; }
    private Godot.Color[,] Grid;
    private Vector2 BrushPosition;
    private Godot.Color CurrentColor = Colors.Red;
    private Errors.Error error { set; get; }
    private (int line, int position) ExecutePosition { set; get; }
    private int BrushSize;

    public void Initialize(int newSize)
    {
        CanvasSize = newSize;
        Grid = new Godot.Color[CanvasSize, CanvasSize];

        //Initilize grid color as White
        for (int y = 0; y < CanvasSize; y++)
        {
            for (int x = 0; x < CanvasSize; x++)
            {
                Grid[x, y] = Colors.White;
            }
        }
        //Set brush position
        BrushPosition = new Vector2(CanvasSize / 2, CanvasSize / 2);
        CurrentColor = Colors.Transparent;
        BrushSize = 1;
        QueueRedraw();
    }

    public override void _Draw()
    {
        //Calculate cell size
        var viewPortSize = GetViewportRect().Size;
        float cellSize = Math.Min(viewPortSize.X / CanvasSize, viewPortSize.Y / CanvasSize);

        //Draw grid
        for (int y = 0; y < CanvasSize; y++)
        {
            for (int x = 0; x < CanvasSize; x++)
            {
                var rect = new Rect2(x * cellSize, y * cellSize, cellSize, cellSize);
                DrawRect(rect, Grid[x, y], true);
                DrawRect(rect, new Godot.Color(0.8f, 0.8f, 0.8f, 0.3f), false);
            }
        }

        //Draw brush
        float brusRadius = cellSize / 3;
        var brushNewPosition = new Vector2(BrushPosition.X * cellSize + cellSize / 2, BrushPosition.Y * cellSize + cellSize / 2);
        DrawCircle(brushNewPosition, brusRadius, Colors.DarkSlateGray);
    }

    public void SetExecutePosition(AST node)
    {
        ExecutePosition = new(node.Line, node.Position);
    }

    #region ICanvas Functions
    public void Spawn(int x, int y)
    {
        if (IsInsideCanvas(x, y) == false)
        {
            error = new Errors.Error(ErrorType.Runtime, "Spawn point out of the canvas", ExecutePosition.line, ExecutePosition.position);
        }
        else
        {
            BrushPosition = new Vector2(x, y);
            QueueRedraw();
        }
    }
    public void Color(string color)
    {
        CurrentColor = ToColor(color);
    }
    public void DrawCircle(int dirX, int dirY, int radius)
    {
        //Validate directions
        if (dirX > 1) dirX = 1;
        if (dirX < -1) dirX = -1;
        if (dirY > 1) dirY = 1;
        if (dirY < -1) dirY = -1;
        //Check radius direction
        if (radius < 0)
        {
            radius *= -1;
            dirX *= -1;
            dirY *= -1;
        }
        
        //Set initial position
        int newX = (int)BrushPosition.X;
        int newY = (int)BrushPosition.Y;

        //Move to Circle Center
        for (int i = 0; i < radius; i++)
        {
            newX += dirX;
            newY += dirY;
        }

        //Draw circle
        int x = 0;
        int y = radius;
        int d = 3 - 2 * radius;

        while (x <= y)
        {
            // Dibujar los ocho octantes
            DrawPixel(newX + x, newY + y, CurrentColor);
            DrawPixel(newX - x, newY + y, CurrentColor);
            DrawPixel(newX + x, newY - y, CurrentColor);
            DrawPixel(newX - x, newY - y, CurrentColor);
            DrawPixel(newX + y, newY + x, CurrentColor);
            DrawPixel(newX - y, newY + x, CurrentColor);
            DrawPixel(newX + y, newY - x, CurrentColor);
            DrawPixel(newX - y, newY - x, CurrentColor);

            if (d < 0)
            {
                d += 4 * x + 6;
            }
            else
            {
                d += 4 * (x - y) + 10;
                y--;
            }
            x++;
        }
        if (IsInsideCanvas(newX, newY))
        {
            BrushPosition = new Vector2(newX, newY);
            return;
        }
        else
        {
            error = new Errors.Error(ErrorType.Runtime, "Wall-E position out of the canvas", ExecutePosition.line, ExecutePosition.position);
        }
    }

    public void DrawLine(int dirX, int dirY, int distance)
    {
        //Validate directions
        if (dirX > 1) dirX = 1;
        if (dirX < -1) dirX = -1;
        if (dirY > 1) dirY = 1;
        if (dirY < -1) dirY = -1;
        //Check distance direction
        if (distance < 0)
        {
            distance *= -1;
            dirX *= -1;
            dirY *= -1;
        }

        //Set initial position
        int x = (int)BrushPosition.X;
        int y = (int)BrushPosition.Y;

        //Draw line
        for (int i = 0; i < distance; i++)
        {
            x += dirX;
            y += dirY;
            if (CurrentColor != Colors.Transparent)
            {
                DrawPixel(x, y, CurrentColor);
            }
        }
        if (IsInsideCanvas(x, y))
        {
            BrushPosition = new Vector2(x, y);
            return;
        }
        else
        {
            error = new Errors.Error(ErrorType.Runtime, "Wall-E position out of the canvas", ExecutePosition.line, ExecutePosition.position);
        }
    }

    public void DrawRectangle(int dirX, int dirY, int distance, int width, int height)
    {
        //Validate directions
        if (dirX > 1) dirX = 1;
        if (dirX < -1) dirX = -1;
        if (dirY > 1) dirY = 1;
        if (dirY < -1) dirY = -1;
        //Check distance direction
        if (distance < 0)
        {
            distance *= -1;
            dirX *= -1;
            dirY *= -1;
        }
        
        //Set initial position
        int x = (int)BrushPosition.X;
        int y = (int)BrushPosition.Y;
        //Move to Rectangle Center
        for (int i = 0; i < distance; i++)
        {
            x += dirX;
            y += dirY;
        }
        //Draw Rectangle
        for (int j = x - width; j <= x + width; j++) // Horizontal lines
        {
            DrawPixel(j, y + height / 2, CurrentColor);
            DrawPixel(j, y - height / 2, CurrentColor);
        }
        for (int i = y - height / 2; i <= y + height / 2; i++) // Vertical lines
        {
            DrawPixel(x + width, i, CurrentColor);
            DrawPixel(x - width, i, CurrentColor);
        }

        if (IsInsideCanvas(x, y))
        {
            BrushPosition = new Vector2(x, y);
        }
        else
        {
            error = new Errors.Error(ErrorType.Runtime, "Wall-E position out of the canvas", ExecutePosition.line, ExecutePosition.position);
        }
    }

    public void Fill()
    {
        int aux = BrushSize;
        BrushSize = 1;
        Fill(Grid[(int)BrushPosition.X, (int)BrushPosition.Y], (int)BrushPosition.X, (int)BrushPosition.Y);
        BrushSize = aux;
    }

    private void Fill(Godot.Color initialColor, int x, int y)
    {
        if (!IsInsideCanvas(x, y) || Grid[x, y] != initialColor)
            return;

        DrawPixel(x, y, CurrentColor);
        Grid[x, y] = CurrentColor; // Asegúrate de evitar rellenos infinitos

        foreach (var dir in Directions)
        {
            int newX = x + dir.x;
            int newY = y + dir.y;

            if (IsInsideCanvas(newX, newY) && Grid[newX, newY] == initialColor)
            {
                Fill(initialColor, newX, newY);
            }
        }
    }


    (int x, int y)[] Directions = { (1, 0), (-1, 0), (0, 1), (0, -1), (1, 1), (1, -1), (-1, 1), (-1, -1) };
    public int GetActualX()
    {
        return (int)BrushPosition.X;
    }

    public int GetActualY()
    {
        return (int)BrushPosition.Y;
    }

    public int GetCanvasSize()
    {
        return CanvasSize;
    }

    public int GetColorCount(string color, int x1, int y1, int x2, int y2)
    {
        int colorCount = 0;

        if (IsInsideCanvas(x1, y1) && IsInsideCanvas(x2, y2))
        {
            for (int i = y1; i <= y2; i++)
            {
                for (int j = x1; j <= x2; j++)
                {
                    if (ToColor(color) == Grid[j, i])
                    {
                        colorCount++;
                    }
                }
            }
        }
        return colorCount;
    }

    public int IsBrushColor(string color)
    {
        return ToColor(color) == CurrentColor ? 1 : 0;
    }

    public int IsBrushSize(int size)
    {
        return BrushSize == size ? 1 : 0;
    }

    public int IsCanvasColor(string color, int vertical, int hotizontal)
    {
        int x = (int)BrushPosition.X + hotizontal;
        int y = (int)BrushPosition.Y + vertical;
        if (IsInsideCanvas(x, y))
        {
            return Grid[x, y] == ToColor(color) ? 1 : 0;
        }
        else
        {
            return 0;
        }
    }

    public void Size(int size)
    {
        if (size % 2 == 0)
        {
            BrushSize = size - 1;
        }
        BrushSize = size;
    }

    public Errors.Error GetErrors()
    {
        return error;
    }
    #endregion

    #region Auxiliar Functions
    private void DrawPixel(int x, int y, Godot.Color color)
    {
        for (int i = y - (BrushSize / 2); i <= y + (BrushSize / 2); i++)
        {
            for (int j = x - (BrushSize / 2); j <= x + (BrushSize / 2); j++)
            {
                if (IsInsideCanvas(j, i))
                {
                    Grid[j, i] = color;
                }
            }
        }

        QueueRedraw();
    }
    private Godot.Color ToColor(string color)
    {
        //Godot.Colors cant be used in Dictionaryes
        switch (color)
        {
            case "Transparent":
                return Colors.Transparent;
            case "Red":
                return Colors.Red;
            case "Blue":
                return Colors.Blue;
            case "Green":
                return Colors.Green;
            case "Yellow":
                return Colors.Yellow;
            case "Orange":
                return Colors.Orange;
            case "Purple":
                return Colors.Purple;
            case "Black":
                return Colors.Black;
            case "Whithe":
                return Colors.White;
            default:
                return Colors.Transparent;
        }
    }

    private bool IsInsideCanvas(int x, int y)
    {
        return x < CanvasSize && x >= 0 && y < CanvasSize && y >= 0;
    }

    private bool IsDirection(int x, int y)
    {
        foreach (var dir in Directions)
        {
            if (dir.x == x && dir.y == y)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}