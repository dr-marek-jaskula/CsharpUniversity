# Basics Of Custom Migrations

In order to implement this approach we need to manually change the migration file

Up method is the method that will apply changes to the database when the following command is called:

```cmd
dotnet ef database update
```

Down method is the method that will apply changes to the database when the followin command is called:

```cmd
dotnet ef database update <name_of_previous_migrations>
```

To change the migrations:

1. We create a new migrations: 

```cmd
dotnet ef migrations add <migration name>
```

2. Go to migrations -> clean all the migration class (delete everything inside the class)
3. Copy it to the new class (for simplicity we will keep it here in scope-range namespace)
4. We can change it here because the class is partial.
5. Override up and down methods (remember to remove them from the migrations folder)
6. Make migrations

Examples are presented in other files in this folder
