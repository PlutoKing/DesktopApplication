/*──────────────────────────────────────────────────────────────
 * FileName     : LFNode.cs
 * Created      : 2021-05-15 14:43:18
 * Author       : Xu Zhe
 * Description  : 节点
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Algorithm
{
    /// <summary>
    /// 节点
    /// </summary>
    public class LFNode<T>:ICloneable
    {
        #region Fields
        private T _data;                    // 节点数据域
        #endregion

        #region Properties
        /// <summary>
        /// 数据域属性
        /// </summary>
        public T Data { get => _data; set => _data = value; }


        #endregion

        #region Constructors
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="data"></param>
        public LFNode(T data)
        {
            _data = data;
        }

        public LFNode(LFNode<T> obj)
        {
            this._data = obj._data;
        }
        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFNode<T> Clone()
        {
            return new LFNode<T>(this);
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

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}