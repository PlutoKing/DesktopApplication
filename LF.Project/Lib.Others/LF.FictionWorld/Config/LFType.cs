/*──────────────────────────────────────────────────────────────
 * FileName     : LFType.cs
 * Created      : 2021-05-25 15:46:33
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.ComponentModel;

namespace LF.FictionWorld
{
    /// <summary>
    /// 分类方法
    /// </summary>
    public class LFType : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int _index = 0;         // 索引号
        private string _value;          // 值
        private string _brief;          // 简介
        private LFType _parent;         // 父节点
        private LFTypeList _childs;     // 子分类

        #endregion

        #region Properties
        /// <summary>
        /// 索引号
        /// </summary>
        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Index"));
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }

        /// <summary>
        /// 简介
        /// </summary>
        public string Brief
        {
            get => _brief;
            set
            {
                _brief = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Brief"));
            }
        }
        /// <summary>
        /// 父分类
        /// </summary>
        public LFType Parent
        {
            get => _parent; set
            {
                _parent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }
        /// <summary>
        /// 子分类
        /// </summary>
        public LFTypeList Childs
        {
            get => _childs; set
            {
                _childs = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Childs"));
            }
        }
        #endregion

        #region Constructors
        public LFType()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="obj">源对象</param>
        public LFType(LFType obj)
        {
            _index = obj._index;
            _value = obj._value;
            _brief = obj._brief;
            _parent = obj._parent;
            if (obj._childs != null)
            {
                _childs = new LFTypeList();
                _childs = _childs.Clone();
            }

        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFType Clone()
        {
            return new LFType(this);
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



        #endregion
    }
}