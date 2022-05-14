namespace EFCore.EF_Core_basics;

public class ClassInheritance
{
    //We have to different approaches of an Entity Framework Core in case when the inheritance occurs.
    //They are presented on the pictures

    //Type-per-type looks like a preferred approach, but the statistics shows that Table-per-hierarchy performance is better (because joins are main performance issues in relational database systems)
    //Therefore, use "Table-per-hierarchy"

    //Comparison is done for "Table-per-hierarchy" vs "Table-per-type" approach at "Performance-comparison"
}