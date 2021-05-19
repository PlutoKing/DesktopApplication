/*──────────────────────────────────────────────────────────────
 * FileName     : LFEvent
 * Created      : 2020-10-06 16:00:13
 * Author       : Xu Zhe
 * Description  : 事件。运行事件，更改部分属性。
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
    /// 事件
    /// </summary>
    public class LFEvent : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private long _index;        // 索引
        private string _name;       // 名称
        private int _id;            // ID
        private string _brief;      // 简介

        private LFDate _date = new LFDate();    // 日期
        #endregion

        #region Properties
        /// <summary>
        /// 索引
        /// </summary>
        public long Index { get => _index; set => _index = value; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get => _name; set => _name = value; }

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get => _id; set => _id = value; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Brief { get => _brief; set => _brief = value; }

        /// <summary>
        /// 日期
        /// </summary>
        public LFDate Date { get => _date; set => _date = value; }

        #endregion

        #region Constructors
        public LFEvent()
        {
        }

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="name">名称</param>
        public LFEvent(long index, string name)
        {
            _index = index;
            _name = name;

            Decode();
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFEvent(LFEvent rhs)
        {
            this._index = rhs._index;
            this._name = rhs._name;
            this._id = rhs._id;
            this._brief = rhs._brief;
            this._date = rhs._date.Clone();
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFEvent Clone()
        {
            return new LFEvent(this);
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
        #region Common

        /// <summary>
        /// 解码
        /// </summary>
        public void Decode()
        {
            int date = (int)(_index / 100);
            _date = new LFDate(date);

            _id = (int)(_index % 100);
        }

        /// <summary>
        /// 编码
        /// </summary>
        public void Encode()
        {
            long index = _date.Code;

            /* 重复性检测 */
            if (_id == 0)
            {
                _id = 1;
                foreach (LFRole role in World.Data.RoleList)
                {
                    if (role.Index / 100 == index)
                    {
                        _id++;
                    }
                }
            }

            _index = index * 100 + _id;
        }
        #endregion


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
