#!/usr/bin/env bash

mkdir temp
cd ./temp

git clone https://github.com/wieslawsoltes/resvg-test-suite.git

dotnet publish ../src/svgc/svgc.csproj -c Release -f netcoreapp3.1 -r win7-x64 /p:PublishTrimmed=True /p:PublishSingleFile=False /p:PublishReadyToRun=True -o ./svgc

mkdir resvgtestsuite

./svgc/svgc -j ../resvg-test-suite.json

dotnet new console -n resvgtestsuite -o resvgtestsuite
dotnet add ./resvgtestsuite/resvgtestsuite.csproj package -v 2.80.2 SkiaSharp
dotnet add ./resvgtestsuite/resvgtestsuite.csproj package -v 2.80.2 SkiaSharp.NativeAssets.Linux
dotnet build ./resvgtestsuite/resvgtestsuite.csproj
