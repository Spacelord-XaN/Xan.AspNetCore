param (
    [Parameter(Mandatory=$true)]
    [string]$version,

    [Parameter(Mandatory=$true)]
    [string]$apiKey
)
dotnet build ./Xan.AspNetCore.sln --configuration Release
dotnet pack ./Xan.AspNetCore.sln --configuration Release
dotnet nuget push ./src/Xan.AspNetCore/bin/Release/Xan.AspNetCore.$version.nupkg --api-key $apiKey --source https://api.nuget.org/v3/index.json