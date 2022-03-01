using System.Runtime.InteropServices;

namespace CsharpBasics.Introduction;

//Guid is a struct 
//GUID stands for Global Unique Identifier.
//A GUID is a 128-bit integer (16 bytes) that you can use across all computers and networks wherever a unique identifier is required.
//Therefore, there are 2^122

//In order to create a Guid we should use the method "NewGuid"

//GuidAttribute attribute is typically used in an application to expose a type to COM
// Guid for the interface IMyInterface. (explicitly set the guid), to 
[Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4")]
interface IGUID
{
    void MyMethod();
}

// Guid for the class MyTestClass.
[Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8")]
public class GUID : IGUID
{
    public void MyMethod() 
    { 
    }

    public static void InvokeGuidExamples()
    {
        //common way to create a guid
        Guid id = Guid.NewGuid();
        Console.WriteLine(id.ToString());

        //examine the guid attribute
        GuidAttribute IMyInterfaceAttribute = (GuidAttribute)Attribute.GetCustomAttribute(typeof(IGUID), typeof(GuidAttribute));

        System.Console.WriteLine("IMyInterface Attribute: " + IMyInterfaceAttribute.Value);

        // Use the string to create a guid.
        Guid myGuid1 = new Guid(IMyInterfaceAttribute.Value);

        // Use a byte array to create a guid.
        Guid myGuid2 = new Guid(myGuid1.ToByteArray());

        //Other way
        Guid myGuid3 = Guid.Parse("0b214de7-8958-4956-8eed-28f9ba2c47c6");

        if (myGuid1.Equals(myGuid2))
            System.Console.WriteLine("myGuid1 equals myGuid2");
        else
            System.Console.WriteLine("myGuid1 does not equal myGuid2");

        // Equality operator can also be used to determine if two guids have same value.
        if (myGuid1 == myGuid2)
            System.Console.WriteLine("myGuid1 == myGuid2");
        else
            System.Console.WriteLine("myGuid1 != myGuid2");
    }
}
