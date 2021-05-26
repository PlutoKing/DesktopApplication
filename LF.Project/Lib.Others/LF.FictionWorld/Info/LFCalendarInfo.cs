/*──────────────────────────────────────────────────────────────
 * FileName     : LFCalendarInfo.cs
 * Created      : 2021-05-25 11:59:38
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.FictionWorld
{
    public class LFCalendarInfo:INotifyPropertyChanged,ICloneable
    {
        #region Fields
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;       // 定义属性改变事件

        private int _id;        // ID
        private string _name;   // 名称
        private int _count;     // 数量

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
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
            }
        }
        #endregion

        #region Constructors
        public LFCalendarInfo()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFCalendarInfo(LFCalendarInfo rhs)
        {
            ID = rhs.ID;
            Name = rhs.Name;
            Count = rhs.Count;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFCalendarInfo Clone()
        {
            return new LFCalendarInfo(this);
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

        #region Serializations
        #endregion

        #region Defaults
        #endregion
    }
}