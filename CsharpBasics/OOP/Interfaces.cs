namespace CsharpBasics.OOP;

internal class Interfaces
{
    //interface is an abstraction that can be implemented by class and structure.
    //Naming interface we should start with capital letter 'I'. For instance: "IReadable"
    //Multiple interfaces can be implemented by one class/structure
    //Interfaces generally does not contain any logic (since c# 8. 0 they, can, but the default implementation is not crucial, it is mostly redundant)
    //Interfaces are used for Dependency Injection and Dependency Inversion!
    //Interlaces provide the outline of a functionality that some class/struct possess
    
    //For more information about Dependency Injection go to "CsharpAdvaced"

    //Great example is the example of "IDispose" interface. 
    //All entities that implements "IDispose" will execute their "Dispose" method when the current scoped is abandoned
    
    //Interfaces can be also used as a markers: an empty interface just to mark the a class/struct.

    //Interfaces members cannot be "private" and by default they are "public"
    //Interfaces can only store methods and properties

    public static void InvokeInterfacesExamples()
    {
        #region First Example (single implementation)
        //crate an object
        Citizen citizen = new(1, "Mark", "Kennedy", new(1992, 4, 20));
        //use the function that is present in the interface
        citizen.Calculate();

        //explicitly cast the citizen to interface
        ITaxCalculator taxCalculator = citizen;
        //the only functionality is the "Calculate" method now.
        taxCalculator.Calculate();

        Politician politician = new("President", new(1987, 10, 12));
        politician.Calculate();
        ITaxCalculator taxCalculator1 = politician;
        //now we have two casted to ITaxCalculator object.
        //The only knowledge we have about them now is that they can use the "Calculate" method (and we can use this method)
        //However, this is the only information we need to know -> use interfaces to provide (inform about) the sufficient functionalities of the casted object (more in DI)
        #endregion

        #region Second Example (multiple implementation)
        //Now, let us consider the example with multiple interfaces (two of them have method with the same method)
        Duelist duelist = new(3, 5, 1);
        duelist.Cut(2);
        duelist.Bash(3);
        // duelist.Feint(2); //cant be used

        //we cast duelist to ISwordAttack
        ISwordAttack duelistSwordAttack = duelist;
        duelist.Cut(3);
        duelistSwordAttack.Feint(2); //possible to use

        IHammerAtack duelistHammerAttack = duelist;
        duelistHammerAttack.Crush(2);
        #endregion

        #region Third Example 
        Vehicle vehicle1 = new("Buick", 4, 160);
        Console.WriteLine(vehicle1.Speed);
        Console.WriteLine(vehicle1.Brand);
        Console.WriteLine(vehicle1.Wheels);

        if (vehicle1 is IDerivable derivable)
        {
            derivable.Move();
            derivable.Stop();
        }
        else
            Console.WriteLine("the {0} cant be driven", vehicle1.Brand);
        #endregion

        #region Fourth Example (interesting one)

        IElectronicDevice Tv1 = TvRemote.GetDevice(); //stworzenie obiektu telewizor, który ogranicza swoją funkcjonalność do metod zawartych w interface IElectronicDevice
        PowerButton powBut1 = new PowerButton(Tv1); //zaznaczyliśmy, że tylko obiekt typu interfacu IElectronicDevice (czyli Tv1 ok) ma dostęp do metod execute i undo ponieważ device w definicji interfacu jest typu interfacu ICommand
        powBut1.Execute();
        powBut1.Undo();
        // Tv1.VoiceFromTheShadowRealm(); //to nie zadziała

        Console.WriteLine("\n \n");

        Televistion Tv2 = TvRemote.GetDevice(); //stworzenie obiektu telewizor, który ma funkcjonalność metod zawarych w interface IElectronicDevice oraz w klasie Telewizor
        Tv2.VoiceFromTv();
        Tv2.On();

        Tv1.MethodWithDefaultImplementation();
        // Tv2.TestCSharp8MethodDefinitionInInterface(); //to nie zdziałą, ponieważ Tv2 nie odziedziczył interface'u
        //czyli albo bierze z interface'u i wtedy nie ma wlasnych, albo z wlasnych i nie ma tych funkcji, które zostały zdefiniowane w interface.

        PowerButton powBut2 = new PowerButton(Tv2); //można stworzyc nowy przycisk na bazie obiektu Television, ale potraktuje to przez pryzmat interface, ktory dalismy w definicji argumentu konstruktora PowerButton
        powBut2.Execute();
        powBut2.Undo();

        IElectronicDevice Tv3 = Tv2 as IElectronicDevice; //to działa, przypisuje nowemy Tv2 ale tylko oraniczone do interface IElectronicDevice, ma wiec rzeczy definiowane w IEletronicdevice i to co jest od niego dziedziczone, ale nie ma tego co jest w klasie Television
        Tv3.MethodWithDefaultImplementation();

        #endregion
    }
}

#region First Example: Citizen and Politician

//Simple interface
public interface ITaxCalculator
{
    //by default this member is public
    //This is only a declaration of a parameterless function that return an integer
    double Calculate();
}

