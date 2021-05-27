/*──────────────────────────────────────────────────────────────
 * FileName     : LFSkillList.cs
 * Created      : 2021-05-27 16:55:20
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.FictionWorld
{
    /// <summary>
    /// 技能列表
    /// </summary>
    public class LFSkillList : ObservableCollection<LFSkill>, ICloneable
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFSkillList()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFSkillList(LFSkillList rhs)
        {
            foreach (LFSkill obj in rhs)
            {
                this.Add(obj.Clone());
            }
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该技能列表相同的新实例</returns>
        public LFSkillList Clone()
        {
            return new LFSkillList(this);
        }
        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该技能列表相同的新实例</returns>
        object ICloneable.Clone()
        {
            return Clone();
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