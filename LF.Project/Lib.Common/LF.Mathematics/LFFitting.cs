/*──────────────────────────────────────────────────────────────
 * FileName     : LFFitting.cs
 * Created      : 2021-05-25 16:10:25
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Mathematics
{
    /// <summary>
    /// 拟合
    /// </summary>
    public class LFFitting
    {
        #region Fields
        private LFVector _x;
        private LFVector _y;
        #endregion

        #region Properties
        /// <summary>
        /// X
        /// </summary>
        public LFVector X { get => _x; set => _x = value; }
        /// <summary>
        /// Y
        /// </summary>
        public LFVector Y { get => _y; set => _y = value; }

        #endregion

        #region Constructors
        public LFFitting()
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// 多项式拟合
        /// </summary>
        /// <param name="n">多项式次数</param>
        /// <returns></returns>
        public double [] PolynomialFitting(int n)
        {
            LFMatrix A = LFMatrix.VandermondeMatrix(_x.Elements, n, _x.Length);
            LFVector a = A / _y;
            return a.Elements;
        }
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}