using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WzorceProjektowe
{
    class DepedencyInversion
    {
        //    High-level modules should not depend on low-level modules. Both should depend on abstractions (e.g., interfaces).
        //    Abstractions should not depend on details.Details(concrete implementations) should depend on abstractions.

        // for example, even changing the data base provider (for example for MySQL to SQLServer) without Dependency Injection (wstrzykiwanie zależności) would require massive changes, but using DI we just need to change interface and would be good. (so the logger has only the list of methods but dont know how the function works). 
        // so interface provides the full list of possibilities without full knowledge. 
    }

    #region Good Example
    class DataBaseManager
    {
        string stringData;

        public void GetDataFromDataBase()
        {
            Console.WriteLine("I'm getting data from module");
            stringData += "Ha ";
        }
    }

    class LoggerManager
    {
        public void LogMeIn()
        {
            Console.WriteLine("I'm Logging in");
        }
    }

    class FileManager
    {
        public void ChangeFile()
        {
            Console.WriteLine("I'm changing files");
        }

    }
    #endregion
}
