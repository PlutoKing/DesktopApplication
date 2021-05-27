/*──────────────────────────────────────────────────────────────
 * FileName     : LFMatrixBase.cs
 * Created      : 2021-05-27 15:58:16
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LF.Mathematics.LinearAlgebra.Storage;

namespace LF.Mathematics.LinearAlgebra
{
    /// <summary>
    /// 矩阵的基础抽象类。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class LFMatrixBase<T> : IFormattable, IEquatable<Matrix<T>>, ICloneable
        where T : struct, IEquatable<T>, IFormattable
    {
        #region Fields

        /// <summary>
        /// 矩阵构造器
        /// </summary>
        public static readonly LFMatrixBuilder<T> Build = LFBuilder<T>.Matrix;

        #endregion

        #region Properties
        /// <summary>
        /// 矩阵内存
        /// </summary>
        public LFMatrixStorage<T> Storage { get; private set; }

        /// <summary>
        /// 列数
        /// </summary>
        public int ColumnCount { get; private set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int RowCount { get; private set; }


        /// <summary>
        /// 索引器，获取第<paramref name="row"/>行第<paramref name="column"/>列的元素，或对其赋值。
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public T this[int row,int column]
        {
            get { return Storage[row, column]; }
            set { Storage[row, column] = value; }
        }

        #endregion

        #region Constructors
        protected LFMatrixBase(LFMatrixStorage<T> storage)
        {

        }
        #endregion

        #region Methods

        public T Get(int row,int column)
        {
            return Storage.Get(row, column);
        }

        public void Set(int row,int column, T value)
        {
            Storage.Set(row, column, value);
        }


        #region Matrix Methods

        /// <summary>
        /// 矩阵转置。
        /// </summary>
        /// <returns></returns>
        public LFMatrixBase<T> Transpose()
        {
            var result = Build.SameAs(this, ColumnCount, RowCount);
            Storage.TransposeToUnchecked(result.Storage, ExistingDataOption.NoClear);
            return result;
        }

        #endregion

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}