namespace CsharpAdvanced.DelegatesEvents;

//Delegates are class members that are "delegated" to ensure communication between different classes. 
//For example in some long running process, we can delegate a member to send information about the progress of this process

//Delegates can serve also just a method pointers, and many developers treats them just like that
//We can create w custom delegate using "delegate" keyword or use one of three type in c#: Func, Action, Predicate

//In this file we will consider only the custom delegates and we will present the class communication during long running process
//Also we will show how to point the method

public class Delegates
{
    //Mostly the delegate should be marked as public
    //We also need to specify the type of the type of the method connected with the delegate
    //At first we will just use the delegate as a simple method pointer
    public delegate void MyMethodPointer(); 

    public static void InvokeDelegatesExamples()
    {
        //najpierw zajmiemy się samymi delegatami: są oni po to aby umożliwić komunikację pomiędzy dwoma obiektami/strukturami czy innymi "rzeczami". Mogą posłuży jako wskaźniki metod, tzn jako takie referencje czyli adresy. Służą też do szybkiego definiowania funkcji (poprzez Action, Func), oraz do tego, aby argumentem funkcji mogła być metoda (tj. inna funkcja)

        // delagate jako wskaźnik

        MyMethodPointer obj1 = new MyMethodPointer(MyMethod); //tworzymy obj1, który jest konkretnym delegatem, gdyż jest typu delegat. Ten obj1 jest delegatem wskazującym na "SomeMethod". Bez nawiasów, ponieważ tylko wskazujemy metodę

        obj1.Invoke(); //metoda invoke wykonuje delegata w wątku. Jako, że przykłądowy delegat jest bezargumentowy no to tez argumentów

        //można jednak po prostu, bezpośrednio
        MyMethod();

        Console.WriteLine("\n\n");

        // tutaj mamy przykład idei porozumiewania sie poprzez delegata. Mamy długo trwający proces (np w tle) i przekazywane na bierząco informacje o progresie.
        MyClass123 obj2 = new MyClass123(); //tworze obiekt z klasy MyClass123
         // obj2.LongRunning(Callback); //długo trwająca operacja. Argumentem LongRunning jest CallBack, zatem stwierdzamy, Ze CallBack jest delegatem.


        //tutaj moje obczajam

        Clothes ubrania1 = new Clothes(); //robie nowy obiekt zwiazany z ubraniami aby je zmieniac jak sie temperatura bedzie zmieniac
        Heat heat1 = new Heat(); //robie nowy obiekt zwiazany z temperaturą, aby ją zmieniac gdy zmieniaja sie pory roku
        MonthRunning uplywMiesiecy1 = new MonthRunning(); //tworze nowy obiekt dajacy plyw miesiecy

        //do tego trzeba threding, bo to leci jako pierwsze!
        uplywMiesiecy1.MonthPassing(MonthCallback);

    }

    static void MyMethod() //to tylko ze pointuje
    {
        Console.WriteLine("hehe");
    }

    static void Callback(int i) //w LongRunning podaliśmy tą metodę, a mogliśmy to zrobić dlatego, że zastosowaliśmy podejście delegatem. LongRunning sie wykonuje i gdy w LongRunning dodzie się do "method(i)" przechodzimy do wykonania tego CallBack, ponieważ "method" jest Callback (jest delegatem typu Callback1)
    {
        Console.WriteLine(i);
    }

    public static int MonthCallback(int i)
    {
        Console.WriteLine(i);
        
        return i;

    }


}

class MyClass123
{
    public void LongRunning(CallBack1 method) //argument będący methodą-delegatem CallBack (tylko tym delegatem)
    {
        for (int i = 0; i < 10000; i++)
        {
            //does something
            method(i); //ta medota "method" jest delegatem typu CallBack1. Odpala się ta metoda od argumentu i, a wiec przechodzimy do definicji metody, która została użyta. Tutaj można by użyć jakieś threading, żeby nie stopować aplikacji (albo async funkcji).
        }

    }

    public delegate void CallBack1(int i); //deklarujemy typ delegata, któego określamy tylko typ return i typ argumentów.

    //podsumowując: chcemy, żeby informacje z LongRunning były gdzies przesyłane. Dlatego tworzymy delegata i odwołujemy sie do tego delegata (kolejny argument). Gdzies w metodzie tego delegata jakoś odpalamy. Potem w głównym kodzie definiujemy funkcję, która będzie tym bezpośrednim delegatem (tj ona bedzie definiowała sposó przekazu informacji (sposob kooperacji)) pomiędzy klasami/strukturami. 
}

//// Kolejny przykład. zrobie komunikacje miedzy dwoma klasami

class Heat
{
    private int _temperatura;

    public int temperatura
    {
        get { return _temperatura; }
        set { _temperatura = value; }
    }




}


class Clothes
{
    public string myClothes = "efe";

}

class MonthRunning
{

    public void MonthPassing(MonthPassingHolder holder)
    {
        for (int i = 1; i < 13; i++)
        {
            Console.WriteLine((Months)i);
            holder(i);
            Thread.Sleep(3000);
        }
    }
    public delegate int MonthPassingHolder(int i);



}

enum Months
{
    Styczen = 1, Luty, Marzec, Kwiecien, Maj, Czerwiec, Lipiec, Sierpien, Wrzesien, Pazdziernik, Listopad, Grudzien
}
