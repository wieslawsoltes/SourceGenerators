#nullable enable
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SP = Svg.Picture;

namespace Svg.Skia
{
    internal class SkiaCodeGenObjectCounter
    {
        public int Picture = -1;
        public int PictureRecorder = -1;
        public int Canvas = -1;
        public int Paint = -1;
        public int Typeface = -1;
        public int ColorFilter = -1;
        public int ImageFilter = -1;
        public int PathEffect = -1;
        public int Shader = -1;
        public int Path = -1;
        public string PictureVarName = "skPicture";
        public string PictureRecorderVarName = "skPictureRecorder";
        public string CanvasVarName = "skCanvas";
        public string PaintVarName = "skPaint";
        public string TypefaceVarName = "skTypeface";
        public string ColorFilterVarName = "skColorFilter";
        public string ImageFilterVarName = "skImageFilter";
        public string PathEffectVarName = "skPathEffect";
        public string ShaderVarName = "skShader";
        public string PathVarName = "skPath";
    }

    internal static class SkiaCodeGenModelExtensions
    {
        private static CultureInfo _ci = CultureInfo.InvariantCulture;

        public static string ToSKPoint(this SP.Point point)
        {
            return $"new SKPoint({point.X.ToString(_ci)}f, {point.Y.ToString(_ci)}f)";
        }

        public static string ToSKPoint3(this SP.Point3 point3)
        {
            return $"new SKPoint3({point3.X.ToString(_ci)}f, {point3.Y.ToString(_ci)}f, {point3.Z.ToString(_ci)}f)";
        }

        public static string ToSKPoints(this IList<SP.Point> points)
        {
            var result = $"new SKPoint[{points.Count}] {{ ";

            for (int i = 0; i < points.Count; i++)
            {
                result += points[i].ToSKPoint();

                if (points.Count > 0 && i < points.Count - 1)
                {
                    result += $", ";
                }
            }

            result += $" }}";

            return result;
        }

        public static string ToSKPointI(this SP.PointI pointI)
        {
            return $"new SKPointI({pointI.X.ToString(_ci)}, {pointI.Y.ToString(_ci)})";
        }

        public static string ToSKSize(this SP.Size size)
        {
            return $"new SKSize({size.Width.ToString(_ci)}f, {size.Height.ToString(_ci)}f)";
        }

        public static string ToSKSizeI(this SP.SizeI sizeI)
        {
            return $"new SKSizeI({sizeI.Width.ToString(_ci)}, {sizeI.Height.ToString(_ci)})";
        }

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

        public static string ToSKFontStyleWeight(this SP.FontStyleWeight fontStyleWeight)
        {
            switch (fontStyleWeight)
            {
                default:
                case SP.FontStyleWeight.Invisible:
                    return "SKFontStyleWeight.Invisible";
                case SP.FontStyleWeight.Thin:
                    return "SKFontStyleWeight.Thin";
                case SP.FontStyleWeight.ExtraLight:
                    return "SKFontStyleWeight.ExtraLight";
                case SP.FontStyleWeight.Light:
                    return "SKFontStyleWeight.Light";
                case SP.FontStyleWeight.Normal:
                    return "SKFontStyleWeight.Normal";
                case SP.FontStyleWeight.Medium:
                    return "SKFontStyleWeight.Medium";
                case SP.FontStyleWeight.SemiBold:
                    return "SKFontStyleWeight.SemiBold";
                case SP.FontStyleWeight.Bold:
                    return "SKFontStyleWeight.Bold";
                case SP.FontStyleWeight.ExtraBold:
                    return "SKFontStyleWeight.ExtraBold";
                case SP.FontStyleWeight.Black:
                    return "SKFontStyleWeight.Black";
                case SP.FontStyleWeight.ExtraBlack:
                    return "SKFontStyleWeight.ExtraBlack";
            }
        }

        public static string ToSKFontStyleWidth(this SP.FontStyleWidth fontStyleWidth)
        {
            switch (fontStyleWidth)
            {
                default:
                case SP.FontStyleWidth.UltraCondensed:
                    return "SKFontStyleWidth.UltraCondensed";
                case SP.FontStyleWidth.ExtraCondensed:
                    return "SKFontStyleWidth.ExtraCondensed";
                case SP.FontStyleWidth.Condensed:
                    return "SKFontStyleWidth.Condensed";
                case SP.FontStyleWidth.SemiCondensed:
                    return "SKFontStyleWidth.SemiCondensed";
                case SP.FontStyleWidth.Normal:
                    return "SKFontStyleWidth.Normal";
                case SP.FontStyleWidth.SemiExpanded:
                    return "SKFontStyleWidth.SemiExpanded";
                case SP.FontStyleWidth.Expanded:
                    return "SKFontStyleWidth.Expanded";
                case SP.FontStyleWidth.ExtraExpanded:
                    return "SKFontStyleWidth.ExtraExpanded";
                case SP.FontStyleWidth.UltraExpanded:
                    return "SKFontStyleWidth.UltraExpanded";
            }
        }

