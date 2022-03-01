﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using VendorClass1;

namespace MultiThreadingLearning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VendorClass publicSample;
        VendorClass privateSample;

        //Jesli tworzymy olbrzymie kolekcje to warto sprawdzic czy many zaznaczone "orefer 32 bit" w properties -> build
        //kolejna rzecza jest to zeby w app.config napisac:
        /*
        <runtime>
        <gcAllowVeryLargeObjects enabled="true"/>
        </runtime>
        
        albo stworzyć długo działającego Taska: AllowLongRunningTask

         
         */
        public MainWindow()
        {
            InitializeComponent();

            publicSample = new VendorClass();
            privateSample = new VendorClass();
        }

        #region Speed
        private void ListCreateStandard_Click(object sender, RoutedEventArgs e)
        {

            /*
            var task = Task.Delay(1).ContinueWith((t) =>
            {
                Dispatcher.Invoke(() =>
                {

                });
            });

            task.Wait();
            //to spowoduje "deadlock".
            //problem jest taki, że próbujemy wywołać UI, a ono nie może się wywołać bo ma czekać aż sie skonczy task
            //trzeba zwrocic szczególną uwagę na kolejność.
            */

            //teraz inny deadlock (ze synchronicznie próbujemy)
           
            try
            {
                ListCreateStandardBtn.IsEnabled = false; 
                long elapsedMs = 0;
                int iterationsTime = int.Parse(IterationsBox.Text);

                //zrobienie tutaj Result zrobi deadlocka. Dlaczego:
                //do tego momentu jestesmy w main UI thread. Zatem Caller Thread to UI thread. Zatem metoda wywływana będzie w UI thread (az nie zrobi swojego threada). Natomiast .Result blokuje thread, aż osiągnie się wynik. Dlatego blokuje main thread i sie mrozi
                //     elapsedMs = TestAsync2(iterationsTime, elapsedMs).Result;
                //rozwiązania potencjalne:
                elapsedMs = Task.Run(() => TestAsync2(iterationsTime, elapsedMs)).Result; //teraz wywołujemy na wątku Ui nowy thread, który jest zamrożony az do momentu, do ktorego wnętrznosic nie dadza rezulatu
                //to sprawi ze nie bedzie dead locka, ale i tak UI sie zmrozi jakby bylo wszystko w jednym wątku
                    
                //robienie .Result to źródła wszelkiego zła -> chyba ze sie bardzo dobrze wie co robi.


                    // .ConfigureAwait(false);
                    // to chyba spowoduje ze kontynuwaca będzie w wątku, który został stworzony a nie wraca do poprzedniego wątku 
                
                //dodajemy metode co nam zrobi blad, a jest voidem i ma w sobie taska ze sleepem

                ListCreateStandardLabelFill(elapsedMs);
                ListCreateStandardBtn.IsEnabled = true; 

            }
            catch (Exception ex)
            {

            }
          

        }

        private async void ListCreateReflectionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListCreateReflectionBtn.IsEnabled = false; 
                await TestAsync();
                ListCreateReflectionBtn.IsEnabled = true; 

            }
            catch (Exception ex)
            {

            }
        }

        private async Task TestAsync()
        {
            int iterationTime = int.Parse(IterationsBox.Text);
            long elapsedMs = 0;

            await Task.Run(() =>
            {
                Type listType = typeof(List<int>);

                var watch = Stopwatch.StartNew();
                for (int i = 0; i < iterationTime; i++)
                {
                    var testList = Activator.CreateInstance(listType);
                }
                elapsedMs = watch.ElapsedMilliseconds;
            });
            ListCrateWithReflectionLabelFill(elapsedMs);
        }

        private async Task TestAsync3() //void jest złem, nigdy nie robić VOID, chyba ze to zło konieczne jak w evencie, bo tam musi byc z typem delegata
        {
            throw new UnauthorizedAccessException();

            await Task.Run(() =>
            {
                Thread.Sleep(2000);

            });
        }

        private async Task<long> TestAsync2(long iterationsTime, long elapsedMs)
        {

            await Task.Run(() =>
            {
                //sztucznie generujemy wyjątki po to aby zobczyc co moze sie stac i jak sobie z tym radzić
                //throw new UnauthorizedAccessException();
                //nie działa przechytywanie błędów, bo w nowym wątku 

                try
                {

                    var list = new List<int>();
                    var watch = Stopwatch.StartNew();
                    for (int i = 0; i < iterationsTime; i++)
                    {
                        list.Add(i);
                    }
                    elapsedMs = watch.ElapsedMilliseconds;
                }
                catch (Exception)
                {

                }
            });
            return elapsedMs;

        }



        private void ListAddStandardBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long elapsedMs = ListAddStandardTime();
                ListAddStandardLabelFill(elapsedMs);
            }
            catch (Exception ex)
            {

            }
        }

        private void ListAddReflectionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long elapsedMs = ListAddReflectionTime();
                ListAddReflectionLabelFill(elapsedMs);
            }
            catch (Exception ex)
            {

            }

        }

        #endregion

        #region Safety
        private void GetVendorPublicItemsBtn_Click(object sender, RoutedEventArgs e)
        {
            VendorPublicItemsListBox.ItemsSource = null;
            VendorPublicItemsListBox.ItemsSource = publicSample.VendorPublicItems;
        }

        private void GetVendorPrivateItemsBtn_Click(object sender, RoutedEventArgs e)
        {
            Type testType = typeof(VendorClass);
            FieldInfo vendorField = testType.GetField("vendorPrivateList", BindingFlags.NonPublic | BindingFlags.Instance);

            List<String> vendorPrivate = vendorField.GetValue(privateSample) as List<String>;

            VendorPrivateItemsListBox.ItemsSource = null;
            VendorPrivateItemsListBox.ItemsSource = vendorPrivate;

        }


        #endregion

        #region HelpersArea
        private string Calculatetime(long elapsedMs)
        {
            return elapsedMs + " m/s";
        }

        private long ListCreateStandardTime()
        {
            int iterationsTime = int.Parse(IterationsBox.Text);

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < iterationsTime; i++)
            {
                var testList = new List<int>();
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            return elapsedMs;
        }

        private long ListCreateWithReflectionTime()
        {
            int iterationsTime = int.Parse(IterationsBox.Text);

            Type listType = typeof(List<int>);

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < iterationsTime; i++)
            {
                var testList = Activator.CreateInstance(listType);
            }
            var elapsedMs = watch.ElapsedMilliseconds;
            return elapsedMs;
        }


        private long ListAddStandardTime()
        {
            int iterationsTime = int.Parse(IterationsBox.Text);

            var list = new List<int>();

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < iterationsTime; i++)
            {
                list.Add(i);
            }
            var elapsedMs = watch.ElapsedMilliseconds;
            return elapsedMs;
        }

        private long ListAddReflectionTime()
        {
            int iterationsTime = int.Parse(IterationsBox.Text);

            var list = new List<int>();

            Type listType = typeof(List<int>);
            Type[] parametersType = { typeof(int) };
            MethodInfo mi = listType.GetMethod("Add", parametersType);

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < iterationsTime; i++)
            {
                mi.Invoke(list, new object[] { i });
            }
            var elapsedMs = watch.ElapsedMilliseconds;
            return elapsedMs;
        }

        private void ListAddStandardLabelFill(long elapsedMs)
        {
            ListAddStandardStd.Content = Calculatetime(elapsedMs);
        }

        private void ListCreateStandardLabelFill(long elapsedMs)
        {
            ListCreateStandardLab.Content = Calculatetime(elapsedMs);
        }

        private void ListCrateWithReflectionLabelFill(long elapsedMs)
        {
            ListCreateReflectionLab.Content = Calculatetime(elapsedMs);
        }

        private void ListAddReflectionLabelFill(long elapsedMs)
        {
            ListAddReflectionStd.Content = Calculatetime(elapsedMs);
        }

        #endregion


    }
}


