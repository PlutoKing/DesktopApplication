/*──────────────────────────────────────────────────────────────
 * FileName     : LFMajorTick.cs
 * Created      : 2021-05-15 19:42:37
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Figure
{
    public class LFMajorTick:LFMinorTick
    {
        #region Fields
        private bool _isBetweenLabels;

        #endregion

        #region Properties
        public bool IsBetweenLabels { get => _isBetweenLabels; set => _isBetweenLabels = value; }

        #endregion

        #region Constructors
        public LFMajorTick()
        {
        }

        #endregion

        #region Methods

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}