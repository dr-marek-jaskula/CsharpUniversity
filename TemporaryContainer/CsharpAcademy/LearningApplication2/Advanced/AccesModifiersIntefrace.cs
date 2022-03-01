using System;

namespace LearningApplication2
{
    internal class AccesModifiersIntefrace
    {
        public static void Podstawy6()
        {
            Warrior warrior1 = new Warrior();
            warrior1.SetBirthdate(new DateTime(1932, 3, 2));
            Console.WriteLine(warrior1.GetBirthdate());

            WybitnyWojownik wybwoj1 = new WybitnyWojownik();
            wybwoj1.HammerAtackken(2);

            //tworzymy obiekt na bazie interface
            //dużo zabawy z rzutowaniem
            ISwordAtack atakMieczydlem = (ISwordAtack)wybwoj1;
            atakMieczydlem.SwordAtakowanko(3); // dopiero teraz wie jakiej metody uzyc, bo takto jak to są metody jawnie podane to on nie wie
            atakMieczydlem.Sorko(2);

            IHammerAtack atakMieczydlem2 = (IHammerAtack)wybwoj1;
            atakMieczydlem2.HammerAtakowanko(2);
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

        //OD momenty c# wersji 8 w interface mozna definiować metody, które potem dziedziczy klasa, aczkolwiek jesli klasa ma juz taką metode to jej własna nadpisuje dziedziczoną
        //dziedziczone metody z interface musza miec taką samą nazwę i tyle samo zmiennych tego samego typu
        //wszystkie dziedziczone metody z interface są public i static
    }

    internal class Warrior
    {
        private string _name; //nie będzie widoczne poza klasą, więc sie nie wywoła w "Podstawy6".
        //!!! tutaj specyfika nazywania właściwości prywatnych!!!! najpierw podłoga a potem camelCase

        private DateTime _birthdate; //zmienna typu czasowego

        public void SetName(string name) // jest to tak zwana "set" method. Dzięki poniższemu zabezpieczeniu mozem miec logike tam gdzie wczesniej jej niebylo przy ustalania właściwości
        {
            if (!String.IsNullOrEmpty(name)) this._name = name; //to robi to, że jeśli string nie jest nullem albo niczym, takie zabezpieczonko
        }

        public string GetName() // jest to tak zwana "get" method
        {
            return _name;
        }

        public void SetBirthdate(DateTime birthdate)
        {
            _birthdate = birthdate; //tutaj nie trzeba this. bo jest prywatne i widzi.
        }

        public DateTime GetBirthdate()
        {
            return _birthdate;
        }
    }

    public interface ITaxCalculator //w .Not wszystkie interfaces zaczynają sie od litery I. Podobnie jak klasy, inferface nie ma implementacji
    {
        // interface "member" nie mają access modifiers!! ponieważ bazowo wszystkie są public
        //interfacy sa po to aby miec luźny dostęp do roznych rzeczy i zmiana interfacu nie wplywa za bardzo na inne rzeczy. Luźna zależność.

        int Calculate(); // deklaracja metody, nie ma tu kodu!! przez co nie zmieni czegos od czeg ozalezy interface
    }

    // tutaj w dół po ogarnieciu dziedziczenia robie

    // interface jest po to aby dziedziczyć z nich bo z wielu można

    //NOWY obiekt można tworzyć na bazie interface

    internal interface ISwordAtack// mogą przechowywać metody ale nie zmienne!!!
    {
        // int abs;  // nie da sie! bo nie mogą mieć właściwości, chyba ze z get; i set; Ponieważ to w nowszej wersji c# zmienili i mozna
        void SwordAtackken(int pkt); //domyślnie wszystkie metody w interface są public, bo inaczej byłby bezużyteczny.

        void SwordAtakowanko(int ptk);

        void Sorko(int k);
    }

    internal interface IHammerAtack// mogą przechowywać metody ale nie zmienne!!!
    {
        // int abs;  // nie da sie! bo nie mogą mieć właściwości
        void HammerAtackken(int pkt); //domyślnie wszystkie metody w interface są public, bo inaczej byłby bezużyteczny.

        void HammerAtakowanko(int ptk);

        void Sorko(int k);
    }

    internal class WybitnyWojownik : Warrior, ISwordAtack, IHammerAtack //podkreśla tutaj ponieważ interface zachowują się jak metody czysto abstrakcyjne bo nie mają ciała. Musimy!! Musimy! zaimplementować wszystkie metody w klasie jakie są w interface
    {
        public void HammerAtackken(int pkt)
        {
            Console.WriteLine("zadaje 2 brazenia");
        }

        public void HammerAtakowanko(int ptk)
        {
            Console.WriteLine("zadaje 5 brazenia");
        }

        public void SwordAtackken(int pkt)
        {
            Console.WriteLine("zadaje 23 brazenia");
        }

        public void SwordAtakowanko(int ptk)
        {
            Console.WriteLine("zadaje 221 brazenia");
        }

        void ISwordAtack.Sorko(int k) // to jest implementacja jawna, jesli nazwa metody jest taka sama w paru interfacach (ctry + .) i tam uzupełnianie z interface
        {
            Console.WriteLine("yooolo");
        }

        void IHammerAtack.Sorko(int k)
        {
            Console.WriteLine("mohohooh");
        }
    }

    //odtąd robie z nowego filmiku o interface

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

    // dalej nowe
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
}