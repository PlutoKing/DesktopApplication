/*──────────────────────────────────────────────────────────────
 * FileName     : LFDrone.cs
 * Created      : 2021-05-12 21:50:33
 * Author       : Xu Zhe
 * Description  : 无人机
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LF.Mechanics;
using LF.Mathematics;

namespace LF.UnmannedAerialVehicle
{
    /// <summary>
    /// 无人机
    /// </summary>
    public class LFDrone
    {
        #region Fields
        private LFBody _body = new LFBody();        // 刚体运动模型

        private LFMap2D _map = new LFMap2D();       // 路径规划地图
        private LFRoute2D _route = new LFRoute2D(); // 路径

        /* 导航、制导与控制 */
        int routeID = 0;                            // 当前目标路径点
        LFVector3 targetPosition = new LFVector3(); // 当前期望目标

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFDrone()
        {

        }
        #endregion

        #region Methods

        /*
        1. 启动无人机，无人机读取任务；
        2. 无人机自主模式，进入循环。
            2.1 航迹规划
            2.2 控制
         */

        /// <summary>
        /// 读取任务
        /// </summary>
        public void ReadTask()
        {

        }

        public void MainLoop()
        {

        }

        

        /// <summary>
        /// 
        /// </summary>
        public void GuidanceModule()
        {

        }
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}