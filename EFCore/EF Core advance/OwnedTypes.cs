namespace EFCore.EF_Core_advance;

public class OwnedTypes
{
    //Owned types are data model references that do not represent the database model, but just a c# class.
    //Therefore, they should not be treated as a separate tables.

    //There are two way of telling Entity Framework Core that the class is owned class.
    //1) Using [Owned] attribute
    //2) Using database configuration (preferred one)

    //The example of the Owned type with configuration:
    //Address and Coordinate

    //For Owned types we do not need "Include" but just "Where"
}