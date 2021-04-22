/*──────────────────────────────────────────────────────────────
 * FileName     : LFArray.cs
 * Created      : 2021-04-22 20:34:36
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
    /// 数列
    /// </summary>
    public class LFArray<T>:ICloneable
    {
        #region Fields
        private T[] _elements;      // 元素
        #endregion

        #region Properties

        /// <summary>
        /// 元素
        /// </summary>
        public T[] Elements { get => _elements; set => _elements = value; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length
        {
            get { return _elements.Length; }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index">索引号</param>
        /// <returns></returns>
        public T this[int index]
        {
            get { return _elements[index]; }
            set { _elements[index] = value; }
        }
        #endregion

        #region Constructors
        public LFArray(T[] elements)
        {
            _elements = elements;
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFArray(LFArray<T> rhs)
        {
            this._elements = rhs._elements;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFArray<T> Clone()
        {
            return new LFArray<T>(this);
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }
        #endregion

        #region Methods

        #region 最大最小
        /// <summary>
        /// 取最小值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="min"></param>
        public void GetMin(out int index, out T min)
        {
            index = 0;
            min = this._elements[0];

            for (int i = 1; i < this.Length; i++)
            {
                if (System.Collections.Comparer.Default.Compare(this[i], min) < 0)
                {
                    min = this[i];
                    index = i;
                }
            }
        }

        /// <summary>
        /// 取最大值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="min"></param>
        public void GetMax(out int index, out T max)
        {
            index = 0;
            max = this._elements[0];

            for (int i = 1; i < this.Length; i++)
            {
                if (System.Collections.Comparer.Default.Compare(this[i], max) > 0)
                {
                    max = this[i];
                    index = i;
                }
            }
        } 
        #endregion
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}