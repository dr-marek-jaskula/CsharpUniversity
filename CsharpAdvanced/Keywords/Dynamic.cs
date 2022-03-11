namespace CsharpAdvanced.Keywords;

public class Dynamic
{
    private static ExampleClass _example = new();
   
    //The "dynamic" keyword is used to declare dynamic types - variables that can change types in runtime (like in js or python)
    //This can cause problem so should be used only when it is crucial
    //Performance of dynamic variables is worse, because at first the program needs to examine the type

    //Dynamic keyword can be used in cases when we obtain data form unknown source and at first we need to examine it

    // Define a dynamic field.  
    private dynamic? name;
    // Define a dynamic property.  
    private dynamic? NameProperty { get; set; }
    // Define a dynamic method with a dynamic parameter type.  
    public dynamic FullNameMethod(dynamic input)
    {
        dynamic firstname = input;
        string lastname = " Chand";

        return firstname + lastname;
    }

    //We can also declare the static and dynamic variable
    static dynamic? _dls;

    public static void InvokeDynamicExamples()
    {
        Dynamic dynamic1 = new();
        dynamic1.name = "Mahesh";

        dynamic dyno = dynamic1.FullNameMethod(dynamic1.name);

        dyno = 38; //dynamic becomes the int
        dyno = 44.95; //dynamic becomes the double
        dyno = true; //dynamic becomes the bool
        dyno = "Hello"; //dynamic becomes the string again
        dyno = DateTime.Now; //dynamic becomes the DateTime

        //Other use of dynamic keyword:

        _example.ExampleMethod("cls");
        _dls = new ExampleClass();
        _dls.ExampleMethod("dls");

        //As we can see, the exception will be thrown when the line below is executed. At compile time the compiler does not alert us about the possible error
        _dls.ExampleMethod(); 
    }

    public class ExampleClass
    {
        public void ExampleMethod(string test) => Console.WriteLine($"{test}");
    }
}

