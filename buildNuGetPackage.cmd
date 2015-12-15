SET version="0.1"

msbuild /p:Configuration=Release Eid.Owin\Eid.Owin.csproj
MKDIR nuget\lib\net451
COPY Eid.Owin\bin\Release\*.dll nuget\lib\net451

COPY Eid.Owin.nuspec nuget\

Tools\NuGet\nuget.exe pack nuget\Eid.Owin.nuspec -Version %version%

RMDIR nuget /S /Q