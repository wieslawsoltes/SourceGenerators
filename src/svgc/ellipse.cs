using SkiaSharp;

namespace Sample
{
    public class ellipse
    {
        public static SKPicture Picture { get; }

        static ellipse()
        {
            Picture = Record();
        }

        private static SKPicture Record()
        {
            using var skPictureRecorder0 = new SKPictureRecorder();
            using var skCanvas0 = skPictureRecorder0.BeginRecording(new SKRect(0f, 0f, 200f, 200f));
            skCanvas0.Save();
            skCanvas0.SetMatrix(new SKMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f));
            skCanvas0.Save();
            skCanvas0.SetMatrix(new SKMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f));
            var skPath0 = new SKPath() { FillType = SKPathFillType.Winding };
            skPath0.MoveTo(170f, 100f);
            skPath0.CubicTo(170f, 66.86f, 134.18f, 40f, 90f, 40f);
            skPath0.CubicTo(45.82f, 40f, 10f, 66.86f, 10f, 100f);
            skPath0.CubicTo(10f, 133.14f, 45.82f, 160f, 90f, 160f);
            skPath0.CubicTo(134.18f, 160f, 170f, 133.14f, 170f, 100f);
            skPath0.Close();
            var skPaint0 = new SKPaint();
            skPaint0.Style = SKPaintStyle.Fill;
            skPaint0.IsAntialias = true;
            skPaint0.StrokeWidth = 0f;
            skPaint0.StrokeCap = SKStrokeCap.Butt;
            skPaint0.StrokeJoin = SKStrokeJoin.Miter;
            skPaint0.StrokeMiter = 4f;
            skPaint0.TextSize = 12f;
            skPaint0.TextAlign = SKTextAlign.Left;
            skPaint0.LcdRenderText = false;
            skPaint0.SubpixelText = false;
            skPaint0.TextEncoding = SKTextEncoding.Utf8;
            skPaint0.Color = new SKColor(0, 0, 0, 255);
            var skShader0 = SKShader.CreateColor(
                new SKColor(255, 0, 0, 255));
            skPaint0.Shader = skShader0;
            skPaint0.BlendMode = SKBlendMode.SrcOver;
            skPaint0.FilterQuality = SKFilterQuality.None;
            skCanvas0.DrawPath(skPath0, skPaint0);
            skCanvas0.Restore();
            skCanvas0.Save();
            skCanvas0.SetMatrix(new SKMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f));
            var skPath1 = new SKPath() { FillType = SKPathFillType.Winding };
            skPath1.AddOval(new SKRect(10f, 40f, 170f, 160f));
            var skPaint1 = new SKPaint();
            skPaint1.Style = SKPaintStyle.Fill;
            skPaint1.IsAntialias = true;
            skPaint1.StrokeWidth = 0f;
            skPaint1.StrokeCap = SKStrokeCap.Butt;
            skPaint1.StrokeJoin = SKStrokeJoin.Miter;
            skPaint1.StrokeMiter = 4f;
            skPaint1.TextSize = 12f;
            skPaint1.TextAlign = SKTextAlign.Left;
            skPaint1.LcdRenderText = false;
            skPaint1.SubpixelText = false;
            skPaint1.TextEncoding = SKTextEncoding.Utf8;
            skPaint1.Color = new SKColor(0, 0, 0, 255);
            var skShader1 = SKShader.CreateColor(
                new SKColor(0, 128, 0, 255));
            skPaint1.Shader = skShader1;
            skPaint1.BlendMode = SKBlendMode.SrcOver;
            skPaint1.FilterQuality = SKFilterQuality.None;
            skCanvas0.DrawPath(skPath1, skPaint1);
            skCanvas0.Restore();
            skCanvas0.Save();
            skCanvas0.SetMatrix(new SKMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f));
            var skPath2 = new SKPath() { FillType = SKPathFillType.Winding };
            skPath2.AddRect(new SKRect(1f, 1f, 199f, 199f));
            var skPaint2 = new SKPaint();
            skPaint2.Style = SKPaintStyle.Stroke;
            skPaint2.IsAntialias = true;
            skPaint2.StrokeWidth = 1f;
            skPaint2.StrokeCap = SKStrokeCap.Butt;
            skPaint2.StrokeJoin = SKStrokeJoin.Miter;
            skPaint2.StrokeMiter = 4f;
            skPaint2.TextSize = 12f;
            skPaint2.TextAlign = SKTextAlign.Left;
            skPaint2.LcdRenderText = false;
            skPaint2.SubpixelText = false;
            skPaint2.TextEncoding = SKTextEncoding.Utf8;
            skPaint2.Color = new SKColor(0, 0, 0, 255);
            var skShader2 = SKShader.CreateColor(
                new SKColor(0, 0, 0, 255));
            skPaint2.Shader = skShader2;
            skPaint2.BlendMode = SKBlendMode.SrcOver;
            skPaint2.FilterQuality = SKFilterQuality.None;
            skCanvas0.DrawPath(skPath2, skPaint2);
            skCanvas0.Restore();
            skCanvas0.Restore();
            var skPicture0 = skPictureRecorder0.EndRecording();
            return skPicture0;
        }

        public static void Draw(SKCanvas skCanvas)
        {
            skCanvas.DrawPicture(Picture);
        }
    }
}
