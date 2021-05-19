/*──────────────────────────────────────────────────────────────
 * FileName     : LFMinorTick.cs
 * Created      : 2021-05-15 19:35:47
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Shapes;

namespace LF.Figure
{
    public class LFMinorTick
    {
        #region Fields
        protected bool _isOutside = false;
        protected bool _isInside = true;
        protected bool _isOpposite = false;
        protected bool _isCrossOutside = false;
        protected bool _isCrossInside = false;

        protected double _lineWidth = 1.0;
        protected double _size = 3.0;

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
        public double LineWidth { get => _lineWidth; set => _lineWidth = value; }

        /// <summary>
        /// 长度
        /// </summary>
        public double Size { get => _size; set => _size = value; }


        #endregion

        #region Constructors
        public LFMinorTick()
        {
        }
        #endregion

        #region Methods

        public void Draw(Canvas canvas, double pixVal, double topPix,
                    double shift, double scaledTic)
        {
            if (this.IsOutside)
                canvas.Children.Add(new Line()
                {
                    X1 = pixVal,
                    Y1 = shift,
                    X2 = pixVal,
                    Y2 = shift + scaledTic,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                });
            // draw the cross tick
            if (_isCrossOutside)
                canvas.Children.Add(new Line()
                {
                    X1 = pixVal,
                    Y1 = 0,
                    X2 = pixVal,
                    Y2 = scaledTic,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                });
            // draw the inside tick
            if (this.IsInside)
                canvas.Children.Add(new Line()
                {
                    X1 = pixVal,
                    Y1 = shift,
                    X2 = pixVal,
                    Y2 = shift - scaledTic,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                });
            // draw the inside cross tick
            if (_isCrossInside)
                canvas.Children.Add(new Line()
                {
                    X1 = pixVal,
                    Y1 = 0,
                    X2 = pixVal,
                    Y2 = - scaledTic,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                });
            // draw the opposite tick
            if (this.IsOpposite)
                canvas.Children.Add(new Line()
                {
                    X1 = pixVal,
                    Y1 = topPix,
                    X2 = pixVal,
                    Y2 = topPix + scaledTic,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                });
        }

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}