using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor.Tools;

public class LineTool : DrawingTool
{
    private Line? _currentLine;

    public override void OnMouseDown(Canvas canvas, Point position)
    {
        IsDrawing = true;

        _currentLine = new Line
        {
            X1 = position.X,
            Y1 = position.Y,
            X2 = position.X,
            Y2 = position.Y,
            Stroke = CreateBrush(),
            StrokeThickness = StrokeThickness,
            StrokeStartLineCap = PenLineCap.Round,
            StrokeEndLineCap = PenLineCap.Round
        };

        canvas.Children.Add(_currentLine);
    }

    public override void OnMouseMove(Canvas canvas, Point position)
    {
        if (!IsDrawing || _currentLine == null)
        {
            return;
        }

        _currentLine.X2 = position.X;
        _currentLine.Y2 = position.Y;
    }

    public override void OnMouseUp(Canvas canvas, Point position)
    {
        if (!IsDrawing || _currentLine == null)
        {
            return;
        }

        _currentLine.X2 = position.X;
        _currentLine.Y2 = position.Y;

        IsDrawing = false;
        _currentLine = null;
    }
}