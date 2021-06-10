/*──────────────────────────────────────────────────────────────
 * FileName     : LFMatrix.cs
 * Created      : 2021-05-31 15:39:45
 * Author       : Xu Zhe
 * Description  : 矩阵的计算符方法
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace LF.Mathematics.LinearAlgebra
{
    public abstract partial class LFMatrix<T>
    {
        /// <summary>
        /// 一元加法
        /// </summary>
        /// <param name="M">加数</param>
        /// <returns></returns>
        public static LFMatrix<T> operator +(LFMatrix<T> M)
        {
            return M.Clone();
        }
    }
}