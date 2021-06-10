/*──────────────────────────────────────────────────────────────
 * FileName     : LFMatrixParseStorage.cs
 * Created      : 2021-05-31 11:00:45
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
    /// 稀疏矩阵内存
    /// </summary>
    public class LFParseMatrixStorage<T> : LFMatrixStorage<T>
            where T : struct, IEquatable<T>, IFormattable
    {
        #region Fields

        /// <summary>
        /// 行指针
        /// </summary>
        /// <remarks>第一个数表示第一个非零元素所在行，其余元素表示每一行的元素个数。</remarks>
        public readonly int[] RowPointers;
        public readonly int[] ColumnIndices;
        public T[] Data;
        #endregion

        #region Properties
        public override bool IsDense => false;

        #endregion

        #region Constructors
        /// <summary>
        /// 构造<paramref name="m"/>行<paramref name="n"/>列的系数矩阵内存
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        public LFParseMatrixStorage(int m,int n)
            :base(m,n)
        {
            RowPointers = new int[m + 1];
            ColumnIndices = new int[0];
            Data = new T[0];
        }


        public override T Get(int m, int n)
        {
            throw new NotImplementedException();
        }

        public override void Set(int m, int n, T value)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public void SetOrder()
        {
            for (int i = 0; i < RowCount; i++)
            {
                int index = RowPointers[i];
                int count = RowPointers[i + 1] - index; // 后一个减去前一个表示数量
                if (count > 1)
                {
                    LFSort.Sort(ColumnIndices, Data, index, count);
                }
            }
        } 
        
        #endregion
    }
}