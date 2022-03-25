﻿namespace EFCore;

public class ReadMe
{
    //In order to create a separate project that deal with database, we need to:

    //1. We create new "Class Library" project
    //2. We install:
    //Microsoft.EntityFrameworkCore.SqlServer or MySql.EntityFrameworkCore
    //Microsoft.EntityFrameworkCore.Design (for migrations)
    //Microsoft.Extensions.Configuration.Json (for appsettings)

    //3. We add appsettings.json

    //4. We ensure that we have the latest version of dotnet ef (https://www.nuget.org/packages/dotnet-ef/):
    //dotnet tool install --global dotnet-ef --version 6.0.3
    //Or newer version (or update)
    //dotnet tool update --global dotnet-ef --version 6.0.3

    //5. We add our DbContext (it this project it is called "MyDbContext")

    //6. We need to add data context factory, here called "MyDbContextFactory"
    //See: "DbContextFactory.cs" file

    //7. We add the conncetion string to the appsettings.json:
    //To connect to MySql: "Server=localhost;Database=CedrusMechanic;Uid=root;Pwd=DataBase;"
    //To connect to MS Sql Server: "Server=(localdb)\\MSSQLLocalDB;Database=ShareForFuture;Trusted_Connection=True"

    //If we are using MS SQL Server we can open "SQL Server Object Explorer" and "Server Explorer" to get for example the connection string

    //8. Create data models id "Data models" folder (models are done for educational use)

    //9. Configure data models (data configuration should be in the file in which data model is)
    //To examine the one-to-one, one-to-many, many-to-many configuration, to go some model classes

    //10. We open the power shell (or other CLI), we go to directory where there class library is and then:
    //dotnet ef migrations add Init

    //If we want to see the SQL statement that are generated by the migrations we can use:
    //dotnet ef migrations script --output migrate.sql
    //This will generate the file "migrate.sql" in which all SQL statements are present
}
