using System.Diagnostics;

namespace CsharpAdvanced.DelegatesEvents;

public class EventsBasic
{
    //Events are methods that are triggered when certain circumstances occurs
    //To define events we use "event" keyword
    //The naming convention is to use prefix "On" for method that publish the event.
    //Moreover, it should be protected, virtual and void

    //1. At first we create a class that store some data (in our case it will be agenda class)
    public class Agenda
    {
        public DateTime AgendaDate { get; set; }
        public string AgendaName { get; set; } = string.Empty;
    }

    //2. Then we create a class, that will contain our events, based on our Agenda class
    public class AgendaMenager
    {
        //3. We create an action (AgendaEventHandler) that will be used for our events. Its arguments are: our object and some custom args (we define them below)
        public delegate void AgendaEventHandler(object o, AgendaEventArgs e);

        //4. Then we define our event, based on the our delegate
        public event AgendaEventHandler? AddedAgenda;

        //4. Better! However, c# provide a shorter way, without creating our delegate. So we just need to use build in EventHandler class, with our event args
        public event EventHandler<AgendaEventArgs>? AddedAgendaShorter;

        //5. Next, we create a publisher: "On" prefix, and protected virtual void.
        protected virtual void OnAddedAgenda(Agenda newAgenda)
        {
            //To execute stored events, we need to just write EventName(this, someArgs)
            if (AddedAgenda is not null)
                AddedAgenda(this, new AgendaEventArgs() { Agenda = newAgenda });
            else
                Debug.WriteLine("Publisher says: event is null!"); ;
        }

        //5. Better! Version for build in EventHandler
        protected virtual void OnAddedAgendaShorter(Agenda newAgenda)
        {
            if (AddedAgendaShorter is not null)
                AddedAgendaShorter(this, new AgendaEventArgs() { Agenda = newAgenda });
            else
                Debug.WriteLine("Publisher says: event is null!"); ;
        }

        //Method, that will trigger the event
        public void AddAgenda(Agenda newAgenda)
        {
            Debug.WriteLine("AddAgenda: start adding agenda...");
            Thread.Sleep(3000);
            OnAddedAgenda(newAgenda);
            Debug.WriteLine("AddAgenda: done");
        }

        //Same but for build in EventHandler
        public void AddAgendaShorter(Agenda newAgenda)
        {
            Debug.WriteLine("AddAgenda: start adding agenda...");
            Thread.Sleep(3000);
            OnAddedAgendaShorter(newAgenda);
            Debug.WriteLine("AddAgenda: done");
        }
    }

    //6. We create an AgendaEventArgs, class that needs to inherit from the "EventArgs" class
    public class AgendaEventArgs : EventArgs
    {
        public Agenda? Agenda { get; set; }
    }

    //7. We can create other class for publishing purpose
    public class SmsSender
    {
        public void OnAddedAgenda(object o, AgendaEventArgs e)
        {
            Debug.WriteLine("SMS Sender: SMS was send! Data:" + e.Agenda?.AgendaDate + " Title: " + e.Agenda?.AgendaName);
        }

        public void OnAddedAgendaShorter(object? o, AgendaEventArgs e)
        {
            Debug.WriteLine("SMS Sender: SHORTER SMS was send! Data:" + e.Agenda?.AgendaDate + " Title: " + e.Agenda?.AgendaName);
        }
    }

    //We can also do just:
    public static void EventNumberTwo(object o, AgendaEventArgs e)
    {
        Debug.WriteLine("This is event number two");
    }

    public static void InvokeEventsExamples()
    {
        AgendaMenager amgr = new();
        SmsSender sms = new();

        amgr.AddAgenda(new()
        {
            AgendaDate = DateTime.Now.AddDays(2),
            AgendaName = "Important meeting 1",
        });

        //We add publishers to our event (we use syntax +=)
        amgr.AddedAgenda += sms.OnAddedAgenda; 

        //Now when we add an agenda...
        amgr.AddAgenda(new()
        {
            AgendaDate = DateTime.Now.AddDays(3),
            AgendaName = "Matrix"
        });

        amgr.AddedAgenda += EventNumberTwo;

        amgr.AddAgenda(new() 
        { 
            AgendaDate = DateTime.Now.AddDays(1), 
            AgendaName = "NBA",
        });

        amgr.AddedAgendaShorter += sms.OnAddedAgendaShorter;

        amgr.AddAgendaShorter(new Agenda()
        {
            AgendaDate = DateTime.Now.AddDays(20),
            AgendaName = "Fifa"
        });


        #region Other Example
        
        Debug.WriteLine("Press A to simulate a button click");
        var key = Console.ReadLine();
        if (key == "a")
        {
            KeyPressed();
        }

        #endregion
        }

    static void KeyPressed()
    {
        Button button = new();

        button.ClickEvent += (s, args) =>
        {
            Debug.WriteLine($"Your clicked a button {args.Name}");
        };

        button.OnClick();
    }
}

#region Other Example

public class Button
{
    public EventHandler<MyCustomArguments>? ClickEvent;

    public void OnClick()
    {
        ClickEvent?.Invoke(this, new() { Name = "Marek" });
    }
}

public class MyCustomArguments : EventArgs
{
    public string Name { get; set; } = string.Empty;
}


#endregion