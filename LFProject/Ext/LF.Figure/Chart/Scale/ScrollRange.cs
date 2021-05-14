﻿/*──────────────────────────────────────────────────────────────
 * FileName     : ScrollRange
 * Created      : 2019-12-21 14:52:11
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LF.Figure
{
    public struct ScrollRange
    {
        private bool _isScrollable;
        private double _min;
        private double _max;

        /// <summary>
        /// Construct a <see cref="ScrollRange" /> object given the specified data values.
        /// </summary>
        /// <param name="min">The minimum axis value limit for the scroll bar</param>
        /// <param name="max">The maximum axis value limit for the scroll bar</param>
        /// <param name="isScrollable">true to make this item scrollable, false otherwise</param>
        public ScrollRange(double min, double max, bool isScrollable)
        {
            _min = min;
            _max = max;
            _isScrollable = isScrollable;
        }

        /// <summary>
        /// Sets the scroll range to default values of zero, and sets the <see cref="IsScrollable" />
        /// property as specified.
        /// </summary>
        /// <param name="isScrollable">true to make this item scrollable, false otherwise</param>
        public ScrollRange(bool isScrollable)
        {
            _min = 0.0;
            _max = 0.0;
            _isScrollable = isScrollable;
        }

        /// <summary>
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">The <see cref="ScrollRange"/> object from which to copy</param>
        public ScrollRange(ScrollRange rhs)
        {
            _min = rhs._min;
            _max = rhs._max;
            _isScrollable = rhs._isScrollable;
        }

        /// <summary>
        /// Gets or sets a property that determines if the <see cref="Axis" /> corresponding to
        /// this <see cref="ScrollRange" /> object can be scrolled.
        /// </summary>
        public bool IsScrollable
        {
            get { return _isScrollable; }
            set { _isScrollable = value; }
        }

        /// <summary>
        /// The minimum axis value limit for the scroll bar.
        /// </summary>
        public double Min
        {
            get { return _min; }
            set { _min = value; }
        }
        /// <summary>
        /// The maximum axis value limit for the scroll bar.
        /// </summary>
        public double Max
        {
            get { return _max; }
            set { _max = value; }
        }
    }
}
