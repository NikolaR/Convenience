@echo off
set NUGET="%~dp0.nuget\nuget.exe"

echo Checking for NuGet updates...
%NUGET% update -self

echo .
echo .
echo Generating NuGet package...
%NUGET% pack "%~dp0Convenience\Convenience.csproj"
