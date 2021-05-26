/*──────────────────────────────────────────────────────────────
 * FileName     : ColorConverter
 * Created      : 2020/10/2 15:16:32
 * Author       : Xu Zhe
 * Description  : 颜色转换类
 *                将不同类型的动作设置成不同的背景
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;

namespace LF.Controls
{
    /// <summary>
    /// 颜色转换类
    /// </summary>
    public class ColorConverter : IValueConverter
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ColorConverter()
        {
        }
        #endregion

        #region Methods
        public object Convert(object value, Type typeTarget, object param, CultureInfo culture)
        {
            int i = (int)value;
            SolidColorBrush brush = new SolidColorBrush();
            switch (i)
            {
                case 0:
                    brush.Color = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 1:
                    brush.Color = Color.FromArgb(128, 255, 169, 46);
                    break;
                case 2:
                    brush.Color = Color.FromArgb(128, 118, 126, 145);
                    break;
                case 3:
                    brush.Color = Color.FromArgb(128, 86, 115, 255);
                    break;
                case 4:
                    brush.Color = Color.FromArgb(128, 5, 202, 174);
                    break;
                default:
                    brush.Color = Color.FromArgb(255, 240, 240, 240);
                    break;
            }
            return brush;
        }

        public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture)
        {
            Brush color = (Brush)value;
            if (color == Brushes.Red)
            {
                return 0;
            }
            else if (color == Brushes.Orange)
            {
                return 1;
            }
            else if (color == Brushes.Yellow)
            {
                return 2;
            }
            else if (color == Brushes.Green)
            {
                return 3;
            }
            else if (color == Brushes.Blue)
            {
                return 4;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}
