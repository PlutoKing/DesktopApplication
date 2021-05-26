/*──────────────────────────────────────────────────────────────
 * FileName     : LFImage.cs
 * Created      : 2021-05-19 21:52:07
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LF.ImageProcessing
{
    public class LFImage
    {
        #region Fields
        private Bitmap _bitmap;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFImage()
        {
        }
        #endregion

        #region Methods
        public  Bitmap ToGray()
        {
            Bitmap bmp = (Bitmap)_bitmap.Clone();
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //获取该点的像素的RGB的颜色
                    Color color = bmp.GetPixel(i, j);
                    //利用公式计算灰度值
                    int gray = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                    Color newColor = Color.FromArgb(gray, gray, gray);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            return bmp;
        }

        /// <summary>
        /// 图像灰度反转
        /// </summary>
        /// <returns></returns>
        public Bitmap GrayReverse()
        {
            Bitmap bmp = (Bitmap)_bitmap.Clone();
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //获取该点的像素的RGB的颜色
                    Color color = bmp.GetPixel(i, j);
                    Color newColor = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            return bmp;
        }

        /// <summary>
        /// 图像二值化1：取图片的平均灰度作为阈值，低于该值的全都为0，高于该值的全都为255
        /// </summary>
        /// <returns></returns>
        public Bitmap Binarization()
        {
            Bitmap bmp = (Bitmap)_bitmap.Clone();

            int average = 0;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    average += (color.R + color.G + color.B) / 3;
                }
            }
            average = (int)average / (bmp.Width * bmp.Height);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //获取该点的像素的RGB的颜色
                    Color color = bmp.GetPixel(i, j);
                    int value = 255 - (color.R + color.G + color.B) / 3;
                    Color newColor = value > average ? Color.FromArgb(0, 0, 0) : Color.FromArgb(255, 255, 255);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            return bmp;
        }

        public Bitmap Binarization(int average)
        {
            Bitmap bmp = (Bitmap)_bitmap.Clone();

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //获取该点的像素的RGB的颜色
                    Color color = bmp.GetPixel(i, j);
                    int value = 255 - color.B;
                    Color newColor = value > average ? Color.FromArgb(0, 0, 0) : Color.FromArgb(255, 255, 255);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            return bmp;
        }

        /// <summary>
        /// 图像染色
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="color">颜色</param>
        /// <returns></returns>
        public Bitmap Coloring( Color color)
        {
            Bitmap bmp = ToGray();
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //获取该点的像素的RGB的颜色
                    Color c = bmp.GetPixel(i, j);
                    int value = 255 - c.B;
                    Color newColor = value > 64 ? color : Color.FromArgb(255, 255, 255);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            return bmp;
        }

        public double[] GetGrayHistogram()
        {
            Bitmap bmp = (Bitmap)_bitmap.Clone();

            double[] countPixel = new double[256];
            //将图像数据复制到byte中
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpdata = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpdata.Scan0;

            int bytes = bmp.Width * bmp.Height * 3;
            byte[] grayValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

            //统计直方图信息
            byte temp = 0;
            int maxPixel = 0;
            Array.Clear(countPixel, 0, 256);
            for (int i = 0; i < bytes; i++)
            {
                temp = grayValues[i];
                countPixel[temp]++;
                if (countPixel[temp] > maxPixel)
                {
                    maxPixel = (int)countPixel[temp];
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpdata);
            return countPixel;
        }

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}