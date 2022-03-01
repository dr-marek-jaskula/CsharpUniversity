using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WzorceProjektowe
{
    enum OS_TYPE
    {
        Windows,
        OsX
    }

    // Przykład GUIFactory 

    abstract class GUIFactory
    {
        /// <summary>
        /// getFactory returns concrete Factory,
        ///instead parameter abstract Factory can get OS_Type from outer method
        /// </summary>
        /// <param name="type">Operating System</param>
        /// <returns></returns>
        public static GUIFactory GetFactory(OS_TYPE type)
        {
            return type switch
            {
                OS_TYPE.Windows => new WinFactory(),
                OS_TYPE.OsX => new OSXFactory(),
                _ => throw new NotImplementedException(),
            };
        }

        public abstract Button CreateButton();
    }


    class WinFactory : GUIFactory
    {
        public override Button CreateButton()
        {
            return new WinButton();
        }
    }


    class OSXFactory : GUIFactory
    {
        public override Button CreateButton()
        {
            return new OSXButton();
        }
    }

    abstract class Button
    {
        public abstract void Paint();
    }

    class WinButton : Button
    {
        public override void Paint()
        {
            Console.WriteLine("Przycisk WinButton");
        }
    }

    class OSXButton : Button
    {
        public override void Paint()
        {
            Console.WriteLine("Przycisk OSXButton");
        }
    }


    public class Application
    {
        public static void AbstractFactoryPattern()
        {
            GUIFactory factory = GUIFactory.GetFactory(OS_TYPE.Windows);
            Button button = factory.CreateButton();
            button.Paint();
            Console.ReadLine();
        }
        // Wyświetlony zostanie tekst:
        //   "Przycisk WinButton"
        // lub:
        //   "Przycisk OSXButton"
    }
}
