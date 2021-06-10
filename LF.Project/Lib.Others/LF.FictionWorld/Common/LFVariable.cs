/*──────────────────────────────────────────────────────────────
 * FileName     : LFVariable.cs
 * Created      : 2021-05-28 17:07:28
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
    /// 变量，随时间变化的值
    /// </summary>
    public class LFVariable : INotifyPropertyChanged, ICloneable
    {
        #region Fields

        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private LFDate _date = new LFDate();    // 日期
        private LFDate _endDate = new LFDate(); // 结束日期

        private long _code;                    // 索引
        private string _value;                  // 值
        private double _age;                     // 年龄


        #endregion

        #region Properties

        /// <summary>
        /// 日期
        /// </summary>
        public LFDate Date
        {
            get => _date;
            set
            {
                _date = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
            }
        }
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
        /// 值
        /// </summary>
        public string Value
        {
            get => _value; set
            {
                _value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public double Age
        {
            get => _age;
            set
            {
                _age = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Age"));
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public LFDate EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EndDate"));
            }
        }

        #endregion

        #region Constructors
        public LFVariable()
        {
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs">源对象</param>
        public LFVariable(LFVariable rhs)
        {
            _date = rhs._date;
            _code = rhs._code;
            _value = rhs._value;
            _age = rhs._age;
            _endDate = rhs._endDate;
        }

        /// <summary>
        /// 拷贝函数
        /// </summary>
        /// <returns></returns>
        public LFVariable Clone()
        {
            return new LFVariable(this);
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
        /// <summary>
        /// 相等性检测
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool Equals(LFVariable v)
        {
            return (_date.Equals(v._date) && _code == v._code);
        }
        #endregion
    }
}