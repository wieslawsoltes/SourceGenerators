using System;
using SkiaSharp;
using Svg;
using Svg.Skia;

namespace Test
{
    class Program
    {
        static void Debug(string path, string namespaceName, string className)
        {
            var svg = System.IO.File.ReadAllText(path);
            SvgDocument.SkipGdiPlusCapabilityCheck = true;
            SvgDocument.PointsPerInch = 96;
            var svgDocument = SvgDocument.FromSvg<SvgDocument>(svg);
            if (svgDocument != null)
            {
                var picture = SKSvg.ToModel(svgDocument);
                if (picture != null && picture.Commands != null)
                {
                    var text = SkiaCodeGen.Generate(picture, namespaceName, className);
                    Console.WriteLine(text);
                }
            }
        }

        static void Main(string[] args)
        {
            //Debug(@"c:\DOWNLOADS\GitHub\SourceGenerators\Test\Svg\__AJ_Digital_Camera.svg", "Svg", "AJ_Digital_Camera");
            //Debug(@"c:\DOWNLOADS\GitHub\SourceGenerators\Test\Svg\__tiger.svg", "Svg", "tiger");
            //Debug(@"c:\DOWNLOADS\GitHub\SourceGenerators\Test\Svg\e-ellipse-001.svg", "Svg", "e_ellipse_001");
            Debug("/home/ubuntu/projects/SourceGenerators/Test/Svg/__AJ_Digital_Camera.svg", "Svg", "AJ_Digital_Camera");
            Debug("/home/ubuntu/projects/SourceGenerators/Test/Svg/__tiger.svg", "Svg", "tiger");
            Debug("/home/ubuntu/projects/SourceGenerators/Test/Svg/e-ellipse-001.svg", "Svg", "e_ellipse_001");

            var ellipse = new e_ellipse_001();
            var rect = new e_rect_001();
            Console.WriteLine($"{ellipse.GetType()}");
            Console.WriteLine($"{rect.GetType()}");
        }
    }
}
