using System;
using Svg;

namespace Svg.Skia.SourceGenerator.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var ellipse = new e_ellipse_001();
            var rect = new e_rect_001();
            Console.WriteLine($"Generated class {ellipse.GetType()} from Svg file.");
            Console.WriteLine($"Generated class {rect.GetType()} from Svg file.");
        }
    }
}
