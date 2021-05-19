/*──────────────────────────────────────────────────────────────
 * FileName     : LFChartFigure.cs
 * Created      : 2021-05-15 17:31:26
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

namespace LF.Figure
{
    /// <summary>
    /// 图表控件
    /// </summary>
    public class LFChartFigure:UserControl
    {
        #region Fields

        private Canvas _canvas;
        private LFLabel _title;
        private double _titleGap;            // 

        private LFChart _chart;
        private LFXAxis _xAxis;
        private LFYAxis _yAxis;

        double titleHeight = 0;
        #endregion

        #region Properties

        /// <summary>
        /// 绘图区
        /// </summary>
        public LFChart Chart { get => _chart; set => _chart = value; }

        #endregion

        #region Constructors
        public LFChartFigure()
        {
            _canvas = new Canvas();
            //Padding = new Thickness(3);
            //this.ClipToBounds = true;
            this.Content = _canvas;

            _title = new LFLabel();
            _title.HorizontalContentAlignment = HorizontalAlignment.Center;
            _xAxis = new LFXAxis();
            _yAxis = new LFYAxis();
            _chart = new LFChart();

            SizeChanged += ChartFigure_SizeChanged;
        }



        #endregion

        #region Methods

        #region Rendering Methods

        /// <summary>
        /// 绘制
        /// </summary>
        public void Draw()
        {
            _canvas.Children.Clear();


            /* 绘制标题 */
            _title.Draw(_canvas, RenderSize.Width / 2, 0);
            titleHeight = _title.GetTextSize().Height;

            double x, y, width, height;
            GetChartArea(out x, out y, out width, out height);

            if (width < 1 || height < 1)
                return;

            

            _chart.Width = width;
            _chart.Height = height;
            Canvas.SetLeft(_chart, x);
            Canvas.SetTop(_chart, y + titleHeight);
            _canvas.Children.Add(_chart);

            /* 绘制坐标轴 */
            _xAxis.Draw(_canvas, x, height + titleHeight, width);
            _yAxis.Draw(_canvas, x, y + titleHeight, height);
        }

        #endregion

        #region Sizing Methods

        /// <summary>
        /// 计算绘图区尺寸
        /// </summary>
        private void GetChartArea(out double x, out double y,out double width,out double height)
        {
            x = 0;
            y = 0;
            width = RenderSize.Width;
            height = RenderSize.Height - titleHeight;

            double totSpaceY = 0;
            double totSpaceL = 0;
            double totSpaceR = 0;
            
            double minSpaceL = 0;
            double minSpaceR = 0;
            double minSpaceB = 0;
            double minSpaceT = 0;
            double spaceB = 0, spaceT = 0, spaceL = 0, spaceR = 0;

            minSpaceB = _xAxis.GetSpace();
            minSpaceL = _yAxis.GetSpace();

            totSpaceL = Math.Max(totSpaceL, minSpaceL);
            totSpaceR = Math.Max(totSpaceR, minSpaceR);
            spaceB = Math.Max(spaceB, minSpaceB);
            spaceT = Math.Max(spaceT, minSpaceT);

            x += totSpaceL;
            width -= totSpaceL + totSpaceR;
            height -= spaceT +spaceB;
            y += spaceT;
        }

        #endregion

        #endregion

        private void ChartFigure_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {

            Draw();
        }
    }
}