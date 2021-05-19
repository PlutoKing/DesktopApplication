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
    public class LFDepartment : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        private int _id;
        private string _title;
        private string _brief;
        private int _level;

        private LFMember _leader;

        private LFMemberList _members = new LFMemberList();
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
        /// <param name="rhs"></param>
        public LFDepartment(LFDepartment rhs)
        {
            this._id = rhs._id;
            this._title = rhs._title;
            this._brief = rhs._brief;
            this._level = rhs._level;

            this._leader = rhs._leader;
            this._members = rhs._members.Clone();
            this._departments = rhs._departments.Clone();
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
