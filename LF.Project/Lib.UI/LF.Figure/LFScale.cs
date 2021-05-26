/*──────────────────────────────────────────────────────────────
 * FileName     : LFScale.cs
 * Created      : 2021-05-15 19:33:22
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
using System.Globalization;

namespace LF.Figure
{
    /// <summary>
    /// 比例刻度
    /// </summary>
    public class LFScale:Control
    {
        #region Fields
        private LFAxis _ownerAxis;

        private double _min = 0;
        private double _max = 5;
        private double _minorStep = 0.1;
        private double _majorStep = 0.5;
        private double _exponent;
        private double _baseTick;

        private double _minPix;
        private double _maxPix;
        private double _minLinTemp;
        private double _maxLinTemp;


        #endregion

        #region Properties

        /// <summary>
        /// 所属坐标轴
        /// </summary>
        public LFAxis OwnerAxis { get => _ownerAxis; set => _ownerAxis = value; }
        /// <summary>
        /// 最小值
        /// </summary>
        public double Min { get => _min; set => _min = value; }
        /// <summary>
        /// 最大值
        /// </summary>
        public double Max { get => _max; set => _max = value; }
        /// <summary>
        /// 副步长
        /// </summary>
        public double MinorStep { get => _minorStep; set => _minorStep = value; }
        /// <summary>
        /// 主步长
        /// </summary>
        public double MajorStep { get => _majorStep; set => _majorStep = value; }
        public double Exponent { get => _exponent; set => _exponent = value; }
        public double BaseTick { get => _baseTick; set => _baseTick = value; }

        #endregion

        #region Constructors
        public LFScale()
        {
        }
        #endregion

        #region Methods

        #region Sizing Methods

        /// <summary>
        /// 设置位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public void SetScaleData(double x,double y,double w,double h)
        {
            if(_ownerAxis is LFXAxis)
            {
                _minPix = x;
                _maxPix = x + w;
            }
            else
            {
                _minPix = y;
                _maxPix = y + h;
            }
        }

        /// <summary>
        /// 计算空间大小
        /// </summary>
        /// <returns></returns>
        public Size GetScaleMaxSpace()
        {
            double dVal;
            int nTicks = GetTickCounts();
            double startVal = GetBaseTick();

            Size maxSpace = new Size(0, 0);

            for(int i = 0; i < nTicks; i++)
            {
                dVal = GetMajorTickValue(startVal, i);

                LFLabel tmpLabel = new LFLabel();
                tmpLabel.Text = dVal.ToString();

                Size size = tmpLabel.GetTextSize();

                if (size.Height > maxSpace.Height)
                    maxSpace.Height = size.Height;
                if (size.Width > maxSpace.Width)
                    maxSpace.Width = size.Width;
            }

            return maxSpace;
        }


        #endregion

        #region Calculating Methods

        /// <summary>
        /// 计算刻度值
        /// </summary>
        /// <param name="baseVal"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        public double GetMajorTickValue(double baseVal,int tick)
        {
            return baseVal + _majorStep * tick;
        }

        /// <summary>
        /// 计算刻度线数目
        /// </summary>
        /// <returns></returns>
        public int GetTickCounts()
        {
            int n = 1;

            n = (int)((_max - _min) / _majorStep + 0.01) + 1;

            if (n < 1)
                n = 1;
            else if (n > 1000)
                n = 1000;

            return n;

        }

        /// <summary>
        /// 计算基础刻度
        /// </summary>
        /// <returns></returns>
        public double GetBaseTick()
        {
            if (_baseTick != double.MaxValue)
                return _baseTick;
            else
                return Math.Ceiling(_min / _majorStep - 0.00000001) * _majorStep;
        }


        #endregion

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}