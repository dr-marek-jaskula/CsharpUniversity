using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;

namespace LearningApplication3
{
    public class AttributeMethodParameter
    {
        public static void LearnAtriParameterMethod()
        {
            Monster monster = new Monster() { FirstName = "Belhas", LastName = "Omidur" };
            monster.Work("dłowiesz");
        }

    }


    public class Monster
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /// <summary>
        /// Function with attribute on parameter
        /// </summary>
        /// <param name="value"></param>
        public void Work([DisplayProperty("myValue")] string value)
        {
            Console.WriteLine("hehe");

            Type type = value.GetType();
            var attribute = type.GetCustomAttribute<DisplayPropertyAttribute>();
            Console.WriteLine($"My true name is {attribute.DisplayName} and i will {value}");
        }
    }

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class DisplayPropertyAttribute : Attribute
    {
        public string DisplayName { get; set; }

        public DisplayPropertyAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }
}
