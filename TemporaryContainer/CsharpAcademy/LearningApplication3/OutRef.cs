using System;
using System.Collections.Generic;
using System.Text;

namespace LearningApplication3
{
    class OutRef
    {
        public static void ProgramX()
        {

            int y = 10;
            TestOutKeyWord1(ref y);
            Console.WriteLine(y);
            //ref robi, że przekazuje do wnętrza metody zarówno wartość zmiennej jak i referencję do zmiennej. Dlatego musi być ona wcześniej określona. Wartość zostanie przekazana do metody, powiekszona o 10 i zwrócona, więc y=20 po metodzie.

            int x;
            //int x=1000;
            TestOutKeyWord2(out x); 
            Console.WriteLine(x);
            //out robi, że przekazuje jedynie referencje do zmiennej, ale nie jej wartość, więc niezależnie od tego jaka jest na początku, będzie musiała być określona wewnątrz metody, tam też będą na niej operacje i następnie wartość zostanie zmieniona.

            int number;
            int number2;
            bool result4 = int.TryParse("14", out number);
            bool result5 = int.TryParse("abc", out number2);
            Console.WriteLine(result4);
            Console.WriteLine(number);
            Console.WriteLine(result5);
            Console.WriteLine(number2);
        }

        public static void TestOutKeyWord1(ref int variable)
        {
            variable+=10;

        }

        public static void TestOutKeyWord2(out int variable)
        {
            variable = 0;
            variable+=10;

        }

    }


}
