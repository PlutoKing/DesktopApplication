/*──────────────────────────────────────────────────────────────
 * FileName     : HUD.cs
 * Created      : 2021-06-17 11:39:45
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LF.GCS.Project.Controls
{
    /// <summary>
    /// HUD.xaml 的交互逻辑
    /// </summary>
    public partial class HUD : UserControl
    {
        #region Properties


        /// <summary>
        /// 滚转角
        /// </summary>
        public double Roll
        {
            get { return (double)GetValue(RollProperty); }
            set { SetValue(RollProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Roll.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RollProperty =
            DependencyProperty.Register("Roll", typeof(double), typeof(HUD), new PropertyMetadata(0.0d, OnPropertyChanged));



        #endregion

        #region Constructors
        public HUD()
        {
            InitializeComponent();

            Loaded += HUD_Loaded;
            SizeChanged += HUD_SizeChanged;
        }

        private void HUD_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Draw();
        }


        #endregion

        #region Methods

        /// <summary>
        /// 绘制HUD
        /// </summary>
        public void Draw()
        {
            root.Children.Clear();

            int fontOffset = (int)(RenderSize.Height / 20);
            int borderOffset = 10;
            int r = (int)(RenderSize.Height / 2 - 2 * fontOffset - borderOffset);

            root.Children.Add(new Line()
            {
                X1 = 0,
                X2 = RenderSize.Width,
                Y1 = RenderSize.Height/2,
                Y2 = RenderSize.Height / 2,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            });
            int angle = 60;
            for (int a = -180; a <= 180; a += 5)
            {
                if (a >= Roll - angle && a <= Roll + angle)
                {
                    TransformGroup tg = root.RenderTransform as TransformGroup;
                    if (tg == null)
                        tg = new TransformGroup();
                    tg.Children.Add(new RotateTransform(a - Roll, RenderSize.Width / 2, RenderSize.Height / 2));

                    TransformGroup textTrans = new TransformGroup();
                    textTrans.Children.Add(new TranslateTransform(RenderSize.Width / 2, RenderSize.Height / 2));
                    textTrans.Children.Add(new RotateTransform(a - Roll));

                    // 平移到中心位置
                    if (a % 30 == 0)
                    {
                        root.Children.Add(new Line
                        {
                            X1 = 0+ RenderSize.Width / 2,
                            Y1 = r+ RenderSize.Height / 2,
                            X2 = 0+ RenderSize.Width / 2,
                            Y2 = r + fontOffset+ RenderSize.Height / 2,
                            Stroke = Brushes.Black,
                            StrokeThickness = 1,
                            RenderTransform = tg,
                        });

                        TextBox text = new TextBox();
                        text.Text = (Math.Abs(a)).ToString();
                        text.Width = fontOffset * 2;
                        text.Height = fontOffset;
                        text.HorizontalContentAlignment = HorizontalAlignment.Center;
                        Canvas.SetLeft(text, -fontOffset);
                        Canvas.SetTop(text, r + fontOffset);
                        text.RenderTransform = textTrans;
                        root.Children.Add(text);
                    }
                    else
                    {
                        root.Children.Add(new Line
                        {
                            X1 = 0 + RenderSize.Width / 2,
                            Y1 = r + RenderSize.Height / 2,
                            X2 = 0 + RenderSize.Width / 2,
                            Y2 = r + fontOffset/2 + RenderSize.Height / 2,
                            Stroke = Brushes.Black,
                            StrokeThickness = 1,
                            RenderTransform = tg,
                        });
                    }
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// 属性变化事件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void HUD_Loaded(object sender, RoutedEventArgs e)
        {
            Draw();
        }

        #endregion
    }
}
