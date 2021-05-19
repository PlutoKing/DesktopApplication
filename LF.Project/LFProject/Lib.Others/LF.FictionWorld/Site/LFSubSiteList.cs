/*──────────────────────────────────────────────────────────────
 * FileName     : LFSubSiteList
 * Created      : 2020-09-28 11:16:36
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
using System.Collections.ObjectModel;

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
        /// <param name="rhs"></param>
        public LFSubSiteList(LFSubSiteList rhs)
        {
            foreach (LFSubSite obj in rhs)
            {
                this.Add(obj.Clone());
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
            return this.Clone();
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
