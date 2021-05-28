/*──────────────────────────────────────────────────────────────
 * FileName     : LFLevel.cs
 * Created      : 2021-05-27 20:24:26
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace LF.FictionWorld
{
    /// <summary>
    /// 修炼层数
    /// </summary>
    public class LFLevel : INotifyPropertyChanged, ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private byte _id;           // ID
        private string _name;       // 名称
        private string _brief;      // 简介

        #endregion

        #region Properties
        /// <summary>
        /// ID
        /// </summary>
        public byte ID
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
        public LFLevel()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFLevel(LFLevel rhs)
        {
            _id = rhs._id;
            _name = rhs._name;
            _brief = rhs._brief;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该实例相等的新实例</returns>
        public LFLevel Clone()
        {
            return new LFLevel(this);
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns>与该实例相等的新实例</returns>
        object ICloneable.Clone()
        {
            return Clone();
        }
        #endregion

        #region Methods

        #endregion
    }
}