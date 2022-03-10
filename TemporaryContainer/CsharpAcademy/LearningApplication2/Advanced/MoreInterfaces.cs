using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LearningApplication2
{
    class MoreInterfaces
    {
        //Interesting thing about interfacer:
        public void InterestingThing(ILogger logger)
        {
            if (logger is FileLogger fileLogger) //to nie tworzy nowej zmiennej, a tylko zmienia jej nazwe, czyli logger = fileLogger (jest to efektywne)
            {
                fileLogger.LogMessage("Log me thunder", LogLevel.Criteria); 
            }

            else if (logger is ConsoleLogger consoleLogger)
            {
                consoleLogger.LogMessage("Log me koko", LogLevel.Debug);
            }
        }


        public static void Program11()
        {
            TestingClass testingObject1 = new TestingClass("Bohater");
            testingObject1.TestingHammerAtack();
            testingObject1.TestingSwordAtack();
            //Wszystkie dziedziczone metody z interfacu są bazowo static
            Console.WriteLine(testingObject1.TestingName);

            ITestingSword testingObject2 = new TestingClass();
            testingObject2.TestingCommonMethod();

            ITestingHammer testingObject3 = new TestingClass();
            testingObject3.TestingCommonMethod();

            //bardzo interesujące. Dziedzicząc z interfaców klasa tworzy obiekty, które mogą wywoływać methody tylko nie ze spornych (czyli nie explicit defined) metod z interfaców. Ze spornych nie mogę, chyba, że zostaną typowo pod to zbudowane i wtedy mogą korzystać ze wszystkich metod z danego interfacu, ale nie mogą korzystac z metod z innych interfaców 

            ITestingSword testingObject4 = testingObject1 as ITestingSword; //tworzy nowy obiekt od razu tak jak obiekt 1 ale pod interface ITestingSword
            ITestingHammer testingObject5 = new TestingClass() as ITestingHammer; //tworzy nowy obiekt testingObject5 taki jak pusty obiekt z TestingObject pod klasę ITestingHammer

            //sposob wywołania method z interfacó w explicid implementation
            Console.WriteLine("\n\n");
            (testingObject1 as ITestingHammer).TestingCommonMethod(); // w ten sposób można dawać metode, traktując obiekt 1 chwilowo jak z interfaceu konkretnego bo potem juz nie jest
            testingObject1.TestingSwordAtack();

            //mozna jedne explicit a drugie nie.
            Console.WriteLine("\n\n");

            (testingObject1 as ITestingSpear).TestingSpearAtack(); //czyli do zdefiniowanych metod w interfacach jest tak jakby byly explicid zaimplementowanem wiec trzeba zrobic obiekt bepzposrednio pod interface


            testingObject1.TestingDziewiczenieMethod();
            testingObject1.TestingDziewiczenieMethod2();


            Console.WriteLine(testingObject1.TestingHpView);
            Console.WriteLine(testingObject1.TestingName); // biorąc obiekt pod interface on nie ma właściwości


            (testingObject1 as ITestingHammer).TestingCommonMethod();
            Console.WriteLine(testingObject1.TestingHpView);
            
            //zatem wykorzystuje tylko w tym momencie ten interface, ale obiekt dlaej jest obiektem.
            Console.WriteLine("\n\n\n");
            TestingDziewiczenieIntercafe dziewiczenieTestingObject1 = new TestingDziewiczenieIntercafe("Great Hero of Magic",2);
            Console.WriteLine(dziewiczenieTestingObject1.DziedziczenieName);
            Console.WriteLine(dziewiczenieTestingObject1.DziewiczeniePower);

            dziewiczenieTestingObject1.TestingDziewiczenieMethod();
            Console.WriteLine(dziewiczenieTestingObject1.TestingName);
            Console.WriteLine(dziewiczenieTestingObject1.TestingHpView);
            dziewiczenieTestingObject1.TestingSwordAtack();
            dziewiczenieTestingObject1.TestingDziewiczenieMethod2();
            dziewiczenieTestingObject1.TestingHammerAtack();
            (dziewiczenieTestingObject1 as ITestingHammer).TestingCommonMethod();
            (dziewiczenieTestingObject1 as ITestingSword).TestingCommonMethod();
            Console.WriteLine(dziewiczenieTestingObject1.TestingHpView);
            (dziewiczenieTestingObject1 as ITestingSpear).TestingSpearAtack();
            //dziedziczy wszytko na bazowych ustawieniach



            //Teraz nowe odnośnie tych console Logger
            var logger = GetLogger();
            ILogger logger2 = new ConsoleLogger();

            Console.WriteLine("tu nie powie");
            logger.LogMessage("Say something1 verbose");
            Console.WriteLine("tu powie");
            logger2.LogMessage("Say something", LogLevel.Debug);

            ILogger f1 = new FieldInObjectCutToInterface(); //tworze obiekt "obcięty" do interface'u ILogger. 
            Console.WriteLine(f1.logLevel); //Dzięki temu użyje się przypisanie występujące w 103 lini, czyli LogLevel ILogger.logLevel => LogLevel.Criteria;
        }

        static ILogger GetLogger()
        {
            //return new FileLogger(@"D:\tsy.txt"); //dopierusje na koncu

            return new ConsoleLogger
            {
                logLevel = LogLevel.Debug
            };

        }

    }

    class FieldInObjectCutToInterface : ILogger
    {

        public LogLevel logLevel { get; set; }

        LogLevel ILogger.logLevel => LogLevel.Criteria;

        public void LogMessage(string message, LogLevel level = LogLevel.Verbose)
        {
            Console.WriteLine("Im loggin in {0}", level);
        }
    }

    interface ITestingSword
    {
        void TestingSwordAtack();
        void TestingCommonMethod();
    }

    interface ITestingHammer
    {
        void TestingHammerAtack();
        void TestingCommonMethod();
    }

    interface ITestingSpear : ITestingDziewiczenie, ITestingDziewiczenie2 //dziedziczy z dwóch innych interfaców
    {
        void TestingSpearAtack()
        {
            Console.WriteLine("Spear Atack!!!"); //jest to automatycznie dziedziczone jesli jest tutaj zdefiniowane
        }
    }

    class TestingClass : ITestingSword, ITestingHammer, ITestingSpear
    {
        public string TestingName;
        private int TestingHp;
        public int TestingHpView { get { return TestingHp; }  }

        public static void TestingUniqueMethod()
        {
            Console.WriteLine("my secred atack");
        }
        public TestingClass(string TestingName = "jak nic to zomo")
        {
            this.TestingName = TestingName;
            this.TestingHp = 100;
        }

        void ITestingHammer.TestingCommonMethod()
        {
            Console.WriteLine("Test of Common Method from Hammer");
            this.TestingHp -= 30;
        }

        void ITestingSword.TestingCommonMethod() //to nie musi być publiczne bo bezpośrednio zabiera z interfacu, a tam są publiczne
        {
            Console.WriteLine("Test of Common Method from Sword");
            this.TestingHp -= 11;
        }

        public void TestingHammerAtack() //to musi być publiczne bo inaczej nie pobierze, gdyż to nadpisuje metode z interfacu
        {
            Console.WriteLine("Test of Hammer Atack 10dmg");
            this.TestingHp -= 10;
        }
        public void TestingSwordAtack()
        {
            Console.WriteLine("Test of Sword Atack 13dmg");
            this.TestingHp -= 13;
        }

        public void TestingDziewiczenieMethod()
        {
         Console.WriteLine("Dziewiczenie 1 obtainted");
        }

        public void TestingDziewiczenieMethod2()
        {
         Console.WriteLine("Dziewiczenie 2 obtainted");
        }
    }

    //teraz zobaczymy, ze interface moze dziedziczyc z interfacu

    interface ITestingDziewiczenie
    {
        void TestingDziewiczenieMethod();
    }

    interface ITestingDziewiczenie2
    {
        void TestingDziewiczenieMethod2();
    }

    // teraz sprawdzimy, czy klasa dziedzicząca z klasy, dziedziczy interfacy

    class TestingDziewiczenieIntercafe : TestingClass
    {
        public string DziedziczenieName;
        public int DziewiczeniePower;

        public TestingDziewiczenieIntercafe(string DziewiczenieName, int DziewiczeniePower = 4) : base(DziewiczenieName) //w tym momencie dziedziczę konstruktor, który jest albo z jednym argumentem wpisywanym i dlatego robi TestingName=DziewiczenieName, albo konstruktor bazowy (bo tam wtedy jest przypisanie "jak nic to zomo" i bezargumentowo przy tym założeniu sie odpala) i wtedy TestingHp ustala sie na 100
        {
            this.DziedziczenieName = DziewiczenieName;
            this.DziewiczeniePower = DziewiczeniePower;
        }

        public static void DziedziczenieDodatkowaFunkcjaNieWiemPoCo()
        {
            Console.WriteLine("po co to wszystko");
        }
    }



    public interface ILogger
    {
        LogLevel logLevel { get; } //defniujemy field w interface i teraz do klasy. 
        void LogMessage(string message, LogLevel level = LogLevel.Verbose);


    }

    // tutaj zrobie enum pod własciwosci interfacu
    public enum LogLevel
    {
        Verbose = 0,
        Debug = 1,
        Criteria = 7
    }


    public class ConsoleLogger : ILogger
    {
        public LogLevel logLevel { get; set; } 

        public void LogMessage(string message, LogLevel level)
        {
            if (level >= logLevel)
            {
                Console.WriteLine("Console logger says {0}", message);
            }

        }
    }

    public class FileLogger : ILogger
    {

        #region Private Members //to jest chyba rodziaj komentarza 
        private readonly string mLogPath;
        #endregion

        public FileLogger(string logPath)
        {
            mLogPath = logPath;
            var directory = Path.GetDirectoryName(mLogPath);
            Directory.CreateDirectory(directory);
        }

        public LogLevel logLevel { get ; set ; }

        public void LogMessage(string message, LogLevel level)
        {
            if (level >= logLevel)
            {
            using (var fileStream = new StreamWriter(File.OpenWrite(mLogPath)))
            {
                fileStream.BaseStream.Seek(0, SeekOrigin.End);
                fileStream.WriteLine(message);
            }
            }
        }
    }

}
