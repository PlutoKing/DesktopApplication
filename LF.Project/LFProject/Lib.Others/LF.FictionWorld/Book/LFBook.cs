/*──────────────────────────────────────────────────────────────
 * FileName     : LFBook
 * Created      : 2020-09-28 18:12:59
 * Author       : Xu Zhe
 * Description  : 功法
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.ComponentModel;

namespace LF.FictionWorld
{
    /// <summary>
    /// 秘籍
    /// </summary>
    public class LFBook : INotifyPropertyChanged, ICloneable
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

        private LFType _level = new LFType();      // 等级

        private LFAttribute _attributes = new LFAttribute();    // 属性

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
        /// 属性
        /// </summary>
        public LFAttribute Attributes { get => _attributes; set => _attributes = value; }
        /// <summary>
        /// 等级
        /// </summary>
        public LFType Level { get => _level; set => _level = value; }
        /// <summary>
        /// 属性
        /// </summary>
        public string Attribute
        {
            get
            {
                return _attributes.ToString();
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LFBook()
        {
        }

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="name">名称</param>
        public LFBook(int index, string name)
        {
            _index = index;
            _name = name;

            Decode();
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFBook(LFBook rhs)
        {
            this._index = rhs._index;
            this._name = rhs._name;
            this._id = rhs._id;
            this._brief = rhs._brief;

            this._attributes = rhs._attributes.Clone();
            this._level = rhs._level;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFBook Clone()
        {
            return new LFBook(this);
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

        /// <summary>
        /// 解码
        /// </summary>
        public void Decode()
        {
            // 等级1位+属性4位+编号3位=8位
            int l = (int)(_index / 10000000);
            int a = (int)(_index % 10000000) / 1000;
            _level = World.Setting.Levels.GetType(l);
            _attributes = new LFAttribute(a);
            _id = (int)(_index % 1000);
        }

        /// <summary>
        /// 编码
        /// </summary>
        public void Encode()
        {
            int index = _level.Index * 10000;
            index += _attributes.Code;

            /* 重复性检测 */
            if (_id == 0)
            {
                _id = 1;
                foreach (LFBook obj in World.Data.BookList)
                {
                    if (obj.Index / 1000 == index)
                    {
                        _id++;
                    }
                }
            }

            _index = index * 1000 + _id;
        }

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
