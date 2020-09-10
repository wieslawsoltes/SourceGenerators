//#define DIAGNOSTICTS
#nullable enable
using System.Text;
using SP = Svg.Picture;

namespace Svg.Skia
{
    public class SkiaCodeGen
    {
        public static string Generate(SP.Picture picture, string namespaceName, string className)
        {
            var counter = new SkiaCodeGenObjectCounter();

            var sb = new StringBuilder();

            sb.AppendLine($"using System;");
            sb.AppendLine($"using System.Collections.Generic;");
#if DIAGNOSTICTS
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
#if DIAGNOSTICTS
            sb.AppendLine($"            var sw = new Stopwatch();");
            sb.AppendLine($"            sw.Start();");
#endif
            sb.AppendLine($"            Picture = Record();");
#if DIAGNOSTICTS
            sb.AppendLine($"            sw.Stop();");
            sb.AppendLine($"            Console.WriteLine($\"{className}.Record() {{sw.Elapsed.TotalMilliseconds}}ms\");");
#endif
            sb.AppendLine($"        }}");
            sb.AppendLine($"");
            sb.AppendLine($"        private static SKPicture Record()");
            sb.AppendLine($"        {{");


            var indent = "            ";

            sb.AppendLine($"{indent}var disposables = new List<IDisposable>();");

            var counterPicture = ++counter.Picture;
            picture.ToSKPicture(counter, sb, indent);

            sb.AppendLine($"{indent}foreach (var disposable in disposables)");
            sb.AppendLine($"{indent}{{");
            sb.AppendLine($"{indent}    disposable?.Dispose();");
            sb.AppendLine($"{indent}}}");

            sb.AppendLine($"{indent}return {counter.PictureVarName}{counterPicture};");

            sb.AppendLine($"        }}");
            sb.AppendLine($"");
            sb.AppendLine($"        public static void Draw(SKCanvas {counter.CanvasVarName})");
            sb.AppendLine($"        {{");
#if DIAGNOSTICTS
            sb.AppendLine($"            var sw = new Stopwatch();");
            sb.AppendLine($"            sw.Start();");
#endif
            sb.AppendLine($"            {counter.CanvasVarName}.DrawPicture(Picture);");
#if DIAGNOSTICTS
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
