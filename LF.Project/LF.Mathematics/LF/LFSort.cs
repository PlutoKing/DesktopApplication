/*──────────────────────────────────────────────────────────────
 * FileName     : Sorting.cs
 * Created      : 2021-05-31 11:05:06
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace LF.Mathematics
{
    /// <summary>
    /// 排序算法
    /// </summary>
    public static class LFSort
    {
        #region Sort Methods
        /// <summary>
        /// 排序算法
        /// </summary>
        /// <typeparam name="T">键类型</typeparam>
        /// <param name="keys">键列表</param>
        /// <param name="comparer">比较器</param>
        public static void Sort<T>(IList<T> keys, IComparer<T> comparer = null)
        {
            int count = keys.Count;
            if (count <= 1)
            {
                return;
            }

            if (null == comparer)
            {
                comparer = Comparer<T>.Default;
            }

            if (count == 2)
            {
                if (comparer.Compare(keys[0], keys[1]) > 0)
                {
                    LFList.Swap(keys, 0, 1);
                }
                return;
            }

            // 数目较少的时候，使用插值排序
            if (count <= 10)
            {
                for (int i = 1; i < count; i++)
                {
                    var key = keys[i];
                    int j = i - 1;
                    while (j >= 0 && comparer.Compare(keys[j], key) > 0)
                    {
                        keys[j + 1] = keys[j];
                        j--;
                    }
                    keys[j + 1] = key;
                }
                return;
            }

            // array case
            if (keys is T[] keysArray)
            {
                Array.Sort(keysArray, comparer);
                return;
            }

            // generic list case
            if (keys is List<T> keysList)
            {
                keysList.Sort(comparer);
                return;
            }

            // local sort implementation
            QuickSort(keys, comparer, 0, count - 1);
        }

        /// <summary>
        /// 排序算法
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TItem">对象类型</typeparam>
        /// <param name="keys">键列表</param>
        /// <param name="items">对象列表</param>
        /// <param name="comparer">比较器</param>
        public static void Sort<TKey, TItem>(IList<TKey> keys, IList<TItem> items, IComparer<TKey> comparer = null)
        {
            int count = keys.Count;
            if (count <= 1)
            {
                return;
            }

            if (null == comparer)
            {
                comparer = Comparer<TKey>.Default;
            }

            if (count == 2)
            {
                if (comparer.Compare(keys[0], keys[1]) > 0)
                {
                    LFList.Swap(keys, 0, 1);
                    LFList.Swap(items, 0, 1);
                }
                return;
            }

            // insertion sort
            if (count <= 10)
            {
                for (int i = 1; i < count; i++)
                {
                    var key = keys[i];
                    var item = items[i];
                    int j = i - 1;
                    while (j >= 0 && comparer.Compare(keys[j], key) > 0)
                    {
                        keys[j + 1] = keys[j];
                        items[j + 1] = items[j];
                        j--;
                    }
                    keys[j + 1] = key;
                    items[j + 1] = item;
                }
                return;
            }

            // array case
            if (keys is TKey[] keysArray && items is TItem[] itemsArray)
            {
                Array.Sort(keysArray, itemsArray, comparer);
                return;
            }

            // local sort implementation
            QuickSort(keys, items, comparer, 0, count - 1);
        }

        /// <summary>
        /// 排序算法
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TItem1"></typeparam>
        /// <typeparam name="TItem2"></typeparam>
        /// <param name="keys"></param>
        /// <param name="items1"></param>
        /// <param name="items2"></param>
        /// <param name="comparer"></param>
        public static void Sort<TKey, TItem1, TItem2>(IList<TKey> keys, IList<TItem1> items1, IList<TItem2> items2, IComparer<TKey> comparer = null)
        {
            int count = keys.Count;
            if (count <= 1)
            {
                return;
            }

            if (null == comparer)
            {
                comparer = Comparer<TKey>.Default;
            }

            if (count == 2)
            {
                if (comparer.Compare(keys[0], keys[1]) > 0)
                {
                    LFList.Swap(keys, 0, 1);
                    LFList.Swap(items1, 0, 1);
                    LFList.Swap(items2, 0, 1);
                }
                return;
            }

            // insertion sort
            if (count <= 10)
            {
                for (int i = 1; i < count; i++)
                {
                    var key = keys[i];
                    var item1 = items1[i];
                    var item2 = items2[i];
                    int j = i - 1;
                    while (j >= 0 && comparer.Compare(keys[j], key) > 0)
                    {
                        keys[j + 1] = keys[j];
                        items1[j + 1] = items1[j];
                        items2[j + 1] = items2[j];
                        j--;
                    }
                    keys[j + 1] = key;
                    items1[j + 1] = item1;
                    items2[j + 1] = item2;
                }
                return;
            }

            // local sort implementation
            QuickSort(keys, items1, items2, comparer, 0, count - 1);
        }

        /// <summary>
        /// 排序算法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="comparer"></param>
        public static void Sort<T>(IList<T> keys, int index, int count, IComparer<T> comparer = null)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (count < 0 || index + count > keys.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (count <= 1)
            {
                return;
            }

            if (null == comparer)
            {
                comparer = Comparer<T>.Default;
            }

            if (count == 2)
            {
                if (comparer.Compare(keys[index], keys[index + 1]) > 0)
                {
                    LFList.Swap(keys, index, index + 1);
                }
                return;
            }

            // insertion sort
            if (count <= 10)
            {
                int to = index + count;
                for (int i = index + 1; i < to; i++)
                {
                    var key = keys[i];
                    int j = i - 1;
                    while (j >= index && comparer.Compare(keys[j], key) > 0)
                    {
                        keys[j + 1] = keys[j];
                        j--;
                    }
                    keys[j + 1] = key;
                }
                return;
            }

            // array case
            if (keys is T[] keysArray)
            {
                Array.Sort(keysArray, index, count, comparer);
                return;
            }

            // generic list case
            if (keys is List<T> keysList)
            {
                keysList.Sort(index, count, comparer);
                return;
            }

            // fall back: local sort implementation
            QuickSort(keys, comparer, index, count - 1);
        }

        /// <summary>
        /// 排序算法
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="keys"></param>
        /// <param name="items"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="comparer"></param>
        public static void Sort<TKey, TItem>(IList<TKey> keys, IList<TItem> items, int index, int count, IComparer<TKey> comparer = null)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (count < 0 || index + count > keys.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (count <= 1)
            {
                return;
            }

            if (null == comparer)
            {
                comparer = Comparer<TKey>.Default;
            }

            if (count == 2)
            {
                if (comparer.Compare(keys[index], keys[index + 1]) > 0)
                {
                    LFList.Swap(keys, index, index + 1);
                    LFList.Swap(items, index, index + 1);
                }
                return;
            }

            // insertion sort
            if (count <= 10)
            {
                int to = index + count;
                for (int i = index + 1; i < to; i++)
                {
                    var key = keys[i];
                    var item = items[i];
                    int j = i - 1;
                    while (j >= index && comparer.Compare(keys[j], key) > 0)
                    {
                        keys[j + 1] = keys[j];
                        items[j + 1] = items[j];
                        j--;
                    }
                    keys[j + 1] = key;
                    items[j + 1] = item;
                }
                return;
            }

            // array case
            if (keys is TKey[] keysArray && items is TItem[] itemsArray)
            {
                Array.Sort(keysArray, itemsArray, index, count, comparer);
                return;
            }

            // fall back: local sort implementation
            QuickSort(keys, items, comparer, index, count - 1);
        }

        /// <summary>
        /// 排序算法
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TItem1"></typeparam>
        /// <typeparam name="TItem2"></typeparam>
        /// <param name="keys"></param>
        /// <param name="items1"></param>
        /// <param name="items2"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="comparer"></param>
        public static void Sort<TKey, TItem1, TItem2>(IList<TKey> keys, IList<TItem1> items1, IList<TItem2> items2, int index, int count, IComparer<TKey> comparer = null)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (count < 0 || index + count > keys.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (count <= 1)
            {
                return;
            }

            if (null == comparer)
            {
                comparer = Comparer<TKey>.Default;
            }

            if (count == 2)
            {
                if (comparer.Compare(keys[index], keys[index + 1]) > 0)
                {
                    LFList.Swap(keys, index, index + 1);
                    LFList.Swap(items1, index, index + 1);
                    LFList.Swap(items2, index, index + 1);
                }
                return;
            }

            // insertion sort
            if (count <= 10)
            {
                int to = index + count;
                for (int i = index + 1; i < to; i++)
                {
                    var key = keys[i];
                    var item1 = items1[i];
                    var item2 = items2[i];
                    int j = i - 1;
                    while (j >= index && comparer.Compare(keys[j], key) > 0)
                    {
                        keys[j + 1] = keys[j];
                        items1[j + 1] = items1[j];
                        items2[j + 1] = items2[j];
                        j--;
                    }
                    keys[j + 1] = key;
                    items1[j + 1] = item1;
                    items2[j + 1] = item2;
                }
                return;
            }

            // fall back: local sort implementation
            QuickSort(keys, items1, items2, comparer, index, count - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="primary"></param>
        /// <param name="secondary"></param>
        /// <param name="primaryComparer"></param>
        /// <param name="secondaryComparer"></param>
        public static void SortAll<T1, T2>(IList<T1> primary, IList<T2> secondary, IComparer<T1> primaryComparer = null, IComparer<T2> secondaryComparer = null)
        {
            if (null == primaryComparer)
            {
                primaryComparer = Comparer<T1>.Default;
            }

            if (null == secondaryComparer)
            {
                secondaryComparer = Comparer<T2>.Default;
            }

            // local sort implementation
            QuickSortAll(primary, secondary, primaryComparer, secondaryComparer, 0, primary.Count - 1);
        }

        #endregion

        #region Bubble Sort Methods

        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <param name="comparer"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void BubbleSort<T>(IList<T> keys,IComparer<T> comparer, int left,int right)
        {

        }

        #endregion

        #region Quick Sort Methods
        /// <summary>
        /// Recursive implementation for an in place quick sort on a list.
        /// </summary>
        /// <remarks>列表上就地快速排序的递归实现。</remarks>
        /// <typeparam name="T">The type of the list on which the quick sort is performed.</typeparam>
        /// <param name="keys">The list which is sorted using quick sort.</param>
        /// <param name="comparer">The method with which to compare two elements of the quick sort.</param>
        /// <param name="left">The left boundary of the quick sort.</param>
        /// <param name="right">The right boundary of the quick sort.</param>
        public static void QuickSort<T>(IList<T> keys, IComparer<T> comparer, int left, int right)
        {
            do
            {
                // Pivoting
                int a = left;
                int b = right;
                int p = a + ((b - a) >> 1); // midpoint

                if (comparer.Compare(keys[a], keys[p]) > 0)
                {
                    LFList.Swap(keys, a, p);
                }

                if (comparer.Compare(keys[a], keys[b]) > 0)
                {
                    LFList.Swap(keys, a, b);
                }

                if (comparer.Compare(keys[p], keys[b]) > 0)
                {
                    LFList.Swap(keys, p, b);
                }

                T pivot = keys[p];

                // Hoare Partitioning
                do
                {
                    while (comparer.Compare(keys[a], pivot) < 0)
                    {
                        a++;
                    }

                    while (comparer.Compare(pivot, keys[b]) < 0)
                    {
                        b--;
                    }

                    if (a > b)
                    {
                        break;
                    }

                    if (a < b)
                    {
                        LFList.Swap(keys, a, b);
                    }

                    a++;
                    b--;
                } while (a <= b);

                // In order to limit the recursion depth to log(n), we sort the
                // shorter partition recursively and the longer partition iteratively.
                if ((b - left) <= (right - a))
                {
                    if (left < b)
                    {
                        QuickSort(keys, comparer, left, b);
                    }

                    left = a;
                }
                else
                {
                    if (a < right)
                    {
                        QuickSort(keys, comparer, a, right);
                    }

                    right = b;
                }
            } while (left < right);
        }

        /// <summary>
        /// Recursive implementation for an in place quick sort on a list while reordering one other list accordingly.
        /// </summary>
        /// <typeparam name="T">The type of the list on which the quick sort is performed.</typeparam>
        /// <typeparam name="TItems">The type of the list which is automatically reordered accordingly.</typeparam>
        /// <param name="keys">The list which is sorted using quick sort.</param>
        /// <param name="items">The list which is automatically reordered accordingly.</param>
        /// <param name="comparer">The method with which to compare two elements of the quick sort.</param>
        /// <param name="left">The left boundary of the quick sort.</param>
        /// <param name="right">The right boundary of the quick sort.</param>
        public static void QuickSort<T, TItems>(IList<T> keys, IList<TItems> items, IComparer<T> comparer, int left, int right)
        {
            do
            {
                // Pivoting
                int a = left;
                int b = right;
                int p = a + ((b - a) >> 1); // midpoint

                if (comparer.Compare(keys[a], keys[p]) > 0)
                {
                    LFList.Swap(keys, a, p);
                    LFList.Swap(items, a, p);
                }

                if (comparer.Compare(keys[a], keys[b]) > 0)
                {
                    LFList.Swap(keys, a, b);
                    LFList.Swap(items, a, b);
                }

                if (comparer.Compare(keys[p], keys[b]) > 0)
                {
                    LFList.Swap(keys, p, b);
                    LFList.Swap(items, p, b);
                }

                T pivot = keys[p];

                // Hoare Partitioning
                do
                {
                    while (comparer.Compare(keys[a], pivot) < 0)
                    {
                        a++;
                    }

                    while (comparer.Compare(pivot, keys[b]) < 0)
                    {
                        b--;
                    }

                    if (a > b)
                    {
                        break;
                    }

                    if (a < b)
                    {
                        LFList.Swap(keys, a, b);
                        LFList.Swap(items, a, b);
                    }

                    a++;
                    b--;
                } while (a <= b);

                // In order to limit the recursion depth to log(n), we sort the
                // shorter partition recursively and the longer partition iteratively.
                if ((b - left) <= (right - a))
                {
                    if (left < b)
                    {
                        QuickSort(keys, items, comparer, left, b);
                    }

                    left = a;
                }
                else
                {
                    if (a < right)
                    {
                        QuickSort(keys, items, comparer, a, right);
                    }

                    right = b;
                }
            } while (left < right);
        }

        /// <summary>
        /// Recursive implementation for an in place quick sort on one list while reordering two other lists accordingly.
        /// </summary>
        /// <typeparam name="T">The type of the list on which the quick sort is performed.</typeparam>
        /// <typeparam name="TItems1">The type of the first list which is automatically reordered accordingly.</typeparam>
        /// <typeparam name="TItems2">The type of the second list which is automatically reordered accordingly.</typeparam>
        /// <param name="keys">The list which is sorted using quick sort.</param>
        /// <param name="items1">The first list which is automatically reordered accordingly.</param>
        /// <param name="items2">The second list which is automatically reordered accordingly.</param>
        /// <param name="comparer">The method with which to compare two elements of the quick sort.</param>
        /// <param name="left">The left boundary of the quick sort.</param>
        /// <param name="right">The right boundary of the quick sort.</param>
        public static void QuickSort<T, TItems1, TItems2>(
            IList<T> keys, IList<TItems1> items1, IList<TItems2> items2,
            IComparer<T> comparer,
            int left, int right)
        {
            do
            {
                // Pivoting
                int a = left;
                int b = right;
                int p = a + ((b - a) >> 1); // midpoint

                if (comparer.Compare(keys[a], keys[p]) > 0)
                {
                    LFList.Swap(keys, a, p);
                    LFList.Swap(items1, a, p);
                    LFList.Swap(items2, a, p);
                }

                if (comparer.Compare(keys[a], keys[b]) > 0)
                {
                    LFList.Swap(keys, a, b);
                    LFList.Swap(items1, a, b);
                    LFList.Swap(items2, a, b);
                }

                if (comparer.Compare(keys[p], keys[b]) > 0)
                {
                    LFList.Swap(keys, p, b);
                    LFList.Swap(items1, p, b);
                    LFList.Swap(items2, p, b);
                }

                T pivot = keys[p];

                // Hoare Partitioning
                do
                {
                    while (comparer.Compare(keys[a], pivot) < 0)
                    {
                        a++;
                    }

                    while (comparer.Compare(pivot, keys[b]) < 0)
                    {
                        b--;
                    }

                    if (a > b)
                    {
                        break;
                    }

                    if (a < b)
                    {
                        LFList.Swap(keys, a, b);
                        LFList.Swap(items1, a, b);
                        LFList.Swap(items2, a, b);
                    }

                    a++;
                    b--;
                } while (a <= b);

                // In order to limit the recursion depth to log(n), we sort the
                // shorter partition recursively and the longer partition iteratively.
                if ((b - left) <= (right - a))
                {
                    if (left < b)
                    {
                        QuickSort(keys, items1, items2, comparer, left, b);
                    }

                    left = a;
                }
                else
                {
                    if (a < right)
                    {
                        QuickSort(keys, items1, items2, comparer, a, right);
                    }

                    right = b;
                }
            } while (left < right);
        }

        /// <summary>
        /// Recursive implementation for an in place quick sort on the primary and then by the secondary list while reordering one secondary list accordingly.
        /// </summary>
        /// <typeparam name="T1">The type of the primary list.</typeparam>
        /// <typeparam name="T2">The type of the secondary list.</typeparam>
        /// <param name="primary">The list which is sorted using quick sort.</param>
        /// <param name="secondary">The list which is sorted secondarily (on primary duplicates) and automatically reordered accordingly.</param>
        /// <param name="primaryComparer">The method with which to compare two elements of the primary list.</param>
        /// <param name="secondaryComparer">The method with which to compare two elements of the secondary list.</param>
        /// <param name="left">The left boundary of the quick sort.</param>
        /// <param name="right">The right boundary of the quick sort.</param>
        public static void QuickSortAll<T1, T2>(
            IList<T1> primary, IList<T2> secondary,
            IComparer<T1> primaryComparer, IComparer<T2> secondaryComparer,
            int left, int right)
        {
            do
            {
                // Pivoting
                int a = left;
                int b = right;
                int p = a + ((b - a) >> 1); // midpoint

                int ap = primaryComparer.Compare(primary[a], primary[p]);
                if (ap > 0 || ap == 0 && secondaryComparer.Compare(secondary[a], secondary[p]) > 0)
                {
                    LFList.Swap(primary, a, p);
                    LFList.Swap(secondary, a, p);
                }

                int ab = primaryComparer.Compare(primary[a], primary[b]);
                if (ab > 0 || ab == 0 && secondaryComparer.Compare(secondary[a], secondary[b]) > 0)
                {
                    LFList.Swap(primary, a, b);
                    LFList.Swap(secondary, a, b);
                }

                int pb = primaryComparer.Compare(primary[p], primary[b]);
                if (pb > 0 || pb == 0 && secondaryComparer.Compare(secondary[p], secondary[b]) > 0)
                {
                    LFList.Swap(primary, p, b);
                    LFList.Swap(secondary, p, b);
                }

                T1 pivot1 = primary[p];
                T2 pivot2 = secondary[p];

                // Hoare Partitioning
                do
                {
                    int ax;
                    while ((ax = primaryComparer.Compare(primary[a], pivot1)) < 0 || ax == 0 && secondaryComparer.Compare(secondary[a], pivot2) < 0)
                    {
                        a++;
                    }

                    int xb;
                    while ((xb = primaryComparer.Compare(pivot1, primary[b])) < 0 || xb == 0 && secondaryComparer.Compare(pivot2, secondary[b]) < 0)
                    {
                        b--;
                    }

                    if (a > b)
                    {
                        break;
                    }

                    if (a < b)
                    {
                        LFList.Swap(primary, a, b);
                        LFList.Swap(secondary, a, b);
                    }

                    a++;
                    b--;
                } while (a <= b);

                // In order to limit the recursion depth to log(n), we sort the
                // shorter partition recursively and the longer partition iteratively.
                if ((b - left) <= (right - a))
                {
                    if (left < b)
                    {
                        QuickSortAll(primary, secondary, primaryComparer, secondaryComparer, left, b);
                    }

                    left = a;
                }
                else
                {
                    if (a < right)
                    {
                        QuickSortAll(primary, secondary, primaryComparer, secondaryComparer, a, right);
                    }

                    right = b;
                }
            } while (left < right);
        } 
        #endregion
    }
}