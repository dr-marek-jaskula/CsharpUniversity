using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LearningApplication2
{
    class Delegates2AndLambdaActionFunc
    {

        //delegaci to są typu method, dlatego mają return type, argument type
        //definies a method that returns an int and has one int as an input
        //defines the signature (return type and parameters)
        public delegate int Manipulate(int a);
        public delegate int Manipulate2(long a);

        //Action is just a delegate
        public delegate void MyAction(); //no input, return nothing

        public static void Program24()
        {

            //invoking a normal method
            var b = NormalMethod(2);
            Console.WriteLine(b);

            //Deletaci są po to aby funcje mogły mieć argumenty, którymi są inne funkcje

            //Create an instance of the delegate
            Manipulate normalMethodDelagate = new Manipulate(NormalMethod);
            int normalResult = normalMethodDelagate(3);
            Console.WriteLine(normalResult);
            Console.WriteLine(RunAnotherMethod(NormalMethod));

            int normalMethod2Result = RunAnotherMethod2(NormalMethod, 10);
            Console.WriteLine(normalMethod2Result);


            //Anonymous function po to by nie robić takich funkcji tylko od razu
            //Anonymous method is a delegate() {} and it returns a delage

            // to za długie: Manipulate anonymousMethodDelagate = new Manipulate(NormalMethod);
            Manipulate anonymousMethodDelagate = delegate (int a) { return a * 2; };
            //linia 43 i linia 30 są identyczne. Różnica taka że dla 38 nie trzeba dodatkowo definiowac nic. To był pierwszy etap polepszania, teraz można znacznie lepiej

            var anonymousResult = anonymousMethodDelagate(3);
            Console.WriteLine(anonymousResult);

            //Lambda expression teraz: are anything with "=>" and a left/right value
            //They return a delagate (so a method that can be invoked)
            // or an expression of a delagate (so it can be complied and then executed)

            Manipulate lambdaDelegate = a => a * 2; //po lewo jest input, a po prawo body funkcji. Typ jest brany z delegata. 
            //gdyby dać Manipulate2, to bedzie plakal ze nie mozna int do longa

            var lambdaResult = lambdaDelegate(5);
            Console.WriteLine(lambdaResult);

            //nicer say to write a lambda
            Manipulate nicerLambdaDelegate = (a) => { return a * 2; };
            var lambdaResult2 = nicerLambdaDelegate(5);
            Console.WriteLine(lambdaResult2);

            //ostatnia cześć to:

            //Lambda can return an Expression
            Expression<Manipulate> expressionLambda = a => a * 2;
            expressionLambda.Compile().Invoke(2); //to już trudniejszy temat


            //linijka 18, z MyAction, to jest to samo co:
            MyAction myAction = () => { Console.WriteLine("hehe"); };

            //Aby to ominąć robi się tak: (tj to jest dokładnie to samo)
            Action myAction2 = () => { Console.WriteLine("haha"); };
            // Action jest to po prostu delegate with no raturn type and optional input
            Action<int> myAction3 = a => { Console.WriteLine(a); };
            // Action jest typu void
            Action<string, int, bool> myAction4 = (myString, myInt, myBool) => { Console.WriteLine("i na co to wszystko"); };


            MyFunc myFunc = (a) => a + 3;

            Func<int, int> myFunc1 = a => a + 10;
            Console.WriteLine(myFunc1(4));

            //Func jest to po prostu delegat z return type 


            // Mimic the FirstOrDefauly Linq expression
            var items = new List<string>(new[] { "a", "b", "b", "d", "e", "f", "g" });
            Func<string, bool> aa = (string item) => item == "c";
            var foundItem = items.FirstOrDefault(aa);
            Console.WriteLine(foundItem);

            //mozna tez tak
            var foundItem2 = items.FirstOrDefault(items => items == "c");
            Console.WriteLine(foundItem2);

            //Calling out veriosn on above
            var foundItem3 = items.GetFirstOrDefault(item => item == "c");
            Console.WriteLine(foundItem3);

        }

        //tutaj o Func. To jest po prostu delegat z return typem int

        public delegate int MyFunc(int t);



        /// <summary>
        /// A normal looking method
        /// </summary>
        /// <param name="a">The input value</param>
        /// <returns>Return twice the input value</returns>
        public static int NormalMethod(int a)
        {
            return a * 2;
        }


        public static int RunAnotherMethod(Manipulate theMethod)
        {
            return theMethod(5);
        }

        /// <summary>
        /// Acces method as an input
        /// </summary>
        /// <param name="theMethod"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int RunAnotherMethod2(Manipulate theMethod, int i)
        {
            return theMethod(i);
        }


  
    }



    public static class Helpers
    {

        public static TResult GetFirstOrDefault<TResult>(this List<TResult> items, Func<TResult, bool> findMatch) //this musi byc na początku. Dodaliśmy generics aby uzytkownik sam okreslał input
        {
            foreach (var item in items)
            {
                if (findMatch(item))
                {
                    return item;
                }

                return default(TResult);
            }
            return default(TResult);
        }




    }






}
