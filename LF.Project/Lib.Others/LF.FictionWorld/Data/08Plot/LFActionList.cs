/*──────────────────────────────────────────────────────────────
 * FileName     : LFActionList.cs
 * Created      : 2021-05-30 12:35:38
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace LF.FictionWorld
{
    /// <summary>
    /// 动作列表，表示<see cref="LFAction"/>的列表
    /// </summary>
    public class LFActionList : ObservableCollection<LFAction>, ICloneable
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFActionList()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFActionList(LFActionList rhs)
        {
            foreach (LFAction obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFActionList Clone()
        {
            return new LFActionList(this);
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
    }
}