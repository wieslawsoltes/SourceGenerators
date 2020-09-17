//#define USE_DIAGNOSTICTS
//#define USE_PAINT_RESET
//#define USE_PATH_RESET
#nullable enable
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        public int TextBlob = -1;
        public int Font = -1;
        public int ColorFilter = -1;
        public int ImageFilter = -1;
        public int PathEffect = -1;
        public int Shader = -1;
        public int Path = -1;
        public int Image = -1;
        public string PictureVarName = "skPicture";
        public string PictureRecorderVarName = "skPictureRecorder";
        public string CanvasVarName = "skCanvas";
        public string PaintVarName = "skPaint";
        public string TypefaceVarName = "skTypeface";
        public string TextBlobVarName = "skTextBlob";
        public string FontVarName = "skFont";
        public string ColorFilterVarName = "skColorFilter";
        public string ImageFilterVarName = "skImageFilter";
        public string PathEffectVarName = "skPathEffect";
        public string ShaderVarName = "skShader";
        public string PathVarName = "skPath";
        public string ImageVarName = "skImage";
        public string FontManagerVarName = "skFontManager";
        public string FontStyleVarName = "skFontStyle";
        public string FontStyleSetVarName = "skFontStyleSet";
    }

    internal static class SkiaCodeGenModelExtensions
    {
        private static CultureInfo _ci = CultureInfo.InvariantCulture;

        private static readonly char[] s_fontFamilyTrim = { '\'' };

        private static string? EspaceString(string? text)
        {
            return text?.Replace("\"", "\\\"");
        }

        public static string ToByteArray(this byte[] array)
        {
            var result = $"new byte[{array.Length}] {{ ";

            for (int i = 0; i < array.Length; i++)
            {
                result += $"{array[i].ToString(_ci)}";

                if (array.Length > 0 && i < array.Length - 1)
                {
                    result += $", ";
                }
            }

            result += $" }}";

            return result;
        }

        public static string ToFloatArray(this float[] array)
        {
            var result = $"new float[{array.Length}] {{ ";

            for (int i = 0; i < array.Length; i++)
            {
                result += $"{array[i].ToString(_ci)}f";

                if (array.Length > 0 && i < array.Length - 1)
                {
                    result += $", ";
                }
            }

            result += $" }}";

            return result;
        }

        public static string ToStringArray(this string[] array)
        {
            var result = $"new string[{array.Length}] {{ ";

            for (int i = 0; i < array.Length; i++)
            {
                result += $"\"{array[i]}\"";

                if (array.Length > 0 && i < array.Length - 1)
                {
                    result += $", ";
                }
            }

            result += $" }}";

            return result;
        }

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

        public static void ToSKImage(this SP.Image image, SkiaCodeGenObjectCounter counter, StringBuilder sb, string indent)
        {
            var counterImage = counter.Image;

            if (image.Data == null)
            {
                sb.AppendLine($"{indent}var {counter.ImageVarName}{counterImage} = default(SKImage);");
                return;
            }

            sb.Append($"{indent}var {counter.ImageVarName}{counterImage} = ");
            sb.AppendLine($"SKImage.FromEncodedData({image.Data.ToByteArray()});");
        }

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

        public static void ToSKTypeface(this SP.Typeface? typeface, SkiaCodeGenObjectCounter counter, StringBuilder sb, string indent)
        {
            var counterTypeface = counter.Typeface;

            if (typeface == null || typeface.FamilyName == null)
            {
                sb.AppendLine($"{indent}var {counter.TypefaceVarName}{counterTypeface} = SKTypeface.Default");
                return;
            }

            var fontFamily = typeface.FamilyName;
            var fontWeight = typeface.Weight.ToSKFontStyleWeight();
            var fontWidth = typeface.Width.ToSKFontStyleWidth();
            var fontStyle = typeface.Style.ToSKFontStyleSlant();
#if false
            sb.AppendLine($"{indent}var {counter.TypefaceVarName}{counterTypeface} = SKTypeface.FromFamilyName(\"{fontFamily}\", {fontWeight}, {fontWidth}, {fontStyle});");
#else
            var fontFamilyNames = fontFamily?.Split(',')?.Select(x => x.Trim().Trim(s_fontFamilyTrim))?.ToArray();
            if (fontFamilyNames != null && fontFamilyNames.Length > 0)
            {
                sb.AppendLine($"{indent}var {counter.TypefaceVarName}{counterTypeface} = default(SKTypeface);");
                sb.AppendLine($"{indent}var fontFamilyNames{counterTypeface} = {fontFamilyNames?.ToStringArray()};");
                sb.AppendLine($"{indent}var defaultName{counterTypeface} = SKTypeface.Default.FamilyName;");
                sb.AppendLine($"{indent}var {counter.FontManagerVarName}{counterTypeface} = SKFontManager.Default;");
                sb.AppendLine($"{indent}var {counter.FontStyleVarName}{counterTypeface} = new SKFontStyle({fontWeight}, {fontWidth}, {fontStyle});");
                sb.AppendLine($"{indent}foreach (var fontFamilyName{counterTypeface} in fontFamilyNames{counterTypeface})");
                sb.AppendLine($"{indent}{{");
                sb.AppendLine($"{indent}    var {counter.FontStyleSetVarName}{counterTypeface} = {counter.FontManagerVarName}{counterTypeface}.GetFontStyles(fontFamilyName{counterTypeface});");
                sb.AppendLine($"{indent}    if ({counter.FontStyleSetVarName}{counterTypeface}.Count > 0)");
                sb.AppendLine($"{indent}    {{");
                sb.AppendLine($"{indent}        {counter.TypefaceVarName}{counterTypeface} = {counter.FontManagerVarName}{counterTypeface}.MatchFamily(fontFamilyName{counterTypeface}, {counter.FontStyleVarName}{counterTypeface});");
                sb.AppendLine($"{indent}        if ({counter.TypefaceVarName}{counterTypeface} != null)");
                sb.AppendLine($"{indent}        {{");
                sb.AppendLine($"{indent}            if (!defaultName{counterTypeface}.Equals(fontFamilyName{counterTypeface}, StringComparison.Ordinal)");
                sb.AppendLine($"{indent}                && defaultName{counterTypeface}.Equals({counter.TypefaceVarName}{counterTypeface}.FamilyName, StringComparison.Ordinal))");
                sb.AppendLine($"{indent}            {{");
                //sb.AppendLine($"{indent}                {counter.TypefaceVarName}{counterTypeface}?.Dispose();");
                sb.AppendLine($"{indent}                {counter.TypefaceVarName}{counterTypeface} = null;");
                sb.AppendLine($"{indent}                continue;");
                sb.AppendLine($"{indent}            }}");
                sb.AppendLine($"{indent}            break;");
                sb.AppendLine($"{indent}        }}");
                sb.AppendLine($"{indent}    }}");
                sb.AppendLine($"{indent}}}");
            }
            else
            {
                sb.AppendLine($"{indent}var {counter.TypefaceVarName}{counterTypeface} = SKTypeface.Default;");
            }
#endif
        }

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
                        sb.Append($"{indent}var {counter.ShaderVarName}{counterShader} = ");
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
                            sb.Append($"{indent}var {counter.ShaderVarName}{counterShader} = ");
                            sb.AppendLine($"SKShader.CreateLinearGradient(");
                            sb.AppendLine($"{indent}    {linearGradientShader.Start.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.End.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.Colors.ToSKColors()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.ColorPos.ToFloatArray()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.Mode.ToSKShaderTileMode()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.LocalMatrix.Value.ToSKMatrix()});");
                            return;
                        }
                        else
                        {
                            sb.Append($"{indent}var {counter.ShaderVarName}{counterShader} = ");
                            sb.AppendLine($"SKShader.CreateLinearGradient(");
                            sb.AppendLine($"{indent}    {linearGradientShader.Start.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.End.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.Colors.ToSKColors()},");
                            sb.AppendLine($"{indent}    {linearGradientShader.ColorPos.ToFloatArray()},");
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
                            sb.Append($"{indent}var {counter.ShaderVarName}{counterShader} = ");
                            sb.AppendLine($"SKShader.CreateTwoPointConicalGradient(");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.Start.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.StartRadius.ToString(_ci)}f,");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.End.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.EndRadius.ToString(_ci)}f,");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.Colors.ToSKColors()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.ColorPos.ToFloatArray()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.Mode.ToSKShaderTileMode()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.LocalMatrix.Value.ToSKMatrix()});");
                            return;
                        }
                        else
                        {
                            sb.Append($"{indent}var {counter.ShaderVarName}{counterShader} = ");
                            sb.AppendLine($"SKShader.CreateTwoPointConicalGradient(");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.Start.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.StartRadius.ToString(_ci)}f,");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.End.ToSKPoint()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.EndRadius.ToString(_ci)}f,");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.Colors.ToSKColors()},");
                            sb.AppendLine($"{indent}    {twoPointConicalGradientShader.ColorPos.ToFloatArray()},");
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

                        sb.Append($"{indent}var {counter.ShaderVarName}{counterShader} = ");
                        sb.AppendLine($"SKShader.CreatePicture(");
                        sb.AppendLine($"{indent}    {counter.PictureVarName}{counterPicture},");
                        sb.AppendLine($"{indent}    SKShaderTileMode.Repeat,");
                        sb.AppendLine($"{indent}    SKShaderTileMode.Repeat,");
                        sb.AppendLine($"{indent}    {pictureShader.LocalMatrix.ToSKMatrix()},");
                        sb.AppendLine($"{indent}    {pictureShader.Tile.ToSKRect()});");
                        sb.AppendLine($"{indent}{counter.PictureVarName}{counterPicture}?.Dispose();");
                        return;
                    }
                case SP.PerlinNoiseFractalNoiseShader perlinNoiseFractalNoiseShader:
                    {
                        sb.Append($"{indent}var {counter.ShaderVarName}{counterShader} = ");
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
                        sb.Append($"{indent}var {counter.ShaderVarName}{counterShader} = ");
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

        public static void ToSKColorFilter(this SP.ColorFilter? colorFilter, SkiaCodeGenObjectCounter counter, StringBuilder sb, string indent)
        {
            var counterColorFilter = counter.ColorFilter;

            switch (colorFilter)
            {
                case SP.BlendModeColorFilter blendModeColorFilter:
                    {
                        sb.Append($"{indent}var {counter.ColorFilterVarName}{counterColorFilter} = ");
                        sb.AppendLine($"SKColorFilter.CreateBlendMode(");
                        sb.AppendLine($"{indent}    {blendModeColorFilter.Color.ToSKColor()},");
                        sb.AppendLine($"{indent}    {blendModeColorFilter.Mode.ToSKBlendMode()});");
                        return;
                    }
                case SP.ColorMatrixColorFilter colorMatrixColorFilter:
                    {
                        if (colorMatrixColorFilter.Matrix == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ColorFilterVarName}{counterColorFilter} = default(SKColorFilter);");
                            return;
                        }

                        sb.Append($"{indent}var {counter.ColorFilterVarName}{counterColorFilter} = ");
                        sb.AppendLine($"SKColorFilter.CreateColorMatrix(");
                        sb.AppendLine($"{indent}    {colorMatrixColorFilter.Matrix.ToFloatArray()});");
                        return;
                    }
                case SP.LumaColorColorFilter _:
                    {
                        sb.Append($"{indent}var {counter.ColorFilterVarName}{counterColorFilter} = ");
                        sb.AppendLine($"SKColorFilter.CreateLumaColor();");
                        return;
                    }
                case SP.TableColorFilter tableColorFilter:
                    {
                        if (tableColorFilter.TableA == null
                            || tableColorFilter.TableR == null
                            || tableColorFilter.TableG == null
                            || tableColorFilter.TableB == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ColorFilterVarName}{counterColorFilter} = default(SKColorFilter);");
                            return;
                        }

                        sb.Append($"{indent}var {counter.ColorFilterVarName}{counterColorFilter} = ");
                        sb.AppendLine($"SKColorFilter.CreateTable(");
                        sb.AppendLine($"{indent}    {tableColorFilter.TableA.ToByteArray()},");
                        sb.AppendLine($"{indent}    {tableColorFilter.TableR.ToByteArray()},");
                        sb.AppendLine($"{indent}    {tableColorFilter.TableG.ToByteArray()},");
                        sb.AppendLine($"{indent}    {tableColorFilter.TableB.ToByteArray()});");
                        return;
                    }
                default:
                    {
                        sb.AppendLine($"{indent}var {counter.ColorFilterVarName}{counterColorFilter} = default(SKColorFilter);");
                        return;
                    }
            }
        }

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

        public static void ToSKImageFilter(this SP.ImageFilter? imageFilter, SkiaCodeGenObjectCounter counter, StringBuilder sb, string indent)
        {
            var counterImageFilter = counter.ImageFilter;

            switch (imageFilter)
            {
                case SP.ArithmeticImageFilter arithmeticImageFilter:
                    {
                        if (arithmeticImageFilter.Background == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = default(SKImageFilter);");
                            return;
                        }

                        var counterImageFilterBackground = ++counter.ImageFilter;
                        if (arithmeticImageFilter.Background == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterBackground} = default(SKImageFilter);");
                        }
                        else
                        {
                            arithmeticImageFilter.Background?.ToSKImageFilter(counter, sb, indent);
                        }

                        var counterImageFilterForeground = ++counter.ImageFilter;
                        if (arithmeticImageFilter.Foreground == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterBackground} = default(SKImageFilter);");
                        }
                        else
                        {
                            arithmeticImageFilter.Foreground?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateArithmetic(");
                        sb.AppendLine($"{indent}    {arithmeticImageFilter.K1.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {arithmeticImageFilter.K2.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {arithmeticImageFilter.K3.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {arithmeticImageFilter.K4.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {arithmeticImageFilter.EforcePMColor.ToString(_ci).ToLower()},");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterBackground},");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterForeground},");
                        sb.AppendLine($"{indent}    {arithmeticImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.BlendModeImageFilter blendModeImageFilter:
                    {
                        if (blendModeImageFilter.Background == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = default(SKImageFilter);");
                            return;
                        }

                        var counterImageFilterBackground = ++counter.ImageFilter;
                        if (blendModeImageFilter.Background == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterBackground} = default(SKImageFilter);");
                        }
                        else
                        {
                            blendModeImageFilter.Background?.ToSKImageFilter(counter, sb, indent);
                        }

                        var counterImageFilterForeground = ++counter.ImageFilter;
                        if (blendModeImageFilter.Foreground == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterBackground} = default(SKImageFilter);");
                        }
                        else
                        {
                            blendModeImageFilter.Foreground?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateBlendMode(");
                        sb.AppendLine($"{indent}    {blendModeImageFilter.Mode.ToSKBlendMode()},");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterBackground},");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterForeground},");
                        sb.AppendLine($"{indent}    {blendModeImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.BlurImageFilter blurImageFilter:
                    {
                        var counterImageFilterInput = ++counter.ImageFilter;
                        if (blurImageFilter.Input == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterInput} = default(SKImageFilter);");
                        }
                        else
                        {
                            blurImageFilter.Input?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateBlur(");
                        sb.AppendLine($"{indent}    {blurImageFilter.SigmaX.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {blurImageFilter.SigmaY.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterInput},");
                        sb.AppendLine($"{indent}    {blurImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.ColorFilterImageFilter colorFilterImageFilter:
                    {
                        if (colorFilterImageFilter.ColorFilter == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = default(SKImageFilter);");
                            return;
                        }

                        var counterColorFilter = ++counter.ColorFilter;
                        colorFilterImageFilter.ColorFilter?.ToSKColorFilter(counter, sb, indent);

                        var counterImageFilterInput = ++counter.ImageFilter;
                        if (colorFilterImageFilter.Input == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterInput} = default(SKImageFilter);");
                        }
                        else
                        {
                            colorFilterImageFilter.Input?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateColorFilter(");
                        sb.AppendLine($"{indent}    {counter.ColorFilterVarName}{counterColorFilter},");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterInput},");
                        sb.AppendLine($"{indent}    {colorFilterImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.DilateImageFilter dilateImageFilter:
                    {
                        var counterImageFilterInput = ++counter.ImageFilter;
                        if (dilateImageFilter.Input == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterInput} = default(SKImageFilter);");
                        }
                        else
                        {
                            dilateImageFilter.Input?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateDilate(");
                        sb.AppendLine($"{indent}    {dilateImageFilter.RadiusX.ToString(_ci)},");
                        sb.AppendLine($"{indent}    {dilateImageFilter.RadiusY.ToString(_ci)},");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterInput},");
                        sb.AppendLine($"{indent}    {dilateImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.DisplacementMapEffectImageFilter displacementMapEffectImageFilter:
                    {
                        if (displacementMapEffectImageFilter.Displacement == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = default(SKImageFilter);");
                            return;
                        }

                        var counterImageFilterDisplacement = ++counter.ImageFilter;
                        displacementMapEffectImageFilter.Displacement?.ToSKImageFilter(counter, sb, indent);

                        var counterImageFilterInput = ++counter.ImageFilter;
                        if (displacementMapEffectImageFilter.Input == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterInput} = default(SKImageFilter);");
                        }
                        else
                        {
                            displacementMapEffectImageFilter.Input?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateDisplacementMapEffect(");
                        sb.AppendLine($"{indent}    {displacementMapEffectImageFilter.XChannelSelector.ToSKColorChannel()},");
                        sb.AppendLine($"{indent}    {displacementMapEffectImageFilter.YChannelSelector.ToSKColorChannel()},");
                        sb.AppendLine($"{indent}    {displacementMapEffectImageFilter.Scale.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterDisplacement},");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterInput},");
                        sb.AppendLine($"{indent}    {displacementMapEffectImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.DistantLitDiffuseImageFilter distantLitDiffuseImageFilter:
                    {
                        var counterImageFilterInput = ++counter.ImageFilter;
                        if (distantLitDiffuseImageFilter.Input == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterInput} = default(SKImageFilter);");
                        }
                        else
                        {
                            distantLitDiffuseImageFilter.Input?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateDistantLitDiffuse(");
                        sb.AppendLine($"{indent}    {distantLitDiffuseImageFilter.Direction.ToSKPoint3()},");
                        sb.AppendLine($"{indent}    {distantLitDiffuseImageFilter.LightColor.ToSKColor()},");
                        sb.AppendLine($"{indent}    {distantLitDiffuseImageFilter.SurfaceScale.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {distantLitDiffuseImageFilter.Kd.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterInput},");
                        sb.AppendLine($"{indent}    {distantLitDiffuseImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.DistantLitSpecularImageFilter distantLitSpecularImageFilter:
                    {
                        var counterImageFilterInput = ++counter.ImageFilter;
                        if (distantLitSpecularImageFilter.Input == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterInput} = default(SKImageFilter);");
                        }
                        else
                        {
                            distantLitSpecularImageFilter.Input?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateDistantLitSpecular(");
                        sb.AppendLine($"{indent}    {distantLitSpecularImageFilter.Direction.ToSKPoint3()},");
                        sb.AppendLine($"{indent}    {distantLitSpecularImageFilter.LightColor.ToSKColor()},");
                        sb.AppendLine($"{indent}    {distantLitSpecularImageFilter.SurfaceScale.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {distantLitSpecularImageFilter.Ks.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {distantLitSpecularImageFilter.Shininess.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterInput},");
                        sb.AppendLine($"{indent}    {distantLitSpecularImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.ErodeImageFilter erodeImageFilter:
                    {
                        var counterImageFilterInput = ++counter.ImageFilter;
                        if (erodeImageFilter.Input == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterInput} = default(SKImageFilter);");
                        }
                        else
                        {
                            erodeImageFilter.Input?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateErode(");
                        sb.AppendLine($"{indent}    {erodeImageFilter.RadiusX.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {erodeImageFilter.RadiusY.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterInput},");
                        sb.AppendLine($"{indent}    {erodeImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.ImageImageFilter imageImageFilter:
                    {
                        if (imageImageFilter.Image == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = default(SKImageFilter);");
                            return;
                        }

                        var counterImage = ++counter.Image;
                        imageImageFilter.Image?.ToSKImage(counter, sb, indent);

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateImage(");
                        sb.AppendLine($"{indent}    {counter.ImageVarName}{counterImage},");
                        sb.AppendLine($"{indent}    {imageImageFilter.Src.ToSKRect()},");
                        sb.AppendLine($"{indent}    {imageImageFilter.Dst.ToSKRect()},");
                        sb.AppendLine($"{indent}    SKFilterQuality.High);");
                        return;
                    }
                case SP.MatrixConvolutionImageFilter matrixConvolutionImageFilter:
                    {
                        if (matrixConvolutionImageFilter.Kernel == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = default(SKImageFilter);");
                            return;
                        }

                        var counterImageFilterInput = ++counter.ImageFilter;
                        if (matrixConvolutionImageFilter.Input == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterInput} = default(SKImageFilter);");
                        }
                        else
                        {
                            matrixConvolutionImageFilter.Input?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateMatrixConvolution(");
                        sb.AppendLine($"{indent}    {matrixConvolutionImageFilter.KernelSize.ToSKSizeI()},");
                        sb.AppendLine($"{indent}    {matrixConvolutionImageFilter.Kernel.ToFloatArray()},");
                        sb.AppendLine($"{indent}    {matrixConvolutionImageFilter.Gain.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {matrixConvolutionImageFilter.Bias.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {matrixConvolutionImageFilter.KernelOffset.ToSKPointI()},");
                        sb.AppendLine($"{indent}    {matrixConvolutionImageFilter.TileMode.ToSKShaderTileMode()},");
                        sb.AppendLine($"{indent}    {matrixConvolutionImageFilter.ConvolveAlpha.ToString(_ci).ToLower()},");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterInput},");
                        sb.AppendLine($"{indent}    {matrixConvolutionImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.MergeImageFilter mergeImageFilter:
                    {
                        if (mergeImageFilter.Filters == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = default(SKImageFilter);");
                            return;
                        }

                        var imageFilters = mergeImageFilter.Filters;

                        sb.AppendLine($"{indent}var {counter.ImageFilterVarName}s{counterImageFilter} = new SKImageFilter[{imageFilters.Length}]");

                        for (int i = 0; i < imageFilters.Length; i++)
                        {
                            var imageFilterItem = imageFilters[i];
                            var counterImageFilterItem = ++counter.ImageFilter;
                            if (imageFilterItem == null)
                            {
                                sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterItem} = default(SKImageFilter);");
                            }
                            else
                            {
                                imageFilterItem.ToSKImageFilter(counter, sb, indent);
                            }
                            sb.AppendLine($"{indent}{counter.ImageFilterVarName}s{counterImageFilter}[{i}] = {counter.ImageFilterVarName}{counterImageFilterItem};");
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateMerge(");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}s{counterImageFilter},");
                        sb.AppendLine($"{indent}    {mergeImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.OffsetImageFilter offsetImageFilter:
                    {
                        var counterImageFilterInput = ++counter.ImageFilter;
                        if (offsetImageFilter.Input == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterInput} = default(SKImageFilter);");
                        }
                        else
                        {
                            offsetImageFilter.Input?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateOffset(");
                        sb.AppendLine($"{indent}    {offsetImageFilter.Dx.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {offsetImageFilter.Dy.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterInput},");
                        sb.AppendLine($"{indent}    {offsetImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.PaintImageFilter paintImageFilter:
                    {
                        if (paintImageFilter.Paint == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = default(SKImageFilter);");
                            return;
                        }

                        var counterPaint = ++counter.Paint;
                        paintImageFilter.Paint.ToSKPaint(counter, sb, indent);

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreatePaint(");
                        sb.AppendLine($"{indent}    {counter.PaintVarName}{counterPaint},");
                        sb.AppendLine($"{indent}    {paintImageFilter.CropRect?.ToCropRect()});");

                        // NOTE: Do not dispose created SKTypeface by font manager.
                        //if (saveLayerCanvasCommand.Paint.Typeface != null)
                        //{
                        //    sb.AppendLine($"{indent}if ({counter.PaintVarName}{counterPaint}.Typeface != SKTypeface.Default)");
                        //    sb.AppendLine($"{indent}{{");
                        //    sb.AppendLine($"{indent}    {counter.PaintVarName}{counterPaint}.Typeface?.Dispose();"); ;
                        //    sb.AppendLine($"{indent}}}");
                        //}
                        if (paintImageFilter.Paint.Shader != null)
                        {
                            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Shader?.Dispose();");
                        }
                        if (paintImageFilter.Paint.ColorFilter != null)
                        {
                            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ColorFilter.Dispose()");
                        }
                        if (paintImageFilter.Paint.ImageFilter != null)
                        {
                            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ImageFilter?.Dispose();");
                        }
                        if (paintImageFilter.Paint.PathEffect != null)
                        {
                            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.PathEffect?.Dispose();");
                        }
#if !USE_PAINT_RESET
                        sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}?.Dispose();");
#endif
                        return;
                    }
                case SP.PictureImageFilter pictureImageFilter:
                    {
                        if (pictureImageFilter.Picture == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = default(SKImageFilter);");
                            return;
                        }

                        var counterPicture = ++counter.Picture;
                        pictureImageFilter.Picture.ToSKPicture(counter, sb, indent);

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreatePicture(");
                        sb.AppendLine($"{indent}    {counter.PictureVarName}{counterPicture},");
                        sb.AppendLine($"{indent}    {pictureImageFilter.Picture.CullRect.ToSKRect()});");
                        return;
                    }
                case SP.PointLitDiffuseImageFilter pointLitDiffuseImageFilter:
                    {
                        var counterImageFilterInput = ++counter.ImageFilter;
                        if (pointLitDiffuseImageFilter.Input == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterInput} = default(SKImageFilter);");
                        }
                        else
                        {
                            pointLitDiffuseImageFilter.Input?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreatePointLitDiffuse(");
                        sb.AppendLine($"{indent}    {pointLitDiffuseImageFilter.Location.ToSKPoint3()},");
                        sb.AppendLine($"{indent}    {pointLitDiffuseImageFilter.LightColor.ToSKColor()},");
                        sb.AppendLine($"{indent}    {pointLitDiffuseImageFilter.SurfaceScale.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {pointLitDiffuseImageFilter.Kd.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterInput},");
                        sb.AppendLine($"{indent}    {pointLitDiffuseImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.PointLitSpecularImageFilter pointLitSpecularImageFilter:
                    {
                        var counterImageFilterInput = ++counter.ImageFilter;
                        if (pointLitSpecularImageFilter.Input == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterInput} = default(SKImageFilter);");
                        }
                        else
                        {
                            pointLitSpecularImageFilter.Input?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreatePointLitSpecular(");
                        sb.AppendLine($"{indent}    {pointLitSpecularImageFilter.Location.ToSKPoint3()},");
                        sb.AppendLine($"{indent}    {pointLitSpecularImageFilter.LightColor.ToSKColor()},");
                        sb.AppendLine($"{indent}    {pointLitSpecularImageFilter.SurfaceScale.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {pointLitSpecularImageFilter.Ks.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {pointLitSpecularImageFilter.Shininess.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterInput},");
                        sb.AppendLine($"{indent}    {pointLitSpecularImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.SpotLitDiffuseImageFilter spotLitDiffuseImageFilter:
                    {
                        var counterImageFilterInput = ++counter.ImageFilter;
                        if (spotLitDiffuseImageFilter.Input == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterInput} = default(SKImageFilter);");
                        }
                        else
                        {
                            spotLitDiffuseImageFilter.Input?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateSpotLitDiffuse(");
                        sb.AppendLine($"{indent}    {spotLitDiffuseImageFilter.Location.ToSKPoint3()},");
                        sb.AppendLine($"{indent}    {spotLitDiffuseImageFilter.Target.ToSKPoint3()},");
                        sb.AppendLine($"{indent}    {spotLitDiffuseImageFilter.SpecularExponent.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {spotLitDiffuseImageFilter.CutoffAngle.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {spotLitDiffuseImageFilter.LightColor.ToSKColor()},");
                        sb.AppendLine($"{indent}    {spotLitDiffuseImageFilter.SurfaceScale.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {spotLitDiffuseImageFilter.Kd.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterInput},");
                        sb.AppendLine($"{indent}    {spotLitDiffuseImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.SpotLitSpecularImageFilter spotLitSpecularImageFilter:
                    {
                        var counterImageFilterInput = ++counter.ImageFilter;
                        if (spotLitSpecularImageFilter.Input == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterInput} = default(SKImageFilter);");
                        }
                        else
                        {
                            spotLitSpecularImageFilter.Input?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateSpotLitSpecular(");
                        sb.AppendLine($"{indent}    {spotLitSpecularImageFilter.Location.ToSKPoint3()},");
                        sb.AppendLine($"{indent}    {spotLitSpecularImageFilter.Target.ToSKPoint3()},");
                        sb.AppendLine($"{indent}    {spotLitSpecularImageFilter.SpecularExponent.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {spotLitSpecularImageFilter.CutoffAngle.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {spotLitSpecularImageFilter.LightColor.ToSKColor()},");
                        sb.AppendLine($"{indent}    {spotLitSpecularImageFilter.SurfaceScale.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {spotLitSpecularImageFilter.Ks.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {spotLitSpecularImageFilter.SpecularExponent.ToString(_ci)}f,");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterInput},");
                        sb.AppendLine($"{indent}    {spotLitSpecularImageFilter.CropRect?.ToCropRect()});");
                        return;
                    }
                case SP.TileImageFilter tileImageFilter:
                    {
                        var counterImageFilterInput = ++counter.ImageFilter;
                        if (tileImageFilter.Input == null)
                        {
                            sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilterInput} = default(SKImageFilter);");
                        }
                        else
                        {
                            tileImageFilter.Input?.ToSKImageFilter(counter, sb, indent);
                        }

                        sb.Append($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = ");
                        sb.AppendLine($"SKImageFilter.CreateTile(");
                        sb.AppendLine($"{indent}    {tileImageFilter.Src.ToSKRect()},");
                        sb.AppendLine($"{indent}    {tileImageFilter.Dst.ToSKRect()},");
                        sb.AppendLine($"{indent}    {counter.ImageFilterVarName}{counterImageFilterInput});");
                        return;
                    }
                default:
                    {
                        sb.AppendLine($"{indent}var {counter.ImageFilterVarName}{counterImageFilter} = default(SKImageFilter);");
                        return;
                    }
            }
        }

        public static void ToSKPathEffect(this SP.PathEffect? pathEffect, SkiaCodeGenObjectCounter counter, StringBuilder sb, string indent)
        {
            var counterPathEffect = counter.PathEffect;

            switch (pathEffect)
            {
                case SP.DashPathEffect dashPathEffect:
                    {
                        if (dashPathEffect.Intervals == null)
                        {
                            sb.AppendLine($"{indent}var {counter.PathEffectVarName}{counterPathEffect} = default(SKPathEffect);");
                            return;
                        }

                        sb.Append($"{indent}var {counter.PathEffectVarName}{counterPathEffect} = ");
                        sb.AppendLine($"SKPathEffect.CreateDash(");
                        sb.AppendLine($"{indent}    {dashPathEffect.Intervals.ToFloatArray()},");
                        sb.AppendLine($"{indent}    {dashPathEffect.Phase.ToString(_ci)}f);");
                        return;
                    }
                default:
                    {
                        sb.AppendLine($"{indent}var {counter.PathEffectVarName}{counterPathEffect} = default(SKPathEffect);");
                        return;
                    }
            }
        }

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

#if USE_PAINT_RESET
            sb.AppendLine($"{indent}var {counter.PaintVarName}{counterPaint} = {counter.PaintVarName};");
            sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Reset();");
#else
            sb.AppendLine($"{indent}var {counter.PaintVarName}{counterPaint} = new SKPaint();");
#endif

            // SKPaint defaults:
            // Style=Fill
            // IsAntialias=false
            // StrokeWidth=0
            // StrokeCap=Butt
            // StrokeJoin=Miter
            // StrokeMiter=4
            // TextSize=12
            // TextAlign=Left
            // LcdRenderText=false
            // SubpixelText=false
            // TextEncoding=Utf8
            // Color=#ff000000
            // BlendMode=SrcOver
            // FilterQuality=None

            if (paint.Style != SP.PaintStyle.Fill)
            {
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Style = {paint.Style.ToSKPaintStyle()};");
            }

            if (paint.IsAntialias != false)
            {
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.IsAntialias = {paint.IsAntialias.ToString(_ci).ToLower()};");
            }

            if (paint.StrokeWidth != 0f)
            {
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.StrokeWidth = {paint.StrokeWidth.ToString(_ci)}f;");
            }

            if (paint.StrokeCap != SP.StrokeCap.Butt)
            {
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.StrokeCap = {paint.StrokeCap.ToSKStrokeCap()};");
            }

            if (paint.StrokeJoin != SP.StrokeJoin.Miter)
            {
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.StrokeJoin = {paint.StrokeJoin.ToSKStrokeJoin()};");
            }

            if (paint.StrokeMiter != 4f)
            {
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.StrokeMiter = {paint.StrokeMiter.ToString(_ci)}f;");
            }

            if (paint.TextSize != 12f)
            {
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.TextSize = {paint.TextSize.ToString(_ci)}f;");
            }

            if (paint.TextAlign != SP.TextAlign.Left)
            {
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.TextAlign = {paint.TextAlign.ToSKTextAlign()};");
            }

            if (paint.Typeface != null)
            {
                var counterTypeface = ++counter.Typeface;
                paint.Typeface?.ToSKTypeface(counter, sb, indent);
                sb.AppendLine($"{indent}if ({counter.TypefaceVarName}{counterTypeface} == null)");
                sb.AppendLine($"{indent}{{");
                sb.AppendLine($"{indent}    {counter.TypefaceVarName}{counterTypeface} = SKTypeface.Default;");
                sb.AppendLine($"{indent}}}");
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Typeface = {counter.TypefaceVarName}{counterTypeface};");
            }

            if (paint.LcdRenderText != false)
            {
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.LcdRenderText = {paint.LcdRenderText.ToString(_ci).ToLower()};");
            }

            if (paint.SubpixelText != false)
            {
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.SubpixelText = {paint.SubpixelText.ToString(_ci).ToLower()};");
            }

            if (paint.TextEncoding != SP.TextEncoding.Utf8)
            {
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.TextEncoding = {paint.TextEncoding.ToSKTextEncoding()};");
            }

            if (paint.Color != null && paint.Color.Value.Alpha != 255 && paint.Color.Value.Red != 0 && paint.Color.Value.Green != 0 && paint.Color.Value.Blue != 0)
            {
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Color = {(paint.Color == null ? "SKColor.Empty" : ToSKColor(paint.Color.Value))};");
            }

            if (paint.Shader != null)
            {
                var counterShader = ++counter.Shader;
                paint.Shader.ToSKShader(counter, sb, indent);
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Shader = {counter.ShaderVarName}{counterShader};");
            }

            if (paint.ColorFilter != null)
            {
                var counterColorFilter = ++counter.ColorFilter;
                paint.ColorFilter.ToSKColorFilter(counter, sb, indent);
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ColorFilter = {counter.ColorFilterVarName}{counterColorFilter};");
            }

            if (paint.ImageFilter != null)
            {
                var counterImageFilter = ++counter.ImageFilter;
                paint.ImageFilter.ToSKImageFilter(counter, sb, indent);
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ImageFilter = {counter.ImageFilterVarName}{counterImageFilter};");
            }

            if (paint.PathEffect != null)
            {
                var counterPathEffect = ++counter.PathEffect;
                paint.PathEffect.ToSKPathEffect(counter, sb, indent);
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.PathEffect = {counter.PathEffectVarName}{counterPathEffect};");
            }

            if (paint.BlendMode != SP.BlendMode.SrcOver)
            {
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.BlendMode = {paint.BlendMode.ToSKBlendMode()};");
            }

            if (paint.FilterQuality != SP.FilterQuality.None)
            {
                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.FilterQuality = {paint.FilterQuality.ToSKFilterQuality()};");
            }
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
            var counterPath = counter.Path;

#if USE_PATH_RESET
            sb.AppendLine($"{indent}var {counter.PathVarName}{counterPath} = {counter.PathVarName};");
            sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}.Reset();");
            if (path.FillType != SP.PathFillType.Winding)
            {
                sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}.FillType = {path.FillType.ToSKPathFillType()};");
            }
#else
            sb.AppendLine($"{indent}var {counter.PathVarName}{counterPath} = new SKPath();");
            if (path.FillType != SP.PathFillType.Winding)
            {
                sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}.FillType = {path.FillType.ToSKPathFillType()};");
            }
#endif

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
                            sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}.MoveTo({x.ToString(_ci)}f, {y.ToString(_ci)}f);");
                        }
                        break;
                    case SP.LineToPathCommand lineToPathCommand:
                        {
                            var x = lineToPathCommand.X;
                            var y = lineToPathCommand.Y;
                            sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}.LineTo({x.ToString(_ci)}f, {y.ToString(_ci)}f);");
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
                            sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}.ArcTo({rx.ToString(_ci)}f, {ry.ToString(_ci)}f, {xAxisRotate.ToString(_ci)}f, {largeArc}, {sweep}, {x.ToString(_ci)}f, {y.ToString(_ci)}f);");
                        }
                        break;
                    case SP.QuadToPathCommand quadToPathCommand:
                        {
                            var x0 = quadToPathCommand.X0;
                            var y0 = quadToPathCommand.Y0;
                            var x1 = quadToPathCommand.X1;
                            var y1 = quadToPathCommand.Y1;
                            sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}.QuadTo({x0.ToString(_ci)}f, {y0.ToString(_ci)}f, {x1.ToString(_ci)}f, {y1.ToString(_ci)}f);");
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
                            sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}.CubicTo({x0.ToString(_ci)}f, {y0.ToString(_ci)}f, {x1.ToString(_ci)}f, {y1.ToString(_ci)}f, {x2.ToString(_ci)}f, {y2.ToString(_ci)}f);");
                        }
                        break;
                    case SP.ClosePathCommand _:
                        {
                            sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}.Close();");
                        }
                        break;
                    case SP.AddRectPathCommand addRectPathCommand:
                        {
                            var rect = addRectPathCommand.Rect.ToSKRect();
                            sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}.AddRect({rect});");
                        }
                        break;
                    case SP.AddRoundRectPathCommand addRoundRectPathCommand:
                        {
                            var rect = addRoundRectPathCommand.Rect.ToSKRect();
                            var rx = addRoundRectPathCommand.Rx;
                            var ry = addRoundRectPathCommand.Ry;
                            sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}.AddRoundRect({rect}, {rx.ToString(_ci)}f, {ry.ToString(_ci)}f);");
                        }
                        break;
                    case SP.AddOvalPathCommand addOvalPathCommand:
                        {
                            var rect = addOvalPathCommand.Rect.ToSKRect();
                            sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}.AddOval({rect});");
                        }
                        break;
                    case SP.AddCirclePathCommand addCirclePathCommand:
                        {
                            var x = addCirclePathCommand.X;
                            var y = addCirclePathCommand.Y;
                            var radius = addCirclePathCommand.Radius;
                            sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}.AddCircle({x.ToString(_ci)}f, {y.ToString(_ci)}f, {radius.ToString(_ci)}f);");
                        }
                        break;
                    case SP.AddPolyPathCommand addPolyPathCommand:
                        {
                            if (addPolyPathCommand.Points != null)
                            {
                                var points = addPolyPathCommand.Points.ToSKPoints();
                                var close = addPolyPathCommand.Close.ToString(_ci).ToLower();
                                sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}.AddPoly(points, {close});");
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        // TODO: ToSKPath (ClipPath)

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

            sb.AppendLine($"{indent}var {counter.PictureRecorderVarName}{counterPictureRecorder} = new SKPictureRecorder();");
            sb.AppendLine($"{indent}var {counter.CanvasVarName}{counterCanvas} = {counter.PictureRecorderVarName}{counterPictureRecorder}.BeginRecording({picture.CullRect.ToSKRect()});");

            if (picture.Commands == null)
            {
                sb.AppendLine($"{indent}var {counter.PictureVarName}{counterPicture} = {counter.PictureRecorderVarName}{counterPictureRecorder}.EndRecording();");
                sb.AppendLine($"{indent}{counter.PictureRecorderVarName}{counterPictureRecorder}?.Dispose();");
                sb.AppendLine($"{indent}{counter.CanvasVarName}{counterCanvas}?.Dispose();");
                return;
            }

            foreach (var canvasCommand in picture.Commands)
            {
                switch (canvasCommand)
                {
                    case SP.ClipPathCanvasCommand clipPathCanvasCommand:
                        {
                            // TODO: ClipPath
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

                                // NOTE: Do not dispose created SKTypeface by font manager.
                                //if (saveLayerCanvasCommand.Paint.Typeface != null)
                                //{
                                //    sb.AppendLine($"{indent}if ({counter.PaintVarName}{counterPaint}.Typeface != SKTypeface.Default)");
                                //    sb.AppendLine($"{indent}{{");
                                //    sb.AppendLine($"{indent}    {counter.PaintVarName}{counterPaint}.Typeface?.Dispose();"); ;
                                //    sb.AppendLine($"{indent}}}");
                                //}
                                if (saveLayerCanvasCommand.Paint.Shader != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Shader?.Dispose();");
                                }
                                if (saveLayerCanvasCommand.Paint.ColorFilter != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ColorFilter.Dispose()");
                                }
                                if (saveLayerCanvasCommand.Paint.ImageFilter != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ImageFilter?.Dispose();");
                                }
                                if (saveLayerCanvasCommand.Paint.PathEffect != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.PathEffect?.Dispose();");
                                }
#if !USE_PAINT_RESET
                                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}?.Dispose();");
#endif
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
                                var counterImage = ++counter.Image;
                                drawImageCanvasCommand.Image.ToSKImage(counter, sb, indent);
                                var source = drawImageCanvasCommand.Source.ToSKRect();
                                var dest = drawImageCanvasCommand.Dest.ToSKRect();
                                var counterPaint = ++counter.Paint;
                                drawImageCanvasCommand.Paint?.ToSKPaint(counter, sb, indent);
                                sb.AppendLine($"{indent}{counter.CanvasVarName}{counterCanvas}.DrawImage({counter.ImageVarName}{counterImage}, {source}, {dest}, {counter.PaintVarName}{counterPaint});");

                                // TODO: Dispose SKImage

                                // NOTE: Do not dispose created SKTypeface by font manager.
                                //if (drawImageCanvasCommand.Paint?.Typeface != null)
                                //{
                                //    sb.AppendLine($"{indent}if ({counter.PaintVarName}{counterPaint}.Typeface != SKTypeface.Default)");
                                //    sb.AppendLine($"{indent}{{");
                                //    sb.AppendLine($"{indent}    {counter.PaintVarName}{counterPaint}.Typeface?.Dispose();"); ;
                                //    sb.AppendLine($"{indent}}}");
                                //}
                                if (drawImageCanvasCommand.Paint?.Shader != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Shader?.Dispose();");
                                }
                                if (drawImageCanvasCommand.Paint?.ColorFilter != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ColorFilter.Dispose()");
                                }
                                if (drawImageCanvasCommand.Paint?.ImageFilter != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ImageFilter?.Dispose();");
                                }
                                if (drawImageCanvasCommand.Paint?.PathEffect != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.PathEffect?.Dispose();");
                                }
#if !USE_PAINT_RESET
                                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}?.Dispose();");
#endif
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

                                // NOTE: Do not dispose created SKTypeface by font manager.
                                //if (drawPathCanvasCommand.Paint.Typeface != null)
                                //{
                                //    sb.AppendLine($"{indent}if ({counter.PaintVarName}{counterPaint}.Typeface != SKTypeface.Default)");
                                //    sb.AppendLine($"{indent}{{");
                                //    sb.AppendLine($"{indent}    {counter.PaintVarName}{counterPaint}.Typeface?.Dispose();"); ;
                                //    sb.AppendLine($"{indent}}}");
                                //}
                                if (drawPathCanvasCommand.Paint.Shader != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Shader?.Dispose();");
                                }
                                if (drawPathCanvasCommand.Paint.ColorFilter != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ColorFilter.Dispose()");
                                }
                                if (drawPathCanvasCommand.Paint.ImageFilter != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ImageFilter?.Dispose();");
                                }
                                if (drawPathCanvasCommand.Paint.PathEffect != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.PathEffect?.Dispose();");
                                }
#if !USE_PAINT_RESET
                                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}?.Dispose();");
#endif
#if !USE_PATH_RESET
                                sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}?.Dispose();");
#endif
                            }
                        }
                        break;
                    case SP.DrawTextBlobCanvasCommand drawPositionedTextCanvasCommand:
                        {
                            if (drawPositionedTextCanvasCommand.TextBlob != null && drawPositionedTextCanvasCommand.TextBlob.Points != null && drawPositionedTextCanvasCommand.Paint != null)
                            {
                                var text = EspaceString(drawPositionedTextCanvasCommand.TextBlob.Text);
                                var points = drawPositionedTextCanvasCommand.TextBlob.Points.ToSKPoints();
                                var counterPaint = ++counter.Paint;
                                drawPositionedTextCanvasCommand.Paint.ToSKPaint(counter, sb, indent);
                                var counterFont = ++counter.Font;
                                sb.AppendLine($"{indent}var {counter.FontVarName}{counterFont} = {counter.PaintVarName}{counterPaint}.ToFont();");
                                var counterTextBlob = ++counter.TextBlob;
                                sb.AppendLine($"{indent}var {counter.TextBlobVarName}{counterTextBlob} = SKTextBlob.CreatePositioned(\"{text}\", {counter.FontVarName}{counterFont}, {points});");
                                var x = drawPositionedTextCanvasCommand.X;
                                var y = drawPositionedTextCanvasCommand.Y;
                                sb.AppendLine($"{indent}{counter.CanvasVarName}{counterCanvas}.DrawText({counter.TextBlobVarName}{counterTextBlob}, {x.ToString(_ci)}f, {y.ToString(_ci)}f, {counter.PaintVarName}{counterPaint});");

                                // NOTE: Do not dispose created SKTypeface by font manager.
                                //if (drawPositionedTextCanvasCommand.Paint.Typeface != null)
                                //{
                                //    sb.AppendLine($"{indent}if ({counter.PaintVarName}{counterPaint}.Typeface != SKTypeface.Default)");
                                //    sb.AppendLine($"{indent}{{");
                                //    sb.AppendLine($"{indent}    {counter.PaintVarName}{counterPaint}.Typeface?.Dispose();"); ;
                                //    sb.AppendLine($"{indent}}}");
                                //}
                                if (drawPositionedTextCanvasCommand.Paint.Shader != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Shader?.Dispose();");
                                }
                                if (drawPositionedTextCanvasCommand.Paint.ColorFilter != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ColorFilter.Dispose()");
                                }
                                if (drawPositionedTextCanvasCommand.Paint.ImageFilter != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ImageFilter?.Dispose();");
                                }
                                if (drawPositionedTextCanvasCommand.Paint.PathEffect != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.PathEffect?.Dispose();");
                                }
#if !USE_PAINT_RESET
                                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}?.Dispose();");
#endif
                            }
                        }
                        break;
                    case SP.DrawTextCanvasCommand drawTextCanvasCommand:
                        {
                            if (drawTextCanvasCommand.Paint != null)
                            {
                                var text = EspaceString(drawTextCanvasCommand.Text);
                                var x = drawTextCanvasCommand.X;
                                var y = drawTextCanvasCommand.Y;
                                var counterPaint = ++counter.Paint;
                                drawTextCanvasCommand.Paint.ToSKPaint(counter, sb, indent);
                                sb.AppendLine($"{indent}{counter.CanvasVarName}{counterCanvas}.DrawText(\"{text}\", {x.ToString(_ci)}f, {y.ToString(_ci)}f, {counter.PaintVarName}{counterPaint});");

                                // NOTE: Do not dispose created SKTypeface by font manager.
                                //if (drawTextCanvasCommand.Paint.Typeface != null)
                                //{
                                //    sb.AppendLine($"{indent}if ({counter.PaintVarName}{counterPaint}.Typeface != SKTypeface.Default)");
                                //    sb.AppendLine($"{indent}{{");
                                //    sb.AppendLine($"{indent}    {counter.PaintVarName}{counterPaint}.Typeface?.Dispose();"); ;
                                //    sb.AppendLine($"{indent}}}");
                                //}
                                if (drawTextCanvasCommand.Paint.Shader != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Shader?.Dispose();");
                                }
                                if (drawTextCanvasCommand.Paint.ColorFilter != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ColorFilter.Dispose()");
                                }
                                if (drawTextCanvasCommand.Paint.ImageFilter != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ImageFilter?.Dispose();");
                                }
                                if (drawTextCanvasCommand.Paint.PathEffect != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.PathEffect?.Dispose();");
                                }
#if !USE_PAINT_RESET
                                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}?.Dispose();");
#endif
                            }
                        }
                        break;
                    case SP.DrawTextOnPathCanvasCommand drawTextOnPathCanvasCommand:
                        {
                            if (drawTextOnPathCanvasCommand.Path != null && drawTextOnPathCanvasCommand.Paint != null)
                            {
                                var text = EspaceString(drawTextOnPathCanvasCommand.Text);
                                var counterPath = ++counter.Path;
                                drawTextOnPathCanvasCommand.Path.ToSKPath(counter, sb, indent);
                                var hOffset = drawTextOnPathCanvasCommand.HOffset;
                                var vOffset = drawTextOnPathCanvasCommand.VOffset;
                                var counterPaint = ++counter.Paint;
                                drawTextOnPathCanvasCommand.Paint.ToSKPaint(counter, sb, indent);
                                sb.AppendLine($"{indent}{counter.CanvasVarName}{counterCanvas}.DrawTextOnPath(\"{text}\", {counter.PathVarName}{counterPath}, {hOffset.ToString(_ci)}f, {vOffset.ToString(_ci)}f, {counter.PaintVarName}{counterPaint});");

                                // NOTE: Do not dispose created SKTypeface by font manager.
                                //if (drawTextOnPathCanvasCommand.Paint.Typeface != null)
                                //{
                                //    sb.AppendLine($"{indent}if ({counter.PaintVarName}{counterPaint}.Typeface != SKTypeface.Default)");
                                //    sb.AppendLine($"{indent}{{");
                                //    sb.AppendLine($"{indent}    {counter.PaintVarName}{counterPaint}.Typeface?.Dispose();"); ;
                                //    sb.AppendLine($"{indent}}}");
                                //}
                                if (drawTextOnPathCanvasCommand.Paint.Shader != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.Shader?.Dispose();");
                                }
                                if (drawTextOnPathCanvasCommand.Paint.ColorFilter != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ColorFilter?.Dispose()");
                                }
                                if (drawTextOnPathCanvasCommand.Paint.ImageFilter != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.ImageFilter?.Dispose();");
                                }
                                if (drawTextOnPathCanvasCommand.Paint.PathEffect != null)
                                {
                                    sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}.PathEffect?.Dispose();");
                                }
#if !USE_PAINT_RESET
                                sb.AppendLine($"{indent}{counter.PaintVarName}{counterPaint}?.Dispose();");
#endif
#if !USE_PATH_RESET
                                sb.AppendLine($"{indent}{counter.PathVarName}{counterPath}?.Dispose();");
#endif
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            sb.AppendLine($"{indent}var {counter.PictureVarName}{counterPicture} = {counter.PictureRecorderVarName}{counterPictureRecorder}.EndRecording();");

            sb.AppendLine($"{indent}{counter.PictureRecorderVarName}{counterPictureRecorder}?.Dispose();");
            sb.AppendLine($"{indent}{counter.CanvasVarName}{counterCanvas}?.Dispose();");
        }
    }

    public class SkiaCodeGen
    {
        public static string Generate(SP.Picture picture, string namespaceName, string className)
        {
            var counter = new SkiaCodeGenObjectCounter();

            var sb = new StringBuilder();

            sb.AppendLine($"using System;");
#if USE_DIAGNOSTICTS
            sb.AppendLine($"using System.Diagnostics;");
#endif
            sb.AppendLine($"using SkiaSharp;");
            sb.AppendLine($"");
            sb.AppendLine($"namespace {namespaceName}");
            sb.AppendLine($"{{");
            sb.AppendLine($"    public class {className}");
            sb.AppendLine($"    {{");
            sb.AppendLine($"        public static SKPicture Picture {{ get; }}");
            sb.AppendLine($"");
            sb.AppendLine($"        static {className}()");
            sb.AppendLine($"        {{");
#if USE_DIAGNOSTICTS
            sb.AppendLine($"            var sw = new Stopwatch();");
            sb.AppendLine($"            sw.Start();");
#endif
            sb.AppendLine($"            Picture = Record();");
#if USE_DIAGNOSTICTS
            sb.AppendLine($"            sw.Stop();");
            sb.AppendLine($"            Console.WriteLine($\"{className}.Record() {{sw.Elapsed.TotalMilliseconds}}ms\");");
#endif
            sb.AppendLine($"        }}");
            sb.AppendLine($"");
            sb.AppendLine($"        private static SKPicture Record()");
            sb.AppendLine($"        {{");


            var indent = "            ";

#if USE_PAINT_RESET
            sb.AppendLine($"{indent}var {counter.PaintVarName} = new SKPaint();");
#endif

#if USE_PATH_RESET
            sb.AppendLine($"{indent}var {counter.PathVarName} = new SKPath();");
#endif

            var counterPicture = ++counter.Picture;
            picture.ToSKPicture(counter, sb, indent);

#if USE_PAINT_RESET
            sb.AppendLine($"{indent}{counter.PaintVarName}?.Dispose();");
#endif
#if USE_PATH_RESET
            sb.AppendLine($"{indent}{counter.PathVarName}?.Dispose();");
#endif

            sb.AppendLine($"{indent}return {counter.PictureVarName}{counterPicture};");

            sb.AppendLine($"        }}");
            sb.AppendLine($"");
            sb.AppendLine($"        public static void Draw(SKCanvas {counter.CanvasVarName})");
            sb.AppendLine($"        {{");
#if USE_DIAGNOSTICTS
            sb.AppendLine($"            var sw = new Stopwatch();");
            sb.AppendLine($"            sw.Start();");
#endif
            sb.AppendLine($"            {counter.CanvasVarName}.DrawPicture(Picture);");
#if USE_DIAGNOSTICTS
            sb.AppendLine($"            sw.Stop();");
            sb.AppendLine($"            Console.WriteLine($\"{className}.Draw() {{sw.Elapsed.TotalMilliseconds}}ms\");");
#endif
            sb.AppendLine($"        }}");
            sb.AppendLine($"    }}");
            sb.AppendLine($"}}");

            var code = sb.ToString();
            return code;
        }
    }
}
