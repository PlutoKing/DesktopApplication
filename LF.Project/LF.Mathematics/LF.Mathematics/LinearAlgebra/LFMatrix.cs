/*──────────────────────────────────────────────────────────────
 * FileName     : LFMatrix.cs
 * Created      : 2021-05-31 10:30:29
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using LF.Mathematics.LinearAlgebra.Storage;

namespace LF.Mathematics.LinearAlgebra
{
    /// <summary>
    /// 矩阵
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public partial class LFMatrix<T> : IEquatable<LFMatrix<T>>
        where T : struct, IEquatable<T>, IFormattable
    {
        #region Fields

        private readonly LFMatrixStorage<T> _storage;   // 矩阵数据内存
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFMatrix()
        {
        }

        public LFMatrix(LFMatrix<T> rhs)
        {
            _storage = rhs._storage;
        }
        public LFMatrix<T> Clone()
        {
            return new LFMatrix(this);
        }
        #endregion

        #region Methods

        public bool Equals(LFMatrix<T> other)
        {
            return _storage.Equals(other._storage);
        }

        #endregion
    }
}