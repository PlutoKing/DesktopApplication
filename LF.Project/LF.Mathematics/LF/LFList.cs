/*──────────────────────────────────────────────────────────────
 * FileName     : LFList.cs
 * Created      : 2021-05-31 15:31:27
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace LF
{
    /// <summary>
    /// IList操作算法
    /// </summary>
    public static class LFList
    {
        /// <summary>
        /// 交换
        /// </summary>
        /// <typeparam name="T">键类型</typeparam>
        /// <param name="keys">键列表</param>
        /// <param name="a">第一个键的索引</param>
        /// <param name="b">第二个键的索引</param>
        public static void Swap<T>(IList<T> keys, int a, int b)
        {
            if (a == b)
            {
                return;
            }

            T local = keys[a];
            keys[a] = keys[b];
            keys[b] = local;
        }
    }
}