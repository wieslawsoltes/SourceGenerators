using System;
using Svg;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var ellipse = new e_ellipse_001();
            var rect = new e_rect_001();
            Console.WriteLine($"{ellipse.GetType()}");
            Console.WriteLine($"{rect.GetType()}");
        }
    }
}
