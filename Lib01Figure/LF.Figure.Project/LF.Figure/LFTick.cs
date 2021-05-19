/*──────────────────────────────────────────────────────────────
 * FileName     : LFTick.cs
 * Created      : 2021-05-14 18:57:40
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
using System.Windows.Shapes;

namespace LF.Figure
{
    public class LFTick : UserControl
    {
        #region Fields
        Canvas root;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFTick()
        {
            root = new Canvas();
            this.Content = root;
            SizeChanged += OnSizeChanged;
        }

        #endregion

        #region Methods
        public double Normalize(double v) => (v - 0) / (100 - 0) * RenderSize.Width;

        private void Draw()
        {
            root.Children.Clear();
            root.Height = 12;
            double v = 0;
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
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                });

                LFLabel tmp = new LFLabel();
                tmp.Width = RenderSize.Width/10;
                tmp.Text = v.ToString();
                tmp.HorizontalContentAlignment = HorizontalAlignment.Center;
                Canvas.SetLeft(tmp, x - tmp.Width / 2);
                Canvas.SetTop(tmp, 0);
                root.Children.Add(tmp);

                v += 10;
            } while (v <= 100);

        }
        #endregion

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Draw();
        }
    }
}