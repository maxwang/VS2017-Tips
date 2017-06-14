# VS2017-Tips
1. When you run add-migration, update-database, maybe you will get:
The term 'update-database' is not recognized as the name of a cmdlet, function, script file....
Just run below command in Package Manager Console to init:(Please check the version before you run it)
C:\Users\Username\.nuget\packages\microsoft.entityframeworkcore.tools\1.1.1\tools\init.ps1

2. apply gitignore on exisiting repository
git rm -r --cached .
git add .
git commit -m ".gitignore is now working"

3.Run dotnet application with envirment name
rem Windows
C:\> set ASPNETCORE_ENVIRONMENT=Development
C:\> dotnet ...

rem Unix
$ export ASPNETCORE_ENVIRONMENT=Development
$ dotnet ...
