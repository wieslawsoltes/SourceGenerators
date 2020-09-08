using System;
using SkiaSharp;
using Svg;
using Svg.Skia;

namespace Test
{
    class Program
    {
        static void Debug()
        {
            var path = @"c:\DOWNLOADS\GitHub\SourceGenerators\Test\Svg\__tiger.svg";
            //var path = @"c:\DOWNLOADS\GitHub\SourceGenerators\Test\Svg\e-ellipse-001.svg";
            //var path = "/home/ubuntu/projects/SourceGenerators/Test/Svg/e-ellipse-001.svg";
            var svg = System.IO.File.ReadAllText(path);
            SvgDocument.SkipGdiPlusCapabilityCheck = true;
            SvgDocument.PointsPerInch = 96;
            var svgDocument = SvgDocument.FromSvg<SvgDocument>(svg);
            if (svgDocument != null)
            {
                var picture = SKSvg.ToModel(svgDocument);
                if (picture != null && picture.Commands != null)
                {
                    var text = SkiaCodeGenerator.Generate(picture, "e_ellipse_001");
                    Console.WriteLine(text);
                }
            }
        }

        static void Main(string[] args)
        {
            Debug();
            var ellipse = new e_ellipse_001();
            var rect = new e_rect_001();
            Console.WriteLine($"{ellipse.GetType()}");
            Console.WriteLine($"{rect.GetType()}");
        }
    }
}
