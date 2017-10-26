using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using System.Threading;

namespace EinKomic.Model
{
    class CvHelper
    {

        static int DIFF = 18;

        static double MaxCutRate = 0.2;

        static double SideIgnore = 0.2;
        static double MidIgnore = 0.1;

        static int DiffCountLimit = 0;

        static double W_H_Rate = 1.0;

        public Boolean AutoSinglePage(Mat mat, Const.FlipDirection direction, out Mat resMat1, out Mat resMat2)
        {
            if (1.0 * mat.Cols / mat.Rows  > W_H_Rate)
            {
                resMat1 = new Mat();
                resMat2 = new Mat();
                return true;
            }
            resMat1 = null;
            resMat2 = null;
            return false;

        }

        public Mat Trim(Mat mat)
        {
            Image<Emgu.CV.Structure.Gray, Byte> img = mat.ToImage<Emgu.CV.Structure.Gray, Byte>();
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            int rightTrim = CvHelper.RightTrim(ref mat, ref img);
            int leftTrim = CvHelper.LeftTrim(ref mat, ref img);
            int topTrim = CvHelper.TopTrim(ref mat, ref img);
            int bottomTrim = CvHelper.BottomTrim(ref mat, ref img);

            stopwatch.Stop(); 
            TimeSpan timeSpan = stopwatch.Elapsed;
            Console.WriteLine("Caculate Trim Rect:" + timeSpan.TotalMilliseconds + "ms");

            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(new System.Drawing.Point(leftTrim, topTrim), new System.Drawing.Size(img.Cols - rightTrim - leftTrim, img.Rows - topTrim - bottomTrim));
            Image<Emgu.CV.Structure.Gray, Byte> resImage = img.GetSubRect(rect);
            return resImage.Mat;
        }

        public static int LeftTrim(ref Mat mat, ref Image<Emgu.CV.Structure.Gray, Byte> img)
        {
            int MaxCut = (int)(mat.Cols * MaxCutRate);
         
            Mat mat_temp = mat.Col(0);
            
            Image<Emgu.CV.Structure.Gray, Byte> img_temp = mat_temp.ToImage<Emgu.CV.Structure.Gray, Byte>();
            double avg = img_temp.GetAverage().Intensity;
            
            int index = -1;
            for (int i = 0; i < MaxCut; i++)
            {
                int count = 0;
                count += countDiffRows(ref mat, ref img, ref i, ref avg,
                    (int)(0),
                    (int)(mat.Rows-1));
                //count += countDiffRows(ref mat, ref img, ref i, ref avg,
                //    (int)(mat.Rows * SideIgnore),
                //    (int)(mat.Rows * (0.5 - MidIgnore / 2)));
                //count += countDiffRows(ref mat, ref img, ref i, ref avg,
                //    (int)(mat.Rows * (0.5 + MidIgnore / 2)),
                //    (int)(mat.Rows * (1.0 - SideIgnore)));
                if (count > DiffCountLimit)
                {
                    index = i-1;
                    break;
                }
            }

            if (index == -1)
                return MaxCut;
            return index+1;
        }


        public static int TopTrim(ref Mat mat, ref Image<Emgu.CV.Structure.Gray, Byte> img)
        {
            int MaxCut = (int)(mat.Rows * MaxCutRate);

            Mat mat_temp = mat.Row(0);
            Image<Emgu.CV.Structure.Gray, Byte> img_temp = mat_temp.ToImage<Emgu.CV.Structure.Gray, Byte>();
            double avg = img_temp.GetAverage().Intensity;

            int index = -1;
            for (int i = 0; i < MaxCut; i++)
            {
                int count = 0;
                count += countDiffCols(ref mat, ref img, ref i, ref avg,
                    (int)(mat.Cols * SideIgnore),
                    (int)(mat.Cols * (0.5 - MidIgnore / 2)) );
                count += countDiffCols(ref mat, ref img, ref i, ref avg,
                    (int)(mat.Cols * (0.5 + MidIgnore / 2)),
                    (int)(mat.Cols * (1.0 - SideIgnore)) );
                if (count > DiffCountLimit)
                {
                    index = i-1;
                    break;
                }
            }

            if (index == -1)
                return MaxCut;
            return index+1;
        }

        public static int RightTrim(ref Mat mat, ref Image<Emgu.CV.Structure.Gray, Byte> img)
        {
            int MaxCut = (int)(mat.Cols * MaxCutRate);

            Mat mat_temp = mat.Col(mat.Cols - 1);
            Image<Emgu.CV.Structure.Gray, Byte> img_temp = mat_temp.ToImage<Emgu.CV.Structure.Gray, Byte>();
            double avg = img_temp.GetAverage().Intensity;

            int index = mat.Cols;
            for (int i = mat.Cols - 1; i > mat.Cols - MaxCut; i--)
            {
                int count = 0;
                count += countDiffRows(ref mat, ref img, ref i, ref avg,
                    (int)(0),
                    (int)(mat.Rows - 1));
                if (count > DiffCountLimit)
                {
                    index = i + 1;
                    break;
                }
            }

            if (index == mat.Cols)
                return MaxCut;
            return mat.Cols - index;
        }

        public static int BottomTrim(ref Mat mat, ref Image<Emgu.CV.Structure.Gray, Byte> img)
        {
            int MaxCut = (int)(mat.Cols * MaxCutRate);

            Mat mat_temp = mat.Row(mat.Rows - 1);
            Image<Emgu.CV.Structure.Gray, Byte> img_temp = mat_temp.ToImage<Emgu.CV.Structure.Gray, Byte>();
            double avg = img_temp.GetAverage().Intensity;
          
            int index = mat.Rows;
            for (int i = mat.Rows - 1; i > mat.Rows - MaxCut; i--)
            {
                int count = 0;
                count += countDiffCols(ref mat,ref img,ref i,ref avg, 
                    (int)(mat.Cols * SideIgnore), 
                    (int)(mat.Cols * (0.5 - MidIgnore / 2)));
                count += countDiffCols(ref mat, ref img, ref i, ref avg,
                    (int)(mat.Cols * (0.5 + MidIgnore / 2)),
                    (int)(mat.Cols * (1.0 - SideIgnore)));
                if (count > DiffCountLimit)
                {
                    index = i + 1;
                    break;
                }
            }

            if (index == mat.Rows)
                return MaxCut;
            return mat.Rows - index;
        }

        private static int countDiffCols(
            ref Mat mat,
            ref Image<Emgu.CV.Structure.Gray, Byte> img,
            ref int i,
            ref double avg,
            int start,
            int end
            )
        {
            int count = 0;
            for (int j = start; j < end; j++)
            {
                var a = img[i, j].Intensity;

                if (a < avg + DIFF && a > avg - DIFF)
                {

                }
                else
                {
                    count++;
                }
            }
            return count;
        }

        private static int countDiffRows(
            ref Mat mat,
            ref Image<Emgu.CV.Structure.Gray, Byte> img,
            ref int i,
            ref double avg,
            int start,
            int end
            )
        {
            int count = 0;
            for (int j = start; j < end; j++)
            {
                var a = img[j, i].Intensity;
                if (a < avg + DIFF && a > avg - DIFF)
                {

                }
                else
                {
                    count++;
                }
            }
            return count;
        }
    }
}
