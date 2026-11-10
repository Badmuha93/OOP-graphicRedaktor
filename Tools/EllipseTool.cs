using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace GraphicEditor.Tools;

public class EllipseTool : DrawingTool
{
    private Point _startPoint;
    private Ellipse? _currentEllipse;

    public override void OnMouseDown(Canvas canvas, Point position)
    {
        IsDrawing = true;
        _startPoint = position;

        _currentEllipse = new Ellipse
        {
            Stroke = CreateBrush(),
            StrokeThickness = StrokeThickness,
            Width = 0,
            Height = 0
        };

        Canvas.SetLeft(_currentEllipse, position.X);
        Canvas.SetTop(_currentEllipse, position.Y);
        canvas.Children.Add(_currentEllipse);
    }

    public override void OnMouseMove(Canvas canvas, Point position)
    {
        if (!IsDrawing || _currentEllipse == null)
        {
            return;
        }

        UpdateBounds(position);
    }

    public override void OnMouseUp(Canvas canvas, Point position)
    {
        if (!IsDrawing || _currentEllipse == null)
        {
            return;
        }

        UpdateBounds(position);
        IsDrawing = false;
        _currentEllipse = null;
    }

    private void UpdateBounds(Point currentPoint)
    {
        if (_currentEllipse == null)
        {
            return;
        }

        double x = Math.Min(_startPoint.X, currentPoint.X);
        double y = Math.Min(_startPoint.Y, currentPoint.Y);
        double width = Math.Abs(currentPoint.X - _startPoint.X);
        double height = Math.Abs(currentPoint.Y - _startPoint.Y);

        Canvas.SetLeft(_currentEllipse, x);
        Canvas.SetTop(_currentEllipse, y);
        _currentEllipse.Width = width;
        _currentEllipse.Height = height;
    }
}