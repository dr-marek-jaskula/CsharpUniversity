using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningApplication3
{
    public enum EmployeeType
    {
        Teacher,
        HeadOfDepartment,
        DeputyHeadMaster,
        HeadMaster,
    }

    //przełączyłem z  -> properties -> application -> .Net Core 3.1 na .Net 5

    class Program
    {
        static void Main()
        {
            //decimal totalSalaries = 0;
            //List<IEmployee> employees = new List<IEmployee>();

            //SeedData(employees);

            //foreach (IEmployee employee in employees)
            //{
            //    totalSalaries += employee.Salary;
            //}

            //Console.WriteLine($"Total Annual Salaries (including bonus): {totalSalaries}");


            //Console.WriteLine($"Total Annual Salaries (including bonus): {employees.Sum<IEmployee>(e => e.Salary)}");
            //to robi to samo co to zakomentowane wyżej

            //zakomentowuje to wszystko wyżej zeby robić dalej:

            //DelegatesPart1.Program1();
            //DelegatesPart2.Program2();
            //TestingSpree.ProgramX();

            Indeksery newIndekser = new Indeksery();

            var a = newIndekser.GetWarriorsByAge(15);

            Indeksery newIndekser2 = new Indeksery();
            var b = newIndekser2[30];
            var c = newIndekser2["Marko"];
            var d = newIndekser2[WarriorProperties.Age, 30];
            var e = newIndekser2[WarriorProperties.Name, "Worko"];
            var f = newIndekser2[WarriorProperties.PowerLvl, 8];
            //var g = newIndekser2[WarriorProperties.PowerLvl, "fmf"];

            //KeyWordYield.Program4();
            //KeyWordDynamic.Program5();
            //MethodOverride.Program6();
            //AttributesLearning.Attri();
            //AttributesLearning2.Attri2();
            //PreprocessorDefinitions.PreStartNow();
            AttributeCustom3.AtriCustom1();
            //SpanStackalloc.LerningSpan();
            //AttributesLearning4.TechingIsGold();
            //AttributesLeratning5.TeachingIsGlod2();
        }

        public static void SeedData(List<IEmployee> employees)
        {
            IEmployee teacher1 = EmployeeFactory.GetEmplyeeInstance(EmployeeType.Teacher,1 , "Bob", "Fisher", 40000);
            IEmployee teacher2 = EmployeeFactory.GetEmplyeeInstance(EmployeeType.Teacher, 2, "Jenny", "Thomas", 40000);
            IEmployee headOfDepartment = EmployeeFactory.GetEmplyeeInstance(EmployeeType.HeadOfDepartment, 3, "Brenda", "Mulins", 50000);
            IEmployee deputyHeadMaster = EmployeeFactory.GetEmplyeeInstance(EmployeeType.DeputyHeadMaster, 4, "Devlin", "Brown", 60000);
            IEmployee headMaster = EmployeeFactory.GetEmplyeeInstance(EmployeeType.HeadMaster, 5, "Damien", "Jones", 80000);
 
            employees.Add(teacher1);
            employees.Add(teacher2);
            employees.Add(headOfDepartment);
            employees.Add(deputyHeadMaster);
            employees.Add(headMaster);
        }

    }




    public class Teacher : EmployeeBase
    {
        public override decimal Salary { get => base.Salary + (base.Salary*0.02m); }

    }



    public class HeadOfDepartment : EmployeeBase
    {
        public override decimal Salary { get => base.Salary + (base.Salary * 0.03m); }

    }


    public class DeputyHeadMaster : EmployeeBase
    {
        public override decimal Salary { get => base.Salary + (base.Salary * 0.04m); }
    }

    public class HeadMaster : EmployeeBase
    {
        public override decimal Salary { get => base.Salary + (base.Salary * 0.05m); }

    }


    public static class EmployeeFactory
    {
        public static IEmployee GetEmplyeeInstance(EmployeeType employeeType, int id, string firstName, string lastName, decimal salary)
        {
            IEmployee employee = null;

            switch ((EmployeeType)employeeType)
            {
                case EmployeeType.Teacher:
                    employee = FactoryPattern<IEmployee, Teacher>.GetInstance();
                    break;
                case EmployeeType.HeadOfDepartment:
                    employee = FactoryPattern<IEmployee, HeadOfDepartment>.GetInstance();
                    break;
                case EmployeeType.DeputyHeadMaster:
                    employee = FactoryPattern<IEmployee, DeputyHeadMaster>.GetInstance();
                    break;
                case EmployeeType.HeadMaster:
                    employee = FactoryPattern<IEmployee, HeadMaster>.GetInstance();
                    break;
                default:
                    break;
            }
            if (employee != null)
            {
                employee.ID = id;
                employee.FirstName = firstName;
                employee.LastName = lastName;
                employee.Salary = salary;
            }
            else
            {
                throw new NullReferenceException();
            }
            return employee;
        }
    }



}
