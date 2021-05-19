/*──────────────────────────────────────────────────────────────
 * FileName     : LFXAxis.cs
 * Created      : 2021-05-15 19:10:24
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
    /// <summary>
    /// X轴
    /// </summary>
    public class LFXAxis:LFAxis
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFXAxis()
        {
            _title.Text = "X Axis";
        }
        #endregion

        #region Methods

        /// <summary>
        /// 绘制坐标轴 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        public void Draw(Canvas canvas,double x,double y,double length)
        {
            /* 1. 绘制轴线 */
            canvas.Children.Add(new Line()
            {
                X1 = x,
                Y1 = y,
                X2 = x + length,
                Y2 = y + 0,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                StrokeStartLineCap = PenLineCap.Square,
                StrokeEndLineCap = PenLineCap.Square
            });
          
            /* 2. 绘制刻度线和网格线 */

            /* 3. 绘制标题 */
            _title.Draw(canvas, x + length/ 2, y);

        }

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}