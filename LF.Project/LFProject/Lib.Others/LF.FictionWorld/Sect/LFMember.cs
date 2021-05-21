/*──────────────────────────────────────────────────────────────
 * FileTitle     : LFMember
 * Created      : 2020-09-28 19:03:50
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
using System.ComponentModel;

namespace LF.FictionWorld
{
    /// <summary>
    /// 成员
    /// </summary>
    public class LFMember : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int _id;        // ID
        private string _title;  // 头衔
        private string _brief;  // 简介
        private int _level;     // 等级
        private int _count;     // 数量
        private LFMemberList _assistants = new LFMemberList();  // 副手
        #endregion

        #region Properties
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get => _id; set => _id = value; }
        /// <summary>
        /// 头衔
        /// </summary>
        public string Title { get => _title; set => _title = value; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Brief { get => _brief; set => _brief = value; }
        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get => _level; set => _level = value; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get => _count; set => _count = value; }
        /// <summary>
        /// 副手
        /// </summary>
        public LFMemberList Assistants { get => _assistants; set => _assistants = value; }
        #endregion

        #region Constructors
        public LFMember()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFMember(LFMember rhs)
        {
            this._id = rhs._id;
            this._title = rhs._title;
            this._brief = rhs._brief;

            this._level = rhs._level;
            this._count = rhs._count;
            this._assistants = rhs._assistants.Clone();
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFMember Clone()
        {
            return new LFMember(this);
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

        #region Events

        /// <summary>
        /// 属性改变事件
        /// </summary>
        /// <param name="info"></param>
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }
}
