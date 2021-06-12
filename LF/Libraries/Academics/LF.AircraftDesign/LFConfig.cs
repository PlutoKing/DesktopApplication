/*──────────────────────────────────────────────────────────────
 * FileName     : LFConfig.cs
 * Created      : 2021-06-11 19:52:34
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace LF.AircraftDesign
{
    /// <summary>
    /// 总体布局
    /// </summary>
    public enum OverallConfiguration
    {
        /// <summary>
        /// 常规布局
        /// </summary>
        Conventional = 1,
        /// <summary>
        /// 飞翼布局
        /// </summary>
        FlyingWing = 2,
        /// <summary>
        /// 鸭式布局
        /// </summary>
        Canard = 3,
        /// <summary>
        /// 串列翼布局
        /// </summary>
        TandemWing = 4,
        /// <summary>
        /// 三翼面布局
        /// </summary>
        ThreeSurface = 5,
        /// <summary>
        /// 联接翼布局
        /// </summary>
        JoinedWing = 6,
    }
}