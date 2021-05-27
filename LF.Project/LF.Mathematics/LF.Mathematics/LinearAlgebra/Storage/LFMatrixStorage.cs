/*──────────────────────────────────────────────────────────────
 * FileName     : LFMatrixStorage.cs
 * Created      : 2021-05-27 10:16:26
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
    /// 矩阵内存。
    /// </summary>
    public abstract class LFMatrixStorage<T> : IEquatable<LFMatrixStorage<T>>
        where T : struct, IEquatable<T>, IFormattable
    {
        #region Fields
        /// <summary>
        /// 定义类型T的0元素。
        /// </summary>
        protected static readonly T Zero;

        /// <summary>
        /// 矩阵行数
        /// </summary>
        [DataMember(Order = 1)]
        public readonly int RowCount;

        /// <summary>
        /// 矩阵列数
        /// </summary>
        [DataMember(Order = 2)]
        public readonly int ColumnCount;

        #endregion

        #region Properties


        /// <summary>
        /// 索引器，获取第<paramref name="row"/>行，第<paramref name="column"/>列的元素，或对其赋值。
        /// </summary>
        /// <param name="row">行数。</param>
        /// <param name="column">列数。</param>
        /// <value>第<paramref name="row"/>行，第<paramref name="column"/>列的元素</value>
        public T this[int row, int column]
        {
            get
            {
                return Get(row, column);
            }
            set
            {
                Set(row, column, value);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// 构造<paramref name="rows"/>行<paramref name="columns"/>列的矩阵内存。
        /// </summary>
        /// <param name="rowCount">行数。</param>
        /// <param name="columnCount">列数。</param>
        public LFMatrixStorage(int rows, int columns)
        {
            // 行数检测
            if (rows < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows), "矩阵的行数应该是非负的！");
            }

            // 列数检测
            if (columns < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns), "矩阵的列数应该是非负的！");
            }

            // 赋值
            RowCount = rows;
            ColumnCount = columns;
        }
        #endregion

        #region Methods

        #region Common Methods

        /// <summary>
        /// 获取第<paramref name="row"/>行，第<paramref name="column"/>列的元素。
        /// </summary>
        /// <param name="row">行数。</param>
        /// <param name="column">列数。</param>
        /// <returns>第<paramref name="row"/>行，第<paramref name="column"/>列的元素。</returns>
        public abstract T Get(int row, int column);

        /// <summary>
        /// 对第<paramref name="row"/>行，第<paramref name="column"/>列的元素赋值。
        /// </summary>
        /// <param name="row">行数。</param>
        /// <param name="column">列数。</param>
        /// <param name="value">值。</param>
        public abstract void Set(int row, int column, T value);

        /// <summary>
        /// 判断另一个矩阵内存是否与该矩阵内存相等。
        /// </summary>
        /// <param name="other">另一个矩阵内存。</param>
        /// <returns>相等-<c>true</c>；不相等-<c>false</c>。</returns>
        public bool Equals(LFMatrixStorage<T> other)
        {
            // 另一个矩阵内存为空，则返回false。
            if (other == null)
            {
                return false;
            }
            // 行列不相等，则返回false。
            if (ColumnCount != other.ColumnCount || RowCount != other.RowCount)
            {
                return false;
            }

            // object相等性判断。
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // 按照某种判定方式判定是否相等。
            return Find2Unchecked(other, (a, b) => !a.Equals(b)) == null;
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(obj as LFMatrixStorage<T>);
        }

        public override int GetHashCode()
        {
            var hashNum = Math.Min(RowCount * ColumnCount, 25);
            int hash = 17;
            unchecked
            {
                for (var i = 0; i < hashNum; i++)
                {
                    int col;
                    int row = Math.DivRem(i, ColumnCount, out col);
                    hash = hash * 31 + Get(row, col).GetHashCode();
                }
            }
            return hash;
        }
        #endregion

        #region Matrix Methods

        /// <summary>
        /// 转置
        /// </summary>
        /// <param name="target"></param>
        /// <param name="existingData"></param>
        public void Transpose(LFMatrixStorage<T> target, ExistingDataOption existingData = ExistingDataOption.Clear)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (RowCount != target.ColumnCount || ColumnCount != target.RowCount)
            {
                var message = $"Matrix dimensions must agree: op1 is {RowCount}x{ColumnCount}, op2 is {target.RowCount}x{target.ColumnCount}.";
                throw new ArgumentException(message, nameof(target));
            }

            if (ReferenceEquals(this, target))
            {
                TransposeSquareInplaceUnchecked();
                return;
            }

            TransposeToUnchecked(target, existingData);
        }


        /// <summary>
        /// 直接转置操作。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="existingData"></param>
        internal virtual void TransposeToUnchecked(LFMatrixStorage<T> target, ExistingDataOption existingData)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                for (int i = 0; i < RowCount; i++)
                {
                    target.Set(j, i, Get(i, j));
                }
            }
        }

        internal virtual void TransposeSquareInplaceUnchecked()
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                for (int i = 0; i < j; i++)
                {
                    T swap = Get(i, j);
                    Set(i, j, Get(j, i));
                    Set(j, i, swap);
                }
            }
        }

        #endregion

        #region Find
        /// <summary>
        /// 在该矩阵内存和另一个矩阵内存中，找出满足<paramref name="predicate"/>判定的一对元素。
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="other">另一个矩阵内存。</param>
        /// <param name="predicate">判定两个元素关系的函数。</param>
        /// <returns>
        /// <para>行数。</para>
        /// <para>列数。</para>
        /// <para>矩阵内存中的元素。</para>
        /// <para>另一个矩阵内存中的元素。</para>
        /// </returns>
        internal virtual Tuple<int, int, T, TOther> Find2Unchecked<TOther>(LFMatrixStorage<TOther> other, Func<T, TOther, bool> predicate)
            where TOther : struct, IEquatable<TOther>, IFormattable
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    var item = Get(i, j);
                    var otherItem = other.Get(i, j);
                    if (predicate(item, otherItem))
                    {
                        return new Tuple<int, int, T, TOther>(i, j, item, otherItem);
                    }
                }
            }
            return null;
        }
        #endregion

        #region Map Methods

        /// <summary>
        /// 矩阵内存元素就地映射。
        /// </summary>
        /// <param name="f">映射方法，自定义函数。</param>
        public virtual void MapInplace(Func<T,T> f,ZeroOption zero)
        {
            for(int i = 0; i < RowCount; i++)
            {
                for(int j = 0; j < ColumnCount; j++)
                {
                    Set(i, j, f(Get(i, j)));
                }
            }
        }

        #endregion

        #endregion


    }

    public enum ExistingDataOption
    {
        
        Clear,
        NoClear,
    }

    /// <summary>
    /// 零元素处理方法选项。
    /// </summary>
    public enum ZeroOption
    {
        /// <summary>
        /// 不跳过零元素。
        /// </summary>
        NoSkip,
        /// <summary>
        /// 跳过零元素。
        /// </summary>
        /// <remarks>该选项可以有效提高稀疏矩阵的运算速度。</remarks>
        Skip,
    }
}