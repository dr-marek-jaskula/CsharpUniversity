# EntityInheritance

There are three approaches for Code-First, when the inheritance occurs:

- Table-per-type
- Table-per-hierarchy
- Table-per-concrete-type

There is no best strategy that fits all scenarios. Each approach have their own advantages and disadvantages. Mainly we can following this guide:

- If we do not require **polymorphic associations** or queries, use Table-per-concrete-type. I recommend this for the top level of your class hierarchy, where polymorphism is not usually required, and when modification of the base class in the future is unlikely.
- If you do require **polymorphic associations** or queries, and subclasses declare relatively few properties use Table-per-hierarchy. Goal is to minimize the number of nullable columns.
- If you do require **polymorphic associations** or queries, and subclasses declare many properties use Table-per-type.

A **polymorphic association** is an association to a base class, thus to all classes in the hierarchy with dynamic resolution of the concrete class at runtime.

Comparison is done for "Table-per-hierarchy" vs "Table-per-type" approaches in "Performance-comparison".

## Table-per-type

Table per Type is about representing inheritance relationships as relational foreign key associations. Every class/subclass that declares persistent properties—including abstract classes—has its own table. The table for subclasses contains columns only for each noninherited property (each property declared by the subclass itself) along with a primary key that is also a foreign key of the base class table.

Table-per-type looks like a preferred approach, but the statistics shows that Table-per-hierarchy performance is better (because joins are main performance issues in relational database systems)

Example of Table-per-type in this project:
> Data models -> Person (parents folder) + Customer + Employee

## Table-per-hierarchy

An entire class hierarchy can be mapped to a single table. This table includes columns for all properties of all classes in the hierarchy. The concrete subclass represented by a particular row is identified by the value of a type discriminator column.

Example of Table-per-hierarchy in this project:
> Data models -> WorkItem (parents folder) + Project, Task, Issue

## Table-per-concrete-type

In Table per Concrete type we use exactly one table for each nonabstract class. All properties of a class, including inherited properties, can be mapped to columns of this table.

So the example of this approach from the database perspective is when we create to separate table, each containing some unique columns and some common columns.