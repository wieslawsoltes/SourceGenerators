# Experimental Svg.Skia Source Generators

SVG to C# Compiler

### About

SVGC compiles SVG drawing markup to C# using SkiaSharp as rendering engine. SGC can be also used as codegen for upcomming C# 9 Source Generator feature.

### Usage

```
svgc -i InputFile.svg -o OutputFile.cs -c ClassName -n NamespaceName
```

```
cd src\svgc
dotnet run -- -i ..\..\samples\Test\Svg\e-ellipse-001.svg  -o ellipse.cs -c ellipse -n Sample
```

### Links

* https://github.com/dotnet/roslyn/blob/master/docs/features/source-generators.cookbook.md
* https://github.com/dotnet/roslyn/blob/master/docs/features/source-generators.md
* https://github.com/dotnet/roslyn-sdk/tree/master/samples/CSharp/SourceGenerators
* https://github.com/dotnet/roslyn/blob/master/src/Test/Utilities/Portable/SourceGeneration/TestGenerators.cs
* https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/

