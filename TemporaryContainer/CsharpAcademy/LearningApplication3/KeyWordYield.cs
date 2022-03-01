using System;
using System.Collections.Generic;
using System.Text;

namespace LearningApplication3
{
    public class KeyWordYield
    {
        public static List<int> list1 = new List<int>() { 1, 2, 3, 10, 4, 5, 6, 7 };

        public static void Program4()
        {

            foreach (var item in list1)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("===============================");
            var list2 = Filter(list1);

            foreach (var item in list2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("===============================");

            var list3 = Filter2(list1);
            foreach (var item in list3)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("===============================");
            var list4 = Filter3(list1);
            foreach (var item in list4)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("===============================");
            //z każdym kolejnym krokiem robi sumę poprzednich
            var list5 = Total(list1);
            foreach (var item in list5)
            {
                Console.WriteLine(item);
            }

        }

        //ten przykład z listami nazywa się custom interation
        #region Custom Iteration

        /// <summary>
        /// Przy yield musi być enumerable (ale chyba moze być enumerator)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<int> Filter(List<int> list)
        {
            foreach (var item in list)
            {
                if (item>4)
                {
                    yield return item;
                }
                else
                {
                    //yield return 0; //to by zrobilo ze zastępuje elegemnty zerami
                }
            }


        }

        public static IEnumerable<int> Filter2(List<int> list)
        {
         yield return 1000;
        }

        public static IEnumerable<int> Filter3(List<int> list)
        {
            for (int i = 0; i < 5; i++)
            {
            yield return i+100;
            }
        }
        #endregion


        #region Stateful Iteration

        /// <summary>
        /// Yield zachowuje stan poprzedniej pętli, dlateog pomimo, że wracając do pętli powinien totalNumber wyzerować, to on pamięta stan pętli i wie, że totalNumber=item[0]. To znaczy nie nadpisuje stanów twardo wpisanych, tylko nadpisuje za pomocą zależności jak i+=1 itp
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static IEnumerable<int> Total(List<int> list)
        {
            int totalNumber = 0;
            foreach (var item in list)
            {
                totalNumber += item;
                yield return totalNumber;
            }
        }

        #endregion

    }
}
