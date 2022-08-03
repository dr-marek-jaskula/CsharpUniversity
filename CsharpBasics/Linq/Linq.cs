namespace CsharpBasics.Linq;

//This is s library that is dedicated for operation on collection
//This important to master at least some of the functionalities that ensures linq
//One of the main advantages of the linq library is harmonizes with Entity Framework Core, letting to translate formulas to SQL statements (make operation on db side)

//Many liq method allow to use predicates (delegates, that can be passed in using lambda expressions) to determine elements on which some operation should be done
//Linq method can be chained, so we can use one method after another, because mostly they return collection

public class Linq
{
    //let us introduce lits that will be use to demonstrate linq possibilities (classes ale defined below)
    public static List<Employee> Employees1 { get; set; } = new();
    public static List<Employee> Employees2 { get; set; } = new();
    public static List<Job> Jobs { get; set; } = new();

    public static void InvokeLinqExamples()
    {
        //First of all let us define some additional arrays (data)
        string[] possibleFirstNames = new string[] { "Dawid", "Mariola", "Maciej", "Zbigniew", "Katarzyna" };
        string[] possibleLastNames = new string[] { "Kowalski", "Nowak", "Pszczynski", "Wolski", "Murek" };

        string[] jobNames = new string[] { "Project manager", "C# Developer", "Tester" };
        string[] jobDescriptions = new string[] { "Lead a group of developers", "Test applications", "Design databases", "Design code architecture", "Do nothing" };

        #region SelectMany, Select

        //SelectMany
        //SelectMany method return a collection obtained from a given collection combined with other collection. Delegate can be used to obtain the expected result
        Employees1 = possibleFirstNames.SelectMany(lastNames => possibleLastNames, (firstName, lastName) => new Employee
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName
        }).ToList();

        //The following example shows, that from the Employee1 list, the list of chars is selected (char are from employee last names)
        var employeesLastNameLetters = Employees1.SelectMany(e => e.LastName).ToList();

        Jobs = jobNames.SelectMany(jobNames => jobDescriptions, (jobName, jobDescription) => new Job
        {
            Name = jobName,
            Description = jobDescription
        }).ToList();

        //Select
        //Select method return a collection obtained from a given collection, based on a delegate that determines the result
        Random random = new();
        var modifiedEmployeesName = Employees1.Select(e => e.FirstName + " Modification " + random.Next(10, 20).ToString()).ToList();
        var modifiedEmployees = modifiedEmployeesName.Select(m => new Employee(m, possibleLastNames[random.Next(0, 5)]));

        #endregion SelectMany, Select

        #region OrderBy i ThenBy

        //OrderBy is a method that is used to sort a collection in a given way. It returns a IOrderEnumerable collection

        IList<Student> StudentLinqList = new List<Student>() {
            new Student() { Id = 1, Name = "John", Age = 18 } ,
            new Student() { Id = 2, Name = "Steve",  Age = 15 } ,
            new Student() { Id = 3, Name = "Bill ",  Age = 25 } ,
            new Student() { Id = 4, Name = "Ram" , Age = 20 } ,
            new Student() { Id = 5, Name = "Ron" , Age = 19 },
            new Student() { Id = 6, Name = "Ron" , Age = 11 } ,
            new Student() { Id = 7, Name = "John" , Age = 71 }
            };

        //sort by name, ascending (alphabetically). Usually preferred syntax.
        var orderByMethod = StudentLinqList.OrderBy(s => s.Name);

        //Custom orderby: base on the first letter of the name, we make custom orderby
        var customOrderByMethod = StudentLinqList.OrderBy(student => "BJSR".IndexOf(student.Name.First()));

        //this gives the same result, however using other keyword syntax. Usually not preferred syntax (mb for joins)
        var orderByAnotherSyntax = from s in StudentLinqList orderby s.Name select s;
        //with Select
        var orderByAnotherSyntax2 = from s in StudentLinqList orderby s.Name select s.Id;

