/*──────────────────────────────────────────────────────────────
 * FileName     : LFAutoPilot.cs
 * Created      : 2021-05-13 15:28:08
 * Author       : Xu Zhe
 * Description  : 自动驾驶仪
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.UnmannedAerialVehicle
{
    public class LFAutoPilot
    {
        #region Fields


        

        /// <summary>
        /// 航点序列
        /// </summary>
        private List<LFWaypoint> _waypoints = new List<LFWaypoint>();

        private int _waypointID;        // 当前目标航点序号

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFAutoPilot()
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