using System.Diagnostics;

namespace CsharpAdvanced.DelegatesEvents;

public class EventsAddRemove
{
    public static void InvokeEventsAddRemoveExamples()
    {
        Cow cow = new() { Name = "Berta" };

        //When adding a delegate by this event everything in the "add" will be executed. So its like a setter
        cow.Mooing += () => Debug.WriteLine("I say: moooooooo");

        //Therefore, Mooooo will be called twice
        cow.PushTheSleepingCow();

        //This will not remove one of the delegates, because it is not properly selected. However, the massage from remove will be executed
        cow.Mooing -= () => Console.WriteLine("moooooooo");
        cow.PushTheSleepingCow();

        ///With event handlers
        Cow2 cow2 = new() { Name = "Arta" };
        cow2.Mooing2 += (s, args) => Console.WriteLine("moooo");
        cow2.EventHandler?.Invoke(cow2, new EventArgs());
    }
}

internal class Cow
{
    public string Name { get; set; } = string.Empty;
    private Action? mooing;

    //"add" and "remove" are like setter and getter
    public event Action? Mooing
    {
        add
        {
            mooing += value;
            mooing += value;
            Debug.WriteLine("Add twice the delegate and write this message");
        }

        remove
        {
            mooing -= value;
            Debug.WriteLine("Remove only once but also write a massage");
        }
    }

    public void PushTheSleepingCow()
    {
        if(mooing is not null)
            mooing();
    }
}

//Class with build in EventHandler
internal class Cow2
{
    public string Name { get; set; } = string.Empty;

    public EventHandler? EventHandler;

    public event EventHandler Mooing2
    {
        add
        {
            Console.WriteLine("I still add mooing");
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
