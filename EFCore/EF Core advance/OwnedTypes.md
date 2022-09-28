# OwnedTypes

Owned types are data model references that do not represent the database model, but just a c# class.

Therefore, they should not be treated as a separate tables.

There are two way of telling Entity Framework Core that the class is owned class:

- Using [Owned] attribute
- Using database configuration (preferred one)

The example of the owned type with its configuration:
Address.Coordinate

For Owned types we do not need "Include", but just "Where".
