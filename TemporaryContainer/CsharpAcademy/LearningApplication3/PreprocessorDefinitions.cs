#define FOO
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningApplication3
{
    public class PreprocessorDefinitions
    {
        //chodzi o definiowanie rzeczy przed startem aplikacji, tak zwane preprocessor symbols
        //robi się to za pomocą hasztaga: Patrz przed "using System"

        public static void PreStartNow()
        {
            //to mowi tyle, że jest jest predefined cos takiego jako FOO to to jest odpala.
#if FOO
            Console.WriteLine("Fooing away");
#endif

            //zobaczmy, że FOO1 nie jest zdefiniowane, wiec to sie nie odpali i widac ze szare jest 
#if FOO1
            Console.WriteLine("Fooing away");
#endif

            //to mowi ze jak puszczam w trybie debugu, to sie to odpali, a jak nie to nie
#if DEBUG
            Console.WriteLine("Im in debug mode!!!");
#endif

            //Trace jest zdefiniowany a Debug mode i Release mode, aczkolwiek można wejść w properties i tam w build i można tam odchaczyć definicję TRACE. Można też tworzyć w properties swoje wlasne stałe 
#if TRACE
            Console.WriteLine("Im in Release mode!!!");
#endif


        }


    }
}
