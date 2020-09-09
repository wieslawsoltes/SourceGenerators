using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SP = Svg.Picture;

namespace Svg
{
    internal static class SkiaCodeGenModelExtensions
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

        public static string ToSKTextEncoding(this SP.TextEncoding textEncoding)
        {
            switch (textEncoding)
            {
                default:
                case SP.TextEncoding.Utf8:
                    return "SKTextEncoding.Utf8";
                case SP.TextEncoding.Utf16:
                    return "SKTextEncoding.Utf16";
                case SP.TextEncoding.Utf32:
                    return "SKTextEncoding.Utf32";
                case SP.TextEncoding.GlyphId:
                    return "SKTextEncoding.GlyphId";
            }
        }

        // TODO: ToSKFontStyleWeight
        // TODO: ToSKFontStyleWidth
        // TODO: ToSKFontStyleSlant
        // TODO: ToSKTypeface

        public static string ToSKColor(this SP.Color color)
        {
            return $"new SKColor({color.Red}, {color.Green}, {color.Blue}, {color.Alpha})";
        }

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

        public static string ToSKBlendMode(this SP.BlendMode blendMode)
        {
            switch (blendMode)
            {
                default:
                case SP.BlendMode.Clear:
                    return "SKBlendMode.Clear";
                case SP.BlendMode.Src:
                    return "SKBlendMode.Src";
                case SP.BlendMode.Dst:
                    return "SKBlendMode.Dst";
                case SP.BlendMode.SrcOver:
                    return "SKBlendMode.SrcOver";
                case SP.BlendMode.DstOver:
                    return "SKBlendMode.DstOver";
                case SP.BlendMode.SrcIn:
                    return "SKBlendMode.SrcIn";
                case SP.BlendMode.DstIn:
                    return "SKBlendMode.DstIn";
                case SP.BlendMode.SrcOut:
                    return "SKBlendMode.SrcOut";
                case SP.BlendMode.DstOut:
                    return "SKBlendMode.DstOut";
                case SP.BlendMode.SrcATop:
                    return "SKBlendMode.SrcATop";
                case SP.BlendMode.DstATop:
                    return "SKBlendMode.DstATop";
                case SP.BlendMode.Xor:
                    return "SKBlendMode.Xor";
                case SP.BlendMode.Plus:
                    return "SKBlendMode.Plus";
                case SP.BlendMode.Modulate:
                    return "SKBlendMode.Modulate";
                case SP.BlendMode.Screen:
                    return "SKBlendMode.Screen";
                case SP.BlendMode.Overlay:
                    return "SKBlendMode.Overlay";
                case SP.BlendMode.Darken:
                    return "SKBlendMode.Darken";
                case SP.BlendMode.Lighten:
                    return "SKBlendMode.Lighten";
                case SP.BlendMode.ColorDodge:
                    return "SKBlendMode.ColorDodge";
                case SP.BlendMode.ColorBurn:
                    return "SKBlendMode.ColorBurn";
                case SP.BlendMode.HardLight:
                    return "SKBlendMode.HardLight";
                case SP.BlendMode.SoftLight:
                    return "SKBlendMode.SoftLight";
                case SP.BlendMode.Difference:
                    return "SKBlendMode.Difference";
                case SP.BlendMode.Exclusion:
                    return "SKBlendMode.Exclusion";
                case SP.BlendMode.Multiply:
                    return "SKBlendMode.Multiply";
                case SP.BlendMode.Hue:
                    return "SKBlendMode.Hue";
                case SP.BlendMode.Saturation:
                    return "SKBlendMode.Saturation";
                case SP.BlendMode.Color:
                    return "SKBlendMode.Color";
                case SP.BlendMode.Luminosity:
                    return "SKBlendMode.Luminosity";
            }
        }

        public static string ToSKFilterQuality(this SP.FilterQuality filterQuality)
        {
            switch (filterQuality)
            {
                default:
                case SP.FilterQuality.None:
                    return "SKFilterQuality.None";
                case SP.FilterQuality.Low:
                    return "SKFilterQuality.Low";
                case SP.FilterQuality.Medium:
                    return "SKFilterQuality.Medium";
                case SP.FilterQuality.High:
                    return "SKFilterQuality.High";
            }
        }

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
            // TODO: Typeface = paint.Typeface?.ToSKTypeface();
            sb.AppendLine($"{indent}skPaint{count}.LcdRenderText = {paint.LcdRenderText.ToString(_ci).ToLower()};");
            sb.AppendLine($"{indent}skPaint{count}.SubpixelText = {paint.SubpixelText.ToString(_ci).ToLower()};");
            sb.AppendLine($"{indent}skPaint{count}.TextEncoding = {paint.TextEncoding.ToSKTextEncoding()};");
            sb.AppendLine($"{indent}skPaint{count}.Color = {(paint.Color == null ? "SKColor.Empty" : ToSKColor(paint.Color.Value))};");
            // TODO: Shader = paint.Shader?.ToSKShader();
            // TODO: ColorFilter = paint.ColorFilter?.ToSKColorFilter();
            // TODO: ImageFilter = paint.ImageFilter?.ToSKImageFilter();
            // TODO: PathEffect = paint.PathEffect?.ToSKPathEffect();
            sb.AppendLine($"{indent}skPaint{count}.BlendMode = {paint.BlendMode.ToSKBlendMode()};");
            sb.AppendLine($"{indent}skPaint{count}.FilterQuality = {paint.FilterQuality.ToSKFilterQuality()};");
        }

        public static string ToSKClipOperation(this SP.ClipOperation clipOperation)
        {
            switch (clipOperation)
            {
                default:
                case SP.ClipOperation.Difference:
                    return "SKClipOperation.Difference";
                case SP.ClipOperation.Intersect:
                    return "SKClipOperation.Intersect";
            }
        }

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
}
