namespace EFCore.EF_Core_advance;

public class ScaffoldExistingDatabase
{
    //In order to recreate the database model form the existing database we use the "Scaffold" approach
    //1) Add a connection string
    //2) Use command
    //dotnet ef dbcontext scaffold "Name=ConnectionStrings:NorthwindConnectionString" Microsoft.EntityFrameworkCore.SqlServer -o NorthwindDataModels
    //Where -o states about the output directory (which will create there is no such folder

    //All code will be generated automatically

    //After we configure the dbContext, we can delete the "OnConfiguring" method from the generated context class
}