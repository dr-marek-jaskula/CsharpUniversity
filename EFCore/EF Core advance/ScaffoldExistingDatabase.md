# ScaffoldExistingDatabase

In order to recreate the database model form the existing database we use the "Scaffold" approach

1) Add a connection string
2) Use command

```cmd
dotnet ef dbcontext scaffold "Name=ConnectionStrings:NorthwindConnectionString" Microsoft.EntityFrameworkCore.SqlServer -o NorthwindDataModels
```

Where -o is the output directory (will create if there is no such folder)

All code will be generated automatically.

After we configure the dbContext, we can delete the "OnConfiguring" method from the generated context class.
