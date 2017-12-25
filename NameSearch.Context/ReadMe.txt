Running Migrations from a Class Library: 
https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet#targeting-class-library-projects-is-not-supported
http://benjii.me/2016/06/entity-framework-core-migrations-for-class-library-projects/

SQLite:
https://docs.microsoft.com/en-us/ef/core/get-started/netcore/new-db-sqlite
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app-xplat/working-with-sql

Migrations:
https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/migrations#introduction-to-migrations

Note: Run commands from Cmd.exe within the project folder.

For example:
dotnet ef migrations add InitialCreate
dotnet ef database update