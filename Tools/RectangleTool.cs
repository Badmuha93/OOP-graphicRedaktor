using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace GraphicEditor.Tools;

public class RectangleTool : DrawingTool
{
    private Point _startPoint;
    private Rectangle? _currentRect;

    public override void OnMouseDown(Canvas canvas, Point position)
    {
        IsDrawing = true;
        _startPoint = position;

        _currentRect = new Rectangle
        {
            Stroke = CreateBrush(),
            StrokeThickness = StrokeThickness,
            Width = 0,
            Height = 0
        };

        Canvas.SetLeft(_currentRect, position.X);
        Canvas.SetTop(_currentRect, position.Y);
        canvas.Children.Add(_currentRect);
    }

    public override void OnMouseMove(Canvas canvas, Point position)
    {
        if (!IsDrawing || _currentRect == null)
        {
            return;
        }

        UpdateBounds(position);
    }

    public override void OnMouseUp(Canvas canvas, Point position)
    {
        if (!IsDrawing || _currentRect == null)
        {
            return;
        }

        UpdateBounds(position);
        IsDrawing = false;
        _currentRect = null;
    }

    private void UpdateBounds(Point currentPoint)
    {
        if (_currentRect == null)
        {
            return;
        }

        double x = Math.Min(_startPoint.X, currentPoint.X);
        double y = Math.Min(_startPoint.Y, currentPoint.Y);
        double width = Math.Abs(currentPoint.X - _startPoint.X);
        double height = Math.Abs(currentPoint.Y - _startPoint.Y);

        Canvas.SetLeft(_currentRect, x);
        Canvas.SetTop(_currentRect, y);
        _currentRect.Width = width;
        _currentRect.Height = height;
    }
}