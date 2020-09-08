using System.Globalization;
using System.Text;
using SP = Svg.Picture;

namespace Svg
{
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
                            sb.AppendLine($"{indent}skCanvas.ClipRect({rect}, {operation}, {antialias});");
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
}
