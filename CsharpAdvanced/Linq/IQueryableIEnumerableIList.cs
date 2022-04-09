using EFCore;
using EFCore.Data_models;
using Microsoft.EntityFrameworkCore;

namespace CsharpBasics.Linq;

public class IQueryableIEnumerableIList
{
    // The inheritance of interfaces:
    // IList : ICollection : IEnumerable
    // IQueryable : IEnumerable

    //In this file we will discuss the differences between the following interfaces (Collection that implements these interfaces):

    //1. IEnumerable
    //IEnumerable is useful when we want to iterate the collection of objects which deals with in-process memory.

    //2. IQueryable
    //IQueryable is useful when we want to iterate a collection of objects which deals with ad-hoc queries against the data source or remote database, like SQL Server.
    //IQueryable is inherited from IEnumerable so whatever IEnumerable can do, we can achieve with IQueryable as well.

    //3. IList
    //List is useful when we want to perform any operation like Add, Remove or Get item at specific index position in the collection.

    //We use IEnumerable and IQueryable for data manipulation while querying the data from a database or collections.

    private readonly MyDbContext _context;

    public IQueryableIEnumerableIList(MyDbContext context)
    {
        _context = context;
    }

    #region IEnumerable, ICollection, IList

    //IEnumerable
    //An IEnumerable is a list or a container which can hold some items.You can iterate through each element in the IEnumerable.
    //You can not edit the items like adding, deleting, updating, etc.instead you just use a container to contain a list of items.
    //It is the most basic type of list container.
    //IEnumerable is best to query data from in-memory collections like List, Array, etc.

    //ICollection
    //All you get in an IEnumerable is an enumerator that helps in iterating over the elements.
    //An IEnumerable does not hold even the count of the items in the list, instead, you have to iterate over the elements to get the count of items.
    //An IEnumerable supports filtering elements using where clause.

    //ICollection supports enumerating over the elements, filtering elements, adding new elements, deleting existing elements,
    //updating existing elements and getting the count of available items in the list.


    //IList
    //IList extends ICollection. An IList can perform all operations combined from IEnumerable and ICollection, and some more operations
    //like inserting or removing an element in the middle of a list.
    //You can use a foreach loop or a for loop to iterate over the elements.

    #endregion

    #region IQueryable

    //IQueryanble -> name suggests that is was create to deal with queries
    //a) Best suitable for LINQ to SQL COnversion
    //b) Supports custom query and lazy loading
    //c) executes a query on a server with all filter conditions on server side and gets record which are matching with the filter condition

    //IQueryable is best to query data from out-memory (like remote database, service) collections.

    //Example
    public void IQueryableExample()
    {
        //We will use the DbFunctions (see DbFunctions on EFCore project, EF Core advance folder)
        IQueryable<Employee> employees = _context.Employees.Where(emp => EF.Functions.Like(emp.FirstName, "J%"));
        var result = employees.Take(2);

        //Generated SQL is similar to:
        //SELECT TOP (2) * FROM [Employees] WHERE [FirstName] LIKE 'J%';

        //But if the IEnumerable is used
        IEnumerable<Employee> employees2 = _context.Employees.Where(emp2 => EF.Functions.Like(emp2.FirstName, "J%"));
        var result2 = employees2.Take(2);

        //Generated SQL is similar to:
        //SELECT * FROM [Employees] WHERE [FirstName] LIKE 'J%';
        //Taking two record is done on the c# side.

        //Therefore, for safety reasons, if the IQuerable is applicable, we should use IQueryable.
    }

    //An IQueryable generates a LINQ to SQL expression that is executed over the database layer.
    //Instead of the generating a Func<T, bool>, IQueryable generates an expression tree and gives Expression<Func<T, bool>> that is executed over the database layer to get data set.

    #endregion IQueryable

    #region IList

    #endregion IList
}

