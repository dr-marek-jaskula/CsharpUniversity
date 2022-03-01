using System;
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

        public MainWindow()
        {
            InitializeComponent();

            publicSample = new VendorClass();
            privateSample = new VendorClass();
        }

        #region Speed
        private async void ListCreateStandard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //teraz chcemy uzyskać wartość z Taska, dlatego robimy generics
                ListCreateStandardBtn.IsEnabled = false; //blokujemy guzik na czas wykonywania taska
                long elapsedMs = 0;
                int iterationsTime = int.Parse(IterationsBox.Text);

                elapsedMs = await TestAsync2(iterationsTime, elapsedMs);

                ListCreateStandardLabelFill(elapsedMs);
                ListCreateStandardBtn.IsEnabled = true; // odblokowywujemy guzik


            }
            catch (Exception ex)
            {

            }

        }

        private async void ListCreateReflectionBtn_Click(object sender, RoutedEventArgs e)
        {
            //dlaczego tu jest async void? Zdarzenie musi być takie jak delegat, a delegat jest void, ale! lepiej z Taskiem.
            try
            {
                //każda funkcja asynchroniczna powinna miec Async na koncu - konwencja, tj norma  
                ListCreateReflectionBtn.IsEnabled = false; //blokujemy guzik na czas wykonywania taska
                await TestAsync();
                ListCreateReflectionBtn.IsEnabled = true; // odblokowywujemy guzik

            }
            catch (Exception ex)
            {

            }
        }

        private async Task TestAsync()
        {
            int iterationTime = int.Parse(IterationsBox.Text);
            long elapsedMs = 0;

            //tu trzeba awaitować taska, bo samo tamto oznacza awaitowanie funkcji asynchronicznej czyli symbolizowanie ze gdzies tam trzeba awaitowac taska
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
            //jako ze task został wywołany, to mozna tu po prostu zmienić zawartosc labela 
            // awaitowany Task przenosi wewnętrze zmienne w funkcji do 
        }

        private async Task<long> TestAsync2(long iterationsTime, long elapsedMs)
        {
            return await Task.Run(() =>
            {
                var list = new List<int>();
                var watch = Stopwatch.StartNew();
                for (int i = 0; i < iterationsTime; i++)
                {
                    list.Add(i);
                }
                elapsedMs = watch.ElapsedMilliseconds;
                return elapsedMs;
            });
            //return elapsedMs; //można albo tutaj return i wtedy bez tych dwóch returnów u góry (tego przed await i tego na koncu taska)
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