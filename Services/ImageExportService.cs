using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphicEditor.Services;

public class ImageExportService
{
    public void SaveCanvasToFile(Canvas canvas, string filePath)
    {
        int width = (int)canvas.ActualWidth;
        int height = (int)canvas.ActualHeight;

        if (width <= 0 || height <= 0)
        {
            return;
        }

        var renderBitmap = new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Pbgra32);

        var visual = new DrawingVisual();
        using (DrawingContext context = visual.RenderOpen())
        {
            var visualBrush = new VisualBrush(canvas);
            context.DrawRectangle(Brushes.White, null, new Rect(0, 0, width, height));
            context.DrawRectangle(visualBrush, null, new Rect(0, 0, width, height));
        }

        renderBitmap.Render(visual);

        string extension = Path.GetExtension(filePath).ToLowerInvariant();
        BitmapEncoder encoder = extension switch
        {
            ".jpg" or ".jpeg" => new JpegBitmapEncoder(),
            _ => new PngBitmapEncoder()
        };

        encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

        using var fileStream = new FileStream(filePath, FileMode.Create);
        encoder.Save(fileStream);
    }
}