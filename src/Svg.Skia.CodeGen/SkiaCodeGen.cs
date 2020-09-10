//#define USE_DIAGNOSTICTS
#define USE_PAINT_RESET
#define USE_PATH_RESET
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

            sb.AppendLine($"{indent}var disposables = new List<IDisposable>();");

#if USE_PAINT_RESET
            sb.AppendLine($"{indent}var {counter.PaintVarName} = new SKPaint();");
            sb.AppendLine($"{indent}disposables.Add({counter.PaintVarName});");
#endif

#if USE_PATH_RESET
            sb.AppendLine($"{indent}var {counter.PathVarName} = new SKPath();");
            sb.AppendLine($"{indent}disposables.Add({counter.PathVarName});");
#endif

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
