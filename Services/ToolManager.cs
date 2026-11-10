using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GraphicEditor.Tools;

namespace GraphicEditor.Services;

public class ToolManager
{
    private DrawingTool _activeTool;
    private Color _currentColor = Colors.Black;
    private double _currentThickness = 2.0;

    public DrawingTool ActiveTool
    {
        get => _activeTool;
        private set => _activeTool = value;
    }

    public Color CurrentColor
    {
        get => _currentColor;
        set
        {
            _currentColor = value;
            _activeTool.StrokeColor = _currentColor;
        }
    }

    public double CurrentThickness
    {
        get => _currentThickness;
        set
        {
            if (value > 0)
            {
                _currentThickness = value;
                _activeTool.StrokeThickness = _currentThickness;
            }
        }
    }

    public ToolManager(DrawingTool initialTool)
    {
        _activeTool = initialTool;
        ApplySettingsToTool();
    }

    public void SetTool(DrawingTool tool)
    {
        _activeTool = tool;
        ApplySettingsToTool();
    }

    public void HandleMouseDown(Canvas canvas, Point position)
    {
        _activeTool.OnMouseDown(canvas, position);
    }

    public void HandleMouseMove(Canvas canvas, Point position)
    {
        _activeTool.OnMouseMove(canvas, position);
    }

    public void HandleMouseUp(Canvas canvas, Point position)
    {
        _activeTool.OnMouseUp(canvas, position);
    }

    private void ApplySettingsToTool()
    {
        _activeTool.StrokeColor = _currentColor;
        _activeTool.StrokeThickness = _currentThickness;
    }
}