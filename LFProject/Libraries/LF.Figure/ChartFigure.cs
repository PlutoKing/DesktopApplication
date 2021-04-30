/*──────────────────────────────────────────────────────────────
 * FileName     : ChartFigure.cs
 * Created      : 2021-04-29 15:40:20
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LF.Figure
{
    public class ChartFigure:UserControl
    {
        #region Fields
        protected Canvas root = new Canvas();

        XAxis xAxis = new XAxis();
        YAxis yAxis = new YAxis();
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public ChartFigure()
        {
            this.Content = root;

            this.Loaded += ChartFigure_Loaded;
            this.SizeChanged += ChartFigure_SizeChanged;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 绘图
        /// </summary>
        public void Draw()
        {
            root.Children.Clear();
            if (RenderSize.Width <= 0) return;

            xAxis.Width = 100;
            root.Children.Add(xAxis);

            Canvas.SetLeft(xAxis, 10);
            Canvas.SetTop(xAxis, 10);
            root.Children.Add(yAxis);

        }

        #endregion

        private void ChartFigure_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            Draw();
        }

        private void ChartFigure_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Draw();
        }

    }
}