namespace LearningApplication2;

class CustomComperes
{
    static public void Program7()
    {

        List<LearnHowToCompare> compareList = new List<LearnHowToCompare>();
        LearnHowToCompare element1 = new LearnHowToCompare("Jordan", 10);
        LearnHowToCompare element2 = new LearnHowToCompare("Emanuel", 15);
        LearnHowToCompare element3 = new LearnHowToCompare("Borko", 5);
        LearnHowToCompare element4 = new LearnHowToCompare("Goblin", 25);

        compareList.Add(element4);
        compareList.Add(element3);
        compareList.Add(element2);
        compareList.Add(element1);

        compareList.Sort(); //tutaj działa to tak, że Sort patrzy z jakiej klasy pochodzą elementy listy i jeśli jest tam funkcja CompareTo to ją stosuje (nadpisana jest względem bazowej funkcji CompareTo)

        compareList.Reverse();

        foreach (LearnHowToCompare item in compareList)
        {
            Console.WriteLine(item.powerLvl + " - " + item.name);
        }

        Console.WriteLine("\n");

        Console.WriteLine(element1.CompareTo(element3)); //teraz automatycznie wykorzystuje funkcję CompareTo z klasy
        Console.WriteLine(3.CompareTo(5)); //zwróćmy uwagę, że bazowe CompareTo robi włąsnie 1,0,-1. Zatem Sort działa tak, że bierze element i porównuje z reśztą i ustawia go wzgledem innych a potem kolejny.

        Console.WriteLine("\n");
        LearHowToComareComparer hehehehehe = new LearHowToComareComparer();
        compareList.Sort(hehehehehe); //To działa tak, że definiujemy sortowanie względem Comparera zdefiniowanego w klasie, z której pochodzi obiekt

        foreach (LearnHowToCompare item in compareList)
        {
            Console.WriteLine(item.powerLvl + " - " + item.name);
        }
    }
}


// Teraz zajmiemy się tym jak porównywać obiekty (sortować listy obiektów). Zrobimy to poprzez dziedziczenie z IComparable i zrobimy przeciążenie na CompareTo dla naszego konkretnego rodziaju obiektu z tej klasy (można by też nadpisać całość)
public class LearnHowToCompare : IComparable //tutaj dziedziczymy. Zrobiłem alt+enter i pokazuje mi co z dziedziczenia moge przeciążyć. Tutaj jest tylko funkcja CompareTo
{
    public string name { get; set; }
    public int powerLvl { get; set; }

    public LearnHowToCompare(string name, int powerLvl)
    {
        this.name = name;
        this.powerLvl = powerLvl;
    }

    public int CompareTo(object obj) // przeciążamy funkcję CompareTo ze względu, a tak naprawdę nadpisujemy
    {
        LearnHowToCompare arg = (LearnHowToCompare)obj;

        if (powerLvl < arg.powerLvl)
            return 1;
        else if (powerLvl == arg.powerLvl)
            return 0;
        else
            return -10; //zmieniając wartości można inaczej posortować
    }
    //    ogolnie!! CompareTo porównuje bierzące wystapienie obiektu z innym obiektem tego typu w kolekcji i zwraca wartość dodatnią,zero lub ujemną, i na tej podstawie sortuje
}
//zeby miec wiecej kryteriów porównawczych nalezy stworzyc nową klasę:

public class LearHowToComareComparer : IComparer<LearnHowToCompare> //typ generyczny, tylko do porównywania elementów klasy LearnHowToCompare
{
    public int Compare(LearnHowToCompare x, LearnHowToCompare y) //nadpisana metoda compare od dwóch argumentów. Tutaj jest tylko funkcja comparer
    {
        if (x.powerLvl < y.powerLvl)
            return 1;
        else if (x.powerLvl == y.powerLvl)
            return 0;
        else
            return -1;
    }
}
