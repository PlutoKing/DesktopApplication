/*──────────────────────────────────────────────────────────────
 * FileName     : LFSubSite.cs
 * Created      : 2021-05-28 10:16:48
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace LF.FictionWorld
{
    public class LFSubSite : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;       // 定义属性改变事件

        private int _id;        // ID
        private string _name;   // 名称
        private string _brief;  // 简介

        #endregion

        #region Properties
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
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
        #endregion

        #region Constructors
        public LFSubSite()
        {
        }
        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="obj"></param>
        public LFSubSite(LFSubSite obj)
        {
            _id = obj._id;
            _name = obj._name;
            _brief = obj._brief;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFSubSite Clone()
        {
            return new LFSubSite(this);
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