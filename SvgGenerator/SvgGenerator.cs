using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using SkiaSharp;
using Svg;
using Svg.Skia;
using SP=Svg.Picture;

namespace Svg
{
    internal static class ModelExtensions
    {
        private static CultureInfo _ci = CultureInfo.InvariantCulture;

        public static string ToSKPoint(this SP.Point point)
        {
            return $"new SKPoint({point.X.ToString(_ci)}f, {point.Y.ToString(_ci)}f)";
        }

        // TODO: ToSKPoint3

        public static string ToSKPoints(this IList<SP.Point> points)
        {
            var result = "new SKPoint[{points.Count}] = {{ ";

            for (int i = 0; i < points.Count; i++)
            {
                result += points[i].ToSKPoint();
            }

            result += " }}";

            return result;
        }

        // TODO: ToSKPointI
        // TODO: ToSKSize
        // TODO: ToSKSizeI

        public static string ToSKRect(this SP.Rect rect)
        {
            return $"new SKRect({rect.Left.ToString(_ci)}f, {rect.Top.ToString(_ci)}f, {rect.Right.ToString(_ci)}f, {rect.Bottom.ToString(_ci)}f)";
        }

        public static string ToSKMatrix(this SP.Matrix matrix)
        {
            return $"new SKMatrix({matrix.ScaleX.ToString(_ci)}f, {matrix.SkewX.ToString(_ci)}f, {matrix.TransX.ToString(_ci)}f, {matrix.SkewY.ToString(_ci)}f, {matrix.ScaleY.ToString(_ci)}f, {matrix.TransY.ToString(_ci)}f, {matrix.Persp0.ToString(_ci)}f, {matrix.Persp1.ToString(_ci)}f, {matrix.Persp2.ToString(_ci)}f)";
        }

        // TODO: ToSKImage

        public static string ToSKPaintStyle(this SP.PaintStyle paintStyle)
        {
            switch (paintStyle)
            {
                default:
                case SP.PaintStyle.Fill:
                    return "SKPaintStyle.Fill";
                case SP.PaintStyle.Stroke:
                    return "SKPaintStyle.Stroke";
                case SP.PaintStyle.StrokeAndFill:
                    return "SKPaintStyle.StrokeAndFill";
            }
        }

        public static string ToSKStrokeCap(this SP.StrokeCap strokeCap)
        {
            switch (strokeCap)
            {
                default:
                case SP.StrokeCap.Butt:
                    return "SKStrokeCap.Butt";
                case SP.StrokeCap.Round:
                    return "SKStrokeCap.Round";
                case SP.StrokeCap.Square:
                    return "SKStrokeCap.Square";
            }
        }

        public static string ToSKStrokeJoin(this SP.StrokeJoin strokeJoin)
        {
            switch (strokeJoin)
            {
                default:
                case SP.StrokeJoin.Miter:
                    return "SKStrokeJoin.Miter";
                case SP.StrokeJoin.Round:
                    return "SKStrokeJoin.Round";
                case SP.StrokeJoin.Bevel:
                    return "SKStrokeJoin.Bevel";
            }
        }

        public static string ToSKTextAlign(this SP.TextAlign textAlign)
        {
            switch (textAlign)
            {
                default:
                case SP.TextAlign.Left:
                    return "SKTextAlign.Left";
                case SP.TextAlign.Center:
                    return "SKTextAlign.Center";
                case SP.TextAlign.Right:
                    return "SKTextAlign.Right";
            }
        }

        // TODO: ToSKTextEncoding
        // TODO: ToSKFontStyleWeight
        // TODO: ToSKFontStyleWidth
        // TODO: ToSKFontStyleSlant
        // TODO: ToSKTypeface
        // TODO: ToSKColor
        // TODO: ToSKColors
        // TODO: ToSKColorF
        // TODO: ToSKColors
        // TODO: ToSKShaderTileMode
        // TODO: ToSKShader
        // TODO: ToSKColorFilter
        // TODO: ToCropRect
        // TODO: ToSKColorChannel
        // TODO: ToSKImageFilter
        // TODO: ToSKImageFilters
        // TODO: ToSKPathEffect
        // TODO: ToSKBlendMode
        // TODO: ToSKFilterQuality

