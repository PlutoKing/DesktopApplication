/*──────────────────────────────────────────────────────────────
 * FileName     : LFMath.cs
 * Created      : 2021-05-13 21:31:49
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Mathematics.Basic
{
    public class LFMath
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFMath()
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// n的阶乘
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Factorial(int n)
        {
            int r = 1;
            for (int i = 1; i <= n; i++)
            {
                r = r * i;
            }
            return r;
        }

        /// <summary>
        /// 二项式系数C_n^r
        /// </summary>
        /// <param name="n"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static int BinomialCoefficient(int n,int r)
        {
            return Factorial(n) / (Factorial(r) * Factorial(n - r));
        }
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}