/*──────────────────────────────────────────────────────────────
 * FileTitle     : LFDepartment
 * Created      : 2020-09-28 19:03:13
 * Author       : Xu Zhe
 * Description  : 部门
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
using System.Xml;

namespace LF.FictionWorld
{
    /// <summary>
    /// 部门：组织结构的单元
    /// </summary>
    public class LFDepartment : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int _id;            // ID
        private string _title;      // 名称
        private string _brief;      // 简介
        private int _level;         // 等级

        // 长官
        private LFMember _leader = new LFMember();
        // 副长官
        private LFMemberList _members = new LFMemberList();
        // 成员
        private LFDepartmentList _departments = new LFDepartmentList();

        #endregion

        #region Properties
        public int ID { get => _id; set => _id = value; }
        public string Title { get => _title; set => _title = value; }
        public string Brief { get => _brief; set => _brief = value; }
        public int Level { get => _level; set => _level = value; }
        public LFMember Leader { get => _leader; set => _leader = value; }
        public LFMemberList Members { get => _members; set => _members = value; }
        public LFDepartmentList Departments { get => _departments; set => _departments = value; }
        #endregion

        #region Constructors
        public LFDepartment()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="obj"></param>
        public LFDepartment(LFDepartment obj)
        {
            _id = obj._id;
            _title = obj._title;
            _brief = obj._brief;
            _level = obj._level;

            _leader = obj._leader;
            _members = obj._members.Clone();
            _departments = obj._departments.Clone();
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFDepartment Clone()
        {
            return new LFDepartment(this);
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
        #endregion
    }
}
