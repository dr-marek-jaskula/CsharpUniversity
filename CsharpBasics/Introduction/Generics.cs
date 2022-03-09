namespace CsharpBasics.Introduction;

internal class Generics
{
    //Generics introduces the concept of type parameters to .NET, which make it possible to design classes and methods that defer the specification of one or more types until the class or method is declared and instantiated by client code.

    public static void InvokeGenericsExamples()
    {
        #region Generic List Example
        GenericList<int> numbers = new();
        numbers.Add(10);
        numbers.Add(20);
        numbers.Add(30);
        foreach (int item in numbers.MyList)
            Console.WriteLine(item);

        GenericList<Book> books = new();
        Book book = new() { ISBN = 1111, Title = "C# Advanced" };
        Book book2 = new() { ISBN = 2222, Title = "C# Beginner" };
        Book book3 = new() { ISBN = 3333, Title = "C# Master" };

        books.Add(book);
        books.Add(book2);
        books.Add(book3);

        int i = 1;
        foreach (Book item in books.MyList)
        {
            Console.WriteLine($"The title of the {i} book is {item.Title} while its ISBN is {item.ISBN}");
            i++;
        }
        #endregion

        #region Second Example

        int a = 5; int b = 4;
        Console.WriteLine(a > b ? a : b);

        Console.WriteLine(Utilities.Max(4, 5));
        book.Price = 10;
        Console.WriteLine(book.Price);
        Console.WriteLine(DiscountCalculator<Book>.CalculateDiscount(book));
    }

    //This is an example of generic method. Independently of give type the swap will be executed. We can also place constraints, same as for classes.
    public static void Swap<T>(ref T first, ref T second) // where T : IEntity, new() 
    {
        //We use the tuple to swap the values
        (second, first) = (first, second);
    }
}

//Example of non generic class and the generic method inside
public class Utilities
{
    static public int Max(int a, int b)
    {
        return a > b ? a : b;
    }

    //We define the method that is a generic one, however with a constraint that T has to implement interface IComarable
    public static T Max<T>(T a, T b) where T : IComparable
    {
        //CompareTo method examines if 'a' precedes 'b'. If so, it returns negative value, if its after the return value is positive. Other this method returns zero
        return a.CompareTo(b) > 0 ? a : b;
    }
}

#region Generic list example
//Rather then use multiple classes for different types, we create one generic class.
public class GenericList<T> 
{
    private readonly List<T> _list;

    public List<T> MyList => _list;

    public GenericList()
    {
        _list = new List<T>();
    }

    public void Add(T value) 
    {
        MyList.Add(value);
    }

    //This overrides the index operation. No matter what we just throw an exception
    public T this[int index] 
    {
        get { throw new NotImplementedException(); } 
        set { throw new AccessViolationException(); }
    }
}
#endregion

#region Interesting example

public class Book : Product
{
    public int ISBN;
}

public class Product
{
    public string? Title { get; set; }
    public float Price { get; set; }
}

//Generic approach is used here with constraint, so the information about the input type can be used as follows
public class DiscountCalculator<T> where T : Product 
{
    public static float CalculateDiscount(T product) => product.Price;
}

#endregion

#region Example with two types

public interface IEntity
{
    public int Id { get; set; }
}

public class DoubleProduct<T, K> where T: class, new() where K : IEntity
{
    public string? Title { get; set; }
    public float Price { get; set; }

    //Just to introduce the concept
    public T TestMethod(K obj)
    {
        int id = obj.Id;
        return new T();
    }
}

#endregion

