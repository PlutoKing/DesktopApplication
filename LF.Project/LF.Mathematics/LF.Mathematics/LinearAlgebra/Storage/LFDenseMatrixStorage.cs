/*──────────────────────────────────────────────────────────────
 * FileName     : LFDenseMatrixStorage.cs
 * Created      : 2021-05-27 10:49:29
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
    /// 稠密矩阵内存。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LFDenseMatrixStorage<T> : LFMatrixStorage<T>
        where T : struct, IEquatable<T>, IFormattable
    {
        #region Fields

        /// <summary>
        /// 矩阵数据
        /// </summary>
        [DataMember(Order = 1)]
        public readonly T[] Data;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        /// <summary>
        /// 构造<paramref name="rows"/>行<paramref name="columns"/>列的稠密矩阵内存。
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        internal LFDenseMatrixStorage(int rows, int columns)
            :base(rows,columns)
        {
            // 对数据进行初始化。
            Data = new T[rows * columns];
        }

        /// <summary>
        /// 构造<paramref name="rows"/>行<paramref name="columns"/>列，数据为<paramref name="data"/>的稠密矩阵内存。
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="data"></param>
        internal LFDenseMatrixStorage(int rows,int columns, T[] data)
            : base(rows, columns)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length != rows * columns)
            {
                throw new ArgumentOutOfRangeException(nameof(data), $"所给数组长度不符合要求，其长度应为 {rows * columns}");
            }

            Data = data;
        }
        #endregion

        #region Methods
        public override T Get(int row, int column)
        {
            return Data[(column * RowCount) + row];
        }

        public override void Set(int row, int column, T value)
        {
            Data[(column * RowCount) + row] = value;
        }

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}