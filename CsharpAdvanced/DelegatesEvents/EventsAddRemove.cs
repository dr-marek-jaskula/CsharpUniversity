using System;

namespace LearningApplication2
{
    internal class EventsAddRemove
    {
        public static void LearningEventsAddRemove()
        {
            Cow cow = new Cow() { name = "Berta" };

            //when adding a dalagate by this even, everything in the "add" will be executed. So its like a setter
            cow.Mooing += () => Console.WriteLine("moooooooo");
            //therefore mooooo will be called twice
            cow.PushTheSleepingCow();

            //this will not remove one of the delagets because it is not properly selected. However the massage from remove will be executed
            cow.Mooing -= () => Console.WriteLine("moooooooo");
            cow.PushTheSleepingCow();

            ///With event handlers
            ///
            Cow2 cow2 = new Cow2() { name = "Arta" };
            cow2.Mooing2 += (s, args) => Console.WriteLine("moooo");
            cow2.EventHandler.Invoke(cow2, new EventArgs());
        }
    }

    internal class Cow
    {
        public string name { get; set; }
        private Action moooing;

        public event Action Mooing
        {
            add
            {
                moooing += value;
                moooing += value;
                Console.WriteLine("Add twice the delegate and write this message");
            }

            remove
            {
                moooing -= value;
                Console.WriteLine("Remove only once but also write a massage1");
            }
        }

        public void PushTheSleepingCow()
        {
            moooing();
        }
    }

    internal class Cow2
    {
        public string name { get; set; }

        public EventHandler EventHandler;

        public event EventHandler Mooing2
        {
            add
            {
                Console.WriteLine("Hehe I add mooing");
                EventHandler += value;
                EventHandler += value;
            }
            remove
            {
                Console.WriteLine("Time to remove moooo");
                EventHandler -= value;
            }
        }
    }
}