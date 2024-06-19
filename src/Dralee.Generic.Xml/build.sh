key=$1 # set the nuget key
ver=$2 # set the version 1.0.0
dotnet build -p:Version=$ver -c:Release
cd bin/Release
dotnet nuget push Dralee.Generic.Xml.$ver.nupkg -k $key -s https://api.nuget.org/v3/index.json --skip-duplicate
cd ../..
echo finish
