rm *.nupkg
nuget pack .\InsertCoinBuddy.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget push *.nupkg