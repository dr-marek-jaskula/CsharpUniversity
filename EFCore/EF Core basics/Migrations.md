# Migrations

Based on the models and database context, migration creates the code that will make changes to the database.

## Create Migrations

To create migrations we write

```cmd
dotnet ef migrations <migration_name>
```

This will create two (auto-generated) files:

- migration file
- snapshot file

For instance files:

- 20220410161201_Init
- MyDbContextModelSnapshot

Naming of these files:

- migration file: time-stamp (2022-04-10 and hour-minutes-seconds) and then underscored migration name
- snapshot file: \<dbcontext_name\>ModelSnapshot

Migration file contains all changes that will be applied to the database.

Snapshot file tracks the current state of the database, based on the previous and current migrations.

## Apply Migrations

After migrations were created, we should apply them to the database by:

```cmd
dotnet ef database update
```

To remove a migration, that was not applied to the database, we just need to use

```cmd
dotnet ef migrations remove 
```

or

```cmd
dotnet ef migrations remove <migration name>
```

To remove the migrations, that were applied to the database, we need to:

- Reverse changes in a database by:

```cmd
dotnet ef database update <name_of_previous_migrations>
```

- Remove the migration
