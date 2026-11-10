using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor.Tools;

public class PencilTool : DrawingTool
{
    private Polyline? _currentLine;

    public override void OnMouseDown(Canvas canvas, Point position)
    {
        IsDrawing = true;

        _currentLine = new Polyline
        {
            Stroke = CreateBrush(),
            StrokeThickness = StrokeThickness,
            StrokeLineJoin = PenLineJoin.Round,
            StrokeStartLineCap = PenLineCap.Round,
            StrokeEndLineCap = PenLineCap.Round
        };

        _currentLine.Points.Add(position);
        canvas.Children.Add(_currentLine);
    }

    public override void OnMouseMove(Canvas canvas, Point position)
    {
        if (!IsDrawing || _currentLine == null)
        {
            return;
        }

        _currentLine.Points.Add(position);
    }

    public override void OnMouseUp(Canvas canvas, Point position)
    {
        if (!IsDrawing)
        {
            return;
        }

        IsDrawing = false;
        _currentLine = null;
    }
}