/*──────────────────────────────────────────────────────────────
 * FileName     : LFLevelList.cs
 * Created      : 2021-05-27 20:24:54
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace LF.FictionWorld
{
    /// <summary>
    /// 修炼内容，是<seealso cref="LFLevel"/>的列表。
    /// </summary>
    public class LFLevelList : ObservableCollection<LFLevel>, ICloneable
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFLevelList()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFLevelList(LFLevelList rhs)
        {
            foreach (LFLevel obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该秘籍列表相同的新实例</returns>
        public LFLevelList Clone()
        {
            return new LFLevelList(this);
        }
        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该秘籍列表相同的新实例</returns>
        object ICloneable.Clone()
        {
            return Clone();
        }
        #endregion

        #region Methods

        #endregion
    }
}