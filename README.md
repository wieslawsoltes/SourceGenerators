# Experimental Svg C# Source Generators

SVG to C# Compiler

### About

SVGC compiles SVG drawing markup to C# using SkiaSharp as rendering engine. SVGC can be also used as codegen for upcomming C# 9 Source Generator feature.

### NuGet

* https://www.nuget.org/packages/Svg.Skia.SourceGenerator
* https://www.nuget.org/packages/svgc
* https://www.nuget.org/packages/Svg.Skia.CodeGen

### Source Generator Usage

Add NuGet package reference to your `csproj`.

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.Net.Compilers.Toolset" Version="0.1.0-preview1" />
</ItemGroup>
```

Include `svg` assests file in your `csproj`.

```xml
<ItemGroup>
  <AdditionalFiles Include="Assets/Sample.svg" NamespaceName="Assets" ClassName="Sample" />
</ItemGroup>
```

Use generated `SKPicture` using static `Picture` property from `Sample` class.

```C#
using SkiaSharp;
using Assets.Sample;

public void Draw(SKCanvas canvas)
{
    canvas.DrawPicture(Sample.Picture);
}
```

### svgc Usage

```
svgc -i InputFile.svg -o OutputFile.cs -c ClassName -n NamespaceName
```

### Links

* https://github.com/dotnet/roslyn/blob/master/docs/features/source-generators.cookbook.md
* https://github.com/dotnet/roslyn/blob/master/docs/features/source-generators.md
* https://github.com/dotnet/roslyn-sdk/tree/master/samples/CSharp/SourceGenerators
* https://github.com/dotnet/roslyn/blob/master/src/Test/Utilities/Portable/SourceGeneration/TestGenerators.cs
* https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/
