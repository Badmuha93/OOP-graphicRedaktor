using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphicEditor.Tools;

public abstract class DrawingTool
{
    private Color _strokeColor = Colors.Black;
    private double _strokeThickness = 2.0;
    private bool _isDrawing;

    public Color StrokeColor
    {
        get => _strokeColor;
        set => _strokeColor = value;
    }

    public double StrokeThickness
    {
        get => _strokeThickness;
        set
        {
            if (value > 0)
            {
                _strokeThickness = value;
            }
        }
    }

    public bool IsDrawing
    {
        get => _isDrawing;
        protected set => _isDrawing = value;
    }

    protected Brush CreateBrush()
    {
        return new SolidColorBrush(StrokeColor);
    }

    public abstract void OnMouseDown(Canvas canvas, Point position);
    public abstract void OnMouseMove(Canvas canvas, Point position);
    public abstract void OnMouseUp(Canvas canvas, Point position);
}