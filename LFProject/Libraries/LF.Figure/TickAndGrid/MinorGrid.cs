/*──────────────────────────────────────────────────────────────
 * FileName     : MinorGrid.cs
 * Created      : 2021-05-12 20:32:12
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LF.Figure
{
    /// <summary>
    /// 副刻度网格线样式
    /// </summary>
    public class MinorGrid : ICloneable
    {
        #region Fields
        protected bool _isVisible;

        protected float _dashOn;
        protected float _dashOff;
        protected float _lineWidth;

        protected SolidColorBrush _color;
        #endregion

        #region Properties

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsVisible { get => _isVisible; set => _isVisible = value; }

        /// <summary>
        /// 虚线样式
        /// </summary>
        public float DashOn { get => _dashOn; set => _dashOn = value; }

        /// <summary>
        /// 虚线样式
        /// </summary>
        public float DashOff { get => _dashOff; set => _dashOff = value; }

        /// <summary>
        /// 线宽
        /// </summary>
        public float LineWidth { get => _lineWidth; set => _lineWidth = value; }

        /// <summary>
        /// 颜色
        /// </summary>
        public SolidColorBrush Color { get => _color; set => _color = value; }

        #endregion

        #region Constructors
        public MinorGrid()
        {
            _dashOn = Default.DashOn;
            _dashOff = Default.DashOff;
            _lineWidth = Default.LineWidth;
            _isVisible = Default.IsVisible;
            _color = Default.Color;
        }

        public MinorGrid(MinorGrid rhs)
        {
            _dashOn = rhs._dashOn;
            _dashOff = rhs._dashOff;
            _lineWidth = rhs._lineWidth;

            _isVisible = rhs._isVisible;

            _color = rhs._color;
        }

        public MinorGrid Clone()
        {
            return new MinorGrid(this);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }


        #endregion

        #region Methods


        #endregion

        #region Serializations
        #endregion

        #region Defaults
        public struct Default
        {
            public static bool IsVisible = false;

            public static float DashOn = 1.0F;
            public static float DashOff = 10.0F;
            public static float LineWidth = 1.0F;

            public static SolidColorBrush Color = Brushes.Gray;
        }
        #endregion
    }
}