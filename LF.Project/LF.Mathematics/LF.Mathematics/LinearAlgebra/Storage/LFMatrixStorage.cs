/*──────────────────────────────────────────────────────────────
 * FileName     : LFMatrixStorage.cs
 * Created      : 2021-05-31 10:31:54
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
    /// 矩阵内存抽象类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LFMatrixStorage<T> : IEquatable<LFMatrixStorage<T>>
        where T : struct, IEquatable<T>, IFormattable
    {
        #region Fields

        private int _rowCount;      // 行数
        private int _columnCount;   // 列数

        #endregion

        #region Properties

        /// <summary>
        /// 行数
        /// </summary>
        public int RowCount { get => _rowCount; }
        /// <summary>
        /// 列数
        /// </summary>
        public int ColumnCount { get => _columnCount; }

        /// <summary>
        /// 是否为稠密矩阵
        /// </summary>
        public abstract bool IsDense { get; }

        
        #endregion

        #region Constructors

        /// <summary>
        /// 构造<paramref name="m"/>行<paramref name="n"/>列的矩阵内存
        /// </summary>
        /// <param name="m">行数</param>
        /// <param name="n">列数</param>
        public LFMatrixStorage(int m,int n)
        {
            if (m < 0)
                throw new ArgumentOutOfRangeException(nameof(m), "矩阵的行数应为非负的！");
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), "矩阵的列数应为非负的！");

            _rowCount = m;
            _columnCount = n;
        }

        #endregion

        #region Methods

        #region Common Methods

        /// <summary>
        /// 获取第<paramref name="m"/>行，第<paramref name="n"/>的元素
        /// </summary>
        /// <param name="m">行数</param>
        /// <param name="n">列数</param>
        /// <returns></returns>
        public abstract T Get(int m, int n);

        /// <summary>
        /// 给第<paramref name="m"/>行，第<paramref name="n"/>的元素赋值
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract void Set(int m, int n, T value);
        
        /// <summary>
        /// 判定两个矩阵内存是否相等
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(LFMatrixStorage<T> other)
        {
            if (other == null)
                return false;
            if (_rowCount != other._rowCount || _columnCount != other._columnCount)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            // 找不到a不等于b，则表示全部相等
            return Find2Unchecked(other, (a, b) => !a.Equals(b)) == null;
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(obj as LFMatrixStorage<T>);
        }

        /// <summary>
        /// 获取Hash码
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashNum = Math.Min(RowCount * ColumnCount, 25);
            int hash = 17;
            unchecked
            {
                for (var i = 0; i < hashNum; i++)
                {
                    int m = Math.DivRem(i, ColumnCount, out int col);
                    hash = hash * 31 + Get(m, col).GetHashCode();
                }
            }
            return hash;
        }
        #endregion


        #region Find

        internal virtual Tuple<int, int, T, TOther> Find2Unchecked<TOther>(
            LFMatrixStorage<TOther> other,
            Func<T, TOther, bool> fun)
            where TOther : struct, IEquatable<TOther>, IFormattable
        {
            for (int i = 0; i < _rowCount; i++)
            {
                for(int j = 0; j < _columnCount; j++)
                {
                    var item = Get(i, j);
                    var otherItem = other.Get(i, j);
                    if (fun(item, otherItem))
                    {
                        return new Tuple<int, int, T, TOther>(i, j, item, otherItem);
                    }
                }
            }
            return null;
        } 

        #endregion

        #endregion
    }
}