#define LOG_INFO
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using ValidationComponent;
using System.Text.Json;
//trzeba system Reflection aby zrobić tutaj global attribute

//builder aplikacji samoistnie deklaruje sporo rzeczy i w tym AssemblyVersion, dlatego robi błąd
//[assembly: AssemblyVersion("2.0.1")]
//to jest global attribute, dlatego jest pomiędyz using a namespace
//assembly to jest progrma lub biblioteka 

[assembly: AssemblyDescription("My Assembly Description")]
//attributes najczęściej nie zmieniają application behavior, ale zmieniają metadane czyli dane spowinowacone

namespace LearningApplication3
{
    class AttributesLearning4
    {
        public static void TechingIsGold()
        {
            //uzywamy reflection
            #region About Assembly and assembly globacl attributes

            Assembly assembly = typeof(Program).Assembly;
            AssemblyName assemblyName = assembly.GetName();
            Version version = assemblyName.Version;

            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute),false);

            var assemblyDescriptionAttribute = attributes[0] as AssemblyDescriptionAttribute;

            Console.WriteLine($"Assembly Name: {assemblyName}");
            Console.WriteLine($"Assembly Version: {version}");

            if(assemblyDescriptionAttribute!=null)
                Console.WriteLine($"Assembly Description: {assemblyDescriptionAttribute.Description}");
            #endregion

            LoggingComponent.Logging.LogToScreen("This code is testing logging functionality");

            #region Customs Atri

            EmployeeCustom emp = new EmployeeCustom();
            string empId = null;
            string firstName = null;
            string postCode = null;

            Type employeeCustomType = typeof(EmployeeCustom);

            if (GetInput(employeeCustomType, "Please enter the emplyee's id", "Id", out empId))
            {
                emp.Id = Int32.Parse(empId);
            }
             
            if (GetInput(employeeCustomType, "Please enter the emplyee's first name", "FirstName", out firstName))
            {
                emp.FirstName = firstName;
            }

            if (GetInput(employeeCustomType, "Please enter the emplyee's post code", "FirstName", out postCode))
            {
                emp.PostCode = postCode;
            }


            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Thank you. Emplyee with first name {emp.FirstName} and Id {emp.Id} has been entered successfully");
            Console.ResetColor();

            Console.WriteLine("---------------------");

            DepartmentCustom dept = new DepartmentCustom();
            string deptShortName = null;
            Type departmentType = typeof(DepartmentCustom);

            if (GetInput(departmentType, "Please enter the emplyee's department code", "DeptShortName", out deptShortName))
            {
                dept.DeptShortName = deptShortName;
            }

            #endregion

            #region Json not custom Atri

            EmployeePredefinedAtri employeePre = new EmployeePredefinedAtri();
            string employeePreId = null;
            string employeePrefirstName = null;
            string employeePrepostCode = null;

            Type employeePreCustomType = typeof(EmployeePredefinedAtri);

            if (GetInput(employeePreCustomType, "Please enter the emplyee's id", "Id", out employeePreId))
            {
                employeePre.Id = Int32.Parse(employeePreId);
            }

            if (GetInput(employeePreCustomType, "Please enter the emplyee's first name", "FirstName", out employeePrefirstName))
            {
                employeePre.FirstName = employeePrefirstName;
            }

            if (GetInput(employeePreCustomType, "Please enter the emplyee's post code", "FirstName", out employeePrepostCode))
            {
                employeePre.PostCode = employeePrepostCode;
            }

            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Thank you. JSON Emplyee with first name {employeePre.FirstName} and Id {employeePre.Id} has been entered successfully");
            Console.ResetColor();

            Console.WriteLine("---------------------");

            var employeeJSON = JsonSerializer.Serialize<EmployeePredefinedAtri>(employeePre);
            Console.WriteLine(employeeJSON);

            #endregion
        }


        private static bool GetInput(Type t, string promprText, string fieldName, out string fieldValue)
        {
            fieldValue = "";
            string enteredValue = "";
            string errorMessage = null;

            do
            {
                Console.WriteLine(promprText);
                enteredValue = Console.ReadLine();

                if(!Validation.PropertyValueIsValid(t,enteredValue,fieldName, out errorMessage))
                {
                    fieldValue = null;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(errorMessage);
                    Console.WriteLine();
                    Console.ResetColor();

                }
                else
                {
                    fieldValue = enteredValue;
                    break;
                }


            }
            while (true); //nieskonczona pętla 

            return true;
        }

    }
}
