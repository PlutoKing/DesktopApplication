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
        /// <summary>
        /// 实现接口：属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private LFDate _date = new LFDate();    // 日期
        private LFDate _endDate = new LFDate(); // 结束日期
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

        /// <summary>
        /// 结束时间
        /// </summary>
        public LFDate EndDate { get => _endDate; set => _endDate = value; }

        #endregion

        #region Constructors
        public LFVariable()
        {

        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="obj">源对象</param>
        public LFVariable(LFVariable obj)
        {
            _date = obj._date;
            _index = obj._index;
            _value = obj._value;
            _age = obj._age;
            _endDate = obj._endDate;
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

        /// <summary>
        /// 相等性检测
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool Equals(LFVariable v)
        {
            return (_date.Equals(v._date) && _index== v._index);
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
