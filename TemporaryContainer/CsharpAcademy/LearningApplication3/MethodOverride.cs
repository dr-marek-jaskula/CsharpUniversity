using System;
using System.Collections.Generic;
using System.Text;

namespace LearningApplication3
{
    public class MethodOverride
    {

        public static void Program6()
        {


            MethodOverride.MethodOverride1();
            MethodOverride.MethodOverride1("Roberto");
            MethodOverride.MethodOverride1("Roberto", false);



        }




        public static void MethodOverride1()
        {
            Console.WriteLine("I will override you by ease");
        }

        public static void MethodOverride1(string text)
        {
            Console.WriteLine("I will override you by ease "+text);
        }

        public static void MethodOverride1(string text, bool logic)
        {
            if (logic)
            {
            Console.WriteLine("I will override you by ease"+text);
            }
            else
            {
                Console.WriteLine("I have lost this time. However expect me in the future");
            }

        }


    }
}
