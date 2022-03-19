#define CustomPredefineValue
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace CsharpAdvanced.Attributes;

//In this file we will talk about some build in attributes, mostly considered in:
//https://www.entityframeworktutorial.net/code-first/key-dataannotations-attribute-in-code-first.aspx

//Some predefined attributes:
// Obsolete - marks types and type members as outdated

#region Obsolete Attribute
public class Calculator
{
    //To inform that a method is outdated we just need to write:
    [Obsolete]
    public static int Add(int firstNumber, int secondNumber)
    {
        return firstNumber + secondNumber;
    }

    //We can also provide more information
    [Obsolete("Dear user, please use Add(List<int> numbersList) Method")]
    public static int Add(int firstNumber, int secondNumber, int thirdNumber)
    {
        return firstNumber + secondNumber + thirdNumber;
    }

    //The second boolean parameter determines if using the method throws an error or not: it is is true, the exception will be thrown
    [Obsolete("U cant use this method", true)] 
    public static int Add(int firstNumber, int secondNumber, int thirdNumber, int fourthnumber)
    {
        return firstNumber + secondNumber + thirdNumber + fourthnumber;
    }

    public static int Add(List<int> numbersList)
    {
        int sum = 0;
        foreach (int numbers in numbersList)
            sum += numbers;
        return sum;
    }
}

#endregion

//Conditional - determines if the method call should be ignored (similar to #if #endif)

#region Conditional Attribute

public class TestingConditionalAtri
{
    public static void DoStuff()
    {
        //standard way
        #if DEBUG
        DebugMethod();
        #endif

        ConditionalDebugMethod();
        ConditionalCustomPredefinedMethod1();
        ConditionalCustomPredefinedMethod2();

        Debug.WriteLine("Debug trace message");
        Trace.WriteLine("Trace message");
    }

    #if DEBUG
    public static void DebugMethod()
    {
        Console.WriteLine("Im DEBUG method");
    }
    #endif

    //We can specify the string parameter. If such parameter is defined, then it is not omitted
    [Conditional("DEBUG")]
    public static void ConditionalDebugMethod()
    {
        Console.WriteLine("I'm conditional DEBUG method");
    }

    [Conditional("RandomParameter")]
    public static void ConditionalCustomPredefinedMethod1()
    {
        Console.WriteLine("I'm conditional custom predefined method with predefined parameter: RandomParameter");
    }

    [Conditional("CustomPredefineValue")]
    public static void ConditionalCustomPredefinedMethod2()
    {
        Console.WriteLine("I'm conditional custom predefined method with predefined parameter: CustomPredefineValue");
    }
}

#endregion

//ModuleInitializer - A “module initializer” is a function that is run when an assembly is first loaded. In many ways this is like a static constructor in C#, but rather than applying to one class it applies to the entire assembly.

//Some requirements are imposed on the method targeted with this attribute:

//    The method must be static.
//    The method must be parameterless.
//    The method must return void.
//    The method must not be generic or be contained in a generic type.
//    The method must be accessible from the containing module.
//        This means the method's effective accessibility must be internal or public.
//        This also means the method cannot be a local function.

#region ModuleInitializer Attribute

public class ModuleInitializerExampleModule
{
    public static string Text { get; set; } = string.Empty;

    [ModuleInitializer]
    public static void Init1()
    {
        Text += "Hello from Init1! ";
    }

    [ModuleInitializer]
    public static void Init2()
    {
        Text += "Hello from Init2! ";
    }
}
#endregion

//Validator attributes:
//Required, Range, StringLength

#region Attributes used for validation (however it is better to use Fluent Api if possible)
public class TestValidationAttr
{
    // Require that the string is not null.
    [Required(ErrorMessage = "Title is required.")]
    public string? TestString0;

    [Required]
    public string? TestString1 { get; set; }

    //We also do not allow empty string
    [Required(ErrorMessage = "Genre must be specified", AllowEmptyStrings = false)]
    public string? Genre;

    //We can get the name of the string by using {0}
    [Required(ErrorMessage = "{0} must be specified", AllowEmptyStrings = false)] 
    public string? Genre2;

    //Range also validates the number
    [Range(1, 100, ErrorMessage = "Price must be between $1 and $100")]
    public int Price { get; set; }

    //The maximum length is 5
    [StringLength(5)]
    public string? Rating { get; set; }

    //The maximum length is 10 and minimum is 5. Error message provided
    [StringLength(10, MinimumLength = 5, ErrorMessage = "Between five and ten is what I want")]
    public string? Rating1 { get; set; }

