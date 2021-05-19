/*──────────────────────────────────────────────────────────────
 * FileName     : LF.cs
 * Created      : 2021-05-15 14:10:33
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF
{
    /// <summary>
    /// 通用方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LF<T> where T : IComparable
    {
        #region Array 数组
        /// <summary>
        /// 求最大值
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="max">最大值</param>
        /// <param name="index">最大值索引</param>
        public static void GetMax(T[] array, out T max, out int index)
        {
            max = array[0];
            index = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(max) > 0)
                {
                    max = array[i];
                    index = i;
                }
            }
        }

        /// <summary>
        /// 求最小值
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="min">最小值</param>
        /// <param name="index">最小值索引</param>
        public static void GetMin(T[] array, out T min, out int index)
        {
            min = array[0];
            index = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(min) < 0)
                {
                    min = array[i];
                    index = i;
                }
            }
        }


        /// <summary>
        /// 数组叫你换
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        public static T[] ArraySwap(T[] array, int index1, int index2)
        {
            T tmp = array[index1];
            array[index1] = array[index2];
            array[index2] = tmp;
            return array;
        }


        /// <summary>
        /// 数组翻转
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        public static T[] ArrayFlip(T[] array, int index1, int index2)
        {
            T[] r = (T[])array.Clone();

            int tmp = 0;
            if (index1 > index2)
            {
                tmp = index1;
                index1 = index2;
                index2 = tmp;
            }
            else if (index1 == index2)
            {
                return r;
            }

            for (int i = index1; i <= index2; i++)
            {
                r[i] = array[index2 + index1 - i];
            }

            return r;
        }

        /// <summary>
        /// 数组滑移
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        public static T[] ArraySlide(T[] array, int index1, int index2)
        {
            T[] r = (T[])array.Clone();
            int tmp = 0;
            if (index1 > index2)
            {
                tmp = index1;
                index1 = index2;
                index2 = tmp;
            }
            else if (index1 == index2)
            {
                return r;
            }

            for (int i = index1; i < index2; i++)
            {
                r[i] = array[i + 1];
            }
            r[index2] = array[index1];

            return r;
        }

        /// <summary>
        /// 随机数组
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int[] RandArray(int n)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < n; i++)
            {
                list.Add(i);
            }

            int[] arr = new int[n];

            for (int i = 0; i < n; i++)
            {
                int temp = new Random().Next(0, list.Count - 1);
                arr[i] = list[temp];
                list.RemoveAt(temp);
            }

            return arr;
        }

        public static T[] SubArray(T[] array, int start, int end)
        {
            int tmp = 0;
            if (start > end)
            {
                tmp = start;
                start = end;
                end = tmp;
            }
            else if (start == end)
            {
                return new T[] { array[start] };
            }
            T[] subArr = new T[end - start + 1];
            for (int i = 0; i <= end - start; i++)
            {
                subArr[i] = array[start + i];
            }
            return subArr;
        }
        #endregion
    }
}