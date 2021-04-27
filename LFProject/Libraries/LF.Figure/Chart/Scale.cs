/*──────────────────────────────────────────────────────────────
 * FileName     : Scale.cs
 * Created      : 2021-04-27 20:37:32
 * Author       : Xu Zhe
 * Description  : 刻度
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Figure
{
    /// <summary>
    /// 刻度
    /// </summary>
    public class Scale
    {
        #region Fields
        private double _min = 0;        // 最小值
        private double _max = 10;        // 最大值
        private double _minorStep = 0.5;  // 副刻度
        private double _majorStep = 1;  // 主刻度
        #endregion

        #region Properties
        public double Min { get => _min; set => _min = value; }
        public double Max { get => _max; set => _max = value; }
        public double MinorStep { get => _minorStep; set => _minorStep = value; }
        public double MajorStep { get => _majorStep; set => _majorStep = value; }

        #endregion

        #region Constructors
        public Scale()
        {
        }

        #endregion

        #region Methods

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}