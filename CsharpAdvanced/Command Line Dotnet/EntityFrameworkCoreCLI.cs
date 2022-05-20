namespace CsharpAdvanced.Command_Line_Dotnet;

public class EntityFrameworkCoreCLI
{
    //dotnet ef migrations add Init -> add new migrations with name "Init"
    //dotnet database update -> update the database by all migrations (can update the certain migration)

    //For creating models from existing database
    //dotnet ef dbcontext scaffold "Name=ConnectionStrings:NorthwindConnectionString" Microsoft.EntityFrameworkCore.SqlServer -o NorthwindDataModels
}