using System;
using System.Diagnostics;
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

namespace Svg
{
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
