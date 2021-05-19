/*──────────────────────────────────────────────────────────────
 * FileName     : LFMap.cs
 * Created      : 2021-05-15 15:11:49
 * Author       : Xu Zhe
 * Description  : 地图
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Simulation
{
    /// <summary>
    /// 地图
    /// </summary>
    public class LFMap
    {
        #region Fields
        private double _xmin;   // X最小值
        private double _xmax;   // X最大值
        private double _ymin;   // Y最小值
        private double _ymax;   // Y最大值
        #endregion

        #region Properties
        #region Range Properties
        /// <summary>
        /// X最小值
        /// </summary>
        public double Xmin { get => _xmin; set => _xmin = value; }
        /// <summary>
        /// X最大值
        /// </summary>
        public double Xmax { get => _xmax; set => _xmax = value; }
        /// <summary>
        /// Y最小值
        /// </summary>
        public double Ymin { get => _ymin; set => _ymin = value; }
        /// <summary>
        /// Y最大值
        /// </summary>
        public double Ymax { get => _ymax; set => _ymax = value; }

        #endregion
        #endregion

        #region Constructors
        public LFMap()
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