#region Copied
/*
// najstarsza forma robienia taska:
int iterationTime = int.Parse(IterationsBox.Text);
long elapsedMs = 0;

Task T = Task.Run(() =>
{
    Type listType = typeof(List<int>);

    var watch = Stopwatch.StartNew();
    for (int i = 0; i < iterationTime; i++)
    {
        var testList = Activator.CreateInstance(listType);
    }
    elapsedMs = watch.ElapsedMilliseconds;


    Dispatcher.Invoke(() =>
    { //Invokujemy Dispatchera na obecnie zsynchronizowanym wątku, ale może do konkretnego obiektu
        ListCrateWithReflectionLabelFill(elapsedMs);
    });
    //sluzy do synchronizowania wątków
    //jest to zastępnik to tego co tam jest dalej zakomentowanie, czyli continueWith.
    //W ciele danego wątku wszystko wykonuje sie synchronicznie, na koncu jest dispatcher
    //nie sprawdza to czy task sie skonczyl, tylko po prostu po porzednich linijkach jest ta ostatnia

    //nie zablokowalismy przycisku, wiec on dopóki nie zrobi zadania moze pobierac kolejne i czasami to ejst dobre a czasami złe.


});


////jest to kontynuacja, co sie stanie gdy tamto sie skonczy
//T.ContinueWith((t) => { 
//ListCrateWithReflectionLabelFill(elapsedMs); //to jest to co ma wykonać się po tym jak skonczy się task. 
//}, TaskScheduler.FromCurrentSynchronizationContext()); //należy schynchronizować konteksty



//TaskScheduler ma informacje o taska, dajemu mu context z wątku

//long elapsedMs = 0;
//int iterationsTime = int.Parse(IterationsBox.Text);

//elapsedMs = Task.Run(() => TestAsync(iterationsTime, elapsedMs)).Result;
*/
#endregion