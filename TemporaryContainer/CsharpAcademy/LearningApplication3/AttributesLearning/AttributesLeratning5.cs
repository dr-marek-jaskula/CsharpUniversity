using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningApplication3
{
    class AttributesLeratning5
    {

        #region ThreadStatic Attribute

        //The [ThreadStatic] creates isolated versions of the same variable in each thread.

        [ThreadStatic] public static int i; // Declaration of the variable i with ThreadStatic Attribute.

        public static void TeachingIsGlod2()
        {
            new Thread(() =>
            {
                for (int x = 0; x < 10; x++)
                {
                    i+=1;
                    Console.WriteLine("Thread A: {0}", i); // Uses one instance of the i variable.
                }
            }).Start();

            new Thread(() =>
            {
                for (int x = 0; x < 10; x++)
                {
                    i+=2;
                    Console.WriteLine("Thread B: {0}", i); // Uses another instance of the i variable.
                }
            }).Start();
        }

        #endregion

    }


}
