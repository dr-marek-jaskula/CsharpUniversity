using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LearningApplication2
{
    class EventEventHandlers
    {
        public static void Program29()
        {
            Console.WriteLine("Press A to simulate a button click");
            var key = Console.ReadLine();
            if (key == "a")
            {
                KeyPressed();
            }



        }

        static void KeyPressed()
        {
            Button button = new Button();
            button.ClickEvent += (s, args) =>
            {
                Console.WriteLine($"Your clicked a button {args.Name}");
            };
            button.OnClick();
        }


    }

    public class Button
    {
        public EventHandler<MyCustomArguments> ClickEvent;

        public void OnClick()
        {
            MyCustomArguments myCustomArguments = new MyCustomArguments();
            myCustomArguments.Name = "Ian";

            ClickEvent.Invoke(this, myCustomArguments); //this to tutaj oznacza, że z tej klasy
        }

    }

    public class MyCustomArguments: EventArgs
    {
        public string Name { get; set; }

    }

}