internal class Citizen : ITaxCalculator
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly Birthday { get; set; }

    public Citizen()
    {
    }

    public Citizen(int id, string firstName, string lastName, DateOnly birthday)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Birthday = birthday;
    }

    //implementing interface method and defining its behavior for this class
    public double Calculate()
    {
        Console.WriteLine("Calculating tax common citizen...");
        return 0.19;
    }
}

internal class Politician : ITaxCalculator
{
    public string Status { get; set; } = string.Empty;
    public DateOnly Birthday { get; set; }

    public Politician()
    {
    }

    public Politician(string status, DateOnly birthday)
    {
        Status = status;
        Birthday = birthday;
    }

    //implementing interface method and defining its behavior for this class (other body then for citizen)
    public double Calculate()
    {
        Console.WriteLine("Calculating tax for politician...");
        return 0.10;
    }
}

#endregion

#region Second example: Duelist

internal interface ISwordAttack
{
    void Slice(int enemyResistance);
    void Cut(int enemyResistance);

    //the third method will be common for ISwordAttack and IHammerAtack
    void Feint(int enemyResistance);

    //The method with the default implementation
    void DefaultSwordAttack(int enemyResistance)
    {
        Console.WriteLine("Deal 1 damage");
    }
}

internal interface IHammerAtack
{
    void Bash(int enemyResistance);
    void Crush(int enemyResistance);

    //the third method will be common for ISwordAttack and IHammerAtack
    void Feint(int enemyResistance);  

    void DefaultHammerAttack(int enemyResistance)
    {
        Console.WriteLine("Deal 1 damage");
    }
}

//the method with the default implementation does not need to be specified in the class but it can be
internal class Duelist : ISwordAttack, IHammerAtack
{
    public string WeaponInRigthHand { get; set; } = "Hammer";
    public string WeaponInLeftHand { get; set; } = "Sword";
    public int Dexterity { get; set; }
    public int Strength { get; set; }
    public int Intelligence { get; set; }

    public Duelist(int dexterity, int strength, int intelligence)
    {
        Dexterity = dexterity;
        Strength = strength;
        Intelligence = intelligence;
    }

    public void Bash(int enemyResistance)
    {
        Console.WriteLine($"Deal {Strength - enemyResistance/2} damage");
    }

    public void Crush(int enemyResistance)
    {
        Console.WriteLine($"Deal {Strength/2} damage");
    }

    public void Slice(int enemyResistance)
    {
        Console.WriteLine($"Deal {Strength + Dexterity - enemyResistance} damage");
    }

    public void Cut(int enemyResistance)
    {
        Console.WriteLine($"Deal {3/2 * Dexterity - enemyResistance} damage");
    }

    //Due to the fact there are two methods of the same name from two different interface that this class implement
    //We need to specify both in the following manner
    void ISwordAttack.Feint(int enemyResistance) 
    {
        Console.WriteLine($"Deal {Intelligence + Dexterity - enemyResistance} damage");
    }

    void IHammerAtack.Feint(int enemyResistance)
    {
        Console.WriteLine($"Deal {Intelligence + Strength - enemyResistance} damage");
    }
}

#endregion

#region Third example: Vehicle

internal interface IDerivable
{
    int Wheels { get; set; } 
    public double Speed { get; set; }
    void Move(); 
    void Stop();
}

internal class Vehicle : IDerivable
{
    public string Brand { get; set; }
    public int Wheels { get; set; }
    public double Speed { get; set; }

    public Vehicle(string brand, int wheels = 4, double speed = 50)
    {
        Brand = brand;
        Wheels = wheels;
        Speed = speed;
    }

    public void Move()
    {
        Console.WriteLine($"The {Brand} Moves Forward at {Speed} MPH");
    }

    public void Stop()
    {
        Console.WriteLine($"The {Brand} stops");
        Speed = 0;
    }
}

#endregion

#region Fourth example: TV

internal interface IElectronicDevice
{
    void On();

    void Off();

    void VolumeUp();

    void VolumeDown();

    void MethodWithDefaultImplementation()
    {
        Console.WriteLine("so the definitions work in the interfaces now");
    } 
}

internal interface ICommand
{
    void Execute();

    void Undo();
}

internal class Televistion : IElectronicDevice
{
    public int Volume { get; set; }

    public Televistion()
    {
        Volume = 50;
    }

    public void Off()
    {
        Console.WriteLine("Tv is off");
    }

    public void On()
    {
        Console.WriteLine("Tv is on");
    }

    public void VolumeDown()
    {
        if (Volume is not 0) 
            Volume--;
        Console.WriteLine($"TV volume is at the lvl {Volume}");
    }

    public void VolumeUp()
    {
        if (Volume is not 100) 
            Volume++;
        Console.WriteLine($"TV volume is at the lvl {Volume}");
    }

    public void VoiceFromTv()
    {
        Console.WriteLine("I see you");
    }
}

internal class PowerButton : ICommand 
{
    private readonly IElectronicDevice _device; //the private field to store the dependency

    public PowerButton(IElectronicDevice device) //we inject the dependency (clue of Dependency Injection)
    {
        _device = device;
    }

    //These two method use the injected dependency

    public void Execute() 
    {
        _device.On();
    }

    public void Undo()
    {
        _device.Off();
    }
}

internal class TvRemote //a TV "factory"
{
    public static Televistion GetDevice()
    {
        return new Televistion();
    }
}

#endregion 

