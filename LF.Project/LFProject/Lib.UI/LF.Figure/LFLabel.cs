/*──────────────────────────────────────────────────────────────
 * FileName     : LFLabel.cs
 * Created      : 2021-05-15 17:39:23
 * Author       : Xu Zhe
 * Description  : 标签
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Globalization;
using System.ComponentModel;

namespace LF.Figure
{
    /// <summary>
    /// 标签
    /// </summary>
    public class LFLabel:Control
    {
        #region Fields
        private string _text;
        #endregion

        #region Properties

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get => _text; set => _text = value; }


        #endregion

        #region Constructors
        public LFLabel()
        {
            _text = "XXXXX";
        }

        #endregion

        #region Methods

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Draw(Canvas canvas,double x, double y)
        {
            Label tmp = new Label();
            tmp.Content = _text;
            tmp.Padding = new Thickness(0);


            double w = GetTextSize().Width;

            if(HorizontalContentAlignment == HorizontalAlignment.Right)
            {
                Canvas.SetLeft(tmp, x-w);
            }
            else if (this.HorizontalContentAlignment == HorizontalAlignment.Center)
            {
                Canvas.SetLeft(tmp, x - w / 2);
            }
            else
            {
                Canvas.SetLeft(tmp, x);
            }
            Canvas.SetTop(tmp, y);

            

            canvas.Children.Add(tmp);
        }

        /// <summary>
        /// 绘制Label
        /// </summary>
        /// <param name="canvas">画布</param>
        /// <param name="x">位置</param>
        /// <param name="y">位置</param>
        /// <param name="angle">角度</param>
        public void Draw(Canvas canvas, double x, double y,double angle)
        {
            Label tmp = new Label();
            tmp.Content = _text;
            tmp.Padding = new Thickness(0);

            double w = GetTextSize().Width;
            double h = GetTextSize().Height;

            TransformGroup tfGroup = new TransformGroup();
            RotateTransform rt = new RotateTransform();
            rt.Angle = angle;
            rt.CenterX = w / 2;
            rt.CenterY = 0;
            tfGroup.Children.Add(rt);
            tmp.RenderTransform = tfGroup;


            if (HorizontalContentAlignment == HorizontalAlignment.Right)
            {
                Canvas.SetLeft(tmp, x - w);
            }
            else if (this.HorizontalContentAlignment == HorizontalAlignment.Center)
            {
                Canvas.SetLeft(tmp, x - w / 2);
            }
            else
            {
                Canvas.SetLeft(tmp, x);
            }
            Canvas.SetTop(tmp, y);

            
            canvas.Children.Add(tmp);
        }

        /// <summary>
        /// 计算Label尺寸
        /// </summary>
        /// <returns></returns>
        public Size GetTextSize()
        {
            var formattedText = new FormattedText(
                      _text,
                      CultureInfo.CurrentUICulture,
                      FlowDirection.LeftToRight,
                      new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                      FontSize,
                      Brushes.Black
                      );
            return new Size(formattedText.Width, formattedText.Height);

        }


        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}