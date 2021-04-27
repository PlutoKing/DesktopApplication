/*──────────────────────────────────────────────────────────────
 * FileName     : Axis.cs
 * Created      : 2021-04-27 20:24:26
 * Author       : Xu Zhe
 * Description  : 轴的抽象类
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LF.Figure
{
    /// <summary>
    /// 轴的抽象类
    /// </summary>
    public class Axis:UserControl
    {
        #region Fields
        Canvas root;
        #endregion

        #region Properties



        [Category("LF"), Description("刻度")]
        public Scale Scale
        {
            get { return (Scale)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Scale.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(Scale), typeof(Axis), new PropertyMetadata(default(Scale), OnPropertyChanged));




        [Category("LF"), Description("是否显示轴线段")]
        public bool IsAxisSegmentVisible
        {
            get { return (bool)GetValue(IsAxisSegmentVisibleProperty); }
            set { SetValue(IsAxisSegmentVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAxisSegmentVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAxisSegmentVisibleProperty =
            DependencyProperty.Register("IsAxisSegmentVisible", typeof(bool), typeof(Axis), new PropertyMetadata(default(bool),OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)=> (d as Axis)?.Refresh();


        #endregion

        #region Constructors
        public Axis()
        {
            root = new Canvas();
            this.Content = root;

            Scale = new Scale();
            SizeChanged += (s, e) =>
            {
                (s as Axis)?.Refresh();
            };
        }
        #endregion

        #region Methods

        public double Normalize(double v) => (v - Scale.Min) / (Scale.Max - Scale.Min) * RenderSize.Width;

        /// <summary>
        /// 刷新重绘
        /// </summary>
        public void Refresh()
        {
            root.Children.Clear();
            if (RenderSize.Width <= 0) return;

            double v = Scale.Min;
            double x;
            do
            {
                x = Normalize(v);
                root.Children.Add(new Line
                {
                    X1 = x,
                    X2 = x,
                    Y1 = 0,
                    Y2 = -10,
                    Stroke = Brushes.Chocolate,
                    StrokeThickness = 2,
                });
                v += Scale.MajorStep;
            } while (v <= Scale.Max);
        }

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}