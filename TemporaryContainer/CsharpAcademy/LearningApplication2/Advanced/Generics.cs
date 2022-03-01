using System;
using System.Collections.Generic;
using System.Text;

namespace LearningApplication2
{
    class Generics
    {
        public static void Podstawy6()
        {
            Book book = new Book { Isbn = 1111, Title = "C# Adcanced" };
            Book book2 = new Book { Isbn = 2222, Title = "C# Begginer" };
            Book book3 = new Book { Isbn = 3333, Title = "C# Master" };
            Console.WriteLine($"Tytuł ksiazki to {book.Title}, natomiast jej number isbn to {book.Isbn}");

            GenericList<int> numbers = new GenericList<int>(); //tutaj jest to lista liczb
            numbers.Add(10);
            numbers.Add(20);
            numbers.Add(30);
            foreach (int item in numbers.MyList)
            {
                Console.WriteLine(item);
            }
            numbers.MyList = new List<int>();
            foreach (int item in numbers.MyList)
            {
                Console.WriteLine(item);
            }


            GenericList<Book> books = new GenericList<Book>(); //tutaj jest to lista książek 
            books.Add(book);
            books.Add(book2);
            books.Add(book3);
            int i = 1;
            foreach (Book item in books.MyList)
            {
                
                Console.WriteLine($"The title of the {i} book is {item.Title} while its Isbn is {item.Isbn}");
                i++;
            }


            //            tak naprawdę generics są już porbione i są w System.Collections.Generic po kropce można spobie popatrzeć co tam jest.

            GenericDictionary<string, Book> dictionary1 = new GenericDictionary<string, Book>();
            dictionary1.Add("ksiega magii", book);
            int a = 5; int b = 4;
            Console.WriteLine(a > b ? a : b);

            Console.WriteLine(Utilities.Max(4, 5));
            //zeby porownac ten max z T, trzeba by jakiś interface zrobić albo coś takiego.

            book.Price = 10;
            Console.WriteLine(book.Price);
            Console.WriteLine(DiscountCalculator<Book>.CalculateDiscount(book));


            var number = new Nullable<int>(5);
            Console.WriteLine("Has value?" + number.HasValue);
            Console.WriteLine("Value: " +number.GetValueOrDefault());

            var number1 = new Nullable<int>();
            Console.WriteLine("Has value?" + number1.HasValue);
            Console.WriteLine("Value: " + number1.GetValueOrDefault());
        }


        /// <summary>
        /// przyjmujemy typ generyczny do metody. W tej metodzie zabieramy referencje.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public static void Swap<T>(ref T first, ref T second) // where T : IEntity, new() //ograniczenia tak samo jak przy klasach
        {
            T temp = first;
            first = second;
            second = temp;
        
        //np zamieni to elementy w array intów (bez dodatkowych ograniczeń)
        }


    }



    //zamiast kilku roznych klas robi sie generic, co pozwala korzystac wielokrotnie z jednej klasy bez kar wydajnosciowych np. jesli chce zrobic liste obiektow i tam wrzucic pare obiektów ale tez i pare liczb, to bedzie traktowal liczbe jako obiekt przez co straci wydajnosc. Po to są generics

    public class GenericList<T> //generics mają parametr i on jest w takich nawiasach. Najczęściej przyjmuje sie oznaczenie T, od type/template
    {
        private List<T> _list;

        public List<T> MyList
        {
            get {
                // _list.Clear(); // a z tym to przy wywołaniu robi psikusa i czyści listę
                return _list; 
                }

            set 
            {
                //_list = value; //zakomentowane sprawia, że nic się nie przypisze tej liscie, a wyświetli się tekst
                Console.WriteLine("Za mała ta lista na takiego jak ty");   
            }
        }


        public GenericList()
        {
            _list = new List<T>();
        }

        public void Add(T value) // to te to nie wiemy co to jest, jest to precyzowane przez uzytkownika w momencie startu
        {
            MyList.Add(value);

        }

        public T this[int index] //co to jest?
        {
            get { throw new NotImplementedException(); } //co to jest??
        }

    }


    public class Book : Product
    {
        public int Isbn;

    }


    public class GenericDictionary<TKey, TValue> //zodnie ze zwyczajem robi się PascalCase rozpoczynając od dużego T. Dwie wartości bo discionary
    {
        public void Add(TKey key, TValue value)
        {

        }
    }

    public class Utilities
    {
        static public int Max(int a, int b)
        {
            return a > b ? a : b; //to jest skrótowe pisanie: jeśli a>b == true to zwróć a, w przeciwnym razie zwróc b
        }
        static public T Max<T>(T a, T b) where T : IComparable //nie wiedząc czym jest T, traktuje a i b jako obiekty, natomiast my tu chcemy pokazać, że a i b zapewniają porónywali interface!!! (IComparable jest do interface). Jest to ograniczenie!!//
        {
            //Jest to generyczna metoda!! w klasie niegenerycznej!!! Jest to ok !!!
            return a.CompareTo(b) > 0 ? a : b; //compare to patrzy, czy a poprzedza b czy jest za b, jesli poprzedza zwraca wartość ujemną, jesli jest na tym samym miejscu (to samo) to daje zero, a jak jest następne to daje dodatnią wartość. 
        }

    }


    public class DiscountCalculator<TProduct> where TProduct : Product //to mowi ze TProduct ma byc produktem albo jego dzieckiem (w tym wypadku Book)
    {
        static public float CalculateDiscount(TProduct product)
        {
            return product.Price;
        }
    }

    public class Product
    {
        public string Title { get; set; }
        public float Price { get; set; }


    }

    public class Nullable<T> where T : struct
    {
        private object _value;

        public Nullable()
        {

        }
        public Nullable(T value)
        {
            _value = value;
            
        }

        public bool HasValue
        {
            get { return _value != null;  }
        }
        public T GetValueOrDefault()
        {
            if (HasValue)
            {
                return (T)_value;
            }
            return default(T);

        }

    }
    /// to Nullable to struktura ktora juz jest w System.

    public class Anything<T> where T : IComparable, new() //to drugie ograniczenie mowi ze typ T musi mieć bezparametrowy konstruktor
    {
        public void DoSomething(T value)
        {
            var obj = new T();
        }


    }

    public interface IEntity
    {
        public int Id { get; set; }
    }

    //typ T jest typem referencyjnym (jak customowe klasy ale też i string) oraz ma bezparametrowy konsturktor, a K implementuje IEntity interface
    public class Product2<T, K> where T: class, new() where K : IEntity
    {
        public string Title { get; set; }
        public float Price { get; set; }

        public T TestMethod(K obj)
        {
            int id = obj.Id;

            return new T();
        }
    }

}


