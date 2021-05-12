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
    public abstract class Axis:UserControl
    {
        #region Fields
        /// <summary>
        /// 绘图
        /// </summary>
        protected Canvas root;

        public TextBlock font;
        #endregion

        #region Properties



        /// <summary>
        /// 刻度
        /// </summary>
        [Category("LF"), Description("刻度")]
        public Scale Scale
        {
            get { return (Scale)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Scale.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(Scale), typeof(Axis), new PropertyMetadata(default(Scale), OnPropertyChanged));



        /// <summary>
        /// 副刻度线样式
        /// </summary>
        public MinorTick MinorTick
        {
            get { return (MinorTick)GetValue(MinorTickProperty); }
            set { SetValue(MinorTickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinorTick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinorTickProperty =
            DependencyProperty.Register("MinorTick", typeof(MinorTick), typeof(Axis), new PropertyMetadata(default(MinorTick), OnPropertyChanged));


        /// <summary>
        /// 主刻度线样式
        /// </summary>
        public MajorTick MajorTick
        {
            get { return (MajorTick)GetValue(MajorTickProperty); }
            set { SetValue(MajorTickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MajorTick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MajorTickProperty =
            DependencyProperty.Register("MajorTick", typeof(MajorTick), typeof(Axis), new PropertyMetadata(default(MajorTick), OnPropertyChanged));


        /// <summary>
        /// 副网格线样式
        /// </summary>
        public MinorGrid MinorGrid
        {
            get { return (MinorGrid)GetValue(MinorGridProperty); }
            set { SetValue(MinorGridProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinorGrid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinorGridProperty =
            DependencyProperty.Register("MinorGrid", typeof(MinorGrid), typeof(Axis), new PropertyMetadata(default(MinorGrid), OnPropertyChanged));



        /// <summary>
        /// 主网格线样式
        /// </summary>
        public MajorGrid MajorGrid
        {
            get { return (MajorGrid)GetValue(MajorGridProperty); }
            set { SetValue(MajorGridProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MajorGrid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MajorGridProperty =
            DependencyProperty.Register("MajorGrid", typeof(MajorGrid), typeof(Axis), new PropertyMetadata(default(MajorGrid), OnPropertyChanged));





        /// <summary>
        /// 是否显示轴线段
        /// </summary>
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

        public abstract double Normalize(double v);

        /// <summary>
        /// 刷新重绘
        /// </summary>
        public abstract void Refresh();

        public void Draw()
        {
            
        }


        /// <summary>
        /// 绘制副刻度线
        /// </summary>
        /// <param name="pixVal"></param>
        /// <param name="topPix"></param>
        /// <param name="shift"></param>
        /// <param name="scaledTic"></param>
        public void DrawMinorTick(float pixVal, float topPix,
                    float shift, float scaledTic)
        {
            if (MinorTick.IsOutside)
            {
                root.Children.Add(new Line
                {
                    X1 = pixVal,
                    Y1 = 0.0d,
                    X2 = pixVal,
                    Y2 = shift + scaledTic,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                });
            }
            if (MinorTick.IsCrossOutside)
            {
                root.Children.Add(new Line
                {
                    X1 = pixVal,
                    Y1 = shift,
                    X2 = pixVal,
                    Y2 = -scaledTic,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                });
            }
            if (MinorTick.IsInside)
            {
                root.Children.Add(new Line
                {
                    X1 = pixVal,
                    Y1 = shift,
                    X2 = pixVal,
                    Y2 = shift + scaledTic,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                });
            }
            if(MinorTick.IsCrossInside)
            {
                root.Children.Add(new Line
                {
                    X1 = pixVal,
                    Y1 = 0.0d,
                    X2 = pixVal,
                    Y2 = -scaledTic,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                });
            }
            if (MinorTick.IsOpposite)
            {
                root.Children.Add(new Line
                {
                    X1 = pixVal,
                    Y1 = topPix,
                    X2 = pixVal,
                    Y2 = topPix + scaledTic,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                });
            }
        }

        /// <summary>
        /// 绘制副网格线
        /// </summary>
        /// <param name="pixVal"></param>
        /// <param name="topPix"></param>
        public void DrawMinorGrid(float pixVal, float topPix)
        {
            root.Children.Add(new Line
            {
                X1 = pixVal,
                Y1 = 0.0d,
                X2 = pixVal,
                Y2 = topPix,
                Stroke = Brushes.Gray,
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection() { 2, 3 },
        });
        }
        #region Drawing Methods 绘图计算方法

        

        #endregion

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}