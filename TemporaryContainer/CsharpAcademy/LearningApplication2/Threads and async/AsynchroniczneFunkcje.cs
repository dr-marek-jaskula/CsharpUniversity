using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;


namespace LearningApplication2
{
    class AsynchroniczneFunkcje
    {
        public static void Program21()
        {
            Console.WriteLine("Hello");
            FryingPatetos();
            Console.WriteLine("gegegege");
            Console.ReadLine();

        }

        public static async void FryingPatetos() //funkcja asynchroniczna staje sie backgroundowa, czyli główny thread nie czeka az sie ona cała wykona. Precyzyjniej: jeśli gówny thread się kończy, ona jest natychmiast przerywana. Może być ona typu void (ale z jakiś powodów odradzają)
        {
            Console.WriteLine("Time to fry patetos");
            await Task.Delay(3000); //Metoda async jest uruchamiana synchronicznie (normalnie) do momentu, aż osiągnie swoje pierwsze wyrażenie await, w którym to momencie Metoda jest wstrzymana do momentu ukończenia zadania. To znaczy, że dalszy kod funkcji nie jest realizowany, aż do momentu gdy nie ukończy się procedura z await. Dzieje się to jednak z tle, a główny thread leci dalej (można zobaczyć, że "gegegege" się napisze)
            //Jeśli metoda m odyfikowana przez słowo kluczoweasync nie zawiera wyrażenia lub instrukcji await metoda jest wykonywana synchronicznie.
            Console.WriteLine("Fry the Pateto");

        }

    }
}


