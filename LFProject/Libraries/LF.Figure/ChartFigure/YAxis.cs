/*──────────────────────────────────────────────────────────────
 * FileName     : YAxis.cs
 * Created      : 2021-04-29 15:11:07
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

namespace LF.Figure
{
    /// <summary>
    /// Y轴
    /// </summary>
    public class YAxis:Axis
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public YAxis()
        {
        }

        #endregion

        #region Methods
        public override double Normalize(double v) => (v - Scale.Min) / (Scale.Max - Scale.Min) * RenderSize.Height;

        public override void Refresh()
        {
            root.Children.Clear();
            if (RenderSize.Height <= 0) return;

            double v = Scale.Min;
            double x;
            root.Children.Add(new Line
            {
                X1 = 0,
                X2 = 0,
                Y1 = 0,
                Y2 = RenderSize.Height,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            });
            do
            {
                x = Normalize(v);
                root.Children.Add(new Line
                {
                    X1 = 0,
                    X2 = 10,
                    Y1 = x,
                    Y2 = x,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
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