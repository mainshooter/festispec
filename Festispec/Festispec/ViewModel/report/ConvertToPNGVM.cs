using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Festispec.ViewModel.report
{
    public static class ConvertToPNGVM
    {

        public static Image SnapShotPng(FrameworkElement source, double zoom)
        {
            try
            {
                double actualHeight = source.RenderSize.Height;
                double actualWidth = source.RenderSize.Width;

                double renderHeight = actualHeight * zoom;
                double renderWidth = actualWidth * zoom;

                RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
                VisualBrush sourceBrush = new VisualBrush(source);


                DrawingVisual drawingVisual = new DrawingVisual();

                DrawingContext drawingContext = drawingVisual.RenderOpen();

                using (drawingContext)
                {

                    drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(renderWidth, renderHeight)));
                }
                renderTarget.Render(drawingVisual);

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderTarget));
                BitmapSource bitmap;
                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    stream.Position = 0;
                    bitmap = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                }

                var tmpImg = new Image { Source = bitmap };
                return tmpImg;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}
