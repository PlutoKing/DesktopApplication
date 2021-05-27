/*──────────────────────────────────────────────────────────────
 * FileName     : LFTrigonometry.cs
 * Created      : 2021-05-27 10:03:02
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
    /// 三角函数类。
    /// </summary>
    public class LFTrigonometry
    {

        #region Fields


        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFTrigonometry()
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// 角度制转化为弧度制。
        /// </summary>
        /// <param name="degree">角度。</param>
        /// <returns>弧度。</returns>
        public static double DegreeToRadian(double degree)
        {
            return degree * LFConstants.UnitRadian;
        }

        /// <summary>
        /// 弧度制转化为角度制。
        /// </summary>
        /// <param name="radian">弧度。</param>
        /// <returns>角度。</returns>
        public static double RadianToDegree(double radian)
        {
            return radian / LFConstants.UnitRadian;
        }
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}