        //this is the same, because the by default the ascending order is selected
        var orderByAnotherWay3 = from s in StudentLinqList orderby s.Name ascending select s.Id;

        //descending
        var orderByDescending = StudentLinqList.OrderByDescending(s => s.Name);

        //descending by keyword syntax
        var orderByDescendingAnotherWay = from s in StudentLinqList orderby s.Name descending select s;

        //order by two criteria, keyword syntax
        var orderByDoubleAscending = from s in StudentLinqList orderby s.Name ascending, s.Id ascending select s;

        //Using method syntax, it has to be done as follows
        var orderByThenByAscending = StudentLinqList.OrderBy(s => s.Name).ThenBy(s => s.Id);

        //grouping, using keyword syntax. Grouping on common first character of a name. Results in a collection of collections
        var sortedGroups = from s in StudentLinqList
                           orderby s.Name, s.Id
                           group s by s.Name[0] into newGroup
                           orderby newGroup.Key
                           select newGroup;

        #endregion OrderBy i ThenBy

        #region Where, GroupBy

        //arrays with random data
        string[] FirstNameList = new string[] { "Marek", "Czarek", "Arek", "Darek", "Michał", "Paweł", "Andrzej", "Olek", "Arek", "Darek" };
        string[] LastNameList = new string[] { "Wilczynski", "Gromacki", "Wolski", "Mebel", "Broziak", "Adamczyk", "Ordi", "Nowy", "Ziemicki", "Rudzki" };

