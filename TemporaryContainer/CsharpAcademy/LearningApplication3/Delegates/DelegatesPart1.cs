using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LearningApplication3
{



    class DelegatesPart1
    {
        delegate void LogDel(string text); //mozna go definiowac w samym namespace
        //delegaty mogąbyć od static methods oraz z instance

        public static void Program1()
        {
            LogDel logDel = new LogDel(LogTextToFile);

            //logDel("teeext");
            //logDel.Invoke("teeeeeext");

            Console.WriteLine("Please enter your name");
            string name = Console.ReadLine();

            logDel(name);


            Log log = new Log();
            LogDel logDel1 = new LogDel(log.LogTextToScreen2);

            logDel1.Invoke("hohohoo"); //tutaj widać ze metody dla instancji działają


            LogDel LogTextToScreenDel, LogTextToFileDel;
            LogTextToScreenDel = new LogDel(log.LogTextToScreen2);
            LogTextToFileDel = new LogDel(log.LogTextToFile2);

            //multi delegates. Łączy się je plusem i dzięki temu odpala ona wszystkie delegaty
            LogDel multiLogDel = LogTextToScreenDel + LogTextToFileDel;

            multiLogDel.Invoke("frfrfrf2020");
            //zatem dodawanie delegatów działa


            LogText(multiLogDel, name);

        }

        static void LogTextToScreen(string text)
        {
            Console.WriteLine($"{DateTime.Now}: {text}");

        }

        static void LogTextToFile(string text)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt"), true))
            {
                sw.WriteLine($"{DateTime.Now}: {text}");
            }
            //StreamWriter jest klasą do zapisywania do plików. Potrzebuje on ścieżki i tego, czy nadpisywać czy dopisywać (czyli bool). Daliśmy, że dopisuje.
            // ponadto ciężkę dostaliśmy w taki sposób, że wzieliśmy kalsę Path, i z niej metodę combine, która łączy dwie ścieżki: np gdyby jedna była relatywna a druga absolutna to robi absolutną itp. Te dwie ścieżki to: (obecna ścieżka, uzyskana za pomocą AppDomain) oraz "Log.txt" zeby zrobić właśnie taki plik.
        }


        static void LogText(LogDel logDel, string text)
        {
            logDel(text);
        }

    }

    public class Log
    {
        public void LogTextToScreen2(string text)
        {
            Console.WriteLine($"{DateTime.Now}: {text}");

        }

        public void LogTextToFile2(string text)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt"), true))
            {
                sw.WriteLine($"{DateTime.Now}: {text}");
            }
            //StreamWriter jest klasą do zapisywania do plików. Potrzebuje on ścieżki i tego, czy nadpisywać czy dopisywać (czyli bool). Daliśmy, że dopisuje.
            // ponadto ciężkę dostaliśmy w taki sposób, że wzieliśmy kalsę Path, i z niej metodę combine, która łączy dwie ścieżki: np gdyby jedna była relatywna a druga absolutna to robi absolutną itp. Te dwie ścieżki to: (obecna ścieżka, uzyskana za pomocą AppDomain) oraz "Log.txt" zeby zrobić właśnie taki plik.
        }
    }

}
