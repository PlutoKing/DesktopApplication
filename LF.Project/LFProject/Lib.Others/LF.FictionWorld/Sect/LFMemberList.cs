/*──────────────────────────────────────────────────────────────
 * FileName     : LFMemberList
 * Created      : 2020-09-28 19:03:58
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.ObjectModel;

namespace LF.FictionWorld
{
    public class LFMemberList : ObservableCollection<LFMember>, ICloneable
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public LFMemberList()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFMemberList(LFMemberList rhs)
        {
            foreach (LFMember obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFMemberList Clone()
        {
            return new LFMemberList(this);
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
