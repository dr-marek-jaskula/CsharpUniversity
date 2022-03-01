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
        //crate an object
        Citizen citizen = new Citizen(1, "Mark", "Kennedy", new(1992, 4, 20));
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

        /////////////////////////////////
        Duelist wybwoj1 = new Duelist();
        wybwoj1.Bash(2);

        //tworzymy obiekt na bazie interface
        //dużo zabawy z rzutowaniem
        ISwordAttack atakMieczydlem = (ISwordAttack)wybwoj1;
        atakMieczydlem.Cut(3); // dopiero teraz wie jakiej metody uzyc, bo takto jak to są metody jawnie podane to on nie wie
        atakMieczydlem.Feint(2);

        IHammerAtack atakMieczydlem2 = (IHammerAtack)wybwoj1;
        atakMieczydlem2.Crush(2);
        //dlatego najczęściej się implementuje interface niejawnie, zeby tak sie nie bawić. To jest obejście takiego samego nazewnictwa.

        Console.WriteLine("\n \n");
        Vehicle vehicle1 = new Vehicle("Buick", 4, 160);
        Console.WriteLine(vehicle1.Speed);
        Console.WriteLine(vehicle1.Brand);
        Console.WriteLine(vehicle1.Wheels);

        if (vehicle1 is IDerivable) //to znaczy czy jest zbudowany z klasy dziedziczącej dany interface
        {
            vehicle1.Move();
            vehicle1.Stop();
        }
        else
        {
            Console.WriteLine("the {0} cant be driven", vehicle1.Brand);
        }

        Console.WriteLine("\n \n");

        IElectronicDevice Tv1 = TvRemote.GetDevice(); //stworzenie obiektu telewizor, który ogranicza swoją funkcjonalność do metod zawartych w interface IElectronicDevice
        PowerButton powBut1 = new PowerButton(Tv1); //zaznaczyliśmy, że tylko obiekt typu interfacu IElectronicDevice (czyli Tv1 ok) ma dostęp do metod execute i undo ponieważ device w definicji interfacu jest typu interfacu ICommand
        powBut1.Execute();
        powBut1.Undo();
        // Tv1.VoiceFromTheShadowRealm(); //to nie zadziała

        Console.WriteLine("\n \n");

        Televistion Tv2 = TvRemote.GetDevice(); //stworzenie obiektu telewizor, który ma funkcjonalność metod zawarych w interface IElectronicDevice oraz w klasie Telewizor
        Tv2.VoiceFromTheShadowRealm();
        Tv2.On();

        Tv1.TestCSharp8MethodDefinitionInInterface();
        // Tv2.TestCSharp8MethodDefinitionInInterface(); //to nie zdziałą, ponieważ Tv2 nie odziedziczył interface'u
        //czyli albo bierze z interface'u i wtedy nie ma wlasnych, albo z wlasnych i nie ma tych funkcji, które zostały zdefiniowane w interface.

        PowerButton powBut2 = new PowerButton(Tv2); //można stworzyc nowy przycisk na bazie obiektu Television, ale potraktuje to przez pryzmat interface, ktory dalismy w definicji argumentu konstruktora PowerButton
        powBut2.Execute();
        powBut2.Undo();

        IElectronicDevice Tv3 = Tv2 as IElectronicDevice; //to działa, przypisuje nowemy Tv2 ale tylko oraniczone do interface IElectronicDevice, ma wiec rzeczy definiowane w IEletronicdevice i to co jest od niego dziedziczone, ale nie ma tego co jest w klasie Television
        Tv3.TestCSharp8MethodDefinitionInInterface();

        //Kolejny filmik nowy przerabiam o interface

        Axe axe1 = new Axe("Axe of the Destiny");
        axe1.Equip();
        axe1.TakeDamage(20);
        axe1.Sell();
        Console.WriteLine("\n");

        Spear spear1 = new Spear("Spear of the Meteor");
        spear1.Equip();
        spear1.TakeDamage(15);
        spear1.Sell();

        axe1.TurinIn();
        Console.WriteLine("\n\n\n");
        //create an inventory

        IItem[] inventory = new IItem[2]; // jest to tablica w obiektów, które dziedziczą z interface IIthem. Wszytko co mają to tylko to dziedziczone, ale nie bezpośrednio co ma klasa
        inventory[0] = axe1;
        inventory[1] = spear1;

        for (int i = 0; i < inventory.Length; i++)
        {
            IPartOfQuest questItem = inventory[i] as IPartOfQuest; //to oznacza tyle, że jeśli inventory element jest obiektem pochodzącym z klasy, która dziedziczy z interface'u IPartOfQuest, to zostaje transformowana w zmienną questItem. Jeśli natomiast nie jest to postawi null
            if (questItem != null)
            {
                questItem.TurinIn();
            }
            //interesujące, obiekt questItem nie istnieje poza tą pętlą. W pętli można tylko użyć metod z interface'u. To pewnie dlatego, że tutaj nie wie
            //questItem.TurinIn(); //napisanie tego tutaj zrobi wyjątek i bedzie crash pomimo ze poram nie podpowiada ze bedzie źle. Powód to taki, że obiekt questItem będzie null, ponieważ nie bedzie z IPartOfQuest, wiec zrobi sie null a null nie ma metody TurinIn
        }
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
    int Wheels { get; set; } //wtf zawiera włąsciwosć ale z getterem i setterm i nie ma prolemu?
    public double Speed { get; set; }

    void Move(); //one są bazowo abstract, czyli bez ciała, i bez argumentow

    void Stop();
}

