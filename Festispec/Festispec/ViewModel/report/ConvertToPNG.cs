using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Festispec.ViewModel.report
{
    public static class ConvertToPNG
    {
        //public static void SnapShotPNG(this UIElement source, int zoom)
        //{
        //    try
        //    {
        //        double actualHeight = source.RenderSize.Height;
        //        double actualWidth = source.RenderSize.Width;

        //        double renderHeight = actualHeight * zoom;
        //        double renderWidth = actualWidth * zoom;

        //        RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
        //        VisualBrush sourceBrush = new VisualBrush(source);

        //        DrawingVisual drawingVisual = new DrawingVisual();
        //        DrawingContext drawingContext = drawingVisual.RenderOpen();

        //        using (drawingContext)
        //        {
        //            drawingContext.PushTransform(new ScaleTransform(zoom, zoom));
        //            drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(actualWidth, actualHeight)));
        //        }
        //        renderTarget.Render(drawingVisual);
        //        //FlowDocument flowDocument = new FlowDocument();
        //        //flowDocument.Blocks.Add(drawingVisual);
        //        PrintDialog printDialog = new PrintDialog();
        //        printDialog.PrintVisual(drawingVisual, "Rapport");
        //        //PngBitmapEncoder encoder = new PngBitmapEncoder();
        //        //encoder.Frames.Add(BitmapFrame.Create(renderTarget));
        //        //using (FileStream stream = new FileStream("C:\\temp", FileMode.Create, FileAccess.Write))
        //        //{
        //        //    encoder.Save(stream);
        //        //}
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //}

        public static void SnapShotPng(FrameworkElement source, double zoom)
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
                PrintDialog pd = new PrintDialog();
                pd.PrintVisual(tmpImg, "Rapport");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
