#!/usr/bin/env bash

mkdir temp
cd ./temp

git clone https://github.com/wieslawsoltes/SVG.git

dotnet publish ../src/svgc/svgc.csproj -c Release -f netcoreapp3.1 -r win7-x64 /p:PublishTrimmed=True /p:PublishSingleFile=False /p:PublishReadyToRun=True -o ./svgc

mkdir W3CTestSuite

./svgc/svgc -j ../W3CTestSuite.json

dotnet new console -n W3CTestSuite -o W3CTestSuite
dotnet add ./W3CTestSuite/W3CTestSuite.csproj package -v 2.80.2 SkiaSharp
dotnet add ./W3CTestSuite/W3CTestSuite.csproj package -v 2.80.2 SkiaSharp.NativeAssets.Linux
dotnet build ./W3CTestSuite/W3CTestSuite.csproj
