/*──────────────────────────────────────────────────────────────
 * FileName     : LFCalendarInfoList.cs
 * Created      : 2021-05-25 12:01:30
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
    public class LFCalendarInfoList : ObservableCollection<LFCalendarInfo>, ICloneable
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LFCalendarInfoList()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="obj"></param>
        public LFCalendarInfoList(LFCalendarInfoList obj)
        {
            foreach (LFCalendarInfo info in obj)
            {
                Add(info.Clone());
            }
        }
        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFCalendarInfoList Clone()
        {
            return new LFCalendarInfoList(this);
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

        /// <summary>
        /// 获取ID
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public int GetID(string name)
        {
            foreach(LFCalendarInfo info in this)
            {
                if (info.Name == name)
                    return info.ID;
            }
            return -1;
        }

        #endregion

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}