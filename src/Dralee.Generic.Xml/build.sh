key=$1 # set the nuget key
dotnet pack
cd bin/Release
dotnet nuget push Dralee.Generic.Xml.1.0.0.nupkg -k $key -s https://api.nuget.org/v3/index.json --skip-duplicate
cd ../..
echo finish
