namespace CsharpBasics.OOP;

#region Keyword used for classes
///By default class is internal, which means that the scope of this class is narrowed to the current .cs file
class InternalClassWithoutKeyword
{
}

//The preferred approach is to use the internal keyword
internal class InternalClassWithKeyword
{
}

//Public class means that the class is available in the whole project (assembly)
public class PublicClass
{
    //Private class is the class that can not be defined directly in the namespace. Private keyword makes it available only inside the other class
    private class PrivateClass
    {
    }
}

//A sealed class is a class that cannot be inherited
public sealed class PublicSealdClass
{
}

//A static class is a class that can not be instantiated. Therefore, is can not contain constructors.
//What is more static class can only contain static members. 
//Last difference is that the static class is sealed
public static class PublicStaticClass
{
}

//An abstract class is a class that can not be instantiated, but can be inherited and can contain constructor
//Abstract class can not be sealed
//Abstract class members must be also abstract and children of abstract class must implement all the abstract members of the parent
//Abstract class is used to achieve Dependency Injection, to make the structure of the program loosely coupled. However, commonly used are interfaces, but abstract classes can be useful in some cases
public abstract class PublicAbstractClass
{
}

//Partial keyword allows the designer to define a part of the class in one scope and the other part in the other scoped.
//It is very useful in dealing with auto generated code, for example obtained from migrations in entity framework core
public partial class PublicPartialClass 
{
    public void Work()
    {
    }
}
public partial class PublicPartialClass 
{
    public void Lunch()
    {
    }
}

#endregion Keyword used for classes

//This class will be used as a container for constructors
public class Constructors : BaseConstructors
{
    //One of the major object oriented programming rule is "Encapsulation", which means that the access to the class member should be limited: only public member should be available to use for user

    #region Member for constructors examples

    //properties should be named using PascalCase
    public int Age { get; set; }

    //property with the default value set to string.Empty which is ""
    public string Name { get; set; } = string.Empty;

    //public fields should be named using camelCase
    public string favoriteColor = string.Empty;

    //private fields should be named using _camelCase (underscoredCamelCase)
    private double _salary;

    //its full property, bound to the _calary by the getter and setter
    public double Salary
    {
        get { return _salary; }
        set { _salary = value; }
    }

    //static readonly field of type DateTime. Static field cannot be reached from instances, but from the class definition like "ConstructorExaples.globalTime".
    //Such static members can be set for example using static constructor
    public static readonly DateTime globalTime;

    //Readonly member can be set by the constructor and then it can not be modified
    public readonly int readonlyNumber;

    //Property with "init" setter. This means that this value can be set only in constructor or directly when calling the constructor like: ConstructorExamples() { initMNumber = 5 };
    public int initNumber { get; init; }

    #endregion Member for constructors examples

    #region Basic constructors

    //Constructor name has to be the same as the class name

    //Basic, empty constructor. If there are no specified constructors, this constructor is designed by default. 
    //The constructor is called that the instance of a class is created
    //the snippet for constructor is "ct" or "ctor"
    //in such basic constructors we can for example instantiate a empty list
    public Constructors()
    {
    }

    //Custom constructor, used to set fields and properties
    //Keyword "this" has to be used for favouriteColor to distinguish the injected field from class field
    public Constructors(int age, string name, string favoriteColor)
    {
        Age = age;
        Name = name;
        this.favoriteColor = favoriteColor;
    }

    //Constructor can set the value of readonly member or property with "init" setter
    public Constructors(int readonlyNumber, int initNumber)
    {
        this.readonlyNumber = readonlyNumber;
        this.initNumber = initNumber;
    }

    #endregion Basic constructors

    #region Static constructors

    //static constructor can not have any other access modifiers, because only one static constructor can be defined.
    //Moreover, the static constructor of the certain class will be called when the first occurrence of this class (its member of instances of) happends
    //Let us notice that the static constructor is called before an instance or a member of this class is called
    //The static constructor can not be inherited or overrided
    static Constructors()
    {
        globalTime = DateTime.Now;
    }

    #endregion Static constructors

    #region Private constructor

    //This constructor can be called only from the inside of the class
    //It is mostly used in case when all members are static and there are not public constructor -> to deny the possibility of creating an instance of a class (but it can be done from the inside of the call if needed)
    //The difference from the approach of using the static class is that, such class can be inherited (when use the protected constructor rather than private)
    private Constructors(string message)
    {
        Console.WriteLine($"This is private constructor message {message}");
    }

    protected Constructors(string message, string messege2)
    {
        Console.WriteLine($"This is private constructor message {message} {messege2}");
    }

