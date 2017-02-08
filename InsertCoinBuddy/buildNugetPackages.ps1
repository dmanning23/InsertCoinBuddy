rm *.nupkg
nuget pack .\InsertCoinBuddy.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget push -source https://www.nuget.org -NonInteractive *.nupkg