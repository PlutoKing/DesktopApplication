/*──────────────────────────────────────────────────────────────
 * FileName     : LFVariable
 * Created      : 2020-09-28 11:32:11
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.ComponentModel;

namespace LF.FictionWorld
{
    /// <summary>
    /// 变量：随时间变化的参数
    /// </summary>
    public class LFVariable : INotifyPropertyChanged, ICloneable
    {
        #region Fields

        public event PropertyChangedEventHandler PropertyChanged;       // 定义属性改变事件

        private LFDate _date = new LFDate();    // 日期
        private long _index;                    // 索引
        private string _value;                  // 值

        private float _age;                     // 年龄

        #endregion

        #region Properties

        /// <summary>
        /// 日期
        /// </summary>
        public LFDate Date { get => _date; set => _date = value; }
        /// <summary>
        /// 索引
        /// </summary>
        public long Index { get => _index; set => _index = value; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get => _value; set => _value = value; }
        /// <summary>
        /// 年龄
        /// </summary>
        public float Age { get => _age; set => _age = value; }

        #endregion

        #region Constructors
        public LFVariable()
        {

        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public LFVariable(LFVariable rhs)
        {
            this._date = rhs._date;
            this._index = rhs._index;
            this._value = rhs._value;
            this._age = rhs._age;
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}
