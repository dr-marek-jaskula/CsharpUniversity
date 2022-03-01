namespace CsharpBasics.Introduction;

//Aim of this file is to introduce the most important collections in c#: List, Dictionary, Stack, Queue.
//Moreover, we will cover the topic of Arrays.

public class Lists
{
    //list is a collection of element of certain type

    public static void InvokeListExamples()
    {
        //create empty list and then add elements
        List<int> integerList = new();
        integerList.Add(3);
        integerList.Add(5);
        integerList.Add(60);
        integerList.Add(600);
        integerList.Add(6000);

        integerList.RemoveAt(2); //removes the element with index 2
        integerList.RemoveAll(x => x > 3); // removes all element that satisfies predicate
        integerList.Remove(1); //removes the first element equal to 1
        integerList.RemoveRange(1, 2); //remove elements starting from index 1 (including this index), and removing two elements

        //create list and fill it with elements
        List<string> stringList = new() { "FirstElement", "SecondElement", "ThirdElement", "SecretElement" };
        
        //to get the number of elements in the list, we use Count property
        int numberOfElements = stringList.Count;

        //to iterate though list elements we can use foreach loop (object has to be iterable, to use foreach, list is iterable)
        foreach (string element in stringList)
            Console.WriteLine(element);

        //in order to create the list with elements of different types, we can
        List<object> objectList = new();
        objectList.Add(1);
        objectList.Add("John");
        objectList.Add('f');
        objectList.Add(new List<int>() { 1, 2, 44 });
    }
}

public class Dictionaries
{
    //dictionary is a collection of key-value pair

    public static void InvokeDictionaryExamples()
    {
        //create empty dictionary and then add elements
        Dictionary<string, int> firstDictionary = new();
        firstDictionary.Add("Salary", 2000);
        firstDictionary.Add("Age", 33);
        firstDictionary.Add("Hight", 187);
        firstDictionary["NewItem"] = 100; //other way to add element to dictionary

        //create dictionary and fill it with elements
        Dictionary<string, string> cities = new()
        {
            { "UK", "London, Manchester, Birmingham" },
            { "USA", "Chicago, New York, Washington" },
            { "India", "Mumbai, New Delhi, Pune" }
        };

        Console.WriteLine(cities["UK"]); //prints value of UK key
        Console.WriteLine(cities["USA"]);//prints value of USA key

        //to get the number of elements in the list, we use Count property
        int numberOfElements = cities.Count;

        //to iterate though list elements we can use foreach loop (object has to be iterable, to use foreach, list is iterable)
        foreach (var element in cities)
            Console.WriteLine($"{element.Key} {element.Value}");
    }
}

public class Arrays
{
    //Arrays are fixed length objects that contains multiple variables
    //Arrays can be one dimensional or multidimensional

    public static void InvokeArrayExamples()
    {
        //create string from a char array
        string stringFromCharArray = new string("University".ToCharArray());
        Console.WriteLine(stringFromCharArray.Length);
        Console.WriteLine($"{stringFromCharArray[0]} is the first character, then its {stringFromCharArray[1]}");

        //Declare a one dimensional array of 5 integers.
        int[] array1 = new int[5]; //length of the array is specified and it cant change
        array1[0] = 3;
        array1[4] = 19;
        Console.WriteLine(array1[0]);
        Console.WriteLine(array1[1]);
        Console.WriteLine(array1[4]);

        Console.WriteLine(Environment.NewLine);

        //Create an array and set element values.
        int[] array2 = new int[] { 1, 3, 5, 7, 9 }; //the length is not necessary to specify, because it is obtained from the right hand side 
        Console.WriteLine(array2[4]);

        // Alternative syntax.
        int[] array3 = { 1, 2, 3, 4, 5, 6 }; 

        // Create a two dimensional array.
        int[,] multiDimensionalArray1 = new int[2, 3];
        multiDimensionalArray1[1, 1] = 5;
        multiDimensionalArray1[1, 2] = 6;
        multiDimensionalArray1[0, 0] = 14;
        Console.WriteLine(multiDimensionalArray1[1, 1]);
        Console.WriteLine(multiDimensionalArray1[0, 0]);
        Console.WriteLine(multiDimensionalArray1[1, 2]);

        Console.WriteLine("\n\n");

        // Create and set array element values.
        int[,] multiDimensionalArray2 = { { 1, 2, 3 }, { 4, 5, 6 } };
        Console.WriteLine(multiDimensionalArray2[0, 1]);
        Console.WriteLine(multiDimensionalArray2[1, 2]);
        Console.WriteLine(multiDimensionalArray2[0, 2]);

        Console.WriteLine(Environment.NewLine);

        // Create a jagged array
        int[][] jaggedArray = new int[4][]; //its an array containing 4 one dimensional arrays

        // Set the values of the first array of the jagged array structure.
        jaggedArray[0] = new int[4] { 1, 2, 3, 4 }; 

        Console.WriteLine(jaggedArray[0][3]);
        //Every elements of the jaggedArray must be declared before the array can be used
        jaggedArray[1] = new int[5];
        jaggedArray[2] = new int[] { 0, 2, 4, 6 };
        jaggedArray[3] = new int[] { 1, 2, 3, 4, 5, 6 };

        Console.WriteLine(jaggedArray[1][2]);
        Console.WriteLine(jaggedArray[2][3]);
        Console.WriteLine(jaggedArray[3][5]);

        Console.WriteLine(Environment.NewLine);

        // Other example of jagged array.
        int[,][] jaggedArray2 = new int[2, 2][]; 

        // Set the values of arrays in the jagged array structure.
        jaggedArray2[0, 0] = new int[4] { 1, 2, 3, 4 };
        jaggedArray2[0, 1] = new int[5];
        jaggedArray2[1, 0] = new int[] { 0, 2, 4, 6 };
        jaggedArray2[1, 1] = new int[] { 1, 2, 3, 4, 5, 6 };

        Console.WriteLine(jaggedArray2[0, 1][1]);
        Console.WriteLine(jaggedArray2[1, 0][2]);
        Console.WriteLine(jaggedArray2[1, 1][4]);
    }
}

public class Stacks
{
    //Stack is a collection in which added element becomes the first element of the collection

    public static void InvokeQueueExamples()
    {
        Stack<int> integerStack = new();
        integerStack.Push(36); 
        integerStack.Push(64); 
        integerStack.Push(11); 
        Console.WriteLine(integerStack.Peek()); //returns the object at the top of the stack
        Console.WriteLine(integerStack.Pop()); //removes and returns the object at the top of the stack
    }
}

public class Queues
{
    //Queue is a collection in which added element becomes the last element of the collection

    public static void InvokeQueueExamples()
    {
        Queue<int> integerQueue = new();
        integerQueue.Enqueue(1); 
        integerQueue.Enqueue(2);
        integerQueue.Enqueue(3);

        Console.WriteLine(integerQueue.Peek()); //returns the object at the beginning of the queue
        Console.WriteLine(integerQueue.Dequeue()); //removes and returns the object at the beginning of the queue
    }
}