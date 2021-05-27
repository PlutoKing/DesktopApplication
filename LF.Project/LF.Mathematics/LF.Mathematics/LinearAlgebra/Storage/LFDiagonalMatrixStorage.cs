/*──────────────────────────────────────────────────────────────
 * FileName     : LFDiagonalMatrixStorage.cs
 * Created      : 2021-05-27 15:54:17
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LF.Mathematics.LinearAlgebra.Storage
{
    /// <summary>
    /// 对角矩阵内存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LFDiagonalMatrixStorage<T> : LFMatrixStorage<T>
        where T : struct, IEquatable<T>, IFormattable
    {
        #region Fields

        [DataMember(Order = 1)]
        public readonly T[] Data;


        #endregion

        #region Properties

        #endregion

        #region Constructors

        /// <summary>
        /// 构造<paramref name="rows"/>行<paramref name="columns"/>列的对角矩阵内存。
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public LFDiagonalMatrixStorage(int rows,int columns)
            : base(rows, columns)
        {
            Data = new T[Math.Min(rows, columns)];
        }

        /// <summary>
        /// 构造<paramref name="rows"/>行<paramref name="columns"/>列的对角矩阵内存，并赋值。
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="data"></param>
        internal LFDiagonalMatrixStorage(int rows, int columns, T[] data)
            : base(rows, columns)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length != Math.Min(rows, columns))
            {
                throw new ArgumentOutOfRangeException(nameof(data), $"The given array has the wrong length. Should be {Math.Min(rows, columns)}.");
            }

            Data = data;
        }
        #endregion

        #region Methods

        #region Common Methods
        public override T Get(int row, int column)
        {
            return row == column ? Data[row] : Zero;
        }

        public override void Set(int row, int column, T value)
        {
            if (row == column)
            {
                Data[row] = value;
            }
            else if (!Zero.Equals(value))
            {
                throw new IndexOutOfRangeException("不能在对角矩阵中设置非对角元素。");
            }
        }

        public override int GetHashCode()
        {
            var hashNum = Math.Min(Data.Length, 25);
            int hash = 17;
            unchecked
            {
                for (var i = 0; i < hashNum; i++)
                {
                    hash = hash * 31 + Data[i].GetHashCode();
                }
            }
            return hash;
        } 
        #endregion

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}