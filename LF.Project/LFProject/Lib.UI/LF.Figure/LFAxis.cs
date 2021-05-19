/*──────────────────────────────────────────────────────────────
 * FileName     : LFAxis.cs
 * Created      : 2021-05-15 19:32:15
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LF.Figure
{
    /// <summary>
    /// 坐标轴
    /// </summary>
    public abstract class LFAxis
    {
        #region Fields
        protected LFScale _scale;             // 比例刻度
        protected LFMinorTick _minorTick;     // 副刻度线样式
        protected LFMajorTick _majorTick;     // 主刻度线样式

        protected LFLabel _title;             // 坐标标题
        private double _axisGap = 0.5;


        internal double _tmpSpace;   // 非properties


        #endregion

        #region Properties

        /// <summary>
        /// 比例刻度
        /// </summary>
        public LFScale Scale { get => _scale; set => _scale = value; }
        /// <summary>
        /// 副刻度线样式
        /// </summary>
        public LFMinorTick MinorTick { get => _minorTick; set => _minorTick = value; }
        /// <summary>
        /// 主刻度线样式
        /// </summary>
        public LFMajorTick MajorTick { get => _majorTick; set => _majorTick = value; }
        /// <summary>
        /// 标题
        /// </summary>
        public LFLabel Title { get => _title; set => _title = value; }

        /// <summary>
        /// 标题间隔
        /// </summary>
        public double AxisGap { get => _axisGap; set => _axisGap = value; }

        #endregion

        #region Constructors
        public LFAxis()
        {
            _scale = new LFScale();
            _minorTick = new LFMinorTick();
            _majorTick = new LFMajorTick();

            _title = new LFLabel();
            _title.HorizontalContentAlignment = HorizontalAlignment.Center;

        }
        #endregion

        #region Methods

        /// <summary>
        /// 计算坐标轴尺寸
        /// </summary>
        /// <returns></returns>
        public double GetSpace()
        {
            double charHeight = _scale.FontSize;

            double ticSize = _majorTick.Size;

            double LabelGap = _axisGap * charHeight;

            _tmpSpace = 0;


            // 开始计算
            bool hasTic = this._majorTick.IsOutside || this._majorTick.IsCrossOutside ||
                    this._minorTick.IsOutside || this._minorTick.IsCrossOutside;

            if (hasTic)
                _tmpSpace += ticSize;

            _tmpSpace += _scale.GetScaleMaxSpace().Height+LabelGap;

            return _tmpSpace;

        }
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}