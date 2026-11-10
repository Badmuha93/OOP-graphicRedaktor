using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GraphicEditor.Services;
using GraphicEditor.Tools;
using Microsoft.Win32;

namespace GraphicEditor;

public partial class MainWindow : Window
{
    private readonly ToolManager _toolManager;
    private readonly ImageExportService _exportService;

    private readonly string[] _colorNames =
    {
        "Чёрный", "Красный", "Синий", "Зелёный", "Оранжевый", "Фиолетовый", "Коричневый"
    };

    private readonly Color[] _colorValues =
    {
        Colors.Black, Colors.Red, Colors.Blue, Colors.Green, Colors.Orange, Colors.Purple, Colors.Brown
    };

    private readonly double[] _thicknessValues = { 1, 2, 4, 6, 8, 12, 16 };

    public MainWindow()
    {
        _toolManager = new ToolManager(new PencilTool());
        _exportService = new ImageExportService();

        InitializeComponent();

        ColorBox.ItemsSource = _colorNames;
        ColorBox.SelectedIndex = 0;

        ThicknessBox.ItemsSource = _thicknessValues;
        ThicknessBox.SelectedIndex = 1;
    }

    private void ToolRadio_Checked(object sender, RoutedEventArgs e)
    {
        if (_toolManager == null)
        {
            return;
        }

        if (PencilRadio.IsChecked == true)
        {
            _toolManager.SetTool(new PencilTool());
        }
        else if (LineRadio.IsChecked == true)
        {
            _toolManager.SetTool(new LineTool());
        }
        else if (RectangleRadio.IsChecked == true)
        {
            _toolManager.SetTool(new RectangleTool());
        }
        else if (EllipseRadio.IsChecked == true)
        {
            _toolManager.SetTool(new EllipseTool());
        }
    }

    private void ColorBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int idx = ColorBox.SelectedIndex;
        if (idx >= 0 && idx < _colorValues.Length)
        {
            _toolManager.CurrentColor = _colorValues[idx];
        }
    }

    private void ThicknessBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ThicknessBox.SelectedItem is double thickness)
        {
            _toolManager.CurrentThickness = thickness;
        }
    }

    private void DrawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Point pos = e.GetPosition(DrawingCanvas);
        DrawingCanvas.CaptureMouse();
        _toolManager.HandleMouseDown(DrawingCanvas, pos);
    }

    private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            Point pos = ClampToCanvas(e.GetPosition(DrawingCanvas));
            _toolManager.HandleMouseMove(DrawingCanvas, pos);
        }
    }

    private void DrawingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        Point pos = ClampToCanvas(e.GetPosition(DrawingCanvas));
        _toolManager.HandleMouseUp(DrawingCanvas, pos);
        DrawingCanvas.ReleaseMouseCapture();
    }

    private void DrawingCanvas_MouseLeave(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed && !DrawingCanvas.IsMouseCaptured)
        {
            Point pos = ClampToCanvas(e.GetPosition(DrawingCanvas));
            _toolManager.HandleMouseUp(DrawingCanvas, pos);
        }
    }

    private Point ClampToCanvas(Point point)
    {
        double x = Math.Clamp(point.X, 0, DrawingCanvas.ActualWidth);
        double y = Math.Clamp(point.Y, 0, DrawingCanvas.ActualHeight);
        return new Point(x, y);
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        DrawingCanvas.Children.Clear();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new SaveFileDialog
        {
            Title = "Сохранить изображение",
            Filter = "PNG изображение (*.png)|*.png|JPEG изображение (*.jpg)|*.jpg",
            DefaultExt = ".png",
            FileName = "drawing.png"
        };

        if (dialog.ShowDialog() == true)
        {
            _exportService.SaveCanvasToFile(DrawingCanvas, dialog.FileName);
            MessageBox.Show("Изображение успешно сохранено.", "Готово");
        }
    }
}