internal class Vehicle : IDerivable
{
    public string Brand { get; set; }
    public int Wheels { get; set; }
    public double Speed { get; set; }

    public Vehicle(string brand, int wheels = 4, double speed = 50) // te równości powodują, że jeśli nie poda się nic to zostaną ustawione bazowe wartości, ale to przy pustym konstruktorze. Jeśli wywoła się inny konstruktor po zmiennych to ten co najbardziej pasuje. Można zrobić dwuznacznie co spowoduje błąd!!!!!
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
        this.Speed = 0;
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

    void TestCSharp8MethodDefinitionInInterface()
    {
        Console.WriteLine("so the definitions work in the interfaces now");
    } //mozna wiec definiowac o c# wersji 8 metody w interface, i mozna jest nadpisywac normalnie
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
        this.Volume = 50;
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
        if (Volume != 0) Volume--;
        Console.WriteLine($"TV volume is at the lvl {Volume}");
    }

    public void VolumeUp()
    {
        if (Volume != 100) Volume++;
        Console.WriteLine($"TV volume is at the lvl {Volume}");
    }

    public void VoiceFromTheShadowRealm()
    {
        Console.WriteLine("I see you, flesh one");
    }
}

internal class PowerButton : ICommand //dziedziczy z interface ICommand
{
    private IElectronicDevice device; //jest to właściwosć, która jest typu interface przez co posiada metody z interface (jest to jak obiekt zdefiniowany za pomocą interface, który otrzymuje metody.

    public PowerButton(IElectronicDevice device) //to mowi, że przypisuje argumentowi wczesniej zdefiniowany device typu interface
    {
        this.device = device;
    }

    public void Execute() //ze wzgledu na to, że device to właściwość (a bardziej obiekt) typu interface ma on metody z interface, ktorych moze uzywac
    {
        device.On();
    }

    public void Undo()
    {
        device.Off();
    }
}

internal class TvRemote //klasa do tworzenia obiektu telewizor, ale wartość metody jest typu interface wiec posiadająca metody interface
{
    public static Televistion GetDevice()
    {
        return new Televistion();
    }
}
#endregion 

#region Fifth example: Weapons

// Kolejny nowy filmik

internal interface IItem
{
    string name { get; set; }
    int goldValue { get; set; }

    void Equip();

    void Sell();
}

internal interface IDamagable
{
    int durability { get; set; } // zmusza naszą klase dziedziczonądo posiadania właściwosci durability (nie trzeba, ale można)

    void TakeDamage(int k); //dowolna nazwa zmiennej, ale ilosć zmiennych musi sie zgadzać w klasie z ktorej to dziedziczymy
}

internal class Axe : IItem, IDamagable, IPartOfQuest
{
    public string name { get; set; }
    public int goldValue { get; set; }
    public int durability { get; set; }

    public Axe(string name)
    {
        this.name = name;
        this.goldValue = 100;
        this.durability = 30;
    }

    public void Equip()
    {
        Console.WriteLine(name + " equipped");
    }

    public void Sell()
    {
        Console.WriteLine(name + " sold for " + goldValue + "dolars");
    }

    public void TakeDamage(int dmg)
    {
        durability -= dmg;
        Console.WriteLine(name + " damage by " + dmg + "it naw has a durability of " + durability);
    }

    public void TurinIn()
    {
        Console.WriteLine(name + " turned in");
    }
}

internal class Spear : IItem, IDamagable
{
    public string name { get; set; }
    public int goldValue { get; set; }
    public int durability { get; set; }

    public Spear(string name)
    {
        this.name = name;
        this.goldValue = 80;
        this.durability = 40;
    }

    public void Equip()
    {
        Console.WriteLine(name + " equipped");
    }

    public void Sell()
    {
        Console.WriteLine(name + " sold for " + goldValue + "dolars");
    }

    public void TakeDamage(int dmg)
    {
        durability -= dmg;
        Console.WriteLine(name + " damage by " + dmg + "it naw has a durability of " + durability);
    }
}

internal interface IPartOfQuest
{
    void TurinIn();
}

#endregion