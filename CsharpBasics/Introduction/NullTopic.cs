namespace CsharpBasics.Introduction;

internal class NullTopic
{
    public static void InvokeNullableOperatorsExample()
    {
        //Nullable operator: '?'
        
        //1) When used after the type it says that the variable can be set to null
        bool? firstNullableOperator;

        //proof
        firstNullableOperator = null;
        firstNullableOperator = false;

        var helper = new HelperClass() {  Property1 = "Prop" };

        //proper null check
        if (helper.Property1 is null)
            Console.WriteLine("This field is a null field:" + helper.Property1);

        //2) When used before the '.' it says that if the helper object is null return null, but if it is not null then return "Property1
        var valueOfProperty1 = helper?.Property1;

        //proof
        HelperClass? nullHelper = null;
        var nullValue =  nullHelper?.Property1;

        //3) When used after the boolean value, it behaves similar to the if statement: if true, then return the result after '?', otherwise return result alfter ':'
        string? bar = helper is null ? null : helper.Property1;

        //Operator '!' after the object says that we are sure that the property is not null (we take the responsibility)
        //var nullValue2 = nullHelper!.Property1; //this would throw an exception

        //Operator "??":
        //The null-coalescing operator ?? returns the value of its left-hand operand if it isn't null;
        //otherwise, it evaluates the right-hand operand and returns its result. 

        var result = nullHelper ?? new HelperClass() { Property1 = "Will be used" };
        var result2 = helper ?? new HelperClass() { Property1 = "Will not be used" };

        //The ?? operator doesn't evaluate its right-hand operand if the left-hand operand evaluates to non-null.
        var result3 = helper ?? SomeMethod();

        //Available since C# 8.0, the null-coalescing assignment operator "??=":
        //assigns the value of its right-hand operand to its left-hand operand only if the left-hand operand evaluates to null.
        var someString = (nullHelper ??= new HelperClass() { Property1 = "Not null one" }).Property1;

        //The ??= operator doesn't evaluate its right-hand operand if the left-hand operand evaluates to non-null.
        var someString2 = (helper ??= SomeMethod())!.Property1;
    }

    private static HelperClass? SomeMethod()
    {
        Console.WriteLine("Some text");
        return new HelperClass() { Property1 = "Some property" };
    }
}
internal class HelperClass
{
    public string? Property1 { get; set; }
}
