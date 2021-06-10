/*──────────────────────────────────────────────────────────────
 * FileName     : LFDenseMatrixStorage.cs
 * Created      : 2021-05-31 10:53:22
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace LF.Mathematics.LinearAlgebra.Storage
{
    /// <summary>
    /// 稠密矩阵内存
    /// </summary>
    public class LFDenseMatrixStorage<T> : LFMatrixStorage<T>
            where T : struct, IEquatable<T>, IFormattable
    {
        #region Fields

        /// <summary>
        /// 内存数据
        /// </summary>
        public T[] Data;
        #endregion

        #region Properties
        public override bool IsDense => true;

        #endregion

        #region Constructors
        /// <summary>
        /// 构造<paramref name="m"/>行<paramref name="n"/>列的稠密矩阵内存
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        public LFDenseMatrixStorage(int m,int n)
            :base(m,n)
        {
            Data = new T[m* n];
        }

        /// <summary>
        /// 构造<paramref name="m"/>行<paramref name="n"/>列的稠密矩阵内存，并赋值
        /// </summary>
        /// <param name="m">行数</param>
        /// <param name="n">列数</param>
        /// <param name="data"></param>
        public LFDenseMatrixStorage(int m, int n, T[] data)
            : base(m, n)
        {
            if(data == null)
                throw new ArgumentNullException(nameof(data));
            if(data.Length != m*n)
                throw new ArgumentOutOfRangeException(nameof(data), $"给定数组长度错误！其长度应为 {m * n}。");

            Data = data;
        }



        #endregion

        #region Methods
        public override T Get(int m, int n)
        {
            return Data[(n * RowCount) + m];
        }

        public override void Set(int m, int n, T value)
        {
            Data[(n * RowCount) + m] = value;
        }
        #endregion
    }
}