        public static void ToSKPaint(this SP.Paint paint, int count, StringBuilder sb, string indent)
        {
            sb.AppendLine($"{indent}var skPaint{count} = new SKPaint();");

            sb.AppendLine($"{indent}skPaint{count}.Style = {paint.Style.ToSKPaintStyle()};");
            sb.AppendLine($"{indent}skPaint{count}.IsAntialias = {paint.IsAntialias.ToString(_ci).ToLower()};");
            sb.AppendLine($"{indent}skPaint{count}.StrokeWidth = {paint.StrokeWidth.ToString(_ci)}f;");
            sb.AppendLine($"{indent}skPaint{count}.StrokeCap = {paint.StrokeCap.ToSKStrokeCap()};");
            sb.AppendLine($"{indent}skPaint{count}.StrokeJoin = {paint.StrokeJoin.ToSKStrokeJoin()};");
            sb.AppendLine($"{indent}skPaint{count}.StrokeMiter = {paint.StrokeMiter.ToString(_ci)}f;");
            sb.AppendLine($"{indent}skPaint{count}.TextSize = {paint.TextSize.ToString(_ci)}f;");
            sb.AppendLine($"{indent}skPaint{count}.TextAlign = {paint.TextAlign.ToSKTextAlign()};");

            // TODO:
/*
            var typeface = paint.Typeface?.ToSKTypeface();
            var textEncoding = paint.TextEncoding.ToSKTextEncoding();
            var color = paint.Color == null ? SKColor.Empty : ToSKColor(paint.Color.Value);
            var shader = paint.Shader?.ToSKShader();
            var colorFilter = paint.ColorFilter?.ToSKColorFilter();
            var imageFilter = paint.ImageFilter?.ToSKImageFilter();
            var pathEffect = paint.PathEffect?.ToSKPathEffect();
            var blendMode = paint.BlendMode.ToSKBlendMode();
            var filterQuality = paint.FilterQuality.ToSKFilterQuality();

            return new SKPaint()
            {
                Typeface = typeface,
                LcdRenderText = paint.LcdRenderText,
                SubpixelText = paint.SubpixelText,
                TextEncoding = textEncoding,
                Color = color,
                Shader = shader,
                ColorFilter = colorFilter,
                ImageFilter = imageFilter,
                PathEffect = pathEffect,
                BlendMode = blendMode,
                FilterQuality = filterQuality
            };
*/
        }

        // TODO: ToSKClipOperation

        public static string ToSKPathFillType(this SP.PathFillType pathFillType)
        {
            switch (pathFillType)
            {
                default:
                case SP.PathFillType.Winding:
                    return "SKPathFillType.Winding";
                case SP.PathFillType.EvenOdd:
                    return "SKPathFillType.EvenOdd";
            }
        }

        public static string ToSKPathArcSize(this SP.PathArcSize pathArcSize)
        {
            switch (pathArcSize)
            {
                default:
                case SP.PathArcSize.Small:
                    return "SKPathArcSize.Small";
                case SP.PathArcSize.Large:
                    return "SKPathArcSize.Large";
            }
        }

        public static string ToSKPathDirection(this SP.PathDirection pathDirection)
        {
            switch (pathDirection)
            {
                default:
                case SP.PathDirection.Clockwise:
                    return "SKPathDirection.Clockwise";
                case SP.PathDirection.CounterClockwise:
                    return "SKPathDirection.CounterClockwise";
            }
        }

        // TODO: ToSKPathOp

