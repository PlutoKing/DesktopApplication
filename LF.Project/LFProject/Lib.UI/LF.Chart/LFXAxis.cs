/*──────────────────────────────────────────────────────────────
 * FileName     : LFXAxis.cs
 * Created      : 2021-05-15 17:03:47
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

namespace LF.Chart
{
    public class LFXAxis:UserControl
    {
        #region Fields
        private Canvas _canvas;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFXAxis()
        {
            _canvas = new Canvas();
            this.Content = _canvas;

            SizeChanged += LFXAxis_SizeChanged;
        }


        #endregion

        #region Methods

        public double Normalize(double v) => (v - 0) / (100 - 0) * RenderSize.Width;

        private void Draw()
        {
            _canvas.Children.Clear();
            _canvas.Height = 12;
            double v = 0;
            double x;
            do
            {
                x = Normalize(v);
                _canvas.Children.Add(new Line
                {
                    X1 = x,
                    X2 = x,
                    Y1 = 0,
                    Y2 = -10,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                });

                ContentPresenter tmp = new ContentPresenter();
                //tmp.Width = RenderSize.Width / 10;
                tmp.Content = v.ToString();
                tmp.HorizontalAlignment = HorizontalAlignment.Center;
                //tmp.HorizontalContentAlignment = HorizontalAlignment.Center;
                Canvas.SetLeft(tmp, x );
                Canvas.SetTop(tmp, 0);
                _canvas.Children.Add(tmp);

                v += 10;
            } while (v <= 100);

        }

        #endregion

        #region Events
        private void LFXAxis_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Draw();
        }

        #endregion

        #region Defaults
        #endregion
    }
}