    #endregion

    #region Copy constructor

    //copy constructor is a constructor that is used to create a new instance that is a copy of the existing object (has same members)
    public Constructors(Constructors constructorsExamples)
    {
        Age = constructorsExamples.Age;
        Name = constructorsExamples.Name;
        favoriteColor = constructorsExamples.favoriteColor;
        //remaining ones if needed
    }

    //// Alternate copy constructor calls the instance constructor.
    //public ConstructorsExamples(ConstructorsExamples constructorsExamples) : this(constructorsExamples.Name, constructorsExamples.Age, constructorsExamples.favoriteColor)
    //{
    //}

    //copy constructors are designed in record by default

    #endregion

    #region Base constructors

    //base constructor are constructor from the parent class
    //in order to use the parent constructors we use the "base" keyword

    public Constructors(bool firstBoolean, bool secondBoolean, char firstCharacter, char secondCharacter) : base(firstCharacter, firstBoolean)
    {
        Console.WriteLine($"Constructor from a child class, called after the constructor from parent class. Second boolean is {secondBoolean} and the second char is {secondCharacter}");
    }

    public Constructors(double firstDouble, double secondDouble) : base(firstDouble)
    {
        Console.WriteLine($"Constructor from a child class, called after the constructor from parent class. Second double is {secondDouble}");
    }

    //to call parameterless constructor is sufficient to write "base()"

    #endregion

    #region Constructor chaining

    //constructor chaining is the methodology of using one constructor in the body of other one
    //this is made for example to make code cleaner and shorter
    //it can be done by using "this" keyword, that refers to other constructors of this class

    public Constructors(DateTime dateTime, int age, string name, string favoriteColor) : this(age, name, favoriteColor)
    {
        Console.WriteLine($"This constructor at first will call the constructor with age, name, favoriteColor parameters and then this code will be called");
    }

    //This constructor is a schema of using the empty constructor to set the value by using other constructor
    //public ConstructorsExamples() : this(18, "DefaultRoman", "black")
    //{
    //}

    //to call parameterless constructor is sufficient to write "this()"

    #endregion

    #region Class Methods

    public void ClassMethod()
    {
        Console.WriteLine("This is class method");
    }

    public static void StaticClassMethod()
    {
        Console.WriteLine("This is static class method");
    }

    //params keyword now allows us to use as an parameter the array or just uncertain number of numbers
    static public int StaticAdd(params int[] numbers)
    {
        int sum = 0;
        foreach (int number in numbers)
            sum += number;
        return sum;
    }

    #endregion

    #region Invoke constructor examples

    /// <summary>
    /// This static method is designer to demonstrate the way for to use constructors. Only for educational use.
    /// </summary>
    public static void InvokeConstructors()
    {
        //Basic, old fashion way to create an instance of a class (moreover here will be called the static constructor - use debug to examine this)
        Constructors constructorsExamples = new Constructors();

        //Using "var" keyword
        var constructorExamples1 = new Constructors();

        //Modern, best practice way
        Constructors constructorsExamples2 = new();

        //Way to use the constructor with parameters
        Constructors constructorsExamples3 = new(25, "Marek", "red");

        //Alternate way, calling default constructor and setting some member. This way can be used to avoid large number of constructors
        //The properties with init can be set by this approach
        //The readonly members can not be set by this approach
        Constructors constructorsExamples4 = new() { Age = 33, Name = "Jackob", favoriteColor = "blue" };

        //Alternate way
        //The properties with init can not be set by this approach
        //The "readonly" members can not be set by this approach
        Constructors constructorsExamples5 = new();
        constructorsExamples5.Age = 44;
        constructorsExamples5.Name = "Michał";
        constructorsExamples5.favoriteColor = "green";

        //call a non static method
        constructorsExamples.ClassMethod();

        //call a static method inside the class
        Constructors.StaticClassMethod();

        //due to the fact that is the same class explicitly calling static method is not required it can be transformed into
        StaticClassMethod();

        //Both ways are valid 
        Console.WriteLine(StaticAdd(new int[] { 1, 2, 3, 4 }));
        Console.WriteLine(StaticAdd(1, 2, 3, 4));
    }
    #endregion
}

//class created to demonstrate the idea of base constructors 
public class BaseConstructors
{
    public BaseConstructors()
    {
    }

    public BaseConstructors(double doubleNumber)
    {
        Console.WriteLine($"Base constructor with double number {doubleNumber}");
    }

    public BaseConstructors(char character, bool boolean)
    {
        Console.WriteLine($"Base constructor with char {character} and boolean {boolean}");
    }
}
