using System;
using System.Collections.Generic;
using System.Text;

namespace LearningApplication3
{
    public enum WarriorProperties
    {
        Name,
        Age,
        PowerLvl,
    }

    public class Indeksery
    {
        private List<Warriors> warriorsList = new List<Warriors>();

        public Indeksery()
        {
            warriorsList.Add(new Warriors() { Age = 25, Name = "Marko", PowerLvl = 10 });
            warriorsList.Add(new Warriors() { Age = 30, Name = "Gremblo", PowerLvl = 8 });
            warriorsList.Add(new Warriors() { Age = 15, Name = "Worko", PowerLvl = 3 });
        }

        //zwykła wyszukiwarka po wieku
        public Warriors GetWarriorsByAge(int age)
        {
            foreach (Warriors item in warriorsList)
            {
                if (item.Age==age)
                {
                    return item;
                }
            }
            return null;
        }

        //zwykła wyszukiwarka po powerlvlu
        public Warriors GetWarriorsByPowerLvl(int powerLvl)
        {
            foreach (Warriors item in warriorsList)
            {
                if (item.PowerLvl == powerLvl)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// A better searcher, however if there is two int, how to deal with this problem?
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public Warriors this[int age]
        {
            get
            {
                foreach (Warriors item in warriorsList)
                {
                    if (item.Age == age)
                    {
                        return item;
                    }
                }
                return null;
            }
        }
        //teraz wystarczy zrobić Indekser[Wolko]

        /// <summary>
        /// A better searcher, however if there is two int, how to deal with this problem?
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Warriors this[string name]
        {
            get
            {
                foreach (Warriors item in warriorsList)
                {
                    if (item.Name == name)
                    {
                        return item;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Best searcher
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Warriors this[WarriorProperties warriorProperties, object x]
        {
            get
            {
                switch ((WarriorProperties)warriorProperties)
                {
                    case WarriorProperties.Name:
                        foreach (Warriors item in warriorsList)
                        {
                            if (item.Name == x.ToString())
                            {
                                return item;
                            }
                        }
                        return null;
                    case WarriorProperties.Age:
                        foreach (Warriors item in warriorsList)
                        {
                            try
                            {
                                if (item.Age == Int32.Parse(x.ToString()))
                                {
                                    return item;
                                }
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        return null;
                    case WarriorProperties.PowerLvl:
                        foreach (Warriors item in warriorsList)
                        {
                            try
                            {
                                if (item.PowerLvl == Int32.Parse(x.ToString()))
                                {
                                    return item;
                                }
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        return null;
                    default:
                        return null;
                }


            }
        }

    }


    public class Warriors
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int PowerLvl { get; set; }
    
    }


}
