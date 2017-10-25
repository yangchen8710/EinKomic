using System;
using System.Windows;
using System.Windows.Controls;
using EinKomic.ViewModel;
using Emgu.CV;
using EinKomic.Model;
using System.Threading;

namespace EinKomic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel mvm;
        Mat mat;
        Image<Emgu.CV.Structure.Gray, Byte> img;
        int leftTrim, rightTrim, topTrim, bottomTrim;
        public MainWindow()
        {
            InitializeComponent();
            mvm = new MainViewModel();
            DataContext = mvm;
            mat = CvInvoke.Imread(@"D:\pic\test1.png", Emgu.CV.CvEnum.ImreadModes.Grayscale);
            img = mat.ToImage<Emgu.CV.Structure.Gray, Byte>();
            //CvInvoke.Imshow("ak", mat);
            //CvInvoke.CvtColor(mat, mat, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            for (int i = 0; i < 1; i++)
            {
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
                //任务 1...
                if(true)
                { 
                Thread thread1 = new Thread(new ThreadStart(dobottom));
                Thread thread2 = new Thread(new ThreadStart(dotop));
                Thread thread3 = new Thread(new ThreadStart(doright));
                Thread thread4 = new Thread(new ThreadStart(doleft));
                thread1.Start();
                thread2.Start();
                thread3.Start();
                thread4.Start();
                thread1.Join();
                thread2.Join();
                thread3.Join();
                thread4.Join();
                }

                if (false)
                {
                    CvHelper.RightTrim(mat,img);
                    CvHelper.LeftTrim(mat, img);
                    CvHelper.TopTrim(mat, img);
                    CvHelper.BottomTrim(mat, img);
                }
                
                //Console.WriteLine("Console.WriteLine(CvHelper.RightTrim(mat));:"+CvHelper.RightTrim(mat));
                //Console.WriteLine("Console.WriteLine(CvHelper.LeftTrim(mat));:" + CvHelper.LeftTrim(mat));
                //Console.WriteLine("Console.WriteLine(CvHelper.TopTrim(mat));:" + CvHelper.TopTrim(mat));
                //Console.WriteLine("Console.WriteLine(CvHelper.BottomTrim(mat));:" + CvHelper.BottomTrim(mat));

                stopwatch.Stop();
                var _result = "stopwatch.ElapsedTicks ：" + stopwatch.ElapsedTicks + "。";
                Console.WriteLine(_result);
            }
            //CvInvoke.Imshow("akx", img);
            Console.WriteLine("bottomTrim:" + bottomTrim + "topTrim:" + topTrim + "rightTrim:" + rightTrim + "leftTrim:" + leftTrim);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(new System.Drawing.Point(leftTrim,topTrim),new System.Drawing.Size(img.Cols- rightTrim- leftTrim,img.Rows - topTrim - bottomTrim));
            var tem = img.GetSubRect(rect);
            CvInvoke.Imshow("akx", tem);

        }

        void dobottom()
        {
            bottomTrim = CvHelper.BottomTrim(mat, img);
        }
        void dotop()
        {
            
            topTrim = CvHelper.TopTrim(mat, img);
        }
        void doright()
        {
            rightTrim = CvHelper.RightTrim(mat, img);
        }
        void doleft()
        {
            leftTrim = CvHelper.LeftTrim(mat, img);
        }

        private void button_input_path_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "Please Select Folder";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    return;
                }
                mvm.InputPath = dialog.SelectedPath;
            }
        }

        private void button_output_path_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "Please Select Folder";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    return;
                }
                mvm.OutputPath = dialog.SelectedPath;
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