        public static string ToSKFontStyleSlant(this SP.FontStyleSlant fontStyleSlant)
        {
            switch (fontStyleSlant)
            {
                default:
                case SP.FontStyleSlant.Upright:
                    return "SKFontStyleSlant.Upright";
                case SP.FontStyleSlant.Italic:
                    return "SKFontStyleSlant.Italic";
                case SP.FontStyleSlant.Oblique:
                    return "SKFontStyleSlant.Oblique";
            }
        }

        // TODO: ToSKTypeface

        public static string ToSKColor(this SP.Color color)
        {
            return $"new SKColor({color.Red}, {color.Green}, {color.Blue}, {color.Alpha})";
        }

        public static string ToSKColors(this SP.Color[] colors)
        {
            var skColors = $"new SKColor[{colors.Length}] {{ ";

            for (int i = 0; i < colors.Length; i++)
            {
                skColors += colors[i].ToSKColor();

                if (colors.Length > 0 && i < colors.Length - 1)
                {
                    skColors += $", ";
                }
            }

            skColors += $" }}";

            return skColors;
        }

        public static string ToSKColorF(this SP.ColorF color)
        {
            return $"new SKColorF({color.Red.ToString(_ci)}f, {color.Green.ToString(_ci)}f, {color.Blue.ToString(_ci)}f, {color.Alpha.ToString(_ci)}f)";
        }

        public static string ToSKColors(this SP.ColorF[] colors)
        {
            var skColors = $"new SKColorF[{colors.Length}] {{ ";

            for (int i = 0; i < colors.Length; i++)
            {
                skColors += colors[i].ToSKColorF();

                if (colors.Length > 0 && i < colors.Length - 1)
                {
                    skColors += $", ";
                }
            }

            skColors += $" }}";

            return skColors;
        }

        public static string ToSKColorPos(this float[] colorPos)
        {
            var result = $"new float[{colorPos.Length}] {{ ";

            for (int i = 0; i < colorPos.Length; i++)
            {
                result += $"{colorPos[i].ToString(_ci)}f";

                if (colorPos.Length > 0 && i < colorPos.Length - 1)
                {
                    result += $", ";
                }
            }

            result += $" }}";

            return result;
        }

        public static string ToSKShaderTileMode(this SP.ShaderTileMode shaderTileMode)
        {
            switch (shaderTileMode)
            {
                default:
                case SP.ShaderTileMode.Clamp:
                    return "SKShaderTileMode.Clamp";
                case SP.ShaderTileMode.Repeat:
                    return "SKShaderTileMode.Repeat";
                case SP.ShaderTileMode.Mirror:
                    return "SKShaderTileMode.Mirror";
                case SP.ShaderTileMode.Decal:
                    return "SKShaderTileMode.Decal";
            }
        }

