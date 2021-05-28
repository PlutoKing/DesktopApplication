/*──────────────────────────────────────────────────────────────
 * FileName     : LFSubSiteList.cs
 * Created      : 2021-05-28 10:16:59
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace LF.FictionWorld
{
    public class LFSubSiteList : ObservableCollection<LFSubSite>, ICloneable
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFSubSiteList()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="obj">源对象</param>
        public LFSubSiteList(LFSubSiteList obj)
        {
            foreach (LFSubSite subSite in obj)
            {
                Add(subSite.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFSubSiteList Clone()
        {
            return new LFSubSiteList(this);
        }
        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return Clone();
        }
        #endregion

        #region Methods

        #endregion
    }
}