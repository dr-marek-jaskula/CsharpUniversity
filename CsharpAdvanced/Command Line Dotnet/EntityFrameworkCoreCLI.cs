namespace CsharpAdvanced.Command_Line_Dotnet;

public class EntityFrameworkCoreCLI
{
    //dotnet ef migrations add Init -> add new migrations with name "Init"
    //dotnet database update -> update the database by all migrations (can update the certain migration)

    //To remove the migrations that was applied to the database we need to first:
    //1. Reverse change in a database by:
    //"dotnet ef database update <name_of_previous_migrations>"
    //2. Remove the migration like in the previous way

    //For creating models from existing database
    //dotnet ef dbcontext scaffold "Name=ConnectionStrings:NorthwindConnectionString" Microsoft.EntityFrameworkCore.SqlServer -o NorthwindDataModels
}