        public static void ToSKShader(this SP.Shader? shader, SkiaCodeGenObjectCounter counter, StringBuilder sb, string indent)
        {
            var counterShader = counter.Shader;

            switch (shader)
            {
                case SP.ColorShader colorShader:
                    {
                        sb.Append($"{indent}using var {counter.ShaderVarName}{counterShader} = ");
                        sb.AppendLine($"SKShader.CreateColor(");
                        sb.AppendLine($"{indent}    {colorShader.Color.ToSKColor()});");
                        return;
                    }
                case SP.LinearGradientShader linearGradientShader:
                    {
                        if (linearGradientShader.Colors == null || linearGradientShader.ColorPos == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ShaderVarName}{counterShader} = default(SKShader);");
                            return;
                        }

                        if (linearGradientShader.LocalMatrix != null)
                        {
                            sb.Append($"{indent}using var {counter.ShaderVarName}{counterShader} = ");
                            sb.AppendLine($"SKShader.CreateLinearGradient(");
                            sb.AppendLine($"{indent}    {linearGradientShader.Start.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.End.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.Colors.ToSKColors()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.ColorPos.ToSKColorPos()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.Mode.ToSKShaderTileMode()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.LocalMatrix.Value.ToSKMatrix()});");
                            return;
                        }
                        else
                        {
                            sb.Append($"{indent}using var {counter.ShaderVarName}{counterShader} = ");
                            sb.AppendLine($"SKShader.CreateLinearGradient(");
                            sb.AppendLine($"{indent}    {linearGradientShader.Start.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.End.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.Colors.ToSKColors()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.ColorPos.ToSKColorPos()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.Mode.ToSKShaderTileMode()});");
                            return;
                        }
                    }
                case SP.TwoPointConicalGradientShader twoPointConicalGradientShader:
                    {
                        if (twoPointConicalGradientShader.Colors == null || twoPointConicalGradientShader.ColorPos == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ShaderVarName}{counterShader} = default(SKShader);");
                            return;
                        }

                        if (twoPointConicalGradientShader.LocalMatrix != null)
                        {
                            sb.Append($"{indent}using var {counter.ShaderVarName}{counterShader} = ");
                            sb.AppendLine($"SKShader.CreateTwoPointConicalGradient(");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.Start.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.StartRadius.ToString(_ci)}f,");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.End.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.EndRadius.ToString(_ci)}f,");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.Colors.ToSKColors()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.ColorPos.ToSKColorPos()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.Mode.ToSKShaderTileMode()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.LocalMatrix.Value.ToSKMatrix()});");
                            return;
                        }
                        else
                        {
                            sb.Append($"{indent}using var {counter.ShaderVarName}{counterShader} = ");
                            sb.AppendLine($"SKShader.CreateTwoPointConicalGradient(");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.Start.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.StartRadius.ToString(_ci)}f,");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.End.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.EndRadius.ToString(_ci)}f,");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.Colors.ToSKColors()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.ColorPos.ToSKColorPos()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.Mode.ToSKShaderTileMode()});");
                            return;
                        }
                    }
                case SP.PictureShader pictureShader:
                    {
                        if (pictureShader.Src == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ShaderVarName}{counterShader} = default(SKShader);");
                            return;
                        }

                        var counterPicture = ++counter.Picture;
                        pictureShader.Src?.ToSKPicture(counter, sb, indent);

                        sb.Append($"{indent}using var {counter.ShaderVarName}{counterShader} = ");
                        sb.AppendLine($"SKShader.CreatePicture(");
                        sb.AppendLine($"{indent}    {counter.PictureVarName}{counterPicture},");
                        sb.AppendLine($"{indent}    SKShaderTileMode.Repeat,");
                        sb.AppendLine($"{indent}    SKShaderTileMode.Repeat,");
                        sb.AppendLine($"{indent}    {pictureShader.LocalMatrix.ToSKMatrix()},");
                        sb.AppendLine($"{indent}    {pictureShader.Tile.ToSKRect()});");
                        return;
                    }
                case SP.PerlinNoiseFractalNoiseShader perlinNoiseFractalNoiseShader:
                    {
                        sb.Append($"{indent}using var {counter.ShaderVarName}{counterShader} = ");
                        sb.AppendLine($"SKShader.CreatePerlinNoiseFractalNoise(");
                        sb.AppendLine($"{indent}    {perlinNoiseFractalNoiseShader.BaseFrequencyX.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {perlinNoiseFractalNoiseShader.BaseFrequencyY.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {perlinNoiseFractalNoiseShader.NumOctaves.ToString(_ci)},");
                        sb.AppendLine($"{indent}    {perlinNoiseFractalNoiseShader.Seed.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {perlinNoiseFractalNoiseShader.TileSize.ToSKPointI()});");
                        return;
                    }
                case SP.PerlinNoiseTurbulenceShader perlinNoiseTurbulenceShader:
                    {
                        sb.Append($"{indent}using var {counter.ShaderVarName}{counterShader} = ");
                        sb.AppendLine($"SKShader.CreatePerlinNoiseTurbulence(");
                        sb.AppendLine($"{indent}    {perlinNoiseTurbulenceShader.BaseFrequencyX.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {perlinNoiseTurbulenceShader.BaseFrequencyY.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {perlinNoiseTurbulenceShader.NumOctaves.ToString(_ci)},");
                        sb.AppendLine($"{indent}    {perlinNoiseTurbulenceShader.Seed.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {perlinNoiseTurbulenceShader.TileSize.ToSKPointI()});");
                        return;
                    }
                default:
                    {
                        sb.AppendLine($"{indent}var {counter.ShaderVarName}{counterShader} = default(SKShader);");
                        return;
                    }
            }
        }

        // TODO: ToSKColorFilter

        public static string ToCropRect(this SP.CropRect cropRect)
        {
            return $"new SKImageFilter.CropRect({cropRect.Rect.ToSKRect()})";
        }

        public static string ToSKColorChannel(this SP.ColorChannel colorChannel)
        {
            switch (colorChannel)
            {
                default:
                case SP.ColorChannel.R:
                    return "SKColorChannel.R";
                case SP.ColorChannel.G:
                    return "SKColorChannel.G";
                case SP.ColorChannel.B:
                    return "SKColorChannel.B";
                case SP.ColorChannel.A:
                    return "SKColorChannel.A";
            }
        }

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

        public static void ToSKPaint(this SP.Paint paint, SkiaCodeGenObjectCounter counter, StringBuilder sb, string indent)
        {
            var counterPaint = counter.Paint;

            sb.AppendLine($"{indent}using var {counter.PaintVarName}{counterPaint} = new SKPaint();");
            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Style = {paint.Style.ToSKPaintStyle()};");
            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.IsAntialias = {paint.IsAntialias.ToString(_ci).ToLower()};");
            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.StrokeWidth = {paint.StrokeWidth.ToString(_ci)}f;");
            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.StrokeCap = {paint.StrokeCap.ToSKStrokeCap()};");
            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.StrokeJoin = {paint.StrokeJoin.ToSKStrokeJoin()};");
            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.StrokeMiter = {paint.StrokeMiter.ToString(_ci)}f;");
            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.TextSize = {paint.TextSize.ToString(_ci)}f;");
            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.TextAlign = {paint.TextAlign.ToSKTextAlign()};");

            if (paint.Typeface != null)
            {
                // TODO: Typeface = paint.Typeface?.ToSKTypeface();
                sb.AppendLine($"{indent} // TODO: {counter.PaintVarName}{counterPaint}.Typeface");
            }

            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.LcdRenderText = {paint.LcdRenderText.ToString(_ci).ToLower()};");
            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.SubpixelText = {paint.SubpixelText.ToString(_ci).ToLower()};");
            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.TextEncoding = {paint.TextEncoding.ToSKTextEncoding()};");
            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Color = {(paint.Color == null ? "SKColor.Empty" : ToSKColor(paint.Color.Value))};");

            if (paint.Shader != null)
            {
                var counterShader = ++counter.Shader;
                paint.Shader.ToSKShader(counter, sb, indent);
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Shader = {counter.ShaderVarName}{counterShader};");
            }

            if (paint.ColorFilter != null)
            {
                // TODO: ColorFilter = paint.ColorFilter?.ToSKColorFilter();
                sb.AppendLine($"{indent} // TODO: {counter.PaintVarName}{counterPaint}.ColorFilter");
            }

            if (paint.ImageFilter != null)
            {
                // TODO: ImageFilter = paint.ImageFilter?.ToSKImageFilter();
                sb.AppendLine($"{indent} // TODO: {counter.PaintVarName}{counterPaint}.ImageFilter");
            }

            if (paint.PathEffect != null)
            {
                // TODO: PathEffect = paint.PathEffect?.ToSKPathEffect();
                sb.AppendLine($"{indent} // TODO: {counter.PaintVarName}{counterPaint}.PathEffect");
            }

            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.BlendMode = {paint.BlendMode.ToSKBlendMode()};");
            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.FilterQuality = {paint.FilterQuality.ToSKFilterQuality()};");
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

        public static void ToSKPath(this SP.Path path, SkiaCodeGenObjectCounter counter, StringBuilder sb, string indent)
        {
            sb.AppendLine($"{indent}using var {counter.PathVarName}{counter.Path} = new SKPath() {{ FillType = {path.FillType.ToSKPathFillType()} }};");

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
                            sb.AppendLine($"{indent}skPath{counter.Path}.MoveTo({x.ToString(_ci)}f, {y.ToString(_ci)}f);");

                        }
                        break;
                    case SP.LineToPathCommand lineToPathCommand:
                        {
                            var x = lineToPathCommand.X;
                            var y = lineToPathCommand.Y;
                            sb.AppendLine($"{indent}skPath{counter.Path}.LineTo({x.ToString(_ci)}f, {y.ToString(_ci)}f);");
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
                            sb.AppendLine($"{indent}skPath{counter.Path}.ArcTo({rx.ToString(_ci)}f, {ry.ToString(_ci)}f, {xAxisRotate.ToString(_ci)}f, {largeArc}, {sweep}, {x.ToString(_ci)}f, {y.ToString(_ci)}f);");
                        }
                        break;
                    case SP.QuadToPathCommand quadToPathCommand:
                        {
                            var x0 = quadToPathCommand.X0;
                            var y0 = quadToPathCommand.Y0;
                            var x1 = quadToPathCommand.X1;
                            var y1 = quadToPathCommand.Y1;
                            sb.AppendLine($"{indent}skPath{counter.Path}.QuadTo({x0.ToString(_ci)}f, {y0.ToString(_ci)}f, {x1.ToString(_ci)}f, {y1.ToString(_ci)}f);");
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
                            sb.AppendLine($"{indent}skPath{counter.Path}.CubicTo({x0.ToString(_ci)}f, {y0.ToString(_ci)}f, {x1.ToString(_ci)}f, {y1.ToString(_ci)}f, {x2.ToString(_ci)}f, {y2.ToString(_ci)}f);");
                        }
                        break;
                    case SP.ClosePathCommand _:
                        {
                            sb.AppendLine($"{indent}skPath{counter.Path}.Close();");
                        }
                        break;
                    case SP.AddRectPathCommand addRectPathCommand:
                        {
                            var rect = addRectPathCommand.Rect.ToSKRect();
                            sb.AppendLine($"{indent}skPath{counter.Path}.AddRect({rect});");
                        }
                        break;
                    case SP.AddRoundRectPathCommand addRoundRectPathCommand:
                        {
                            var rect = addRoundRectPathCommand.Rect.ToSKRect();
                            var rx = addRoundRectPathCommand.Rx;
                            var ry = addRoundRectPathCommand.Ry;
                            sb.AppendLine($"{indent}skPath{counter.Path}.AddRoundRect({rect}, {rx.ToString(_ci)}f, {ry.ToString(_ci)}f);");
                        }
                        break;
                    case SP.AddOvalPathCommand addOvalPathCommand:
                        {
                            var rect = addOvalPathCommand.Rect.ToSKRect();
                            sb.AppendLine($"{indent}skPath{counter.Path}.AddOval({rect});");
                        }
                        break;
                    case SP.AddCirclePathCommand addCirclePathCommand:
                        {
                            var x = addCirclePathCommand.X;
                            var y = addCirclePathCommand.Y;
                            var radius = addCirclePathCommand.Radius;
                            sb.AppendLine($"{indent}skPath{counter.Path}.AddCircle({x.ToString(_ci)}f, {y.ToString(_ci)}f, {radius.ToString(_ci)}f);");
                        }
                        break;
                    case SP.AddPolyPathCommand addPolyPathCommand:
                        {
                            if (addPolyPathCommand.Points != null)
                            {
                                var points = addPolyPathCommand.Points.ToSKPoints();
                                var close = addPolyPathCommand.Close.ToString(_ci).ToLower();
                                sb.AppendLine($"{indent}skPath{counter.Path}.AddPoly(points, {close});");
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        // TODO: ToSKPath

        public static void ToSKPicture(this SP.Picture? picture, SkiaCodeGenObjectCounter counter, StringBuilder sb, string indent)
        {
            var counterPicture = counter.Picture;

            if (picture == null)
            {
                sb.AppendLine($"{indent}var {counter.PictureVarName}{counterPicture} = default(SKPicture);");
                return;
            }

            var counterPictureRecorder = ++counter.PictureRecorder;
            var counterCanvas = ++counter.Canvas;

            sb.AppendLine($"{indent}using var {counter.PictureRecorderVarName}{counterPictureRecorder} = new SKPictureRecorder();");
            sb.AppendLine($"{indent}using var {counter.CanvasVarName}{counterCanvas} = {counter.PictureRecorderVarName}{counterPictureRecorder}.BeginRecording({picture.CullRect.ToSKRect()});");

            if (picture.Commands == null)
            {
                sb.AppendLine($"{indent}var {counter.PictureVarName}{counterPicture} = {counter.PictureRecorderVarName}{counterPictureRecorder}.EndRecording();");
                return;
            }

            foreach (var canvasCommand in picture.Commands)
            {
                switch (canvasCommand)
                {
                    case SP.ClipPathCanvasCommand clipPathCanvasCommand:
                        {
                            // TODO:
                            sb.AppendLine($"{indent}// TODO: ClipPath");
                            //var path = clipPathCanvasCommand.ClipPath.ToSKPath();
                            //var operation = clipPathCanvasCommand.Operation.ToSKClipOperation();
                            //var antialias = clipPathCanvasCommand.Antialias;
                            //skCanvas.ClipPath(path, operation, antialias);
                        }
                        break;
                    case SP.ClipRectCanvasCommand clipRectCanvasCommand:
                        {
                            var rect = clipRectCanvasCommand.Rect.ToSKRect();
                            var operation = clipRectCanvasCommand.Operation.ToSKClipOperation();
                            var antialias = clipRectCanvasCommand.Antialias.ToString(_ci).ToLower();
                            sb.AppendLine($"{indent}{counter.CanvasVarName}{counterCanvas}.ClipRect({rect}, {operation}, {antialias});");
                        }
                        break;
                    case SP.SaveCanvasCommand _:
                        {
                            sb.AppendLine($"{indent}{counter.CanvasVarName}{counterCanvas}.Save();");
                        }
                        break;
                    case SP.RestoreCanvasCommand _:
                        {
                            sb.AppendLine($"{indent}{counter.CanvasVarName}{counterCanvas}.Restore();");
                        }
                        break;
                    case SP.SetMatrixCanvasCommand setMatrixCanvasCommand:
                        {
                            sb.AppendLine($"{indent}{counter.CanvasVarName}{counterCanvas}.SetMatrix({setMatrixCanvasCommand.Matrix.ToSKMatrix()});");
                        }
                        break;
                    case SP.SaveLayerCanvasCommand saveLayerCanvasCommand:
                        {
                            if (saveLayerCanvasCommand.Paint != null)
                            {
                                var counterPaint = ++counter.Paint;
                                saveLayerCanvasCommand.Paint.ToSKPaint(counter, sb, indent);
                                sb.AppendLine($"{indent}{counter.CanvasVarName}{counterCanvas}.SaveLayer({counter.PaintVarName}{counterPaint});");
                            }
                            else
                            {
                                sb.AppendLine($"{indent}{counter.CanvasVarName}{counterCanvas}.SaveLayer();");
                            }
                        }
                        break;
                    case SP.DrawImageCanvasCommand drawImageCanvasCommand:
                        {
                            if (drawImageCanvasCommand.Image != null)
                            {
                                // TODO:
                                sb.AppendLine($"{indent}// TODO: DrawImage");
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
                                var counterPath = ++counter.Path;
                                drawPathCanvasCommand.Path.ToSKPath(counter, sb, indent);
                                var counterPaint = ++counter.Paint;
                                drawPathCanvasCommand.Paint.ToSKPaint(counter, sb, indent);
                                sb.AppendLine($"{indent}{counter.CanvasVarName}{counterCanvas}.DrawPath({counter.PathVarName}{counterPath}, {counter.PaintVarName}{counterPaint});");
                            }
                        }
                        break;
                    case SP.DrawTextBlobCanvasCommand drawPositionedTextCanvasCommand:
                        {
                            if (drawPositionedTextCanvasCommand.TextBlob != null && drawPositionedTextCanvasCommand.TextBlob.Points != null && drawPositionedTextCanvasCommand.Paint != null)
                            {
                                // TODO:
                                sb.AppendLine($"{indent}// TODO: DrawTextBlob");
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
                                var counterPaint = ++counter.Paint;
                                drawTextCanvasCommand.Paint.ToSKPaint(counter, sb, indent);
                                sb.AppendLine($"{indent}{counter.CanvasVarName}{counterCanvas}.DrawText(\"{text}\", {x.ToString(_ci)}, {y.ToString(_ci)}, {counter.PaintVarName}{counterPaint});");
                            }
                        }
                        break;
                    case SP.DrawTextOnPathCanvasCommand drawTextOnPathCanvasCommand:
                        {
                            if (drawTextOnPathCanvasCommand.Path != null && drawTextOnPathCanvasCommand.Paint != null)
                            {
                                // TODO:
                                sb.AppendLine($"{indent}// TODO: DrawTextOnPath");
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

            sb.AppendLine($"{indent}var {counter.PictureVarName}{counterPicture} = {counter.PictureRecorderVarName}{counterPictureRecorder}.EndRecording();");
        }
    }
}