        public static void ToSKPath(this SP.Path path, int count, StringBuilder sb, string indent)
        {
            sb.AppendLine($"{indent}var skPath{count} = new SKPath() {{ FillType = {path.FillType.ToSKPathFillType()} }};");

            if (path.Commands == null)
            {
                return;
            }

            foreach (var pathCommand in path.Commands)
            {
                switch (pathCommand)
                {
                    case SP.MoveToPathCommand moveToPathCommand:
                        {
                            var x = moveToPathCommand.X;
                            var y = moveToPathCommand.Y;
                            sb.AppendLine($"{indent}skPath{count}.MoveTo({x.ToString(_ci)}f, {y.ToString(_ci)}f);");
                            
                        }
                        break;
                    case SP.LineToPathCommand lineToPathCommand:
                        {
                            var x = lineToPathCommand.X;
                            var y = lineToPathCommand.Y;
                            sb.AppendLine($"{indent}skPath{count}.LineTo({x.ToString(_ci)}f, {y.ToString(_ci)}f);");
                        }
                        break;
                    case SP.ArcToPathCommand arcToPathCommand:
                        {
                            var rx = arcToPathCommand.Rx;
                            var ry = arcToPathCommand.Ry;
                            var xAxisRotate = arcToPathCommand.XAxisRotate;
                            var largeArc = arcToPathCommand.LargeArc.ToSKPathArcSize();
                            var sweep = arcToPathCommand.Sweep.ToSKPathDirection();
                            var x = arcToPathCommand.X;
                            var y = arcToPathCommand.Y;
                            sb.AppendLine($"{indent}skPath{count}.ArcTo({rx.ToString(_ci)}f, {ry.ToString(_ci)}f, {xAxisRotate.ToString(_ci)}f, {largeArc}, {sweep}, {x.ToString(_ci)}f, {y.ToString(_ci)}f);");
                        }
                        break;
                    case SP.QuadToPathCommand quadToPathCommand:
                        {
                            var x0 = quadToPathCommand.X0;
                            var y0 = quadToPathCommand.Y0;
                            var x1 = quadToPathCommand.X1;
                            var y1 = quadToPathCommand.Y1;
                            sb.AppendLine($"{indent}skPath{count}.QuadTo({x0.ToString(_ci)}f, {y0.ToString(_ci)}f, {x1.ToString(_ci)}f, {y1.ToString(_ci)}f);");
                        }
                        break;
                    case SP.CubicToPathCommand cubicToPathCommand:
                        {
                            var x0 = cubicToPathCommand.X0;
                            var y0 = cubicToPathCommand.Y0;
                            var x1 = cubicToPathCommand.X1;
                            var y1 = cubicToPathCommand.Y1;
                            var x2 = cubicToPathCommand.X2;
                            var y2 = cubicToPathCommand.Y2;
                            sb.AppendLine($"{indent}skPath{count}.CubicTo({x0.ToString(_ci)}f, {y0.ToString(_ci)}f, {x1.ToString(_ci)}f, {y1.ToString(_ci)}f, {x2.ToString(_ci)}f, {y2.ToString(_ci)}f);");
                        }
                        break;
                    case SP.ClosePathCommand _:
                        {
                            sb.AppendLine($"{indent}skPath{count}.Close();");
                        }
                        break;
                    case SP.AddRectPathCommand addRectPathCommand:
                        {
                            var rect = addRectPathCommand.Rect.ToSKRect();
                            sb.AppendLine($"{indent}skPath{count}.AddRect({rect});");
                        }
                        break;
                    case SP.AddRoundRectPathCommand addRoundRectPathCommand:
                        {
                            var rect = addRoundRectPathCommand.Rect.ToSKRect();
                            var rx = addRoundRectPathCommand.Rx;
                            var ry = addRoundRectPathCommand.Ry;
                            sb.AppendLine($"{indent}skPath{count}.AddRoundRect({rect}, {rx.ToString(_ci)}f, {ry.ToString(_ci)}f);");
                        }
                        break;
                    case SP.AddOvalPathCommand addOvalPathCommand:
                        {
                            var rect = addOvalPathCommand.Rect.ToSKRect();
                            sb.AppendLine($"{indent}skPath{count}.AddOval({rect});");
                        }
                        break;
                    case SP.AddCirclePathCommand addCirclePathCommand:
                        {
                            var x = addCirclePathCommand.X;
                            var y = addCirclePathCommand.Y;
                            var radius = addCirclePathCommand.Radius;
                            sb.AppendLine($"{indent}skPath{count}.AddCircle({x.ToString(_ci)}f, {y.ToString(_ci)}f, {radius.ToString(_ci)}f);");
                        }
                        break;
                    case SP.AddPolyPathCommand addPolyPathCommand:
                        {
                            if (addPolyPathCommand.Points != null)
                            {
                                var points = addPolyPathCommand.Points.ToSKPoints();
                                var close = addPolyPathCommand.Close.ToString(_ci).ToLower();
                                sb.AppendLine($"{indent}skPath{count}.AddPoly(points, {close});");
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        // TODO: ToSKPath
    }

    public class SkiaCodeGenerator
    {
        private static CultureInfo _ci = CultureInfo.InvariantCulture;

        public static string Generate(SP.Picture picture, string className)
        {
            int skPaintCount = -1;
            int skPathCount = -1;
            
            // TODO: Generate code from content.

            var sb = new StringBuilder();

            sb.AppendLine($"using SkiaSharp;");
            sb.AppendLine($"");
            sb.AppendLine($"namespace Svg");
            sb.AppendLine($"{{");
            sb.AppendLine($"    public class {className}");
            sb.AppendLine($"    {{");
            sb.AppendLine($"        public static SKPicture Picture {{ get; }}");
            sb.AppendLine($"");
            sb.AppendLine($"        static {className}()");
            sb.AppendLine($"        {{");
            sb.AppendLine($"            Picture = Record();");
            sb.AppendLine($"        }}");
            sb.AppendLine($"");
            sb.AppendLine($"        private static SKPicture Record()");
            sb.AppendLine($"        {{");
            sb.AppendLine($"            using var skPictureRecorder = new SKPictureRecorder();");
            sb.AppendLine($"            using var skCanvas = skPictureRecorder.BeginRecording({picture.CullRect.ToSKRect()});");

            string indent = "            ";

            foreach (var canvasCommand in picture.Commands)
            {
                switch (canvasCommand)
                {
                    case SP.ClipPathCanvasCommand clipPathCanvasCommand:
                        {
sb.AppendLine($"{indent}// TODO:");
                            //var path = clipPathCanvasCommand.ClipPath.ToSKPath();
                            //var operation = clipPathCanvasCommand.Operation.ToSKClipOperation();
                            //var antialias = clipPathCanvasCommand.Antialias;
                            //skCanvas.ClipPath(path, operation, antialias);
                        }
                        break;
                    case SP.ClipRectCanvasCommand clipRectCanvasCommand:
                        {
sb.AppendLine($"{indent}// TODO:");
                            //var rect = clipRectCanvasCommand.Rect.ToSKRect();
                            //var operation = clipRectCanvasCommand.Operation.ToSKClipOperation();
                            //var antialias = clipRectCanvasCommand.Antialias;
                            //skCanvas.ClipRect(rect, operation, antialias);
                        }
                        break;
                    case SP.SaveCanvasCommand _:
                        {
                            sb.AppendLine($"{indent}skCanvas.Save();");
                        }
                        break;
                    case SP.RestoreCanvasCommand _:
                        {
                            sb.AppendLine($"{indent}skCanvas.Restore();");
                        }
                        break;
                    case SP.SetMatrixCanvasCommand setMatrixCanvasCommand:
                        {
                            sb.AppendLine($"{indent}skCanvas.SetMatrix({setMatrixCanvasCommand.Matrix.ToSKMatrix()});");
                        }
                        break;
                    case SP.SaveLayerCanvasCommand saveLayerCanvasCommand:
                        {
                            if (saveLayerCanvasCommand.Paint != null)
                            {
                                skPaintCount++;
                                saveLayerCanvasCommand.Paint.ToSKPaint(skPaintCount, sb, indent);
                                sb.AppendLine($"{indent}skCanvas.SaveLayer(skPaint{skPaintCount});");
                            }
                            else
                            {
                                sb.AppendLine($"{indent}skCanvas.SaveLayer();");
                            }
                        }
                        break;
                    case SP.DrawImageCanvasCommand drawImageCanvasCommand:
                        {
                            if (drawImageCanvasCommand.Image != null)
                            {
sb.AppendLine($"{indent}// TODO:");
                                //var image = drawImageCanvasCommand.Image.ToSKImage();
                                //var source = drawImageCanvasCommand.Source.ToSKRect();
                                //var dest = drawImageCanvasCommand.Dest.ToSKRect();
                                //var paint = drawImageCanvasCommand.Paint?.ToSKPaint();
                                //skCanvas.DrawImage(image, source, dest, paint);
                            }
                        }
                        break;
                    case SP.DrawPathCanvasCommand drawPathCanvasCommand:
                        {
                            if (drawPathCanvasCommand.Path != null && drawPathCanvasCommand.Paint != null)
                            {
                                skPathCount++;
                                drawPathCanvasCommand.Path.ToSKPath(skPathCount, sb, indent);
                                skPaintCount++;
                                drawPathCanvasCommand.Paint.ToSKPaint(skPaintCount, sb, indent);
                                sb.AppendLine($"{indent}skCanvas.DrawPath(skPath{skPathCount}, skPaint{skPaintCount});");
                            }
                        }
                        break;
                    case SP.DrawTextBlobCanvasCommand drawPositionedTextCanvasCommand:
                        {
                            if (drawPositionedTextCanvasCommand.TextBlob != null && drawPositionedTextCanvasCommand.TextBlob.Points != null && drawPositionedTextCanvasCommand.Paint != null)
                            {
sb.AppendLine($"{indent}// TODO:");
                                //var text = drawPositionedTextCanvasCommand.TextBlob.Text;
                                //var points = drawPositionedTextCanvasCommand.TextBlob.Points.ToSKPoints();
                                //var paint = drawPositionedTextCanvasCommand.Paint.ToSKPaint();
                                //var font = paint.ToFont();
                                //var textBlob = SKTextBlob.CreatePositioned(text, font, points);
                                //skCanvas.DrawText(textBlob, 0, 0, paint);
                            }
                        }
                        break;
                    case SP.DrawTextCanvasCommand drawTextCanvasCommand:
                        {
                            if (drawTextCanvasCommand.Paint != null)
                            {
                                var text = drawTextCanvasCommand.Text;
                                var x = drawTextCanvasCommand.X;
                                var y = drawTextCanvasCommand.Y;
                                skPaintCount++;
                                drawTextCanvasCommand.Paint.ToSKPaint(skPaintCount, sb, indent);
                                sb.AppendLine($"{indent}skCanvas.DrawText(\"{text}\", {x.ToString(_ci)}, {y.ToString(_ci)}, skPaint{skPaintCount});");
                            }
                        }
                        break;
                    case SP.DrawTextOnPathCanvasCommand drawTextOnPathCanvasCommand:
                        {
                            if (drawTextOnPathCanvasCommand.Path != null && drawTextOnPathCanvasCommand.Paint != null)
                            {
sb.AppendLine($"{indent}// TODO:");
                                //var text = drawTextOnPathCanvasCommand.Text;
                                //var path = drawTextOnPathCanvasCommand.Path.ToSKPath();
                                //var hOffset = drawTextOnPathCanvasCommand.HOffset;
                                //var vOffset = drawTextOnPathCanvasCommand.VOffset;
                                //var paint = drawTextOnPathCanvasCommand.Paint.ToSKPaint();
                                //skCanvas.DrawTextOnPath(text, path, hOffset, vOffset, paint);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            sb.AppendLine($"            return skPictureRecorder.EndRecording();");
            sb.AppendLine($"        }}");
            sb.AppendLine($"");
            sb.AppendLine($"        public static void Draw(SKCanvas skCanvas)");
            sb.AppendLine($"        {{");
            sb.AppendLine($"            skCanvas.DrawPicture(Picture);");
            sb.AppendLine($"        }}");
            sb.AppendLine($"    }}");
            sb.AppendLine($"}}");

            var code = sb.ToString();
            return code;
        }
    }

    [Generator]
    public class SvgGenerator : ISourceGenerator
    {
        public void Initialize(InitializationContext context)
        {
            //Debugger.Launch();
        }

        public void Execute(SourceGeneratorContext context)
        {
            try
            {
                ExecuteInternal(context);
            }
            catch (Exception e)
            {
                //This is temporary till https://github.com/dotnet/roslyn/issues/46084 is fixed
                context.ReportDiagnostic(Diagnostic.Create(
                    new DiagnosticDescriptor(
                        "SI0000",
                        "An exception was thrown by the SvgGenerator generator",
                        "An exception was thrown by the SvgGenerator generator: '{0}'",
                        "SvgGenerator",
                        DiagnosticSeverity.Error,
                        isEnabledByDefault: true),
                    Location.None,
                    e.ToString()));
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ExecuteInternal(SourceGeneratorContext context)
        {
           var files = context.AdditionalFiles.Where(at => at.Path.EndsWith(".svg"));

            foreach (var file in files)
            {
                string name = System.IO.Path.GetFileNameWithoutExtension(file.Path);
                string className = name.Replace("-", "_");
                var svg = file.GetText(context.CancellationToken).ToString();
                SvgDocument.SkipGdiPlusCapabilityCheck = true;
                SvgDocument.PointsPerInch = 96;
                var svgDocument = SvgDocument.FromSvg<SvgDocument>(svg);
                if (svgDocument != null)
                {
                    var picture = SKSvg.ToModel(svgDocument);
                    if (picture != null && picture.Commands != null)
                    {
                        var code = SkiaCodeGenerator.Generate(picture, className);
                        var sourceText = SourceText.From(code, Encoding.UTF8);
                        context.AddSource($"{className}.svg.cs", sourceText);
                    }
                }
            }
        }
    }
}