    //Some other examples:
    // https://docs.microsoft.com/pl-pl/aspnet/mvc/overview/older-versions/getting-started-with-aspnet-mvc3/cs/adding-validation-to-the-model

    [StringLength(4, ErrorMessage = "The ThumbnailPhotoFileName value cannot exceed 4 characters. ")]
    public object? ThumbnailPhotoFileName;

    [StringLength(4, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
    public object? PhotoFileName;
    //You can use composite formatting placeholders in the error message: {0} is the name of the property; {1} is the maximum length; and {2} is the minimum length.
}

#endregion

//DataType Attribute plus Phone and Email Attributes - validate 
//Using DataType is used to provide the additional type

#region DataType Attribute plus Phone and Email Attributes

public class TestDataTypeAtri
{
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [DataType(DataType.Currency)]
    public int Price { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name should be minimum 3 characters and a maximum of 50 characters")]
    [DataType(DataType.Text)]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name should be minimum 3 characters and a maximum of 50 characters")]
    [DataType(DataType.Text)]
    public string? LastName { get; set; }

    //We examine if the phone number is well formated
    [DataType(DataType.PhoneNumber)]
    [Phone] // returns boolean value
    public string? PhoneNumber { get; set; }

    //We examine if the email address is well formated
    [DataType(DataType.EmailAddress)]
    [EmailAddress] //true if the specified value is valid or null; otherwise, false.
    public string? Email { get; set; }
}

#endregion

//Attrubites for EF Core, but it is better to use the Fluent Api [examine the EF Core University Project]

#region DataBase Attributes (Entity Framework - tj inny framework): Table, Column, Key, MaxLenght, ConcurencyCheck, Timestamp

//Table:
//https://www.entityframeworktutorial.net/code-first/table-dataannotations-attribute-in-code-first.aspx

//Order:
//https://www.entityframeworktutorial.net/code-first/column-dataannotations-attribute-in-code-first.aspx

//Key:
//https://www.entityframeworktutorial.net/code-first/key-dataannotations-attribute-in-code-first.aspx

[Table("StudentMaster", Schema = "Admin")]
public class Student123
{
    [Key]
    public int StudentKey { get; set; }

    [Key]
    [Column(Order = 2)]
    public int AdmissionNum { get; set; }

    public string? StudentName { get; set; }

    [Column("DoB", TypeName = "DateTime2")]
    public DateTime DateOfBirth { get; set; }
}

#endregion

public class BuildInAttributes
{
    public static void InvokeBuildInAttributesExamples()
    {
        //It is underlined because it is obsolete
        Calculator.Add(10, 20);
        Calculator.Add(10, 20, 39); //we can see the comment from obsolete attribute
        Calculator.Add(new List<int>() { 1, 4, 24, 44, 22, 3 });

        TestingConditionalAtri.DoStuff();

        Console.WriteLine(ModuleInitializerExampleModule.Text);

        Author author = new();
        author.FirstName = "Joydip";
        author.LastName = ""; //No value entered
        author.PhoneNumber = "1234567890";
        author.Email = "joydipkanjilal@yahoo.com";
        ValidationContext context = new ValidationContext(author, null, null);
        List<ValidationResult> validationResults = new();
        bool valid = Validator.TryValidateObject(author, context, validationResults, true);
        
        if (!valid)
            foreach (ValidationResult validationResult in validationResults)
                Debug.WriteLine("{0}", validationResult.ErrorMessage);

        Console.WriteLine("----------------------------");

        Author author2 = new Author();
        author2.FirstName = "Jo";
        author2.LastName = "no"; //No value entered
        author2.PhoneNumber = "its a number";
        author2.Email = "joydipkanjilh!";
        ValidationContext context2 = new ValidationContext(author2, null, null);
        List<ValidationResult> validationResults2 = new();
        bool valid2 = Validator.TryValidateObject(author2, context2, validationResults2, true);

        if (!valid2)
            foreach (ValidationResult validationResult2 in validationResults2)
                Debug.WriteLine("{0}", validationResult2.ErrorMessage);
    }
}

#region Helpers

public class Author
{
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, MinimumLength = 3,
    ErrorMessage = "First Name should be minimum 3 characters and a maximum of 50 characters")]
    [DataType(DataType.Text)]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, MinimumLength = 3,
    ErrorMessage = "Last Name should be minimum 3 characters and a maximum of 50 characters")]
    [DataType(DataType.Text)]
    public string? LastName { get; set; }

    [DataType(DataType.PhoneNumber)]
    [Phone]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string? Email { get; set; }

    [MaxLength(50)]
    public string? OtherName { get; set; }
}

#endregion