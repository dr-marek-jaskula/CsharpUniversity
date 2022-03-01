#define Wygralem
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningApplication3
{
    //https://www.entityframeworktutorial.net/code-first/key-dataannotations-attribute-in-code-first.aspx

    //Atributes allow to add declarative information to your programs
    //This information can then be used at runtime using reflections

    //There are pre-defined attributes provided by .Net
    //It is possible to create own attributes

    //pre-defined attributes:
    // Obsolete - Marks types and type membert outdated
    // WeMethod - to expose a method as an XML Web service method
    // Serializable - indicates that a class can be serialized

    class AttributesLearning2
    {
        public static void Attri2()
        {
            //podkresla ze stare
            Calculator.Add(10, 20);
            Calculator.Add(10, 20, 39); //jak się najedzie myszką na tą metode to widać komentarz, i ze jest obsolete (outdated)
            Calculator.Add(new List<int>() { 1, 4, 24, 44, 22, 3 });

            TestingConditionalAtri.DoStuff();

            Console.WriteLine(ModuleInitializerExampleModule.Text);

            #region For example

            Author author = new Author();
            author.FirstName = "Joydip";
            author.LastName = ""; //No value entered
            author.PhoneNumber = "1234567890";
            author.Email = "joydipkanjilal@yahoo.com";
            ValidationContext context = new ValidationContext(author, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(author, context, validationResults, true);
            
            if (!valid)
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    Console.WriteLine("{0}", validationResult.ErrorMessage);
                }
            }

            Console.WriteLine("----------------------------");

            Author author2 = new Author();
            author2.FirstName = "Jo";
            author2.LastName = "no"; //No value entered
            author2.PhoneNumber = "hahah";
            author2.Email = "joydipkanjilh!";
            ValidationContext context2 = new ValidationContext(author2, null, null);
            List<ValidationResult> validationResults2 = new List<ValidationResult>();
            bool valid2 = Validator.TryValidateObject(author2, context2, validationResults2, true);

            if (!valid2)
            {
                foreach (ValidationResult validationResult2 in validationResults2)
                {
                    Console.WriteLine("{0}", validationResult2.ErrorMessage);
                }
            }

            #endregion
        }

        #region Obsolate Attribute
        public class Calculator
        {
            #region outDatedMethods


            //Obsolate mowi, ze jest "outdated" czyli że metoda jest stara
            [Obsolete]
            public static int Add(int firstNumber, int secondNumber)
            {
                return firstNumber + secondNumber;
            }

            [Obsolete("Dear user, please use Add(List<int> numbersList) Method")] //tutaj użycie Obsolete z wiadomością, której używać, a nie tylko ze ta przedawniona
            public static int Add(int firstNumber, int secondNumber, int thirdNumber)
            {
                return firstNumber + secondNumber + thirdNumber;
            }

            [Obsolete("U cant use this method", true)] //ten drugi parameter true sprawi, że użycie tej metody spowoduje error - exception
            public static int Add(int firstNumber, int secondNumber, int thirdNumber, int fourthnumber)
            {
                return firstNumber + secondNumber + thirdNumber + fourthnumber;
            }
            //the requirement have change

            #endregion
            //u must leave the old methods where they were coz old users may use them - so for the back compability. But new ppl should use new method. How to tell them? Using attribute
            //We have no worning even we use old method. So let us made a warning by atributes to tell the user to use the new method



            public static int Add(List<int> numbersList)
            {
                int sum = 0;
                foreach (int numbers in numbersList)
                {
                    sum += numbers;
                }
                return sum;
            }

        }

        #endregion

        #region Conditional Attribute

        public class TestingConditionalAtri
        {

            public static void DoStuff()
            {
#if DEBUG
                DebugMethod();
#endif

                ConditionalDebugMethod();

                ConditionalCustomPredefinedMethod1();
                ConditionalCustomPredefinedMethod2();

                //te dwa na dole to nie wiem dlaczego nie wchodzą
                Debug.WriteLine("Debug trace message");
                Trace.WriteLine("Trace message");

            }

#if DEBUG
            public static void DebugMethod()
            {
                Console.WriteLine("Im DEBUG method");
            }
#endif

            [Conditional("DEBUG")]
            public static void ConditionalDebugMethod()
            {
                Console.WriteLine("Im conditional DEBUG method");
            }

            [Conditional("Walka")]
            public static void ConditionalCustomPredefinedMethod1()
            {
                Console.WriteLine("Im conditional custom predefined method with predefined parameter Walka");
            }

            [Conditional("Wygralem")]
            public static void ConditionalCustomPredefinedMethod2()
            {
                Console.WriteLine("Im conditional custom predefined method with predefined parameter Wygralem");
            }

        }

        #endregion

        #region ModuleInitializer Attriute

        //cos ten atrybut nei chce działa, tzn nei widzi go
        public class ModuleInitializerExampleModule
        {
            public static string Text { get; set; }

            // [ModuleInitializer]
            public static void Init1()
            {
                Text += "Hello from Init1! ";
            }

           // [ModuleInitializer]
            public static void Init2()
            {
                Text += "Hello from Init2! ";
            }
        }
        #endregion

        #region Required Attribute (Validation test), Plus Attribute Properties and StringLEnght and Range Attributes

        //Validation to jest czy dane są zgodne z oczekiwanymi

        //ten atrybut określa co jest wymagane, inaczej robi exception albo jakis message


        public class TestReqAtri
        {
            // Require that the TestString0 is not null.
            // Use custom validation error.
            [Required(ErrorMessage = "Title is required.")] //tutaj jak wida jest z tym znakiem równości i tak się niektóre propsy atrybutów ustawia
            public string TestString0;

            [Required]
            public string TestString1 { get; set; }

            [Required(ErrorMessage = "Genre must be specified", AllowEmptyStrings = false)]
            public string Genre;

            [Required(ErrorMessage = "{0} must be specified", AllowEmptyStrings = false)] //{0} to chyba nazwa fielda, pewnie kolejne numerki to co innego
            public string Genre2;

            [Range(1, 100, ErrorMessage = "Price must be between $1 and $100")]
            public decimal Price { get; set; }

            [StringLength(5)]
            public string Rating { get; set; }

            [StringLength(10, MinimumLength = 5, ErrorMessage = "Between five and ten is what I want")]
            public string Rating1 { get; set; }

            // https://docs.microsoft.com/pl-pl/aspnet/mvc/overview/older-versions/getting-started-with-aspnet-mvc3/cs/adding-validation-to-the-model
            // tutaj pokazuje jak np dla UI wymagane jest wpisywanie loginu itp

            [StringLength(4, ErrorMessage = "The ThumbnailPhotoFileName value cannot exceed 4 characters. ")]
            public object ThumbnailPhotoFileName;

            [StringLength(4, ErrorMessage = "The {0} value cannot exceed {1} characters. ")] //Tutaj działają te: {0} oi {1}
            public object PhotoFileName;

            //You can use composite formatting placeholders in the error message: {0} is the name of the property; {1} is the maximum length; and {2} is the minimum length.
        }

        #endregion

        #region DataType Attribute plus Phone and Email Attributes

        //dzięki temu dodajemy dodatkowy, spowinowacony z danym propsem typ, który może być przydatny: na przykład ze dana DataTime jest typowo datą, albo że decimal jest typowo walutą (Currency)
        public class TestDataTypeAtri
        {
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "First Name should be minimum 3 characters and a maximum of 50 characters")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Last Name should be minimum 3 characters and a maximum of 50 characters")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }


            //tutaj jest atrybut Phone, który sprawdza czy cos jest dobrze sformatowanym numerem telefonicznym
        [DataType(DataType.PhoneNumber)]
        [Phone] //zwraca boolian value, czyli true jak jest ok, a false jak jest zły
        public string PhoneNumber { get; set; }

            //tutaj jest atrybut EmailAddress, który sprawdza czy coś jest dobrze sformatowanym numerem telefoniznym
        [DataType(DataType.EmailAddress)]
        [EmailAddress] //true if the specified value is valid or null; otherwise, false.
        public string Email { get; set; }
        }

        #endregion

        #region Validation Text!! Example from the internet, it is similar to the previous region

        /// <summary>
        /// Przykład z internetu 
        /// </summary>
        public class Author
        {
            [Required(ErrorMessage = "{0} is required")]
            [StringLength(50, MinimumLength = 3,
            ErrorMessage = "First Name should be minimum 3 characters and a maximum of 50 characters")]
            [DataType(DataType.Text)]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Last Name should be minimum 3 characters and a maximum of 50 characters")]
            [DataType(DataType.Text)]
            public string LastName { get; set; }

            [DataType(DataType.PhoneNumber)]
            [Phone]
            public string PhoneNumber { get; set; }

            [DataType(DataType.EmailAddress)]
            [EmailAddress]
            public string Email { get; set; }

            //mowi, że ma jest maksymalnie 50 characters lenght. Inny sposób, można do stringa i do byte[], ale lepiej inne
            [MaxLength(50)]
            public string HeheName { get; set; }
        }

        #endregion

        #region DataBase Attributes (Entity Framework - tj inny framework): Table, Column, Key, MaxLenght, ConcurencyCheck, Timestamp

        //odnośnie Table:
        //https://www.entityframeworktutorial.net/code-first/table-dataannotations-attribute-in-code-first.aspx

        //odnośnie Order:
        //https://www.entityframeworktutorial.net/code-first/column-dataannotations-attribute-in-code-first.aspx

        //Odnośnie Key:
        //https://www.entityframeworktutorial.net/code-first/key-dataannotations-attribute-in-code-first.aspx

        [Table("StudentMaster", Schema = "Admin")]
        public class Student123
        {
            [Key]
            public int StudentKey { get; set; }

            [Key]
            [Column(Order = 2)]
            public int AdmissionNum { get; set; }

            public string StudentName { get; set; }

            [Column("DoB", TypeName = "DateTime2")]
            public DateTime DateOfBirth { get; set; }
        }

        #endregion

    }

}
