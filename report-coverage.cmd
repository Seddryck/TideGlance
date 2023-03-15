dotnet build TideGlance.sln -c Release --nologo 
dotnet test -c Release /p:CollectCoverage=true /p:CoverletOutputFormat=opencover --no-build --nologo
dotnet reportgenerator "-reports:TideGlance.Testing\coverage.*.opencover.xml" "-targetdir:.\.coverage"
start .coverage/index.html