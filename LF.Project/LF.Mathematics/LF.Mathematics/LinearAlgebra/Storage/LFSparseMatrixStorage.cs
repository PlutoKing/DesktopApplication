/*──────────────────────────────────────────────────────────────
 * FileName     : LFSparseMatrixStorage.cs
 * Created      : 2021-05-27 10:56:45
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
    public class LFSparseMatrixStorage<T> : LFMatrixStorage<T>
        where T : struct, IEquatable<T>, IFormattable
    {
        #region Fields

        /// <summary>
        /// 数据的行索引。
        /// </summary>
        [DataMember(Order = 1)]
        public readonly int[] RowPointers;

        /// <summary>
        /// 数据的列索引。
        /// </summary>
        [DataMember(Order = 2)]
        public int[] ColumnIndices;

        /// <summary>
        /// 稀疏矩阵内存的数据。
        /// </summary>
        [DataMember(Order = 3)]
        public T[] Datas;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        /// <summary>
        /// 构造<paramref name="rows"/>行<paramref name="columns"/>列的稀疏矩阵内存。
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        internal LFSparseMatrixStorage(int rows, int columns)
            : base(rows, columns)
        {
            RowPointers = new int[rows + 1];
            ColumnIndices = new int[0];
            Datas = new T[0];
        }

        /// <summary>
        /// 构造<paramref name="rows"/>行<paramref name="columns"/>列的矩阵内存，并对矩阵内存赋值。
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="rowPointers"></param>
        /// <param name="columnIndices"></param>
        /// <param name="values"></param>
        internal LFSparseMatrixStorage(int rows, int columns, int[] rowPointers, int[] columnIndices, T[] values)
            : base(rows, columns)
        {
            RowPointers = rowPointers;
            ColumnIndices = columnIndices;
            Datas = values;

            // Explicit zeros are not intentionally removed.
            // Sort ColumnIndices.
            NormalizeOrdering();
            NormalizeDuplicates();
        }


        #endregion

        #region Methods
        public override T Get(int row, int column)
        {
            // 检索第row行和第column列对应元素在Datas中的索引。
            var index = FindItem(row, column);
            // 如果该元素的索引非负，则返回对应的元素；否则返回0元素。
            return index >= 0 ? Datas[index] : Zero;
        }

        public override void Set(int row, int column, T value)
        {
            var index = FindItem(row, column);
            if (index >= 0)
            {
                // Non-zero item found in matrix
                if (Zero.Equals(value))
                {
                    // Delete existing item
                    RemoveAtIndexUnchecked(index, row);
                }
                else
                {
                    // Update item
                    Datas[index] = value;
                }
            }
            else
            {
                // Item not found. Add new value
                if (Zero.Equals(value))
                {
                    return;
                }

                index = ~index;
                var valueCount = RowPointers[RowPointers.Length - 1];

                // Check if the storage needs to be increased
                if ((valueCount == Datas.Length) && (valueCount < ((long)RowCount * ColumnCount)))
                {
                    // Value array is completely full so we increase the size
                    // Determine the increase in size. We will not grow beyond the size of the matrix
                    var size = Math.Min(Datas.Length + GrowthSize(), (long)RowCount * ColumnCount);
                    if (size > int.MaxValue)
                    {
                        throw new NotSupportedException($"仅支持尺寸小于{int.MaxValue}元素的稀疏矩阵");
                    }

                    Array.Resize(ref Datas, (int)size);
                    Array.Resize(ref ColumnIndices, (int)size);
                }

                // Move all values (with a position larger than index) in the value array to the next position
                // move all values (with a position larger than index) in the columIndices array to the next position
                Array.Copy(Datas, index, Datas, index + 1, valueCount - index);
                Array.Copy(ColumnIndices, index, ColumnIndices, index + 1, valueCount - index);

                // Add the value and the column index
                Datas[index] = value;
                ColumnIndices[index] = column;

                // add 1 to all the row indices for rows bigger than rowIndex
                // so that they point to the correct part of the value array again.
                for (var i = row + 1; i < RowPointers.Length; i++)
                {
                    RowPointers[i] += 1;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <param name="row"></param>
        private void RemoveAtIndexUnchecked(int itemIndex,int row)
        {
            var valueCount = RowPointers[RowPointers.Length - 1];
            Array.Copy(Datas, itemIndex + 1, Datas, itemIndex, valueCount - itemIndex - 1);
            Array.Copy(ColumnIndices, itemIndex + 1, ColumnIndices, itemIndex, valueCount - itemIndex - 1);

            for (var i = row + 1; i < RowPointers.Length; i++)
            {
                RowPointers[i] -= 1;
            }

            valueCount -= 1;

            // 检查是否需要缩小阵列。如果有很多非零元素，并且存储空间大了两倍，那么这样做是合理的
            if ((valueCount > 1024) && (valueCount < Datas.Length / 2))
            {
                Array.Resize(ref Datas, valueCount);
                Array.Resize(ref ColumnIndices, valueCount);
            }

        }

        /// <summary>
        /// 计算第<paramref name="row"/>行第<paramref name="column"/>列的元素的索引。
        /// </summary>
        /// <param name="row">行数。</param>
        /// <param name="column">列数。</param>
        /// <returns></returns>
        public int FindItem(int row, int column)
        {
            // 确定ColumnIndexes数组中应搜索此项的边界（使用rowIndex）
            return Array.BinarySearch(ColumnIndices, RowPointers[row], RowPointers[row + 1] - RowPointers[row], column);
        }


        int GrowthSize()
        {
            int delta;
            if (Datas.Length > 1024)
            {
                delta = Datas.Length / 4;
            }
            else
            {
                if (Datas.Length > 256)
                {
                    delta = 512;
                }
                else
                {
                    delta = Datas.Length > 64 ? 128 : 32;
                }
            }

            return delta;
        }

        /// <summary>
        /// 规范化，顺序和零项。
        /// </summary>
        public void Normalize()
        {
            NormalizeOrdering();
            NormalizeZeros();
        }

        /// <summary>
        /// 规范化顺序。
        /// </summary>
        public void NormalizeOrdering()
        {
            for (int i = 0; i < RowCount; i++)
            {
                int index = RowPointers[i];
                int count = RowPointers[i + 1] - index;
                if (count > 1)
                {
                    Sorting.Sort(ColumnIndices, Datas, index, count);
                }
            }
        }

        /// <summary>
        /// 规范化零项。
        /// </summary>
        public void NormalizeZeros()
        {
            MapInplace(x => x, ZeroOption.Skip);
        }

        /// <summary>
        /// 规范化重复项。
        /// </summary>
        public void NormalizeDuplicates()
        {
            var builder = LFBuilder<T>.Matrix;

            int valueCount = 0;
            for (int i = 0; i < RowCount; i++)
            {
                int index = RowPointers[i];
                int last = RowPointers[i + 1];
                while (index < last)
                {
                    var col = ColumnIndices[index];
                    var val = Datas[index];
                    index++;
                    while (index < last)
                    {
                        if (ColumnIndices[index] == col)
                        {
                            val = builder.Add(val, Datas[index]);
                            index++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    ColumnIndices[valueCount] = col;
                    Datas[valueCount] = val;
                    valueCount++;
                }
                RowPointers[i + 1] = valueCount;
            }

            // Remove extra space from arrays.
            Array.Resize(ref Datas, valueCount);
            Array.Resize(ref ColumnIndices, valueCount);
        }


        #region Map Methods
        /// <summary>
        /// 矩阵内存元素就地映射。
        /// </summary>
        /// <param name="f">映射方法，自定义函数。</param>
        /// <param name="zero">零元素选项，决定是否跳过零元素。</param>
        public override void MapInplace(Func<T, T> f, ZeroOption zero)
        {
            // 不跳过零元素，或零映射不为零（这种情况无法跳过零元素）。
            if (zero == ZeroOption.NoSkip || !Zero.Equals(f(Zero)))
            {
                var newRowPointers = RowPointers;
                var newColumnIndices = new List<int>(ColumnIndices.Length);
                var newValues = new List<T>(Datas.Length);

                int k = 0;
                for (int row = 0; row < RowCount; row++)
                {
                    newRowPointers[row] = newValues.Count;
                    for (int col = 0; col < ColumnCount; col++)
                    {
                        var item = k < RowPointers[row + 1] && ColumnIndices[k] == col ? f(Datas[k++]) : f(Zero);
                        if (!Zero.Equals(item))
                        {
                            newValues.Add(item);
                            newColumnIndices.Add(col);
                        }
                    }
                }

                ColumnIndices = newColumnIndices.ToArray();
                Datas = newValues.ToArray();
                newRowPointers[RowCount] = newValues.Count;
            }
            // 可以跳过零元素的情况。
            else
            {
                int nonZero = 0;
                for (int row = 0; row < RowCount; row++)
                {
                    var startIndex = RowPointers[row];
                    var endIndex = RowPointers[row + 1];
                    RowPointers[row] = nonZero;
                    for (var j = startIndex; j < endIndex; j++)
                    {
                        var item = f(Datas[j]);
                        if (!Zero.Equals(item))
                        {
                            Datas[nonZero] = item;
                            ColumnIndices[nonZero] = ColumnIndices[j];
                            nonZero++;
                        }
                    }
                }
                Array.Resize(ref ColumnIndices, nonZero);
                Array.Resize(ref Datas, nonZero);
                RowPointers[RowCount] = nonZero;
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