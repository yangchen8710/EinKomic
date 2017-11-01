using System;
using System.Windows;
using System.Windows.Controls;
using EinKomic.ViewModel;
using Emgu.CV;
using EinKomic.Model;


namespace EinKomic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel mvm;
        Mat mat;
        public MainWindow()
        {
            InitializeComponent();
            mvm = new MainViewModel();
            DataContext = mvm;
            mvm.MainFormMessage = "Welcome.";
            mat = CvInvoke.Imread(@"D:\pic\test1.png", Emgu.CV.CvEnum.ImreadModes.Grayscale);
            

            CvHelper cvHelper = new CvHelper();
            Mat resMat1 = new Mat();
            Mat resMat2 = new Mat();
            cvHelper.AutoSinglePage(mat, 0,out resMat1,out resMat2);
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            Mat res = cvHelper.Trim(mat);
            stopwatch.Stop();
            TimeSpan timeSpan = stopwatch.Elapsed;
            Console.WriteLine("Caculate Trim Rect:" + timeSpan.TotalMilliseconds + "ms");

            //CvInvoke.Imshow("akx", res);
            

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
