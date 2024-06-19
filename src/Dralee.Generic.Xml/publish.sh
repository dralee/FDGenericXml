#!/bin/bash
key=$1 # set the nuget key

dotnet build -c:Release
cd bin/Release
file=`ls | grep .nupkg | awk '{print $1}'`
echo $file

dotnet nuget push $file -k $key -s https://api.nuget.org/v3/index.json --skip-duplicate
cd ../..
echo finish
