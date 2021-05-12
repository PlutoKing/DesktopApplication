/*──────────────────────────────────────────────────────────────
 * FileName     : MinorTick.cs
 * Created      : 2021-05-12 20:19:44
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
    public class MinorTick:ICloneable
    {
        #region Fields
        protected bool _isOutside;
        protected bool _isInside;
        protected bool _isOpposite;
        protected bool _isCrossOutside;
        protected bool _isCrossInside;

        protected float _lineWidth;
        protected float _size;

        protected SolidColorBrush _color;

        #endregion

        #region Properties

        /// <summary>
        /// 是否坐标区外侧显示
        /// </summary>
        public bool IsOutside { get => _isOutside; set => _isOutside = value; }

        /// <summary>
        /// 是否在坐标区内侧显示
        /// </summary>
        public bool IsInside { get => _isInside; set => _isInside = value; }

        /// <summary>
        /// 是否在边框上显示
        /// </summary>
        public bool IsOpposite { get => _isOpposite; set => _isOpposite = value; }

        /// <summary>
        /// 是否在原点处坐标区外侧显示
        /// </summary>
        public bool IsCrossOutside { get => _isCrossOutside; set => _isCrossOutside = value; }

        /// <summary>
        /// 是否在原点坐标区内侧显示
        /// </summary>
        public bool IsCrossInside { get => _isCrossInside; set => _isCrossInside = value; }

        /// <summary>
        /// 线宽
        /// </summary>
        public float LineWidth { get => _lineWidth; set => _lineWidth = value; }

        /// <summary>
        /// 长度
        /// </summary>
        public float Size { get => _size; set => _size = value; }

        /// <summary>
        /// 颜色
        /// </summary>
        public SolidColorBrush Color { get => _color; set => _color = value; }

        #endregion

        #region Constructors
        public MinorTick()
        {
            _isOutside = Default.IsOutside;
            _isInside = Default.IsInside;
            _isOpposite = Default.IsOpposite;
            _isCrossOutside = Default.IsCrossOutside;
            _isCrossInside = Default.IsCrossInside;

            _lineWidth = Default.LineWidth;
            _size = Default.Size;
            _color = Default.Color;
        }

        public MinorTick(MinorTick rhs)
        {
            _size = rhs._size;
            _color = rhs._color;
            _lineWidth = rhs._lineWidth;

            this.IsOutside = rhs.IsOutside;
            this.IsInside = rhs.IsInside;
            this.IsOpposite = rhs.IsOpposite;
            _isCrossOutside = rhs._isCrossOutside;
            _isCrossInside = rhs._isCrossInside;
        }

        public MinorTick Clone()
        {
            return new MinorTick(this);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion

        #region Methods

        public float GetScaledTickSize(float scaleFactor)
        {
            return (float)(_size * scaleFactor);
        }

        #endregion

        #region Serializations
        #endregion

        #region Defaults

        public struct Default
        {
            public static bool IsOutside = false;
            public static bool IsInside = false;
            public static bool IsOpposite = false;
            public static bool IsCrossOutside = false;
            public static bool IsCrossInside = false;
            public static float LineWidth = 1.0f;
            public static float Size = 3.0f;
            public static SolidColorBrush Color = Brushes.Black;
        }

        #endregion

    }
}