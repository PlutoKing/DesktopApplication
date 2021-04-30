/*──────────────────────────────────────────────────────────────
 * FileName     : XAxis.cs
 * Created      : 2021-04-29 15:08:52
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace LF.Figure
{
    /// <summary>
    /// X轴
    /// </summary>
    public class XAxis:Axis
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public XAxis()
        {
        }
        #endregion

        #region Methods

        public override double Normalize(double v) => (v - Scale.Min) / (Scale.Max - Scale.Min) * RenderSize.Width;


        /// <summary>
        /// 刷新重绘
        /// </summary>
        public override void Refresh()
        {
            root.Children.Clear();
            if (RenderSize.Width <= 0) return;

            double v = Scale.Min;
            double x;
            root.Children.Add(new Line
            {
                X1 = 0,
                X2 = RenderSize.Width,
                Y1 = 0,
                Y2 = 0,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            });
            do
            {
                x = Normalize(v);
                root.Children.Add(new Line
                {
                    X1 = x,
                    X2 = x,
                    Y1 = 0,
                    Y2 = -10,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                });

                TextBlock textBlock = new TextBlock
                {
                    Text = v.ToString("0"),

                };

                Canvas.SetLeft(textBlock, x - textBlock.RenderSize.Width/2);
                Canvas.SetTop(textBlock, 0);

                root.Children.Add(textBlock);
                
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