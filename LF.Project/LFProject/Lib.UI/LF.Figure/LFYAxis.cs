/*──────────────────────────────────────────────────────────────
 * FileName     : LFYAxis.cs
 * Created      : 2021-05-16 10:37:57
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
    public class LFYAxis:LFAxis
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFYAxis()
        {
            _title.Text = "Y Axis";
        }
        #endregion

        #region Methods
        public void SetTransformMatrix(Canvas canvas)
        {
            TransformGroup tfGroup = new TransformGroup();
            RotateTransform rt = new RotateTransform();
            rt.Angle = 90;
            rt.CenterX = 0;
            rt.CenterY = 0;
            tfGroup.Children.Add(rt);

            canvas.RenderTransform = tfGroup;
        }

        public void Draw(Canvas canvas,double x,double y,double length)
        {
            canvas.Children.Add(new Line()
            {
                X1 = x,
                Y1 = y,
                X2 = x + 0,
                Y2 = y + length,
                Stroke = Brushes.Red,
                StrokeThickness = 2,
                StrokeStartLineCap = PenLineCap.Square,
                StrokeEndLineCap = PenLineCap.Square
            });

           

            //TransformGroup tfGroup = new TransformGroup();
            //RotateTransform rt = new RotateTransform();
            //rt.Angle = 90;
            //rt.CenterX = x;
            //rt.CenterY = y + length / 2;
            //tfGroup.Children.Add(rt);

            //canvas.RenderTransform = tfGroup;
            _title.Draw(canvas, x-_title.GetTextSize().Height, y + length / 2,-90);

            //TransformGroup tfGroup2 = new TransformGroup();
            //RotateTransform rt2 = new RotateTransform();
            //rt2.Angle = -90;
            //rt2.CenterX = x;
            //rt2.CenterY = y + length / 2;
            //tfGroup2.Children.Add(rt2);
            //canvas.RenderTransform = tfGroup2;
        }
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}