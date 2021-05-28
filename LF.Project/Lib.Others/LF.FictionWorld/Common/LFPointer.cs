/*──────────────────────────────────────────────────────────────
 * FileName     : LFPointer.cs
 * Created      : 2021-05-28 15:40:01
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
    /// 指针，用于指向被链接的对象
    /// </summary>
    public class LFPointer : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private long _code;     // 编码
        private string _name;   // 名称

        private string _value;  // 值

        #endregion

        #region Properties
        /// <summary>
        /// 编码
        /// </summary>
        public long Code
        {
            get => _code;
            set
            {
                _code = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Code"));
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
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
        #endregion

        #region Constructors

        #region Common Constructors
        public LFPointer()
        {
        }

        /// <summary>
        /// 构造编码为<paramref name="code"/>名称为<paramref name="name"/>的指针实例。
        /// </summary>
        /// <param name="code">编码。</param>
        /// <param name="name">名称。</param>
        public LFPointer(long code, string name)
        {
            _code = code;
            _name = name;
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFPointer(LFPointer rhs)
        {
            _code = rhs._code;
            _name = rhs._name;
            _value = rhs._value;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFPointer Clone()
        {
            return new LFPointer(this);
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

        #region Object Constructors

        /// <summary>
        /// 构建指向<paramref name="obj"/>的指针实例
        /// </summary>
        /// <param name="obj">物品对象</param>
        public LFPointer(LFItem obj)
        {
            _code = obj.Code;
            _name = obj.Name;
        }
        /// <summary>
        /// 构建指向<paramref name="obj"/>的指针实例
        /// </summary>
        /// <param name="obj">秘籍对象</param>
        public LFPointer(LFBook obj)
        {
            _code = obj.Code;
            _name = obj.Name;
        }
        /// <summary>
        /// 构建指向<paramref name="obj"/>的指针实例
        /// </summary>
        /// <param name="obj">地点对象</param>
        public LFPointer(LFSite obj)
        {
            _code = obj.Code;
            _name = obj.Name;
        }
        /// <summary>
        /// 构建指向<paramref name="obj"/>的指针实例
        /// </summary>
        /// <param name="obj">势力对象</param>
        public LFPointer(LFSect obj)
        {
            _code = obj.Code;
            _name = obj.Name;
        }
        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// 根据指针获取对象
        /// </summary>
        /// <returns>物品对象</returns>
        public LFItem GetItem()
        {
            // 先基于编码查找，如果未查找到，再使用名称查找
            LFItem obj = World.Data.ItemList.GetItem(_code);
            if (obj == null)
                obj = World.Data.ItemList.GetItem(_name);

            return obj;
        }

        /// <summary>
        /// 根据指针获取对象
        /// </summary>
        /// <returns>秘籍对象</returns>
        public LFBook GetBook()
        {
            // 先基于编码查找，如果未查找到，再使用名称查找
            LFBook obj = World.Data.BookList.GetBook(_code);
            if (obj == null)
                obj = World.Data.BookList.GetBook(_name);

            return obj;
        }

        /// <summary>
        /// 根据指针获取对象
        /// </summary>
        /// <returns>地点对象</returns>
        public LFSite GetSite()
        {
            // 先基于编码查找，如果未查找到，再使用名称查找
            LFSite obj = World.Data.SiteList.GetSite(_code);
            if (obj == null)
                obj = World.Data.SiteList.GetSite(_name);

            return obj;
        }
        #endregion
    }
}