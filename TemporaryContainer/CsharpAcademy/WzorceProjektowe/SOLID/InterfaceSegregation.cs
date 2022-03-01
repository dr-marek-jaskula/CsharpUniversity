using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WzorceProjektowe
{
    class InterfaceSegregation
    {
        //clients should not be forced to implement interfaces they don't use. Instead of one fat interface, many small interfaces are preferred based on groups of methods, each one serving one submodule.
    }

    #region Bad Example

    public interface ILead
    {
        void CreateSubTask();
        void AssignTask();
        void WorkOnTask();
    }

    public class TeamLead : ILead
    {
        public void AssignTask()
        {
            Console.WriteLine("TeamLead assigning task");
        }

        public void CreateSubTask()
        {
            Console.WriteLine("TeamLead creating sub task");
        }

        public void WorkOnTask()
        {
            Console.WriteLine("TeamLead implementing perform assigned task");
        }
    }

    public class Manager : ILead
    {
        public void AssignTask()
        {
            Console.WriteLine("Manager assigning task");
        }

        public void CreateSubTask()
        {
            Console.WriteLine("Manager creating sub task");
        }

        /// <summary>
        /// Here we force the manager class to have this WorkOnTask method without any purpose. This is wrong and violates this principle
        /// </summary>
        public void WorkOnTask()
        {
            throw new Exception("Manager cant work on Task");
        }
    }


    #endregion


    #region good Example

    public interface ILead1
    {
        void CreateSubTask();
        void AssignTask();
    }

    public interface IProgrammer
    {
        void WorkOnTask();
    }

    public class TeamLead1 : ILead1, IProgrammer
    {
        public void AssignTask()
        {
            Console.WriteLine("TeamLead assigning task");
        }

        public void CreateSubTask()
        {
            Console.WriteLine("TeamLead creating sub task");
        }

        public void WorkOnTask()
        {
            Console.WriteLine("TeamLead implementing perform assigned task");
        }
    }

    public class Manager1 : ILead1
    {
        public void AssignTask()
        {
            Console.WriteLine("Manager assigning task");
        }

        public void CreateSubTask()
        {
            Console.WriteLine("Manager creating sub task");
        }
    }

    public class Programmer : IProgrammer
    {
        public void WorkOnTask()
        {
            Console.WriteLine("Programmer Work on task");
        }
    }
    #endregion

}
