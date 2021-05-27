/*──────────────────────────────────────────────────────────────
 * FileName     : Sorting.cs
 * Created      : 2021-05-27 11:01:29
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Mathematics
{
    /// <summary>
    /// 排序算法
    /// </summary>
    public static class Sorting
    {
        #region Sort Algorithm
        /// <summary>
        /// 排序算法。
        /// </summary>
        /// <typeparam name="T">键集合中的元素类型。</typeparam>
        /// <param name="keys">键集合。</param>
        /// <param name="comparer">比较方法，定义排序顺序。</param>
        public static void Sort<T>(IList<T> keys, IComparer<T> comparer = null)
        {
            int count = keys.Count;
            if (count <= 1)
            {
                return;
            }

            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }

            if (count == 2)
            {
                if (comparer.Compare(keys[0], keys[1]) > 0)
                {
                    Swap(keys, 0, 1);
                }
                return;
            }

            // 元素较少时，使用插入排序
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

            // 如果集合是数组，直接调用数组排序。
            if (keys is T[] keysArray)
            {
                Array.Sort(keysArray, comparer);
                return;
            }

            // 如果集合是列表，直接调用列表排序
            if (keys is List<T> keysList)
            {
                keysList.Sort(comparer);
                return;
            }

            // 调用快排算法。
            QuickSort(keys, comparer, 0, count - 1);
        }

        /// <summary>
        /// 排序算法。
        /// </summary>
        /// <typeparam name="TKey">键集合元素类型。</typeparam>
        /// <typeparam name="TItem">对象集合元素类型。</typeparam>
        /// <param name="keys">键集合。</param>
        /// <param name="items">对象集合。</param>
        /// <param name="comparer">比较方法，定义排序顺序。</param>
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
                    Swap(keys, 0, 1);
                    Swap(items, 0, 1);
                }
                return;
            }

            // 元素较少时，使用插入排序法。
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

            // 如果集合是数组，直接调用数组排序。
            if (keys is TKey[] keysArray && items is TItem[] itemsArray)
            {
                Array.Sort(keysArray, itemsArray, comparer);
                return;
            }

            // local sort implementation
            QuickSort(keys, items, comparer, 0, count - 1);
        }

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
                    Swap(keys, 0, 1);
                    Swap(items1, 0, 1);
                    Swap(items2, 0, 1);
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
        /// Sort a range of a list of keys, in place using the quick sort algorithm.
        /// </summary>
        /// <typeparam name="T">The type of element in the list.</typeparam>
        /// <param name="keys">List to sort.</param>
        /// <param name="index">The zero-based starting index of the range to sort.</param>
        /// <param name="count">The length of the range to sort.</param>
        /// <param name="comparer">Comparison, defining the sort order.</param>
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
                    Swap(keys, index, index + 1);
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
        /// Sort a list of keys and items with respect to the keys, in place using the quick sort algorithm.
        /// </summary>
        /// <typeparam name="TKey">The type of elements in the key list.</typeparam>
        /// <typeparam name="TItem">The type of elements in the item list.</typeparam>
        /// <param name="keys">List to sort.</param>
        /// <param name="items">List to permute the same way as the key list.</param>
        /// <param name="index">The zero-based starting index of the range to sort.</param>
        /// <param name="count">The length of the range to sort.</param>
        /// <param name="comparer">Comparison, defining the sort order.</param>
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
                    Swap(keys, index, index + 1);
                    Swap(items, index, index + 1);
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
        /// Sort a list of keys, items1 and items2 with respect to the keys, in place using the quick sort algorithm.
        /// </summary>
        /// <typeparam name="TKey">The type of elements in the key list.</typeparam>
        /// <typeparam name="TItem1">The type of elements in the first item list.</typeparam>
        /// <typeparam name="TItem2">The type of elements in the second item list.</typeparam>
        /// <param name="keys">List to sort.</param>
        /// <param name="items1">First list to permute the same way as the key list.</param>
        /// <param name="items2">Second list to permute the same way as the key list.</param>
        /// <param name="index">The zero-based starting index of the range to sort.</param>
        /// <param name="count">The length of the range to sort.</param>
        /// <param name="comparer">Comparison, defining the sort order.</param>
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
                    Swap(keys, index, index + 1);
                    Swap(items1, index, index + 1);
                    Swap(items2, index, index + 1);
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


        #endregion

        #region Quick Sort

        /// <summary>
        /// 快排算法。
        /// </summary>
        /// <typeparam name="T">键集合中的元素类型。</typeparam>
        /// <param name="keys">键集合。</param>
        /// <param name="comparer">比较方法，定义排序顺序。</param>
        /// <param name="left">快排算法的左侧边界。</param>
        /// <param name="right">快排算法的右侧边界。</param>
        static void QuickSort<T>(IList<T> keys, IComparer<T> comparer, int left, int right)
        {
            do
            {
                // 旋转
                int a = left;
                int b = right;
                int p = a + ((b - a) >> 1); // midpoint

                if (comparer.Compare(keys[a], keys[p]) > 0)
                {
                    Swap(keys, a, p);
                }

                if (comparer.Compare(keys[a], keys[b]) > 0)
                {
                    Swap(keys, a, b);
                }

                if (comparer.Compare(keys[p], keys[b]) > 0)
                {
                    Swap(keys, p, b);
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
                        Swap(keys, a, b);
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
        /// 快排算法。
        /// </summary>
        /// <typeparam name="T">键集合中的元素类型。</typeparam>
        /// <typeparam name="TItems">对象集合中的元素类型。</typeparam>
        /// <param name="keys">键集合。</param>
        /// <param name="items">对象集合。</param>
        /// <param name="comparer">比较方法，定义排序顺序。</param>
        /// <param name="left">快排算法的左侧边界。</param>
        /// <param name="right">快排算法的右侧边界。</param>
        static void QuickSort<T, TItems>(IList<T> keys, IList<TItems> items, IComparer<T> comparer, int left, int right)
        {
            do
            {
                // Pivoting
                int a = left;
                int b = right;
                int p = a + ((b - a) >> 1); // midpoint

                if (comparer.Compare(keys[a], keys[p]) > 0)
                {
                    Swap(keys, a, p);
                    Swap(items, a, p);
                }

                if (comparer.Compare(keys[a], keys[b]) > 0)
                {
                    Swap(keys, a, b);
                    Swap(items, a, b);
                }

                if (comparer.Compare(keys[p], keys[b]) > 0)
                {
                    Swap(keys, p, b);
                    Swap(items, p, b);
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
                        Swap(keys, a, b);
                        Swap(items, a, b);
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

        static void QuickSort<T, TItems1, TItems2>(
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
                    Swap(keys, a, p);
                    Swap(items1, a, p);
                    Swap(items2, a, p);
                }

                if (comparer.Compare(keys[a], keys[b]) > 0)
                {
                    Swap(keys, a, b);
                    Swap(items1, a, b);
                    Swap(items2, a, b);
                }

                if (comparer.Compare(keys[p], keys[b]) > 0)
                {
                    Swap(keys, p, b);
                    Swap(items1, p, b);
                    Swap(items2, p, b);
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
                        Swap(keys, a, b);
                        Swap(items1, a, b);
                        Swap(items2, a, b);
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

        #endregion


        #region 键集合基础操作
        /// <summary>
        /// 交换键集合中<paramref name="a"/>处和<paramref name="b"/>处的元素。
        /// </summary>
        /// <typeparam name="T">键集合中的元素类型。</typeparam>
        /// <param name="keys">键集合。</param>
        /// <param name="a">待交换的元素位置。</param>
        /// <param name="b">另一个待交换的元素位置。</param>
        public static void Swap<T>(IList<T> keys, int a, int b)
        {
            if (a == b)
            {
                return;
            }

            T tmp = keys[a];
            keys[a] = keys[b];
            keys[b] = tmp;
        } 
        #endregion
    }
}