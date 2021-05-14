/*──────────────────────────────────────────────────────────────
 * FileName     : LFArray.cs
 * Created      : 2021-05-13 11:54:10
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Algorithm
{
    /// <summary>
    /// 数组
    /// </summary>
    public class LFArray<T> where T : IComparable
    {
        #region Fields
        private T[] _elements;  // 数组元素
        #endregion

        #region Properties

        public int Length { get { return _elements.Length; } }

        #endregion

        #region Constructors
        public LFArray()
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// 获取数组最小值
        /// </summary>
        /// <param name="min"></param>
        /// <param name="index"></param>
        public void GetMin(out T min ,out int index)
        {
            index = 0;
            min = _elements[0];

            for (int i = 1; i < _elements.Length; i++)
            {
                if (_elements[i].CompareTo(min) < 0)
                {
                    min = _elements[i];
                    index = i;
                }
            }
        }


        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}