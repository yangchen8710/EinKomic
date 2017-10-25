using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;

namespace EinKomic.Model
{
    class CvHelper
    {
        static int DIFF = 18;
        public static int LeftTrim(Mat mat, Image<Emgu.CV.Structure.Gray, Byte> img)
        {
            int MaxCut = (int)(mat.Cols * 0.2);
            double LeftRightIgnore = 0.2;
            double MidIgnore = 0.1;
            
            Mat mat_temp = mat.Col(0);
            
            Image<Emgu.CV.Structure.Gray, Byte> img_temp = mat_temp.ToImage<Emgu.CV.Structure.Gray, Byte>();
            double avg = img_temp.GetAverage().Intensity;
            //img = mat.ToImage<Emgu.CV.Structure.Gray, Byte>();
            int index = -1;
            for (int i = 0; i < MaxCut; i++)
            {
                int count = 0;
                for (int j = (int)(mat.Rows * LeftRightIgnore) ; j < (int)(mat.Rows * (0.5- MidIgnore/2)); j++)
                {
                    var a = img[j, i].Intensity;

                    if (a < avg + DIFF && a > avg - DIFF)
                    {

                    }
                    else
                    {
                        //Console.WriteLine(a + ":" + avg);
                        count++;
                    }
                }

                for (int j = (int)(mat.Rows * (0.5 + MidIgnore / 2)); j < (int)(mat.Rows * (1.0 - LeftRightIgnore)); j++)
                {
                    var a = img[j, i].Intensity;

                    if (a < avg + DIFF && a > avg - DIFF)
                    {

                    }
                    else
                    {
                        //Console.WriteLine(a + ":" + avg);
                        count++;
                    }
                }
                //Console.WriteLine("Cols:" + i + ",Count:" + +count);
                //Console.WriteLine("Cols:" + i + ",Count:" + +count);
                if (count > 3)
                {
                    index = i-1;
                    break;
                }
            }
            return index+1;
        }

        public static int TopTrim(Mat mat, Image<Emgu.CV.Structure.Gray, Byte> img)
        {
            int MaxCut = (int)(mat.Rows * 0.2);
            double LeftRightIgnore = 0.2;
            double MidIgnore = 0.1;

            Mat mat_temp = mat.Row(0);
            CvInvoke.Imshow("ak", mat_temp);
            Image<Emgu.CV.Structure.Gray, Byte> img_temp = mat_temp.ToImage<Emgu.CV.Structure.Gray, Byte>();
            double avg = img_temp.GetAverage().Intensity;
            //img = mat.ToImage<Emgu.CV.Structure.Gray, Byte>();
            int index = -1;
            for (int i = 0; i < MaxCut; i++)
            {
                int count = 0;
                for (int j = (int)(mat.Cols * LeftRightIgnore); j < (int)(mat.Cols * (0.5 - MidIgnore / 2)); j++)
                {
                    var a = img[i ,j].Intensity;

                    if (a < avg + DIFF && a > avg - DIFF)
                    {

                    }
                    else
                    {
                        //Console.WriteLine(a + ":" + avg);
                        count++;
                    }
                }

                for (int j = (int)(mat.Cols * (0.5 + MidIgnore / 2)); j < (int)(mat.Cols * (1.0 - LeftRightIgnore)); j++)
                {
                    var a = img[i, j].Intensity;

                    if (a < avg + DIFF && a > avg - DIFF)
                    {

                    }
                    else
                    {
                        //Console.WriteLine(a + ":" + avg);
                        count++;
                    }
                }
                //Console.WriteLine("Cols:" + i + ",Count:" + +count);
                //Console.WriteLine("Cols:" + i + ",Count:" + +count);
                if (count > 3)
                {
                    index = i-1;
                    break;
                }
            }
            return index+1;
        }

        public static int RightTrim(Mat mat, Image<Emgu.CV.Structure.Gray, Byte> img)
        {
            int MaxCut = (int)(mat.Cols * 0.2);
            double LeftRightIgnore = 0.2;
            double MidIgnore = 0.1;

            Mat mat_temp = mat.Col(mat.Cols - 1);
            Image<Emgu.CV.Structure.Gray, Byte> img_temp = mat_temp.ToImage<Emgu.CV.Structure.Gray, Byte>();
            double avg = img_temp.GetAverage().Intensity;
            //img = mat.ToImage<Emgu.CV.Structure.Gray, Byte>();
            int index = mat.Cols;
            for (int i = mat.Cols - 1; i > mat.Cols - MaxCut; i--)
            {
                int count = 0;
                for (int j = (int)(mat.Rows * LeftRightIgnore); j < (int)(mat.Rows * (0.5 - MidIgnore / 2)); j++)
                {
                    var a = img[j, i].Intensity;

                    if (a < avg + DIFF && a > avg - DIFF)
                    {

                    }
                    else
                    {
                        //Console.WriteLine(a + ":" + avg);
                        count++;
                    }
                }

                for (int j = (int)(mat.Rows * (0.5 + MidIgnore / 2)); j < (int)(mat.Rows * (1.0 - LeftRightIgnore)); j++)
                {
                    var a = img[j, i].Intensity;

                    if (a < avg + DIFF && a > avg - DIFF)
                    {

                    }
                    else
                    {
                        //Console.WriteLine(a + ":" + avg);
                        count++;
                    }
                }
                //Console.WriteLine("Cols:" + i + ",Count:" + +count);
                //Console.WriteLine("Cols:" + i + ",Count:" + +count);
                if (count > 3)
                {
                    index = i + 1;
                    break;
                }
            }
            //Console.WriteLine("index:" + index );
            return mat.Cols - index;
        }

        public static int BottomTrim(Mat mat, Image<Emgu.CV.Structure.Gray, Byte> img)
        {
            int MaxCut = (int)(mat.Cols * 0.2);
            double LeftRightIgnore = 0.2;
            double MidIgnore = 0.1;

            Mat mat_temp = mat.Row(mat.Rows - 1);
            Image<Emgu.CV.Structure.Gray, Byte> img_temp = mat_temp.ToImage<Emgu.CV.Structure.Gray, Byte>();
            double avg = img_temp.GetAverage().Intensity;
            //img = mat.ToImage<Emgu.CV.Structure.Gray, Byte>();
            int index = mat.Rows;
            for (int i = mat.Rows - 1; i > mat.Rows - MaxCut; i--)
            {
                int count = 0;
                for (int j = (int)(mat.Cols * LeftRightIgnore); j < (int)(mat.Cols * (0.5 - MidIgnore / 2)); j++)
                {
                    var a = img[i, j].Intensity;

                    if (a < avg + DIFF && a > avg - DIFF)
                    {

                    }
                    else
                    {
                        //Console.WriteLine(a + ":" + avg);
                        count++;
                    }
                }

                for (int j = (int)(mat.Cols * (0.5 + MidIgnore / 2)); j < (int)(mat.Cols * (1.0 - LeftRightIgnore)); j++)
                {
                    var a = img[i, j].Intensity;

                    if (a < avg + DIFF && a > avg - DIFF)
                    {

                    }
                    else
                    {
                        //Console.WriteLine(a + ":" + avg);
                        count++;
                    }
                }
                //Console.WriteLine("Cols:" + i + ",Count:" + +count);
                //Console.WriteLine("Cols:" + i + ",Count:" + +count);
                if (count > 3)
                {
                    index = i + 1;
                    break;
                }
            }
            //Console.WriteLine("index:" + index );
            return mat.Rows - index;
        }

    }
}
