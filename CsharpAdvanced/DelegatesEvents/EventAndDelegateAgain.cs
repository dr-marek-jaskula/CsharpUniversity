using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LearningApplication2
{
    class EventAndDelegateAgain
    {
        public static void Program30()
        {

            Video video1 = new Video() { Title = "Bambusowe Przygody" };

            VideoEncoder videoEncoder1 = new VideoEncoder(); //publisher
            MailService mailService1 = new MailService(); //subscriber
            MessageService messageService1 = new MessageService(); //subsriber
            videoEncoder1.Encode(video1);

            videoEncoder1.VideoEncoded += mailService1.OnVideoEncoded; //dodaj do eventu, funkcję OnVideoEncoded z mailService class

            videoEncoder1.Encode(video1);

            
            videoEncoder1.VideoEncoded += messageService1.OnVideoEncoded;

            videoEncoder1.Encode(video1);

           // videoEncoder1.VideoEncoded += (object source, EventArgs e) => Console.WriteLine("fefe");

            videoEncoder1.Encode(video1);
            //bez zmiany VideoEncoder informowaliśmy inne klasy o tym, że cośtam się zadziało

            
        }


    }





    public class Video
    {
        public string Title { get; set; }

    }

    public class VideoEncoder
    {
        // 1 - define a delegate
        // 2 - define an event based on that delegate
        // 3 - Rise the event

        public delegate void VideoEncodedEventHandler(object source, VideoEventArgs args); // przyjeło się, że object jest pierwsze, co jest obiektem albo clasa z której wysyłamy informacje lub zbieramy. EvenArgs to additional data, we want to send.
        //w .Net ma się konwencje. Dodaje się EventHandler jako name delegate.

        //BAZOWO robiłem EventArgs u góry, ale że na dole stworzyliśmy customową clasę na eventy, to robimy VideoEventArgs

        public event VideoEncodedEventHandler VideoEncoded; // past tense to said something is finished, but we can use continous tance

        //to make the life easier od razu robi sie bez delegata, czyli EventHanlder
        // EventHandler
        // EventHandler<TEventArgs>

        //public EventHandler<VideoEventArgs> VideoEncoded; //to jest to samo co to wyzej. Zakomentować tamto wyżej i to odkomentować aby zweryfikować

        public void Encode(Video video)
        {
            Console.WriteLine("Encoding Video...");
            Thread.Sleep(3000);
            OnVideoEncoded(video); // tym kanałem publishera przekazujemy preści.
        }


        protected virtual void OnVideoEncoded(Video video) //.Net convention, protected, virtual i void, and we start the name with "On"
        {
            //to jest publisher, ten co odpowiada za publikowanie eventów 
            if (VideoEncoded != null)
                VideoEncoded(this, new VideoEventArgs() {Video = video }); //current object is sending publishing, and no additional data, so EventArgs.Empty
            else
                Console.WriteLine("Your event is empty as hell");

        }
    }


    public class MailService
    {
        //sumscriber, tj. recivers of the informations

        public void OnVideoEncoded(object source, VideoEventArgs args) //zbieżność nazw jest celowa, po to by kojażyło się jedno z drugim, nie ma w tej nazwie "mechaniki". 
        {
            Console.WriteLine($"MailService: Send an email about new wideo called {args.Video.Title}");
        }


    }

    public class MessageService
    {
        public void OnVideoEncoded(object source, VideoEventArgs args)
        {
            Console.WriteLine($"MessageServce: Sending a text message about new wideo called {args.Video.Title}");

        }
    }

    public class VideoEventArgs : EventArgs //tworzymy, włąsne custome argumenty aby wyciągać dane. Musi dziedziczyc z EventArgs
    {
        public Video Video { get; set; }
    
    }

}
