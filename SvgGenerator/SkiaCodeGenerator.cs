using System.Globalization;
using System.Text;
using SP = Svg.Picture;

#nullable enable

namespace Svg
{
    public class SkiaCodeGen
    {
        private static CultureInfo _ci = CultureInfo.InvariantCulture;

        public static string Generate(SP.Picture picture, string namespaceName, string className)
        {
            var counter = new SkiaCodeGenObjectCounter();

            var sb = new StringBuilder();

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
            sb.AppendLine($"            Picture = Record();");
            sb.AppendLine($"        }}");
            sb.AppendLine($"");
            sb.AppendLine($"        private static SKPicture Record()");
            sb.AppendLine($"        {{");

            var indent = "            ";
            var counterPicture = ++counter.Picture;
            picture.ToSKPicture(counter, sb, indent);
            sb.AppendLine($"{indent}return {counter.PictureVarName}{counterPicture};");

            sb.AppendLine($"        }}");
            sb.AppendLine($"");
            sb.AppendLine($"        public static void Draw(SKCanvas {counter.CanvasVarName})");
            sb.AppendLine($"        {{");
            sb.AppendLine($"            {counter.CanvasVarName}.DrawPicture(Picture);");
            sb.AppendLine($"        }}");
            sb.AppendLine($"    }}");
            sb.AppendLine($"}}");

            var code = sb.ToString();
            return code;
        }
    }
}
