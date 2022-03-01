using System;
using System.Collections.Generic;
using System.Text;

namespace LearningApplication3
{
    public class KeyWordDynamic
    {
        static ExampleClass cls = new ExampleClass();
        //tworzymy statyczno-dynamiczną zmienną, przy czym statyczna to tylko, że nie musi być instancjonowana
        static dynamic dls;

        public static void Program5()
        {

            #region TestRegion
            /*
            //C# jest jednocześnie statyczny i dynamiczny. Mocno typizowany
            string thisIsString = "aa";
            thisIsString.ToUpper(); // wynik to "AA"

            object thisIsObject = "aa"; //object to "matka wszystkich obiektów"
            //thisIsObject.ToUpper(); //to nie zadziała. Kompilator wie, bo c# jest statyczny, że nie istnieje ToUpper w klasie "object". Wiemy, że istnieje w string, a obiekt jest stringiem, ale kompilator w trakcie pisania nie wie o tym. 
            //to jest statyczne

            dynamic thisIsDynamic = "aa";
            thisIsDynamic.ToUpper(); //dynamic nie ma żadnych podpowiedzi, nie ma też żadnego błędu. To jest zagadka którą rozwiązemy
            */


            //Dynamic sprawdza, kompiluje w czasie rzeczywistym i sprawdza czy może coś zrobić (jak python, czy javaScript).
            //Dynamicznie tworzymy coś, ze nie wiemy co będzie (jak var, ale różnice są)
            //Dynamic pozwala dynamicznie wywoływać (i wiele różnych rzeczy), w czasie rzeczywistym.


            /*
            int myInt = 1 + 3;
            var mvar = 1 + 3; // w momencie kompilacji ogarnia ze to int
            dynamic dyn = 1 + 3; // w trakcie działania programu ogrania ze to int
            object obj = 1 + 3; //tu wchodzi sprawa dziedziczenia. Object staje się intem, bo int dziedziczy z object

            Console.WriteLine(myInt.GetType());
            Console.WriteLine(mvar.GetType());
            Console.WriteLine(dyn.GetType());
            Console.WriteLine(obj.GetType());
            */



            dynamic dyna;
            int a = 20;
            //chcemy skonwertować to na dynamiczne
            // w trakcie trwania programu zrobimy to. Skonwertuje to i stanie sie to typem.

            dyna = a; //tutaj dynamic staje sie intem i przyjmuje wartość 20

            string b = "This is Dynamic!!";

            dyna = b; //tutaj dynamic zmienia typ zmiennej dyna w trakcie trwania programu i przyjmuje wartość "This is Dynamic!!"

            DateTime dt = DateTime.Now;

            dyna = dt; //tutaj dynamic zmienia typ zmiennej na DateTime w trakcie trwania programu i przyjmuje wartość DateTime.Now

            //dynamic nie jest uzywane wszedzie bo moze namieszać

            //wydajność dynamic jest mniejsza, bo dynamic robi typ w czasie rzeczywistym.

            #endregion

            // Jedno z zastosowań dynamic:
            // gdy dostajemy dane z nieznanego źródła. Gdy pobieramy czyjąś stronę i ją analizujemy
            // wrzucamy do dynamic.
            // Nie musiemy wiedzieć co do nas przychodzi. Dynamic samo określi co do nas przyszło.
            // Wszystko bedzie sie działo w czasie rzeczywistym

            // Inne zastosowanie:

            cls.ExampleMethod("cls");
      
            //dynamiczne odwołanie 
            dls = new ExampleClass();
            dls.ExampleMethod("dls");
            dls.ExampleMethod(); //po to by pokazać błąd, omiajmy kompilacje, "to będziemy kompilować w czasie rzeczywistym", zatem on dopiero potem sie zorientuje ze to jest bład, bo wczensniej to nie wie.


            //dynamic ma wiele aspektów. To tylko drobnostka.
        }



    }


    public class ExampleClass
    {

        public void ExampleMethod(string test)
        {
            var a = test;
        //tutaj postawic breakpoint
        }

    }

}
