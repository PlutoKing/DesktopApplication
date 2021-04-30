/*──────────────────────────────────────────────────────────────
 * FileName     : Pane.cs
 * Created      : 2021-04-29 15:53:19
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LF.Figure
{
    /// <summary>
    /// 窗格：基类
    /// </summary>
    public class Pane : UserControl,ICloneable
    {
        #region Fields
        protected RectangleF _area;

        protected TextBlock _title;

        protected bool _isFontScaled;         // 是否字体缩放
        protected bool _isLineWidthScaled;    // 是否线宽缩放
        protected float _baseDimension;       // 
        protected float _titleGap;            // 

        #endregion

        #region Properties
        public RectangleF Area { get => _area; set => _area = value; }
        public TextBlock Title { get => _title; set => _title = value; }
        public bool IsFontScaled { get => _isFontScaled; set => _isFontScaled = value; }
        public bool IsLineWidthScaled { get => _isLineWidthScaled; set => _isLineWidthScaled = value; }
        public float BaseDimension { get => _baseDimension; set => _baseDimension = value; }
        public float TitleGap { get => _titleGap; set => _titleGap = value; }

        #endregion

        #region Constructors
        public Pane(string title,RectangleF area)
        {
            _area = area;
            _title.Text = title;
        }

        public Pane()
           : this("", new RectangleF(0, 0, 0, 0))
        {
        }

        public Pane(Pane rhs)
        {
            _area = rhs.Area;

            _isFontScaled = rhs._isFontScaled;
            _isLineWidthScaled = rhs._isLineWidthScaled;
            _baseDimension = rhs._baseDimension;

            _titleGap = rhs._titleGap;

            _title = rhs._title;
        }

        object ICloneable.Clone()
        {
            throw new NotImplementedException("Can't clone an abstract base type -- child types must implement ICloneable");
        }

        public Pane ShallowClone()
        {
            // return a shallow copy
            return this.MemberwiseClone() as Pane;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 绘制窗格
        /// </summary>
        public virtual void Draw()
        {

        }
        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}