        //create a new list and then fill it with elements
        Employees2 = new List<Employee>();
        for (int i = 0; i < FirstNameList.Length; i++)
            Employees2.Add(new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = FirstNameList[i],
                LastName = LastNameList[i],
            });

        //Filtering by "where" method. For clarity, let us define the filter
        string filter = "Darek";
        var filteredEmployees2 = Employees2.Where(s => s.LastName.StartsWith("R") && s.FirstName == filter);

        //We can give two input parameters to the "Where" predicate: the first one is the element and the second the index
        var filterOnObjectAndItsIndex = Employees2.Where((item, index) => item.LastName.Length < index);

        //ForEach statement executes the lambda expression for each element of a list
        filteredEmployees2.ToList().ForEach(w => Console.WriteLine(w.FirstName + " " + w.LastName));

        //Filtering using keyword syntax
        var filtredEmployees2OtherSyntax = from s in Employees2 where s.LastName.StartsWith("R") && s.FirstName == filter select s;
        filtredEmployees2OtherSyntax.ToList().ForEach(w => Console.WriteLine(w.FirstName + " " + w.LastName));

        //Grouping data into collections. The following example group the data on common FirstName
        var groupedEmployees2 = Employees2.GroupBy(s => s.FirstName);

        //Grouping data by two criteria, using anonymous object
        var groupedEmployees3 = Employees2.GroupBy(s => new { s.FirstName, s.LastName });

        //Same results using keyword syntax
        var groupedEmployees3OtherSyntax = from s in Employees2 group s by new { s.FirstName, s.LastName } into newGroup select newGroup;

        //Chaining linq methods, at first we group data and then we filter the results
        //Any method examines if there is a element that satisfied the predicate
        var groupedEmployees4 = Employees2.GroupBy(s => s.FirstName).Where(s => s.Any(t => t.FirstName == "Darek"));

        #endregion Where, GroupBy

        #region Any, All

        //These two methods help us examine if the any element of the given collection satisfies the predicate, or if all elements of this collection satisfied the predicate

        var isAnyNameStartsWithA = Employees2.Any(s => s.FirstName.StartsWith("A"));

        var isAllNamesStartsWithA = Employees2.All(s => s.FirstName.StartsWith("A"));

        var groupedEmployees8 = Employees2.GroupBy(s => s.FirstName).Where(s => s.All(s => s.LastName.StartsWith("W")));

        #endregion Any, All

        #region Range, Concat, Union, Except, Intersect (set operations)

        //this method generates integer numbers from the given range and amount
        IEnumerable<int> firstEnumerable = Enumerable.Range(0, 5);
        IEnumerable<int> secondEnumerable = Enumerable.Range(3, 6);

        //Concat two collections into one collection
        IEnumerable<int> concatOfEnumerables = Enumerable.Concat(firstEnumerable, secondEnumerable);

        //Union returns a collection that is a sum of both, excluding duplicates (same are math union of sets)
        IEnumerable<int> unionOfEnumerables = Enumerable.Union(firstEnumerable, secondEnumerable);

        //Except method returns the collection that is a set difference of two collections
        IEnumerable<int> exceptOfEnumerables = Enumerable.Except(firstEnumerable, secondEnumerable);

        //Intersect is the set intersection
        IEnumerable<int> intersectOfEnumerables = Enumerable.Intersect(firstEnumerable, secondEnumerable);

        #endregion Range, Concat, Union, Except, Intersect (set operations)

        #region Contains, ElementAt

        //Contains method returns true if the element is the element of the collection
        if (Employees2.Contains(new Employee()))
            Console.WriteLine("New employee is in the collection");

        Random rand = new();

        int randomIndex = rand.Next(0, Employees2.Count());

        //ElementAt return the element at the given position
        Employee emp = Employees2.ElementAt(randomIndex);

        if (Employees2.Contains(emp))
            Console.WriteLine("This element is in the collection");

        #endregion Contains, ElementAt

        #region First, FirstOfDefault, Last, LastOrDefault, Single, SingleOrDefault

        //FirstOrDefault method returns the first element that satisfied the predicate or return null if no elements satisfies the predicate
        var firstOrDefaultEmployee = Employees2.FirstOrDefault(employee => employee.FirstName == "Arek");
        var firstOrDefaultEmployee2 = Employees2.FirstOrDefault(employee => employee.FirstName == "Bob");

        //FirstOrDefault with custom default
        var firstOrDefaultCustomDefault = Employees2
            .Select(empolyee => new { FirstName = empolyee.FirstName, LastName = empolyee.LastName } )
            .FirstOrDefault(e => e.FirstName == "Arek", new { FirstName = "Adam", LastName = "Kozloski "});

        //First method returns the first element that satisfied the predicate or throws an exception
        var firstEmployee = Employees2.First(employee => employee.FirstName == "Arek");

        //LastOrDefault method returns the last element that satisfied the predicate or return null if no elements satisfies the predicate
        var lastOrDefaultEmployee = Employees2.LastOrDefault(employee => employee.FirstName.Length > 3);

        //Last method returns the last element that satisfied the predicate or throws an exception
        var lastEmployee = Employees2.Last(employee => employee.FirstName.Length > 3);

        //SingleOrDefault method returns the element that satisfied the predicate if there ir only ONE such element, if there is no such elements then returns null, and if the is MORE then ONE element it throws an exception
        var singleOrDefaultEmployee = Employees2.SingleOrDefault(employee => employee.FirstName == "Arek");

        //Single method returns the element that satisfied the predicate if there ir only ONE such element, otherwise it throws an exception
        var singleEmployee = Employees2.Single(employee => employee.FirstName == "Arek");

        #endregion First, FirstOfDefault, Last, LastOrDefault, Single, SingleOrDefault

        //Other
        List<string> exampleStrings = new() { "One", "Two", "Never", "Super" };
        //Add "Best" as a last element
        exampleStrings.Append("Best");
        //Add "Zero as first element
        exampleStrings.Prepend("Zero");

        //Create a collection of two element arrays. It takes two first elements, then next two and so on
        var chunkedJobNames = jobNames.Chunk(2);
    }
}

#region Helpers

public class Employee
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.NewGuid();

    public Employee()
    {
    }

    public Employee(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

public class Job
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Job()
    {
    }

    public Job(string name, string description)
    {
        Name = name;
        Description = description;
    }
}

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }

    public Student()
    {
    }

    public Student(int id, string name, int age)
    {
        Id = id;
        Name = name;
        Age = age;
    }
}

#endregion Helpers