using System;
using System.Collections.Generic;
using System.Text;

namespace LearningApplication2
{
    class Delegates3
    {
        public delegate void SomeDeleate();

        public static void Program28()
        {
            SomeDeleate someDele = SomeMethod; //tutaj informuje, że delegate someDele wskazuje na SomeMethod
            someDele.Invoke(); // ten sam efekt co zwykłe wywołanie

            SomeLongRunningDate sm = new SomeLongRunningDate();
            sm.SomeMethod2(CallBackMethod);
            // w tym momencie dowiedzieliśmy się na jakim etapie pracy jest SomeMethod2, z poziomu inne metody tj. CallBackMethod. To znaczy oddelegowaliśmy CallBackMethod do tego.
            //delegato odpowiada za komunikację


        }



        static void SomeMethod()
        {
            Console.WriteLine("I am here!");
            Console.ReadKey();
        }

        static void CallBackMethod(int i)
        {
            Console.WriteLine(i); 
        }

    }

    //teraz komunikacja pomiedzy klasami
    //chcemy dowiedzieć w jakim stanie jest długi proces

    class SomeLongRunningDate
    {
        public delegate void CallBack(int i);

        public void SomeMethod2(CallBack obj)
        {
            for (int i = 0; i < 1000; i++)
            {
                //Console.WriteLine(i.ToString());
                obj(i);
            }

            Console.ReadKey();
        }


    }



}
