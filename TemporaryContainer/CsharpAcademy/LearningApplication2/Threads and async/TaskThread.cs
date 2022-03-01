using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;



namespace LearningApplication2
{
    class TaskThread
    {
        public static void Program20()
        {
            Console.WriteLine("Hello");
            Console.WriteLine($"Running Thread is {Thread.CurrentThread.ManagedThreadId}");
            TaskingSpree1();
            TaskingSpree2();
            TaskingSpree3();
            TaskingSpree4();

            var tsc = new TaskCompletionSource<bool>(); //to i to dolne  sprawia ze program sie nie konczy od razu
            var b = tsc.Task.Result;
        }



        public static void TaskingSpree1()
        {
            Task.Run(() =>
            {
                Console.WriteLine($"Up you go1 + {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(2000);
                Console.WriteLine("Let us fight like a gentelment1");
            });
            //Task.Run wyciąga thread z threadpool oraz Task jest zawsze background czyli, że konczy sie jak sie konczy program.
            Console.WriteLine($"End of the TestingSpree1. On Thread {Thread.CurrentThread.ManagedThreadId}"); 

        }

        public static void TaskingSpree2()
        {
            Task.Run(async () =>
            {
                Console.WriteLine($"Up you go2 + {Thread.CurrentThread.ManagedThreadId}");
                await Task.Delay(3000);
                Console.WriteLine("Let us fight like a gentelment2");
            });
            Console.WriteLine($"End of the TestingSpree2. On Thread {Thread.CurrentThread.ManagedThreadId}");
            //tutaj widzimy, ze wraca do main Threada z tym cw
        }

        //Pierwsze dwa TaskingSpree function są bardzo podobne. Obie odpalają się tak, że cały Task liczony jest jako jedna linijka i to ze jest odpalony i się realizuje asynchronicznie lub w innym wątku, to nie przeszkadza od razu realizować resztu kodu, co widać po "end of the TestingSpree"

        public static async void TaskingSpree3() //zwykle sie voida nie daje na synchronicznych, tylko Task lub jego właściwosć
        {
            await Task.Run(() => //dzieki temu czeka na wykonanie calego taska z przejsciem do kolejnych linii funkcji
            {
                Console.WriteLine($"Up you go3 + {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
                Console.WriteLine("Let us fight like a gentelment3");


                Console.WriteLine("Gimmi your first word");
                Console.ReadLine();
            });
            Console.WriteLine($"End of the TestingSpree3. On Thread {Thread.CurrentThread.ManagedThreadId}");
            //dzięki temu ciągle jest tutaj w jednym i tym samym threadzie //gosc na yotubie mial odwrotnie. tu dp thread 1 a 
         
        }

        //Trzecia TaskingSpree różni się tym, że robi linijka po linijce, to znaczy, że jest asychroniczna, ale do momentu aż nie wykona await nie idzie dalej

        public static async void TaskingSpree4() //zwykle sie voida nie daje na synchronicznych, tylko Task lub jego właściwosć
        {
            await Task.Run(async () => //dzieki temu czeka na wykonanie calego taska z przejsciem do kolejnych linii funkcji
            {
                Console.WriteLine($"Up you go4 + {Thread.CurrentThread.ManagedThreadId}");
                await Task.Delay(1000);
                Console.WriteLine("Let us fight like a gentelment4");
            });
            Console.WriteLine($"End of the TestingSpree4. On Thread {Thread.CurrentThread.ManagedThreadId}");
            //gosc to polecal //gosc na yotubie mial odwrotnie. tu dp thread 1 a 
            // mozna na koneic Taska dać .ConfigureAwait(false) co niby robi ze nie obchodzi go czy wroci do Threada w ktorym jest czy zrobi nowy.
        }

    }










}
