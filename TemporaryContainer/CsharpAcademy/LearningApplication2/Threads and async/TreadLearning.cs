using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;

namespace LearningApplication2
{
    class ThreadLearning
    {

        public static void Program18()
        {

            Console.WriteLine("Hello World");

            Thread t1 = new Thread(() => { Thread.Sleep(2000); Console.WriteLine("hrhr"); });
            t1.Start();
            //Thread t2 = new Thread(() =>
            //{
            //    for (int i = 0; i < 1000; i++)
            //    {
            //        Thread.Sleep(1000);
            //        Console.WriteLine(i);
            //    } 
            //});
            //t2.Start();

            Console.WriteLine("fwwww");

            Thread t3 = new Thread(() =>
            {
                Thread.Sleep(1000000);
                Console.WriteLine("123");
            })
            { IsBackground = true }; //bazowo jest FrontGround ktory powoduje, ze jesli ten thred działa, a main thread sie konczy, to aplikacja dalej dziala. Natomiast background thread powoduje, ze jesli main thread sie skonczy to przerywa background thread i konczy aplikacje
            // alternatywnie można to samo zrobić tak:
            // t3.IsBackground = true;
            t3.Start();



            //Enumerable.Range(0, 10).ToList().ForEach(f=>
            //{
            //    new Thread(() =>
            //{
            //        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} on");
            //        Thread.Sleep(1000);
            //    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} off");

            //}).Start();

            //}); // to robi z biblioteki Linq, lepsza pętla, od 0 do 10, wrzuca do listy i dla każdego robi funkcję, która tworzy thread.


            Enumerable.Range(0, 100).ToList().ForEach(f =>
            {
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} on");
                    Thread.Sleep(1000);
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} off");

                });

            }); // to robi thredy z threadpoola
            //jest to sposób na robienie Threadów w bardziej wydajny sposob. Wrzucasz thready do poola, zeby je potem wykorzystać. Czyli uzyte thready nie kasuje, ale przechowuje, bo tworzenie nowego threada jest pamięciożerne


            Console.WriteLine("fwwww");


            // Teraz zrobimy thread odnosnie ściągania obrazka

            Thread mineThread1 = new Thread(() =>
            {
                Console.WriteLine("Start downloading");
                var webClient = new HttpClient();
                var html = webClient.GetStringAsync("https://angelsix.com/");
                Console.WriteLine("Done downloading");

            });
            mineThread1.Start();
            //chodzi o to ze chcemy aby glowny thread poczekal az sciagnie sie obrazek

            mineThread1.Join(); //to wlasnie to robi, ze dalej glowny thread nie pojdzie, chyba ze mineThread1 sie skonczy. Blokuje glowny thread. Ważna kwestia.!! Z JEDNEJ STRONY BLOKUJE, ale!! sam tez sie blokuje, tzn jesli skonczy sie mineThread1 a potem skonczy sie główny Thread to konczy sie aplikacja (zobaczyc jak to działa z ThreadPool i bez tego jak dziala ThreadPool

            Thread.Sleep(3000);
            Console.WriteLine("All done");



            //to tutaj
            var tsc = new TaskCompletionSource<bool>();
            var b = tsc.Task.Result;
            //powoduje, że thread główny (main, ten z automatu) nie kończy się.
        }

        // Uwagi. Jeśli mam user interface (UI) to caly kontent jest na main thread. Nie można modyfikować rzeczy z user interface z innych threadów niż main. Dlatego jak chcemy coś takiego zrobić to trzeba się odwołać do main thread:

        // Dispatcher.Invoke( ()=>
        //{
        // My.Button.Content - "Logged in" //zmienia napis w przycisku konsoli. 
        //} //ten Dispatcher to jest sposób na przejście do głównego wątku! Każdy obiekt w user interface ma dispatcher
        //jeśli nie jest sie w obiekcie żadnym to można: Application.Current.Dispatcher.Invoke();
        // tak sie dostaniemy do dispatchera, czyli do glownego wątku
}
}
