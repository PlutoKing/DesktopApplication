/*──────────────────────────────────────────────────────────────
 * FileName     : LFChart.cs
 * Created      : 2021-05-15 17:31:14
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

namespace LF.Figure
{
    /// <summary>
    /// 绘图区
    /// </summary>
    public class LFChart:UserControl
    {
        #region Fields
        Canvas canvas;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFChart()
        {
            canvas = new Canvas();
            Content = canvas;

            this.Background = Brushes.LightGray;
        }
        #endregion

        #region Methods

        public void Draw()
        {
            canvas.Children.Clear();
        }

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}