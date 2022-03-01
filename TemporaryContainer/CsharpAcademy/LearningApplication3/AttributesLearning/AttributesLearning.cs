using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LearningApplication3
{
    //Basics of Attributes
    //you can use attributes to inject additional information to the assemblies that can be queried at runtime if needed using reflection
    //An attribute is a piece of additional declarative information that is specified for a declaration
    class AttributesLearning
    {
        public static void Attri()
        {

            var types = from t in Assembly.GetExecutingAssembly().GetTypes() where t.GetCustomAttributes<MyExample3Attribute>().Count() > 0 select t;
            //assembly to to co teraz leci

            foreach (var item in types)
            {
                Console.WriteLine(item.Name);
                foreach (var t in item.GetProperties())
                {
                    Console.WriteLine(t.Name);
                }
                foreach (var t in item.GetMethods())
                {
                    Console.WriteLine(t.Name);
                }
            }


        }


    }

    //atrybuty to po prostu klasy które dziedziczą z klasy Attribute

    public class MyExample1Attribute : Attribute
    {

    }

    //można stosować dany atrybut z pełną nazwą, albo z uciętą o końcówkę Attribute jeśli dodamy takową na końcu nazwy klasy (podobnie jak z Property).
    //tak jak pokazano poniżej

    [MyExample1]
    public class TestClassForAt1
    {
    }

    [MyExample1Attribute]
    public class TestClassForAt2
    {
    }
    //Atrybuty można bazowo stosować do wszystkiego:
    public class TestClassForAt3
    {

        [MyExample1] //jak widać można do fieldów i propsów
        string myString1;

        [MyExample1]
        public int myProp { get; set; }

        //jak widać można dla metod
        [MyExample1]
        public void myMethod() { }



    }


    //teraz zrobimy taki Attribute, którynie koniecznie stosuje się do wszystkiego, bo bazowo jest ustawiony "All"
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field |AttributeTargets.Property)] //to spowoduje, że atrubut można zastosować tylko do metody, fielda lub propsa
    public class MyExample2Attribute : Attribute
    {

    }

    // jesli napiszemy tu [MyExample2] to będzie nam podreślać, chociać inteligence nam i tak to podpowie
    public class myTestClassForAt4
    {
        [MyExample2]
        int myField1;

        [MyExample2]
        public void myMethod1() { }

    }


    [AttributeUsage(AttributeTargets.Class)] //teraz dodamy coś do klasy Atribute
    public class MyExample3Attribute : Attribute
    {
        //definiujemy propsy w atrybucie
        public string Name { get; set; }
        public int Version { get; set; }

    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)] // to AllowMultiple = true, robi ze mozna kilka razy to samo Atribute robić: Patrzeć przkład dwa razy nizej
    public class MyExample4Attribute : Attribute
    {
        //definiujemy propsy w atrybucie
        public string Name { get; set; }
        public int Version { get; set; }

    }

    //dajemy Attribute z ustalonymi propsami
    [MyExample3(Name = "Jhon", Version = 1)]
    public class MyTestingClassForAt5
    {

        public int IntValue { get; set; }

        public void Method() { }

    }

    //dajemy dwa Attribute z ustalonymi propsami, ponieważ jest AllowMultiple
    [MyExample4(Name = "Wenus", Version = 12)]
    [MyExample4(Name = "Ziemia", Version = 2)]
    public class MyTestingClassForAt6
    {
        
        public int IntValue { get; set; }

        public void Method() { }

    }

    public class NoAttributes
    {

    }

    //✔️   DO name custom attribute classes with the suffix "Attribute."

    //✔️ DO apply the AttributeUsageAttribute to custom attributes.

    //✔️ DO provide settable properties for optional arguments.

    //✔️ DO provide get-only properties for required arguments.

    //✔️ DO provide constructor parameters to initialize properties corresponding to required arguments.Each parameter should have the same name (although with different casing) as the corresponding property.

    //❌ AVOID providing constructor parameters to initialize properties corresponding to the optional arguments.

    //In other words, do not have properties that can be set with both a constructor and a setter. This guideline makes very explicit which arguments are optional and which are required, and avoids having two ways of doing the same thing.

    //❌ AVOID overloading custom attribute constructors.

}
