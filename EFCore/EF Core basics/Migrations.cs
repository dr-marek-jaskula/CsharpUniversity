namespace EFCore.EF_Core_basics;

public class Migrations
{
    //Migration, based on the models and dbContext, creates the code to implement changes to the database.
    //To create migrations we write "dotnet ef migrations <migration_name>"
    //They are auto-generated

    //This will create two files:
    //migration file
    //snapshot file

    //for instance:
    //20220410161201_Init
    //MyDbContextModelSnapshot

    //Naming of above files:
    //migration file -> time-stamp (2022-04-10 and hour-minutes-seconds) and then underscored migration name
    //snapshot file -> <dbcontext_name>ModelSnapshot

    //Migration file has all changes that will be applied to the database

    //snapshot file tracks the current state of the database, based on the previous and current migrations.

    //After migrations are created, we should apply them to the database by:
    //"dotnet ef database update"

    //To remove the migration that was not applied to the database we just need to:
    //"dotnet ef migrations remove" (we can specify the migrations name)

    //To remove the migrations that was applied to the database we need to first:
    //1. Reverse change in a database by:
    //"dotnet ef database update <name_of_previous_migrations>"
    //2. Remove the migration like in the previous way
}