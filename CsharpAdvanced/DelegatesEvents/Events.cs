using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;

namespace LearningApplication2
{
    class Events
    {

        public static void Prigram26()
        {

            AgendaMenager amgr = new AgendaMenager();
            SmsSender sms = new SmsSender();

            amgr.AddAgenda(new Agenda()
            {
                AgendaDate = DateTime.Now.AddDays(2),
                AgendaName = "Kubuś Puchatek i dziesięciu rozbójników"
            });

            amgr.AddedAgenda += sms.OnAddedAgenda; //to tak naprawdę jest wskaźnik, wskazujemy metodę, dlatego bez nawiasów. Dodał się do eventu kolejna metoda (są virtual więc się dodają elegancko)


            amgr.AddAgenda(new Agenda()
            {
                AgendaDate = DateTime.Now.AddDays(2),
                AgendaName = "Kubuś Puchatek i dziesięciu rozbójników"
            });

            amgr.AddedAgenda += EventNumber2;

            amgr.AddAgenda(new Agenda() { AgendaDate = DateTime.Now.AddDays(1), AgendaName = "Myszoskoczki" });


            amgr.AddedAgendaShorter += sms.OnAddedAgendaShorter;

            amgr.AddAgendaShorter(new Agenda()
            {
                AgendaDate = DateTime.Now.AddDays(20),
                AgendaName = "Władek i Waldek"
            });


        }

        public static void EventNumber2(object o, AgendaEventArgs e)
        {
            Console.WriteLine("tolololo");
        }

}



    public class AgendaMenager
    {

        public delegate void AddedAgendaEventHandler(object o, AgendaEventArgs e);

        public event AddedAgendaEventHandler AddedAgenda;

        //możliwe pomijanie delegatów, tj bazowo dane
        // EventHandler
        // EventHandler<TEventArs>
        public event EventHandler<AgendaEventArgs> AddedAgendaShorter;


       // tworzymy pubishera

        /// <summary>
        /// Event publisher powinien być 
        /// 1. protected
        /// 2. virtual
        /// 3. void
        /// 4. Nazwa powinna zaczynać się na "On"
        /// </summary>
        /// <param name="newAgenda"></param>
        protected virtual void OnAddedAgenda(Agenda newAgenda)
        {
            if (AddedAgenda != null)
                AddedAgenda(this, new AgendaEventArgs() { Agenda = newAgenda });
            if (AddedAgenda == null)
                Console.WriteLine("Publisher says: event is null!"); ;
        }


        public void AddAgenda(Agenda newAgenda)
        {
            Console.WriteLine("AddAgenda: Zaczynam dodawać ...");
            Thread.Sleep(3000);
            OnAddedAgenda(newAgenda);
            Console.WriteLine("AddAgenda: skonczylem dodawać");   
        }

        protected virtual void OnAddedAgendaShorter(Agenda newAgenda)
        {
            if (AddedAgendaShorter != null)
                AddedAgendaShorter(this, new AgendaEventArgs() { Agenda = newAgenda });
            if (AddedAgendaShorter == null)
                Console.WriteLine("Puhliser says: event is null!"); ;
        }


        public void AddAgendaShorter(Agenda newAgenda)
        {
            Console.WriteLine("AddAgenda: Zaczynam dodawać ...");
            Thread.Sleep(3000);
            OnAddedAgendaShorter(newAgenda);
            Console.WriteLine("AddAgenda: skonczylem dodawać");
        }

    }


    public class SmsSender
    {
        public void OnAddedAgenda(object o, AgendaEventArgs e)
        {
            Console.WriteLine("SMS Sender: SMS was send! Data:" + e.Agenda.AgendaDate + " Tytuł: " + e.Agenda.AgendaName);
        }

        public void OnAddedAgendaShorter(object o, AgendaEventArgs e)
        {
            Console.WriteLine("SMS Sender: SHORTER SMS was send! Data:" + e.Agenda.AgendaDate + " Tytuł: " + e.Agenda.AgendaName);
        }


    }

    public class Agenda
    {
        public DateTime AgendaDate { get; set; }
        public string AgendaName { get; set; }
    }



    public class AgendaEventArgs: EventArgs //robimy własne argumenty, wiec dziedziczymy z EventArgs
    {
        public Agenda Agenda { get; set; }


    }
}
